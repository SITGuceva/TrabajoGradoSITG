using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcesoProyectoF : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null) {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack) {
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "ProcesoProyectoF.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                ResultadoConsulta();
            }
        }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.LBdescarga);
    }

    /*Metodos que se encargan de proyectos final pendientes al director*/
    protected void GVproyfinalp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVproyfinalp.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    protected void GVproyfinalp_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVproyfinalp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RevisaPF"){
            Consulta.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVproyfinalp.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene el codigo del pf en la tabla         
            Titulo.Value = row.Cells[1].Text; //obtiene el titulo del pf en la tabla
            ResultadoContenidoP();
        }
    }
    private void ResultadoConsulta()
    {
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select DISTINCT P.ppro_Codigo, P.pf_titulo, P.pf_Fecha from estudiante e,  proyecto_final p, director s WHERE  P.ppro_Codigo = e.PROP_CODIGO and s.prop_codigo = P.ppro_Codigo and P.pf_aprobacion = 'PENDIENTE' and  s.usu_username='" + Session["id"] + "'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVproyfinalp.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVproyfinalp.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*Metodos para la informacion de un pf especifico*/
    private void ResultadoContenidoP()
    {
        CodigoP.Text = Metodo.Value;    
        TituloP.Text = Titulo.Value;
  
        InformacionP.Visible = true;
        MostrarDDLestadoP.Visible = true;
        Terminar.Visible = true;
        Linfo.Text = "";
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        List<string> list = con.FtpConexion();
        string fileName = "", contentype = "", ruta = "";

        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);

        string sql = "select PF_NOMARCHIVO, PF_DOCUMENTO, PF_TIPO FROM PROYECTO_FINAL WHERE PPRO_CODIGO=" + CodigoP.Text + "";
        List<string> pf = con.consulta(sql, 3, 0);
        fileName = pf[0];
        ruta = pf[1];
        contentype = pf[2];

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
        } catch (WebException a){
            Linfo.Text = a.ToString();
        }
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")) {
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }else {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }
   
    /*Metodos que maneja lo de las observaciones del pf*/
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla()
    {
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = " SELECT PFOBS_CODIGO, PFOBS_DESCRIPCION FROM PF_OBSERVACIONES WHERE PPRO_CODIGO = '" + CodigoP.Text + "'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                }
                GVobservacion.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVobservacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string id = GVobservacion.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from pf_observacion where PFOBS_CODIGO='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){
                cargarTabla();
            }
        }
    }
    protected void GVobservacion_RowUpdating(object sender, GridViewUpdateEventArgs e) {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        GridViewRow row = (GridViewRow)GVobservacion.Rows[e.RowIndex];
        if (conn != null) {
            TextBox observacion = (TextBox)row.Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVobservacion.Rows[e.RowIndex].Cells[0].Controls[0];
            string sql = "update pf_observacion set pfobs_descripcion = '" + observacion.Text + "' where  pfobs_codigo ='" + codigo.Text + "'";
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
        int indice = GVobservacion.EditIndex = e.NewEditIndex;
        cargarTabla();
        GVobservacion.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVobservacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVobservacion.EditIndex = -1;
        cargarTabla();
    }
    protected void MostrarObservaciones(object sender, EventArgs e)
    {
        MostrarDDLestadoP.Visible = true;
        cargarTabla();
        Resultado.Visible = true;
        MostrarAgregarObs.Visible = true;   
        Terminar.Visible = true;       
    }
    protected void Agregar_observacion(object sender, EventArgs e)
    {
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        if (string.IsNullOrEmpty(TBdescripcion.Value) == true){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "No se puede agregar una observacion vacia";
        } else  {         
            string sql = "insert into pf_observaciones (PFOBS_CODIGO, PFOBS_DESCRIPCION, PPRO_CODIGO ,PFOBS_FECHA, PFOBS_REALIZADA) values (OBSPROYFID.nextval,'" + TBdescripcion.Value.ToLower() + "','" + CodigoP.Text + "',TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), 'DIRECTOR')";
            Ejecutar("", sql);
            TBdescripcion.Value = "";
            Resultado.Visible = true;
            cargarTabla();
        }
    }

   /*Eventos de diferentes botones*/
    protected void terminar(object sender, EventArgs e)
    {
        if (DDLestadoP.SelectedIndex.Equals(0)) {
            Linfo.Text = "Debe seleccionar una calificación para el proyecto final.";
        } else{
            string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
            string sql = "update proyecto_final set pf_aprobacion ='" + DDLestadoP.Items[DDLestadoP.SelectedIndex].Value.ToString() + "' where ppro_codigo='" + CodigoP.Text + "'";
            Ejecutar("El proyecto final ha sido revisado con exito, presione click en regresar para revisar otro proyecto final", sql);

            Resultado.Visible = false;
            MostrarAgregarObs.Visible = false;
            MostrarDDLestadoP.Visible = false;
            InformacionP.Visible = false;
            Terminar.Visible = false;          
            IBregresar.Visible = true;
        }

    }
    protected void cancelar(object sender, EventArgs e)
    {
        Resultado.Visible = false;
        MostrarAgregarObs.Visible = false;
        MostrarDDLestadoP.Visible = false;
        InformacionP.Visible = false;
        Terminar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        DDLestadoP.SelectedIndex = 0;
    }
    protected void regresar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        IBregresar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
    }

   
}