using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcesoAnteproyecto : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack) {
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "ProcesoAnteproyecto.aspx");
            if (valida.Equals("false")) {
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                ResultadoConsulta();
            }
        }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.LBdescarga);
    }

    /*Metodos que consultan los anteproyectos pendientes por revisar */
    protected void GVantependiente_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVantependiente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVantependiente.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    private void ResultadoConsulta()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "select DISTINCT an.apro_codigo, an.anp_nombre, an.anp_fecha from estudiante e, anteproyecto an, director s, usuario u WHERE  an.apro_codigo = e.PROP_CODIGO and s.prop_codigo = an.apro_codigo and an.anp_aprobacion = 'PENDIENTE' and  s.usu_username='"+Session["id"]+"'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()) {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVantependiente.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVantependiente.DataBind();
            }
            conn.Close();
        } catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVantependiente_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RevisarAnte"){
            Consulta.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVantependiente.Rows[index];
            Codigo.Value = row.Cells[0].Text; 
            Titulo.Value = row.Cells[1].Text; 
            CargarContenido();  
        }
    }

    /*Metodos que consultan anteproyecto especifico*/
    private void CargarContenido()
    {
        CodigoP.Text = Codigo.Value;
        TituloP.Text = Titulo.Value;
        MostrarCalifica.Visible = true;
        Linfo.Text = "";
        InfoAnteproy.Visible = true;
        Terminar.Visible = true; 
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        List<string> list = con.FtpConexion();
        string fileName = "", contentype = "", ruta = "";

        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);

        string sql = "select ANP_NOMARCHIVO, ANP_DOCUMENTO,ANP_TIPO FROM ANTEPROYECTO WHERE APRO_CODIGO=" + Codigo.Value + "";
        List<string> ante = con.consulta(sql, 3, 0);
        fileName = ante[0];
        ruta = ante[1];
        contentype = ante[2];

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
        }catch (WebException a) {
            Linfo.Text = a.ToString();
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
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "SELECT AOBS_CODIGO, AOBS_DESCRIPCION FROM ANTE_OBSERVACION  WHERE APROP_CODIGO ='" + Codigo.Value + "' and AOBS_REALIZADA ='DIRECTOR'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()) {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                }
                GVobservacion.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVobservacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string id = GVobservacion.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from ante_observacion where AOBS_CODIGO='" + id + "'";
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
            string sql = "update ante_observacion set aobs_descripcion = '" + observacion.Text + "' where  aobs_codigo ='" + Codigo.Value + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()) {
                GVobservacion.EditIndex = -1;
                cargarTabla();
            }
        }
    }
    protected void GVobservacion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVobservacion.EditIndex = e.NewEditIndex;
        cargarTabla();
        GVobservacion.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVobservacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVobservacion.EditIndex = -1;
        cargarTabla();
    }

    /*Eventos de botones para las observaciones*/
    protected void MostrarObservaciones(object sender, EventArgs e)
    {
        cargarTabla();
        Resultado.Visible = true;
        MostrarAgregarObs.Visible = true;
    }
    protected void Agregar_observacion(object sender, EventArgs e)
    {
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        if (string.IsNullOrEmpty(TBdescripcion.Value) == true) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "No puede agregar una observacion en blanco.";
        } else{   
            string sql = "insert into ante_observacion (AOBS_CODIGO, AOBS_DESCRIPCION, APROP_CODIGO ,AOBS_FECHA, AOBS_REALIZADA) values (OBSANTEPID.nextval,'" + TBdescripcion.Value.ToLower() + "','"+ Codigo.Value + "',TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'DIRECTOR')";
            Ejecutar("", sql);
            TBdescripcion.Value = "";
            cargarTabla();
        }
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }
        else
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }

    /*Eventos de los botones terminar-cancelar-regresar*/
    protected void terminar(object sender, EventArgs e)
    {
        if (DDLestadoP.SelectedIndex.Equals(0)){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe seleccionar una calificación.";
        } else {
            string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
            string sql = "update anteproyecto set anp_aprobacion ='" + DDLestadoP.Items[DDLestadoP.SelectedIndex].Value.ToString() + "' where apro_codigo='" + Codigo.Value + "'";
            Ejecutar("El anteproyecto ha sido revisada con exito, presione click en regresar para revisar otra", sql);

            Resultado.Visible = false;
            MostrarAgregarObs.Visible = false;
            MostrarCalifica.Visible = false;
            InfoAnteproy.Visible = false;
            Terminar.Visible = false;
            IBregresar.Visible = true;
            Titulo.Value = "";
            Codigo.Value = "";
        }
    }
    protected void cancelar(object sender, EventArgs e)
    {
        Resultado.Visible = false;
        MostrarAgregarObs.Visible = false;
        MostrarCalifica.Visible = false;
        InfoAnteproy.Visible = false;
        Terminar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        DDLestadoP.SelectedIndex = 0;
        Codigo.Value = "";
        Titulo.Value = "";
    }
    protected void regresar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        IBregresar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
    }

}
