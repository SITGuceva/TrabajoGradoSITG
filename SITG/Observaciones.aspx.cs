using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

public partial class Observaciones : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }else if (!IsPostBack){
            Ingreso.Visible = true;
        }
    }

    /*Metodos que sirven para agregar observaciones*/
    protected void Agregar_observacion(object sender, EventArgs e)
    {
        Btnuevo.Visible = true;
        Btbuscar.Visible = false;
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");

        if(string.IsNullOrEmpty(TBdescripcion.Text) == true){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Escriba la observacion para agregarla";
        }else{
            string texto = "Se agrego la observacion correctamente";
            string sql = "insert into observacion (OBS_CODIGO, OBS_DESCRIPCION, OBS_REALIZADA ,PROP_CODIGO) values (OBSERVACIONPROP.nextval,'" + TBdescripcion.Text + "','Comite', '" + TBcodigo.Text + "')";
            Ejecutar(texto, sql);        
            TBdescripcion.Text = "";
            cargarTabla();
            Resultado.Visible = true;
        }
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }
  
    /*Evento del boton nueva consulta*/
    protected void Nuevo(object sender, EventArgs e)
    {       
        TBdescripcion.Enabled = false;
        TBcodigo.Enabled = true;
        Btnuevo.Visible = false;
        BTagregar.Enabled = false;
        Btbuscar.Visible = true;
        Resultado.Visible = false;
        InfGeneral.Visible = false;
        TBcodigo.Text = "";
        Linfo.Text = "";
    }

    /*Evento que valida si la propuesta existe*/
    protected void Buscar_observacion(object sender, EventArgs e)
    {       
        if (string.IsNullOrEmpty(TBcodigo.Text) == true){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Por favor digite el codigo de la propuesta que desea agregar observaciones";
        }else {
            string sql = @"SELECT COUNT(*) FROM propuesta WHERE prop_codigo ='" + TBcodigo.Text + "'";
            OracleConnection conn = con.crearConexion();
            if (conn != null){              
                OracleCommand cmd = new OracleCommand(sql, conn);
                int resultado = Convert.ToInt32(cmd.ExecuteScalar());
                if (resultado > 0) {

                    TBdescripcion.Enabled = true;
                    TBcodigo.Enabled = false;
                    BTagregar.Enabled = true;
                    Btnuevo.Visible = true;
                    Btbuscar.Visible = false;

                    InfGeneral.Visible = true;
                    CargarIntegrantes();               
                    CargarTitulo();
                       
                    cargarTabla();
                    Resultado.Visible = true;
                }else{         
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "La propuesta no existe, confirme el codigo ingresado";
                    TBcodigo.Text = "";
                }
            }
            conn.Close();
        }
    }

    /*Metodos que cargan la informacion general de la propuesta*/
    protected void GVintegrantes_RowDataBound(object sender, GridViewRowEventArgs e){}
    public void CargarIntegrantes(){
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "select CONCAT(CONCAT(u.usu_apellido, ' '), u.usu_nombre) as integrantes from estudiante e, usuario u  where e.prop_codigo = '" + TBcodigo.Text + "' and u.usu_username = e.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVintegrantes.DataSource = dataTable;
                }
                GVintegrantes.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVtitulo_RowDataBound(object sender, GridViewRowEventArgs e){}
    public void CargarTitulo()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "select prop_titulo from propuesta where prop_codigo = '" + TBcodigo.Text + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVtitulo.DataSource = dataTable;
                }
               GVtitulo.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
       
    /*Metodos para la consulta de las observaciones*/
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla()
    {       
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT OBS_CODIGO, OBS_DESCRIPCION FROM OBSERVACION  WHERE PROP_CODIGO ='" + TBcodigo.Text + "' and OBS_REALIZADA!='Director'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {                 
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVobservacion.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*Metodos que sirven para el modificar-eliminar de la tabla observaciones*/
    protected void GVobservacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
               string id = GVobservacion.Rows[e.RowIndex].Cells[0].Text;
               string sql = "Delete from observacion where OBS_CODIGO='" + id + "'";
               cmd = new OracleCommand(sql, conn);
               cmd.CommandType = CommandType.Text;
               using (OracleDataReader reader = cmd.ExecuteReader()){
                    cargarTabla();
                }
            }
        }
    protected void GVobservacion_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        GridViewRow row = (GridViewRow)GVobservacion.Rows[e.RowIndex];
        if (conn != null){
          
            TextBox observacion = (TextBox)row.Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVobservacion.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update observacion set obs_descripcion = '" + observacion.Text + "' where  obs_codigo ='" + codigo.Text + "'";
            Linfo.Text = sql;
            
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){
                GVobservacion.EditIndex = -1;
                cargarTabla();
            }
        }
    }
    protected void GVobservacion_RowEditing(object sender, GridViewEditEventArgs e)
    {       
        int indice= GVobservacion.EditIndex = e.NewEditIndex;
        cargarTabla();
        GVobservacion.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVobservacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVobservacion.EditIndex = -1;
        cargarTabla();
    }

}









