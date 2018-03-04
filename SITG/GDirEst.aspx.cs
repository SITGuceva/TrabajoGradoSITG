using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GDirEst : Conexion
{
    Conexion con = new Conexion();

    int prop_codigo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        string sql = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido)from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
        DDLlista.Items.AddRange(con.cargardatos(sql));

        RevisarExiste();    
        SolicitudHecha();     
    }

    /*Metodo para saber si existe una propuesta para el estudiante que ingreso*/
    private void RevisarExiste() 
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "SELECT PROP_CODIGO FROM ESTUDIANTE WHERE USU_USERNAME ='" + Session["id"] + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){
                if (drc1.IsDBNull(0)){
                    LBSolicitar.Enabled = false;
                    LBSolicitar.ForeColor = System.Drawing.Color.Gray;
                } else{
                    LBSolicitar.Enabled = true;
                    LBSolicitar.ForeColor = System.Drawing.Color.Black;
                    prop_codigo= drc1.GetInt32(0);
                }
            }
            drc1.Close();
        }
    }

    /*Metodo para saber si ya se tiene una solicitud y depende del estado se habilita la opcion*/
    private void SolicitudHecha()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "SELECT SOL_ESTADO FROM SOLICITUD_DIR WHERE PROP_CODIGO ='" + prop_codigo + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){               
                if (drc1.IsDBNull(0)){
                    LBSolicitar.Enabled = true;
                    LBSolicitar.ForeColor = System.Drawing.Color.Black;
                }else{
                    string estado = drc1.GetString(0);                   
                    if (estado.Equals("Rechazado")){
                        LBSolicitar.Enabled = true;
                        LBSolicitar.ForeColor = System.Drawing.Color.Black;
                    } else{
                        LBSolicitar.Enabled = false;
                        LBSolicitar.ForeColor = System.Drawing.Color.Gray;
                    }
                }
            }
            drc1.Close();          
        }
    }

    /*Evento que envia la solicitud*/
    protected void Aceptar(object sender, EventArgs e)
    {      
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string sql = "insert into solicitud_dir (SOL_ID, SOL_FECHA, SOL_ESTADO, PROP_CODIGO, USU_USERNAME) values(SOLICITUDID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), 'Pendiente', '"+prop_codigo+"','" + DDLlista.Items[DDLlista.SelectedIndex].Value + "')";
        string texto = "Solicitud realizada correctamente";
        Ejecutar(texto, sql); 
        Ingreso.Visible = false;

    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }else {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }

    }
  
    /*Evento que maneja el fronted*/
    protected void Solicitar(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        resultado.Visible = false;
        Linfo.Text = "";
        DDLlista.Items.Clear();
        string sql = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido)from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
        DDLlista.Items.AddRange(con.cargardatos(sql));
    }
    protected void  Consultar(object sender, EventArgs e)
    {
        Linfo.Visible = true;
        Ingreso.Visible = false;
        resultado.Visible = true;
        cargarTabla();
    }

    /*Metodos que sirven para la consulta de las solicitudes realizadas*/
    protected void GVsolicitud_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVsolicitud.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVsolicitud_RowDataBound(object sender, GridViewRowEventArgs e){ }
    public void cargarTabla()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                sql = "select s.sol_id, s.sol_fecha, s.sol_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director  from solicitud_dir s, usuario u where u.usu_username=s.usu_username and s.prop_codigo='" + prop_codigo+"'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVsolicitud.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVsolicitud.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e) {
        GVinfprof.Visible = false;
        Linfo.Text = "";
        Tbotones.Visible = false;
        DDLlista.Enabled = true;
        Bconsulta.Enabled = true;
        DDLlista.Items.Clear();
        string sql = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido)from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
        DDLlista.Items.AddRange(con.cargardatos(sql));
    }

    /*Metodos que sirven para buscar la informacion del profesor*/
    protected void InfProfesor(object sender, EventArgs e)
    {
        DDLlista.Enabled = false;
        Bconsulta.Enabled = false;
        Tbotones.Visible = true;
        CargarInfoProfesor();
        GVinfprof.Visible = true;
    }
    private void CargarInfoProfesor()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                sql = "select usu_telefono, usu_direccion, usu_correo  from usuario  where usu_username='" + DDLlista.Items[DDLlista.SelectedIndex].Value+ "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVinfprof.DataSource = dataTable;
                }
                GVinfprof.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVinfprof_RowDataBound(object sender, GridViewRowEventArgs e){}

}