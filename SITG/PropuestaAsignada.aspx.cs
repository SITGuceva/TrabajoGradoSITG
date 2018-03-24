using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PropuestaAsignada : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        } else if (!IsPostBack) {
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "PropuestaAsignada.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                CargarPropuestas();
            }
        }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVConsultaContenidoP);
    }

    /*Evento del boton seleccionar nueva propuesta*/
    protected void Nueva(object sender, EventArgs e)
    {
        CargarPropuestas();
        ResultadoPropuesta.Visible = true;
        Ingreso.Visible = false;
        ResultadoObservacion.Visible = false;
        ConsultaContenidoP.Visible = false;
        Integrantes.Visible = false;
        ObservacionComite.Visible = false;
        LBMisObservaciones.Visible = false;
        ObservacionComite.Visible = false;
        BTregresar.Visible =false;
        Metodo.Value = "";
        Estado.Value = "";
    }

    /*Metodos para agregar observaciones y descargar el documento*/
    protected void Agregar_observacion(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TBdescripcion.InnerText) == true) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "No se puede agregar una observación si se encuentra vacia.";
        } else {
            string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
            string sql  = "insert into observacion (OBS_CODIGO, OBS_DESCRIPCION, OBS_REALIZADA ,PROP_CODIGO) values (OBSPROPID.nextval,'" + TBdescripcion.Value + "','DIRECTOR', '"+ Metodo.Value + "')";
            Ejecutar("", sql);
            TBdescripcion.Value = "";
            CargarObservaciones();
            Linfo.Text = "";
        }
    }
   //Metodo que agrega una observación a la base de datos
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        } else  {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
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
            ResultadoPropuesta.Visible = false;

            Metodo.Value = row.Cells[0].Text; 
            Estado.Value = row.Cells[2].Text; //obtiene el titulo de la propuesta en la tabla
            ResultadoContenidoP();
            ConsultaContenidoP.Visible = true;
            ObservacionComite.Visible = true;
            CargarObservacionesCom();
            CargarIntegrantes();
            Integrantes.Visible = true;
            BTregresar.Visible = true;
            Linfo.Text = "";

            //Activo ingresar observaciones siempre y cuando la propuesta haya sido rechazada
            if (Estado.Value.Equals("Rechazado")){
                Ingreso.Visible = true;
                ResultadoObservacion.Visible = true;
                CargarObservaciones();
            }
        }
    }
    public void CargarPropuestas()
    {
       string sql = "";
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                sql = "select distinct p.prop_codigo, p.prop_titulo, TO_CHAR( p.PROP_FECHA, 'dd/mm/yyyy') as FECHA , INITCAP(p.PROP_ESTADO) as ESTADO from propuesta p, usuario u, director s where s.USU_USERNAME='" + Session["id"] + "' and p.PROP_CODIGO = s.prop_codigo and s.dir_estado='APROBADO'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVpropuesta.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVpropuesta.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
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
               string sql = "SELECT OBS_CODIGO, OBS_DESCRIPCION FROM OBSERVACION  WHERE PROP_CODIGO ='"+ Metodo.Value + "' and OBS_REALIZADA='Director'";

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

    /*Metodos que visualizan los integrantes de la propuesta*/
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
                sql = "select CONCAT(CONCAT(u.usu_apellido, ' '), u.usu_nombre) as integrantes from estudiante e, usuario u  where e.prop_codigo = '" + Metodo.Value + "' and u.usu_username = e.usu_username";

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
                string sql = "SELECT OBS_CODIGO, OBS_DESCRIPCION FROM OBSERVACION  WHERE PROP_CODIGO ='" + Metodo.Value + "' and OBS_REALIZADA='Comite'";

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

    /*Metodos que consulta el contenido de la propuesta*/
    protected void GVConsultaContenidoP_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVConsultaContenidoP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVConsultaContenidoP.PageIndex = e.NewPageIndex;
        ResultadoContenidoP();
    }
    private void ResultadoContenidoP()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select p.PROP_CODIGO,p.PROP_TITULO, l.LINV_NOMBRE, t.TEM_NOMBRE from propuesta p, lin_investigacion l, tema t " +
                    "where t.LINV_CODIGO = l.LINV_CODIGO and t.TEM_CODIGO = p.TEM_CODIGO  and p.PROP_CODIGO = '" + Metodo.Value + "'";

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
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        List<string> list = con.FtpConexion();
        string fileName = "", contentype = "", ruta = "";

        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);

        string sql = "select PROP_NOMARCHIVO, PROP_DOCUMENTO, PROP_TIPO FROM PROPUESTA WHERE PROP_CODIGO=" + id + "";
        List<string> prop = con.consulta(sql, 3, 0);
        fileName = prop[0];
        ruta = prop[1];
        contentype = prop[2];
        try{
            byte[] bytes = request.DownloadData(ruta + fileName);
            string fileString = System.Text.Encoding.UTF8.GetString(bytes);
            Console.WriteLine(fileString);
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentype;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }catch (WebException a){
            Linfo.Text = a.ToString();
        }
    }


}









