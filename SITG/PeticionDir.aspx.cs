using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PeticionDir : Conexion
{
    Conexion con = new Conexion();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }else if (!IsPostBack){          
            TPeticiones.Visible = true;
            CargarTablaPeticiones();
        }
    }

    /*Metodos que se utilizan para consultar las peticiones de director pendientes*/
    protected void GVpeticion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVpeticion.PageIndex = e.NewPageIndex;
        CargarTablaPeticiones();
    }
    protected void GVpeticion_RowDataBound(object sender, GridViewRowEventArgs e){}
    public void CargarTablaPeticiones()
    {   
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT S.SOL_ID, S.SOL_FECHA, S.SOL_ESTADO, S.PROP_CODIGO, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, u.usu_username " +
                    "FROM SOLICITUD_DIR S, USUARIO U, estudiante e, comite c, PROFESOR p " +
                    "WHERE S.SOL_ESTADO = 'Pendiente' AND U.USU_USERNAME = S.USU_USERNAME and S.PROP_CODIGO = e.PROP_CODIGO  and p.COM_CODIGO = c.COM_CODIGO " +
                    "and c.PROG_CODIGO = e.PROG_CODIGO and p.USU_USERNAME = '"+ Session["id"] + "'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVpeticion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVpeticion.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVpeticion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        string sql = ""; 
        GridViewRow row;

        if (e.CommandName == "Ver"){
            Regresar.Visible = true;
            CargarInfprof(index);
            Tinfprof.Visible = true;
            TPeticiones.Visible = false;
            Linfo.Text = "";

        }else  if (e.CommandName == "Aprobar"){
            row = GVpeticion.Rows[index];  
            HiddenField idprof = row.FindControl("Director") as HiddenField;            
            sql = "update SOLICITUD_DIR set SOL_ESTADO='Aprobado' where PROP_CODIGO='"+ row.Cells[2].Text + "'";
            Ejecutar("Solicitud de Director Aprobada", sql);   
            ExisteRol(idprof.Value);
            CargarTablaPeticiones();
        } else if (e.CommandName == "Rechazar"){
            row = GVpeticion.Rows[index];
            sql = "update SOLICITUD_DIR set SOL_ESTADO='Rechazado' where PROP_CODIGO='" + row.Cells[2].Text + "'";
            Ejecutar("Solicitud de Director Rechazada", sql);
            CargarTablaPeticiones();
        }

    }
    private void ExisteRol(string idprof)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string sql = "select USU_USERNAME from usuario_rol where ROL_ID='DIR' and USU_USERNAME='"+idprof+"'";
            cmd = new OracleCommand(sql, conn);  
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
           
            if (!drc1.HasRows){
                AsignarRol(idprof);
            }
            drc1.Close();
        }
    }
    private void AsignarRol(string idprof)
    { 
        string sql = "insert into USUARIO_ROL (USUROL_ID,USU_USERNAME,ROL_ID) VALUES(USUARIOID.nextval, '" + idprof + "', 'DIR')";
        Ejecutar("Guardo", sql);
    }  
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono"))
        {
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }
        else
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }
     
    /*Evento del boton regresar*/
    protected void regresar(object sender, EventArgs e)
    {
        Regresar.Visible = false;
        TPeticiones.Visible = true;
        CargarTablaPeticiones();
        Tinfprof.Visible = false;
    }

    /*Metodos que se utilizan para consultar la informacion adicional del director*/
    protected void GVinfprof_RowDataBound(object sender, GridViewRowEventArgs e){}
    public void CargarInfprof(int cod)
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select usu_telefono, usu_direccion, usu_correo  from usuario  where usu_username='" +cod+ "'";

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
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
}









