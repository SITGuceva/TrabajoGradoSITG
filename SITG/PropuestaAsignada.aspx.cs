using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PropuestaAsignada : Conexion
{
    Conexion con = new Conexion();
    String CodigoPropuesta; //Recoge el codigo de la propuesta para consultarla posteriormente
    String TituloPropuesta; //Recoge el titulo de la propuesta para consultarla posteriormente
    String EstadoPropuesta; //Recoge el titulo de la propuesta para consultarla posteriormente

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        } else if (!IsPostBack) {
            EstadoPropuestaP.Visible = false;
            BTregresar.Visible = false;
            CargarPropuestas();
        }
        
    }

  
    /*Evento del boton seleccionar nueva propuesta*/
    protected void Nueva(object sender, EventArgs e)
    {
        Ingreso.Visible = false;
        ResultadoObservacion.Visible = false;
        ResultadoPropuesta.Visible = true;
        CargarPropuestas();
        ConsultaContenidoP.Visible = false;
        Integrantes.Visible = false;
        Ingreso.Visible = false;
        ObservacionComite.Visible = false;
        LBMisObservaciones.Visible = false;
        ObservacionComite.Visible = false;
        TCodigoP.Visible = false;
        TTituloP.Visible = false;
        TituloP.Visible = false;
        CodigoP.Visible = false;
        EstadoPropuestaP.Visible = false;
        BTregresar.Visible =false;
    }

    /*Metodos para agregar observaciones y descargar el documento*/
    protected void Agregar_observacion(object sender, EventArgs e)
    {
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string sql  = "insert into observacion (OBS_CODIGO, OBS_DESCRIPCION, OBS_REALIZADA ,PROP_CODIGO) values (OBSERVACIONPROP.nextval,'" + TBdescripcion.Text + "','Director', '"+CodigoP.Text+"')";
        Ejecutar("Observacion ingresada correctamente", sql);
        TBdescripcion.Text = "";
        CargarObservaciones();
    }
   //Metodo que agrega una observación a la base de datos
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
           
        } else  {

        }

    }

    /*Metodos para la consulta de las propuestas asignadas al director*/
    protected void GVpropuesta_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVpropuesta.PageIndex = e.NewPageIndex;
        CargarPropuestas();
    }
    protected void GVpropuesta_RowDataBound(object sender, GridViewRowEventArgs e){}
    protected void GVpropuesta_RowCommand(object sender, GridViewCommandEventArgs e)
    { 
        //Metodo que arma la ventana de observaciones y descripción de la propuesta
        if (e.CommandName == "ver"){

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVpropuesta.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene el codigo de propuesta en la tabla
            CodigoPropuesta = Metodo.Value;
            Metodo.Value = row.Cells[1].Text; //obtiene el titulo de la propuesta en la tabla
            TituloPropuesta = Metodo.Value;
            Metodo.Value = row.Cells[2].Text; //obtiene el titulo de la propuesta en la tabla
            EstadoPropuesta = Metodo.Value;
            ResultadoContenidoP();
            ConsultaContenidoP.Visible = true;
            ResultadoPropuesta.Visible = false;
            ResultadoObservacion.Visible=true;
            CargarObservaciones();
            LBMisObservaciones.Visible = true;
            ObservacionComite.Visible = true;
            CargarObservacionesCom();
            CargarIntegrantes();
            Integrantes.Visible = true;
            TCodigoP.Visible = true;
            TTituloP.Visible = true;
            TituloP.Visible = true;
            CodigoP.Visible = true;
            BTregresar.Visible = true;
            //Activo ingresar observaciones siempre y cuando la propuesta haya sido rechazada
            if (EstadoPropuesta.Equals("RECHAZADO"))
            {
                Ingreso.Visible = true;
            }

        }
    }
    public void CargarPropuestas()
    {
       string sql = "";
        List<ListItem> list = new List<ListItem>();
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                sql = "select distinct p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado  from propuesta p, usuario u, solicitud_dir s where s.USU_USERNAME='" + Session["id"] + "' and p.PROP_CODIGO = s.prop_codigo and sol_estado='Aprobado'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVpropuesta.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                  
                }
                GVpropuesta.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
           
        }
    }

    /*Metodos para la consulta-modificar-eliminar de las observaciones */
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        CargarObservaciones();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) {}
    protected void GVobservacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string id = GVobservacion.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from observacion where OBS_CODIGO='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                CargarObservaciones();
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
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVobservacion.EditIndex = -1;
                CargarObservaciones();
            }
        }
    }
    protected void GVobservacion_RowEditing(object sender, GridViewEditEventArgs e)
    {      
        int indice = GVobservacion.EditIndex = e.NewEditIndex;
        CargarObservaciones();
        GVobservacion.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVobservacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVobservacion.EditIndex = -1;
        CargarObservaciones();
    }
    public void CargarObservaciones()
    {
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
               string sql = "SELECT OBS_CODIGO, OBS_DESCRIPCION FROM OBSERVACION  WHERE PROP_CODIGO ='"+ CodigoP.Text + "' and OBS_REALIZADA='Director'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                  
                }
                GVobservacion.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
            
        }
    }


    protected void GVintegrantes_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void CargarIntegrantes()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select CONCAT(CONCAT(u.usu_apellido, ' '), u.usu_nombre) as integrantes from estudiante e, usuario u  where e.prop_codigo = '" + CodigoP.Text + "' and u.usu_username = e.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVintegrantes.DataSource = dataTable;
                }
                GVintegrantes.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
           
        }
    }



    /*Metodos para la consulta-modificar-eliminar de las observaciones */
    protected void GVObservacionComite_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVObservacionComite.PageIndex = e.NewPageIndex;
        CargarObservacionesCom();
    }
    protected void GVObservacionComite_RowDataBound(object sender, GridViewRowEventArgs e) { }
 
    
    public void CargarObservacionesCom()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "SELECT OBS_CODIGO, OBS_DESCRIPCION FROM OBSERVACION  WHERE PROP_CODIGO ='" + CodigoP.Text + "' and OBS_REALIZADA='Comite'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVObservacionComite.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());

                }
                GVObservacionComite.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {

        }
    }

























    protected void GVConsultaContenidoP_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVConsultaContenidoP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVConsultaContenidoP.PageIndex = e.NewPageIndex;
        ResultadoContenidoP();
    }
    private void ResultadoContenidoP()
    {
        TCodigoP.Text = "Código de la propuesta: ";
        CodigoP.Text = CodigoPropuesta;
        TTituloP.Text = "Título de la propuesta: ";
        TituloP.Text = TituloPropuesta;
        EstadoPropuestaP.Text = EstadoPropuesta;
        LlamarInfo();
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select p.PROP_JUSTIFICACION, p.PROP_OBJETIVOS, l.LPROF_NOMBRE, t.TEM_NOMBRE  ,p.PROP_BIBLIOGRAFIA  from propuesta p, lin_profundizacion l, tema t " +
                    "where t.LPROF_CODIGO = l.LPROF_CODIGO and t.TEM_CODIGO = p.TEM_CODIGO  and p.PROP_CODIGO = '" + CodigoPropuesta + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVConsultaContenidoP.DataSource = dataTable;
                }
                GVConsultaContenidoP.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
        }
    }


    private void LlamarInfo()
    {

        InformacionP.Visible = true;


    }


}









