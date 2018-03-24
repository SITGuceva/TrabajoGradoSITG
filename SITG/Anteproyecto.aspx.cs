using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;
using System.Net;

public partial class Anteproyecto : Conexion
{
    Conexion con = new Conexion();
    string codprop, titulo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
           Response.Redirect("Default.aspx");
        }
        if (!Page.IsPostBack) {
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Anteproyecto.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            } else{
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }

        RevisarAprobadoProp(); // llama metodo que verifica si la propuesta fue aprobada

        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVconsulta);
    }

    /* metodo que verifica si una propuesta fue aprobada y habilita el boton de subir propuesta*/
    private void RevisarAprobadoProp()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "select p.prop_estado, p.prop_codigo, p.prop_titulo, s.dir_estado from propuesta p, estudiante e, director s where e.usu_username='" + Session["id"] + "' and s.prop_codigo=e.prop_codigo and p.prop_codigo = e.prop_codigo";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){
                string estado_propuesta = drc1.GetString(0).ToString();
                string estado_director = drc1.GetString(3).ToString();
                codprop = drc1.GetInt32(1).ToString();
                titulo = drc1.GetString(2).ToString();

                if (estado_propuesta.Equals("APROBADO") && estado_director.Equals("APROBADO")){
                    
                    RevisarAprobadoAnte(); // llama metodo que verifica si el antepryecto fue aprobado
                } else{
                    LBSubir.Enabled = false;
                    LBSubir.ForeColor = System.Drawing.Color.Gray;
                }
            }
            drc1.Close();
        }
    }

    /* Metodo que verifica si un anteproyecto fue aprobado o rechazado*/
    private void RevisarAprobadoAnte()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string sql = "select an.ant_estado, an.ant_aprobacion from anteproyecto an, estudiante e where e.usu_username='" + Session["id"] + "' and an.apro_codigo = e.prop_codigo";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){
                string estado_jurado = drc1.GetString(0);
                string estado_director = drc1.GetString(1);            

                if (estado_jurado.Equals("RECHAZADO")||estado_director.Equals("RECHAZADO")){
                   
                    LBSubir.Enabled = true;
                    LBSubir.ForeColor = System.Drawing.Color.Black;
                    Metodo.Value = "ACTUALIZA";

                    if (estado_jurado.Equals("RECHAZADO")) Responsable.Value = "2";
                    if(estado_director.Equals("RECHAZADO")) Responsable.Value = "1";
                } else {
                    LBSubir.Enabled = false;
                    LBSubir.ForeColor = System.Drawing.Color.Gray;
                }
            }
            drc1.Close();
        }
    }

    /*Metodos que manejan la interfaz del subir-consultar*/
    protected void Subir_anteproyecto(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Consulta.Visible = false;
        Observaciones.Visible = false;
        Linfo.Text = "";
    }
    
    //metodo que llama a la tabla anteproyecto
    protected void Consulta_anteproyecto(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        Ingreso.Visible = false;
        Observaciones.Visible = false;
        Linfo.Text = "";
        BuscarAnteproyecto();
    }

    //metodo que sube el anteproyecto (documento);
    protected void Guardar(object sender, EventArgs e)
    {
        DateTime fecha = DateTime.Today;
        if (FUdocumento.HasFile) {
            string fileExt = System.IO.Path.GetExtension(FUdocumento.FileName);
            if (fileExt == ".pdf" || fileExt == ".doc" || fileExt == ".docx"){
                List<string> list = con.FtpConexion();
                string ruta = list[2] + "ANTEPROYECTOS/";

                if (Metodo.Value.Equals("ACTUALIZA")) {
                    EliminarAnteproyecto(list[0], list[1]);
                   
                    if (Responsable.Value.Equals("1")) {
                        CargarAnteproyecto(ruta, list[0], list[1], 1);
                    }
                    if (Responsable.Value.Equals("2")) {
                        CargarAnteproyecto(ruta, list[0], list[1], 2);
                    }
                } else {
                    bool existe = con.ExisteDirectorio(ruta);
                    if (!existe){
                        con.crearcarpeta(ruta);
                        CargarAnteproyecto(ruta, list[0], list[1], 0);
                    }else { CargarAnteproyecto(ruta, list[0], list[1], 0); }
                }
            } else{
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Formato no permitido, debe subir un archivo en PDF o Word";
            }
        } else {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe elegir un archivo";
        }
    }
    private void EliminarAnteproyecto(string usuario, string pass)
    {
        string ruta = "";
        string sql = "select anp_Nomarchivo, anp_Documento from anteproyecto  where apro_Codigo ='" + codprop + "'";
        List<string> contenido = con.consulta(sql, 2, 0);
        ruta = contenido[1] + contenido[0];

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta);
        request.Method = WebRequestMethods.Ftp.DeleteFile;
        request.Credentials = new NetworkCredential(usuario, pass);
        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) { }

        sql = "update anteproyecto set anp_nomarchivo= null,anp_Documento= null, anp_Tipo=null where apro_Codigo='" + codprop + "'";
        Ejecutar("", sql);
    }
    private void CargarAnteproyecto(string ruta, string usuario, string pass, int i)
    {
        string contentType = "", filename = codprop+FUdocumento.FileName;
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta + filename);
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential(usuario, pass);
        using (Stream fs = FUdocumento.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                contentType = FUdocumento.PostedFile.ContentType;
                byte[] fileContents = br.ReadBytes((Int32)fs.Length);
                StreamReader sourceStream = new StreamReader(FUdocumento.FileContent);
                sourceStream.Close();
                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                var response = (FtpWebResponse)request.GetResponse();
                response.Close();
                string query = "";
                if (i == 0) { 
                     query = "insert into anteproyecto (apro_codigo, anp_nombre, anp_documento, anp_nomarchivo, anp_tipo, anp_fecha) values ('"+codprop+"' ,'"+titulo+"' , '"+ruta+"', '"+filename+"', '"+contentType+ "', TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'))";
                }else if (i == 1) {
                    query = "update anteproyecto set anp_documento='"+ruta+"', anp_nomarchivo='"+filename+"', anp_tipo='"+contentType+ "', anp_fecha=TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), ant_aprobacion='PENDIENTE' where apro_codigo='" + codprop + "'";
                } else if (i == 2){
                    query = "update anteproyecto set anp_documento='"+ruta+"', anp_nomarchivo='"+filename+"', anp_tipo='"+contentType+ "', anp_fecha=TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), ant_estado='PENDIENTE' where apro_codigo='" + codprop + "'";
                }
                Ejecutar("Anteproyecto cargado satisfatoriamente", query);
            }
        }
        quitar();
    }
    private void quitar()
    {
        Ingreso.Visible = false;
        LBSubir.Enabled = false;
        LBSubir.ForeColor = System.Drawing.Color.Gray;
        Metodo.Value = "";
        Responsable.Value = "";
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
  
    /*Metodos que realizan la funcionalidad de consultar el anteproyecto*/
    protected void BuscarAnteproyecto()
    {
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select an.apro_codigo, an.anp_nombre, an.ant_aprobacion, an.ant_estado, an.anp_fecha, an.ant_evaluador  from anteproyecto an, estudiante e where an.apro_codigo = e.prop_codigo and e.usu_username ='" + Session["id"] + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsulta.DataSource = dataTable;
                }
                GVconsulta.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*Metodos que descarga el anteproyecto subido*/
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        List<string> list = con.FtpConexion();
        string fileName = "", contentype = "", ruta = "";

        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);

        string sql = "select ANP_NOMARCHIVO, ANP_DOCUMENTO,ANP_TIPO FROM ANTEPROYECTO WHERE APRO_CODIGO=" + id + "";
        List<string> ante = con.consulta(sql, 3, 0);
        fileName = ante[0];
        ruta = ante[1];
        contentype = ante[2];

        try {
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
        } catch (WebException a) {
            Linfo.Text = a.ToString();
        }
    }

    /*Metodos que se encargan de consultar las observaciones que el director le hizo a la propuesta*/
    protected void GVconsulta_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "buscar")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsulta.Rows[index];
            ResultadoObservaciones();
            Observaciones.Visible = true;
        }
    }
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        ResultadoObservaciones();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void ResultadoObservaciones()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT DISTINCT O.AOBS_CODIGO, O.AOBS_DESCRIPCION, O.AOBS_REALIZADA FROM ANTE_OBSERVACION O, ESTUDIANTE E WHERE E.PROP_CODIGO=O.APROP_CODIGO AND E.USU_USERNAME='" + Session["id"] + "' ORDER BY O.AOBS_CODIGO";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVobservacion.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

}