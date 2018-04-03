using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;
using System.Net;

public partial class Pagos : Conexion
{
    Conexion con = new Conexion();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!Page.IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Pagos.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }
        AnteAprobado();
        revisarpago();
      
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVconsulta);
    }

    /*Metodo que verifica que el anteproyecto fue aprobado.*/
    private void AnteAprobado()  {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string sql = "select A.Apro_Codigo from anteproyecto a , estudiante e where a.apro_codigo = E.Prop_Codigo and E.Usu_Username = '"+Session["id"]+"' and a.anp_estado = 'APROBADO' and a.anp_aprobacion = 'APROBADO'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows) {
                LBSubir_pago.Enabled = true;
                LBSubir_pago.ForeColor = System.Drawing.Color.Black;
            }else{
                LBSubir_pago.Enabled = false;
                LBSubir_pago.ForeColor = System.Drawing.Color.Gray;
            }
            drc1.Close();
        }
    }

    /* Metodo que verifica si el estado del pago*/
    private void revisarpago() {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "select Pag_Estado from pagos where usu_username = '" + Session["id"] + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows) {
                string estado = drc1[0].ToString();
                if (estado.Equals("RECHAZADO")){
                    LBSubir_pago.Enabled = true;
                    LBSubir_pago.ForeColor = System.Drawing.Color.Black;
                    Metodo.Value = "NO";
                } else{
                    LBSubir_pago.Enabled = false;
                    LBSubir_pago.ForeColor = System.Drawing.Color.Gray;
                }
            }else {
                LBSubir_pago.Enabled = true;
                LBSubir_pago.ForeColor = System.Drawing.Color.Black;
            }
            drc1.Close();
        }
    }

    /*Metodos que manejan la interfaz del subir-consultar*/
    protected void Subir_pago(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Consulta.Visible = false;
        Linfo.Text = "";
    }
    protected void Consulta_pago(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        Ingreso.Visible = false;
        Linfo.Text = "";
        BuscarPago();
    }

    /*Evento del boton guardar*/
    protected void Guardar(object sender, EventArgs e)
    {
        if (FUdocumento.HasFile) {
           
            string fileExt = System.IO.Path.GetExtension(FUdocumento.FileName);
            if (fileExt == ".pdf" || fileExt == ".doc" || fileExt == ".docx" || fileExt == ".xls" || fileExt == ".xlsx"){

               
                List<string> list = con.FtpConexion();
                if (Metodo.Value.Equals("NO"))
                {
                    EliminarPago(list[0], list[1]);
                }
                string ruta = list[2] + "PAGOS/";
                bool existe = con.ExisteDirectorio(ruta);
                if (!existe){
                    con.crearcarpeta(ruta);
                    CargarPagos(ruta, list[0], list[1]);
                }else { CargarPagos(ruta, list[0], list[1]); }
            }else {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Formato no permitido, debe subir un archivo en PDF, Word o Excel";
            }              
        } else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe elegir un archivo";
        }
    }
    private void EliminarPago(string usuario, string pass)
    {
        string ruta = "";
        string sql = "select PAG_NOMARCHIVO, PAG_DOCUMENTO FROM PAGOS WHERE USU_USERNAME = '" + Session["id"] + "'";
        List<string> contenido = con.consulta(sql, 2, 0);
        ruta = contenido[1] + contenido[0];

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta);
        request.Method = WebRequestMethods.Ftp.DeleteFile;
        request.Credentials = new NetworkCredential(usuario, pass);
        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) { }

        sql = "Delete from PAGOS where USU_USERNAME='" +Session["id"]+ "'";
        Ejecutar("", sql);
    }
    private void CargarPagos(string ruta, string usuario, string pass)
    {
        
        string contentType = "";
        string filename = Session["id"]+ FUdocumento.FileName;
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta + filename);
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential(usuario, pass);
        using (Stream fs = FUdocumento.PostedFile.InputStream){
            using (BinaryReader br = new BinaryReader(fs)) {
                contentType = FUdocumento.PostedFile.ContentType;
                byte[] fileContents = br.ReadBytes((Int32)fs.Length);
                StreamReader sourceStream = new StreamReader(FUdocumento.FileContent);
                sourceStream.Close();
                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                var response = (FtpWebResponse)request.GetResponse();
                Linfo.Text = response.StatusDescription;
                response.Close();
                string query = "insert into pagos (PAG_ID, PAG_NOMBRE, PAG_DOCUMENTO, PAG_NOMARCHIVO, PAG_TIPO, PAG_FECHA, USU_USERNAME) values " +
                    "(PAGOSID.nextval ,'"+ TBtitulo.Text + "' , '"+ruta+"', '"+filename+"', '"+contentType+ "', TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS') , '" + Session["id"] + "')";
                Ejecutar("Pago guardado satisfatoriamente, cuando sean verificados podrá subir el proyecto final", query);
            }
        }
        Quitar();
    }
    private void Quitar()
    {
        LBSubir_pago.Enabled = false;
        LBSubir_pago.ForeColor = System.Drawing.Color.Gray;
        Ingreso.Visible = false;
        TBtitulo.Text = "";
        Metodo.Value = "";
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

    /*Metodos que realizan la funcionalidad de consultar el documento de los pagos subido por el usuario*/
    protected void BuscarPago()
    {
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "select pag_id, pag_nombre,pag_tipo,pag_fecha, INITCAP(pag_estado) as estado ,pag_observacion from pagos where usu_username = '" + Session["id"] + "'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())  {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsulta.DataSource = dataTable;
                }
                GVconsulta.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);      
        List<string> list = con.FtpConexion();
        string fileName = "", contentype = "", ruta = "";

        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);

        string sql = "select PAG_NOMARCHIVO,PAG_DOCUMENTO, PAG_TIPO FROM PAGOS WHERE PAG_ID=" + id + "";
        List<string> pago = con.consulta(sql, 3, 0);
        fileName = pago[0];
        ruta = pago[1];
        contentype = pago[2];
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
        } catch (WebException a){
            Linfo.Text = a.ToString();
        }
    }

    /*Evento del boton limpiar*/
    protected void Blimpiar_Click(object sender, EventArgs e)
    {
        Linfo.Text = "";
        TBtitulo.Text = "";
    }
}