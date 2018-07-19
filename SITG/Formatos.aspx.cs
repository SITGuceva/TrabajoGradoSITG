using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Formatos : System.Web.UI.Page
{
    Conexion con = new Conexion();
    protected int widestData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Formatos.aspx");
            if (valida.Equals("false")) {
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
         
        }
        widestData = 0;

        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVformatos);
    }

    /*Metodos de crear-consultar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Linfo.Text = "";
        ConsultaFormat.Visible = false;
    }
    protected void Consultar(object sender, EventArgs e)
    {
        Ingreso.Visible = false;
        ConsultaFormat.Visible = true;
        Linfo.Text = "";
        ResultadoConsulta();
    }

    /*Metodos que realizan el guardar */
    protected void Aceptar(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TBnom.Text) == true){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Los campos son obligatorios";
        }else{
            if (FUdocumento.HasFile) {
                string fileExt =System.IO.Path.GetExtension(FUdocumento.FileName);
                if (fileExt == ".pdf" || fileExt == ".doc" || fileExt == ".docx" || fileExt == ".xls" || fileExt == ".xlsx") {
                    List<string> list = con.FtpConexion();
                    string ruta = list[2] + "FORMATOS/";
                    bool existe = con.ExisteDirectorio(ruta);
                    if (!existe){
                        con.crearcarpeta(ruta);
                        Guardar(ruta, list[0], list[1]);
                    }else { Guardar(ruta, list[0], list[1]); }
                }else{
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Formato no permitido, debe subir un archivo en PDF, Word o Excel";
                }
            } else{
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Debe elegir un archivo";
            }
        }
    }
    private void Guardar(string ruta, string usuario, string pass)
    {
        string filename = "", contentType = "";
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta + FUdocumento.FileName);
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential(usuario, pass);
        using (Stream fs = FUdocumento.PostedFile.InputStream) {
            using (BinaryReader br = new BinaryReader(fs)){
                filename = Path.GetFileName(FUdocumento.PostedFile.FileName);
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
                string sql = "insert into FORMATO (FOR_ID, FOR_TITULO, FOR_NOMARCHIVO, FOR_TIPO, FOR_DOCUMENTO) values (formatoid.nextval ,'" + TBnom.Text + "' , '" + filename + "', '" + contentType + "','" + ruta + "')";
                Ejecutar("Formato guardado satisfatoriamente", sql);
            }
        }
        TBnom.Text = "";
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")) {
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        } else {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }
  
    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";
         TBnom.Text = "";
    }

    /*Metodos que realizan la consulta con el modificar e inhabilitar*/
    protected void DownloadFile(object sender, EventArgs e)
    {
        List<string> list = con.FtpConexion();
        int id = int.Parse((sender as LinkButton).CommandArgument);
        string fileName = "", contentype = "", ruta = "";
        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);
        string sql = "select FOR_NOMARCHIVO, FOR_DOCUMENTO, FOR_TIPO FROM FORMATO WHERE FOR_ID=" + id + "";

        OracleConnection conn = con.crearConexion();
        if (conn != null){
            using (OracleCommand cmd = new OracleCommand(sql, conn)){
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader()) {
                    drc1.Read();
                    contentype = drc1["FOR_TIPO"].ToString();
                    fileName = drc1["FOR_NOMARCHIVO"].ToString();
                    ruta = drc1["FOR_DOCUMENTO"].ToString();

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
        }
        conn.Close();
    }
    private void ResultadoConsulta(){
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "SELECT FOR_ID, FOR_TITULO FROM FORMATO ORDER BY FOR_ID";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVformatos.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVformatos.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVformatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVformatos.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    protected void GVformatos_RowDataBound(object sender, GridViewRowEventArgs e) {}
    protected void GVformatos_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            TextBox nombre = (TextBox)GVformatos.Rows[e.RowIndex].Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVformatos.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update formato set for_titulo = '" + nombre.Text + "' where  for_id ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){
                GVformatos.EditIndex = -1;
                ResultadoConsulta();
            }
        }
        conn.Close();
    }
    protected void GVformatos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVformatos.EditIndex = e.NewEditIndex;
        ResultadoConsulta();
        GVformatos.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVformatos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVformatos.EditIndex = -1;
        ResultadoConsulta();
    }
    protected void GVformatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GVformatos.Rows[e.RowIndex].Cells[0].Text;
        eliminar(id);        
    }
    private void eliminar(string id)
    {
        List<string> list = con.FtpConexion();
        string ruta = "";
        string sql = "select FOR_NOMARCHIVO, FOR_DOCUMENTO FROM FORMATO WHERE FOR_ID = '" + id + "'";
        List<string> contenido = con.consulta(sql, 2, 0);
        ruta= contenido[1]+ contenido[0];

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta);
        request.Method = WebRequestMethods.Ftp.DeleteFile;
        request.Credentials = new NetworkCredential(list[0], list[1]);
        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) {}

        sql = "Delete from FORMATO where FOR_ID='" + id + "'";
        Ejecutar("", sql);
        ResultadoConsulta();
    }

}