using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;
using System.Net;

public partial class Propuesta : Conexion
{
    Conexion con = new Conexion();
    System.Data.DataTable table;
    System.Data.DataRow row;
    string codprop;
    int prop_codigo;

    protected void Page_Load(object sender, EventArgs e) {
       if (Session["Usuario"] == null){
                Response.Redirect("Default.aspx");
       }
       if (!Page.IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Propuesta.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                Page.Form.Attributes.Add("enctype", "multipart/form-data");

                table = new System.Data.DataTable();
                table.Columns.Add("CODIGO", typeof(System.String));
                table.Columns.Add("INTEGRANTES", typeof(System.String));
                Session.Add("Tabla", table);
            }
       }
        RevisarExiste();

        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVconsulta);
        scriptManager.RegisterPostBackControl(this.GVinfprof);
    }
  
    /*Metodos de consulta que se necesitan hacer antes de cargar la pagina*/
    private void RevisarExiste(){
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "SELECT PROP_CODIGO FROM ESTUDIANTE WHERE USU_USERNAME ='" + Session["id"] + "' and PROP_CODIGO!=0";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){
                if (drc1.IsDBNull(0)){
                    LBSubir_propuesta.Enabled = true;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Black;

                    LBSolicitar.Enabled = false;
                    LBSolicitar.ForeColor = System.Drawing.Color.Gray;
                } else{
                    LBSubir_propuesta.Enabled = false;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Gray;

                    LBSolicitar.Enabled = true;
                    LBSolicitar.ForeColor = System.Drawing.Color.Black;
                    prop_codigo = drc1.GetInt32(0);
                    Linfo.Text = "No puede cargar mas propuestas, porque ya tiene una en tramite. Esta opción se habilitara en caso de que su propuesta sea rechazada.";

                    RevisarRechaza();
                    SolicitudHecha();
                }
            } else {
                LBSolicitar.Enabled = false;
                LBSolicitar.ForeColor = System.Drawing.Color.Gray;
                Linfo.Text = "No puede solicitar el director, porque aun no ha subido la propuesta. Esta opción se habilitara cuando suba la propuesta.";
            }
            drc1.Close();
        }
    }
    private void RevisarRechaza(){
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string sql = "SELECT p.PROP_ESTADO, p.PROP_CODIGO FROM ESTUDIANTE e, PROPUESTA p WHERE e.USU_USERNAME='"+ Session["id"] + "' and p.PROP_CODIGO = e.PROP_CODIGO";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){
                string estado=drc1.GetString(0).ToString();    
                codprop= drc1.GetInt32(1).ToString();

                if (estado.Equals("RECHAZADO")){
                    LBSubir_propuesta.Enabled = true;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Black;
                    Metodo.Value = "ACTUALIZA";
                    integra.Visible = false;
                }else {
                    LBSubir_propuesta.Enabled = false;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Gray;
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
        if (conn != null) {
            string sql = "SELECT DIR_ESTADO FROM DIRECTOR WHERE PROP_CODIGO ='" + prop_codigo + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows) {
                if (drc1.IsDBNull(0)){
                    LBSolicitar.Enabled = true;
                    LBSolicitar.ForeColor = System.Drawing.Color.Black;
                }else {
                    string estado = drc1.GetString(0);
                    if (estado.Equals("RECHAZADO")){
                        LBSolicitar.Enabled = true;
                        LBSolicitar.ForeColor = System.Drawing.Color.Black;
                    }else {
                        LBSolicitar.Enabled = false;
                        LBSolicitar.ForeColor = System.Drawing.Color.Gray;
                        Linfo.Text += "<br/>" + "No puede realizar mas solicitudes de director, porque ya tiene una en tramite. Esta opción se habilitara si la solicitud es rechazada.";
                    }
                }
            }
            drc1.Close();
        }
    }

    /*Evento que envia la solicitud*/
    protected void SolicitarDir(object sender, EventArgs e)
    {
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string sql = "insert into DIRECTOR (DIR_ID, DIR_FECHA, PROP_CODIGO, USU_USERNAME) values(DIRECTORID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), '" + prop_codigo + "','" + DDLlista.Items[DDLlista.SelectedIndex].Value + "')";
        string texto = "Solicitud realizada correctamente";
        Ejecutar(texto, sql);
        Solicitar.Visible = false;
        LBSolicitar.Enabled = false;
        LBSolicitar.ForeColor = System.Drawing.Color.Gray;
    }

    /*Metodos que manejar la interfaz del subir-consultar- solicitar dir- consultar dir*/
    protected void Subir_propuesta(object sender, EventArgs e)
    {
            Ingreso.Visible = true;
            Consulta.Visible = false;
            Observaciones.Visible = false;
            Solicitar.Visible = false;
            ConsultaSolicitud.Visible = false;
            Linfo.Text = "";
            DDLlprof.Items.Clear();
            string sql = "SELECT LINV_CODIGO, LINV_NOMBRE FROM LIN_INVESTIGACION WHERE LINV_ESTADO='ACTIVO' AND PROG_CODIGO IN" +
                " ( SELECT E.PROG_CODIGO FROM PROGRAMA P, ESTUDIANTE E WHERE P.PROG_CODIGO = E.PROG_CODIGO AND E.USU_USERNAME = '" + Session["id"] + "')";
            DDLlprof.Items.AddRange(con.cargardatos(sql));
            DDLlprof.Items.Insert(0, "Seleccione");
            DDLtema.Items.Insert(0, "Seleccione");
    }
    protected void Consulta_propuesta(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        Ingreso.Visible = false;
        Observaciones.Visible = false;
        Solicitar.Visible = false;
        ConsultaSolicitud.Visible = false;
        Linfo.Text = "";
        BuscarPropuesta(); 
    }
    protected void LBSolicitar_Click(object sender, EventArgs e)
    {
        Solicitar.Visible = true;
        ConsultaSolicitud.Visible = false;
        Consulta.Visible = false;
        Ingreso.Visible = false;
        Observaciones.Visible = false;
        Linfo.Text = "";
        DDLlista.Items.Clear();
        string sql = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido)from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
        DDLlista.Items.AddRange(con.cargardatos(sql));
    }
    protected void Consultar(object sender, EventArgs e) {
         Consulta.Visible = false;
         Ingreso.Visible = false;
         Observaciones.Visible = false;
         Solicitar.Visible = false;
         ConsultaSolicitud.Visible = true;
         cargarTabla();
         Linfo.Text="";
     }

    /*Evento del boton limpiar - cancelar*/
    protected void Cancelar(object sender, EventArgs e)
    {      
        Linfo.Text = "";
        TAnombre.Value = "";
        RespInte.Visible = false;
        TBcodint.Text = "";
        Bintegrante.Enabled = true;
        DDLlprof.SelectedIndex = 0;
        DDLtema.SelectedIndex = 0;
        TBcodint.Enabled = true;
        // table.Clear();
    }
    protected void Limpiar(object sender, EventArgs e)
    {
        GVinfprof.Visible = false;
        Linfo.Text = "";
        Tbotones2.Visible = false;
        DDLlista.Enabled = true;
        DDLlista.Items.Clear();
        string sql = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido)from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
        DDLlista.Items.AddRange(con.cargardatos(sql));
        table = (System.Data.DataTable)(Session["Tabla"]);
        table.Clear();
        Bconsulta.Visible = true;
        Blimpiar.Visible = false;
    }

    /*Metodos que realizan la funcionalidad del subir propuesta*/
    protected void Guardar(object sender, EventArgs e)
    {
         DateTime fecha = DateTime.Today;

        if (DDLlprof.SelectedIndex.Equals(0) || DDLtema.SelectedIndex.Equals(0) || string.IsNullOrEmpty(TAnombre.Value) == true){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Los campos son obligatorios";
        }else if (FUdocumento.HasFile){
            string fileExt = System.IO.Path.GetExtension(FUdocumento.FileName);
            if (fileExt == ".pdf" || fileExt == ".doc" || fileExt == ".docx")
            {
                List<string> list = con.FtpConexion();
                string ruta = list[2] + "PROPUESTAS/";

                if (Metodo.Value.Equals("ACTUALIZA")){
                    EliminarPropuesta(list[0], list[1]);
                    CargarPropuesta(ruta, list[0], list[1], 1);
                }else {
                    bool existe = con.ExisteDirectorio(ruta);
                    if (!existe) {
                       con.crearcarpeta(ruta);
                       CargarPropuesta(ruta, list[0], list[1],0);
                    }else { CargarPropuesta(ruta, list[0], list[1],0); }       
                }
            }else {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Formato no permitido, debe subir un archivo en PDF o Word";
            }
        } else{ 
             Linfo.ForeColor = System.Drawing.Color.Red;
             Linfo.Text = "Debe elegir un archivo";
        }   
    }
    private void CargarPropuesta(string ruta, string usuario, string pass, int i)
    {
        table = (System.Data.DataTable)(Session["Tabla"]);
        DataRow[] currentRows = table.Select(null, null, DataViewRowState.CurrentRows);
        string contentType = "", filename = Session["id"] + FUdocumento.FileName;
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta + filename);
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential(usuario, pass);
        using (Stream fs = FUdocumento.PostedFile.InputStream) {
            using (BinaryReader br = new BinaryReader(fs)){
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

                if (i == 0) { 
                    string query = "insert into propuesta (prop_codigo, prop_titulo, tem_codigo, prop_fecha, prop_documento, prop_nomarchivo, prop_tipo) values (propuestaid.nextval ,'" + TAnombre.Value + "' , '" + DDLtema.Items[DDLtema.SelectedIndex].Value + "',TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),  '" + ruta + "', '" + filename + "', '" + contentType + "')";
                    Ejecutar("1", query);
                    if (Verificador.Value.Equals("Funciono")){
                        string sql = "UPDATE ESTUDIANTE SET PROP_CODIGO = propuestaid.currval  WHERE USU_USERNAME = '" + Session["id"] + "'";
                        Ejecutar("1", sql);
                        if (Verificador.Value.Equals("Funciono")){
                            foreach (DataRow row in currentRows){
                                sql = "UPDATE ESTUDIANTE SET PROP_CODIGO=propuestaid.currval WHERE USU_USERNAME ='" + row["CODIGO"] + "'";
                                Ejecutar("Propuesta cargada satisfatoriamente", sql);
                            }
                            table.Rows.Clear();
                            GVagreinte.DataSource = table;
                            GVagreinte.Visible = false;
                            Verificador.Value = "";
                        }
                    }
                }else if (i ==1){
                    string query = "update  propuesta set prop_titulo='" + TAnombre.Value + "' ,tem_codigo='" + DDLtema.Items[DDLtema.SelectedIndex].Value.ToString() + "',prop_fecha=TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), prop_estado= 'PENDIENTE',prop_documento='"+ruta+"',prop_nomarchivo='"+filename+"', prop_tipo='"+contentType+"' where prop_codigo='" + codprop + "'";
                    Ejecutar("Propuesta cargada satisfatoriamente", query);
                }
            }
        }
        Quitar(i);
    }
    private void Quitar(int i)
    {
        if (i == 0) {
            LBSolicitar.Enabled = true;
            LBSolicitar.ForeColor = System.Drawing.Color.Black;
        }
        Ingreso.Visible = false;
        TAnombre.Value = "";
        LBSubir_propuesta.Enabled = false;
        LBSubir_propuesta.ForeColor = System.Drawing.Color.Gray;
        table = (System.Data.DataTable)(Session["Tabla"]);
        table.Clear();    
    }
    private void EliminarPropuesta(string usuario, string pass)
    {
        string ruta = "";
        string sql = "select Prop_Nomarchivo, Prop_Documento from propuesta  where Prop_Codigo ='" +codprop + "'";
        List<string> contenido = con.consulta(sql, 2, 0);
        ruta = contenido[1] + contenido[0];

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta);
        request.Method = WebRequestMethods.Ftp.DeleteFile;
        request.Credentials = new NetworkCredential(usuario, pass);
        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) { }

        sql = "update propuesta set prop_nomarchivo= null,Prop_Documento= null, Prop_Tipo=null where Prop_Codigo='"+codprop+"'";
        Ejecutar("", sql);
    }
    private void Ejecutar(string texto, string sql){
      string info = con.IngresarBD(sql);
      if (info.Equals("Funciono")){
            if (texto.Equals("1")) {
                Verificador.Value = "Funciono";
            }else{
                Linfo.ForeColor = System.Drawing.Color.Green;
                Linfo.Text = texto;
            } 
      }else{
         Linfo.ForeColor = System.Drawing.Color.Red;
         Linfo.Text = info;
      }
    }
    protected void AgregarIntegrante(object sender, EventArgs e)
    {
        String user = Session["id"].ToString();
        if (TBcodint.Text.Equals(user)){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "No puede agregarse a usted mismo como integrante de esta propuesta, puesto que ya esta ligado a esta. ";
            TBcodint.Text = "";
            Bintegrante.Visible = true;
            TBcodint.Enabled = true;
            RespInte.Visible = false;
            Bnueva.Visible = false;
        } else{
            table = (System.Data.DataTable)(Session["Tabla"]);
            row = table.NewRow();
            row["CODIGO"] = TBcodint.Text;
            row["INTEGRANTES"] = Rnombre.Text;

            table.Rows.Add(row);
            GVagreinte.DataSource = table;
            GVagreinte.DataBind();
        }  
    }
    protected void Bintegrante_Click(object sender, EventArgs e)
    {
        if (Bintegrante.Visible){
            if(string.IsNullOrEmpty(TBcodint.Text) == true) {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Ingrese codigo del estudiante!!";
            } else{
                string sql = "SELECT CONCAT(CONCAT(u.usu_apellido, ' '), u.usu_nombre) as integrantes  FROM USUARIO u, ESTUDIANTE e WHERE e.USU_USERNAME= '" + TBcodint.Text + "' and u.USU_USERNAME = e.USU_USERNAME ";
                List<string> list = con.consulta(sql, 1, 0);
                if (list.Count == 0){
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Codigo del estudiante no existe!!";
                    TBcodint.Text = "";
                    Bintegrante.Enabled = true;
                }else{                
                    Rnombre.Text =  list[0];
                    RespInte.Visible = true;
                    Bintegrante.Visible = false;
                    TBcodint.Enabled = false;
                    Bnueva.Visible = true;
                }
            }
        }else if (Bnueva.Visible){
            Rnombre.Text = "";
            RespInte.Visible = false;
            Bintegrante.Visible = true;
            Bnueva.Visible = false;
            TBcodint.Text = "";
            TBcodint.Enabled = true;
        }
    }
    protected void DDLlprof_SelectedIndexChanged(object sender, EventArgs e)
    {
        Linfo.Text = "";
        if (DDLlprof.SelectedIndex.Equals(0)){
            DDLtema.Items.Clear();
            DDLtema.Items.Insert(0, "Seleccione");
        }else{
            DDLtema.Items.Clear();
            string sql = "SELECT TEM_CODIGO, TEM_NOMBRE FROM TEMA WHERE TEM_ESTADO='ACTIVO' AND LINV_CODIGO='" + DDLlprof.Items[DDLlprof.SelectedIndex].Value.ToString() + "'";
            DDLtema.Items.AddRange(con.cargardatos(sql));
            DDLtema.Items.Insert(0, "Seleccione");
        }
    }

    /*Metodos que realizan la funcionalidad de consultar la propuesta*/
    protected void BuscarPropuesta()
    { 
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select p.PROP_CODIGO,p.PROP_TITULO, l.LINV_NOMBRE, t.TEM_NOMBRE,TO_CHAR( p.PROP_FECHA, 'dd/mm/yyyy') as FECHA ,INITCAP(p.PROP_ESTADO) as ESTADO  from propuesta p, estudiante e, lin_investigacion l, tema t where t.LINV_CODIGO = l.LINV_CODIGO and t.TEM_CODIGO = p.TEM_CODIGO and p.PROP_CODIGO = e.PROP_CODIGO and e.USU_USERNAME = '" + Session["id"] + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
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
    protected void GVconsulta_RowCommand(object sender, GridViewCommandEventArgs e)
    {     
        if (e.CommandName == "buscar"){
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsulta.Rows[index];
            ResultadoObservaciones();
            Observaciones.Visible = true;
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
        } catch (WebException a) {
            Linfo.Text = a.ToString();
        }
    }

    /*Metodo para consultar las observaciones de la propuesta*/
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        ResultadoObservaciones();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void ResultadoObservaciones()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "SELECT DISTINCT O.OBS_CODIGO, O.OBS_DESCRIPCION, O.OBS_REALIZADA FROM OBSERVACION O, ESTUDIANTE E WHERE E.PROP_CODIGO=O.PROP_CODIGO AND E.USU_USERNAME='" + Session["id"] + "' ORDER BY O.OBS_CODIGO";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVobservacion.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }  

    /*Metodos que sirven para buscar la informacion del profesor*/
    protected void InfProfesor(object sender, EventArgs e)
    {
        DDLlista.Enabled = false;
        Bconsulta.Visible = false;
        Tbotones2.Visible = true;
        CargarInfoProfesor();
        GVinfprof.Visible = true;
        Blimpiar.Visible = true;
        Linfo.Text = "";
    }
    private void CargarInfoProfesor()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select usu_username, CONCAT(CONCAT(usu_nombre, ' '),usu_apellido) as NOMBRE,  usu_correo  from usuario  where usu_username='" + DDLlista.Items[DDLlista.SelectedIndex].Value + "'";

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
    protected void GVinfprof_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void DescargarHV(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        List<string> list = con.FtpConexion();
        string fileName = "", contentype = "", ruta = "";
        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);
        string sql = "select PROF_NOMARCHIVO, PROF_DOCUMENTO, PROF_TIPO FROM PROFESOR WHERE USU_USERNAME='" + id + "'";
        List<string> prof = con.consulta(sql, 3, 0);
        fileName = prof[0];
        ruta = prof[1];
        contentype = prof[2];
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
        }catch (WebException a) {
            Linfo.Text = a.ToString();
        }
    }

    /*Metodos que sirven para la consulta de las solicitudes realizadas*/
    protected void GVsolicitud_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVsolicitud.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVsolicitud_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select s.dir_id, s.dir_fecha, s.dir_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.dir_observacion  from director s, usuario u where u.usu_username=s.usu_username and s.prop_codigo='" + prop_codigo + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
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
        } catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

}