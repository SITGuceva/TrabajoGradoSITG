using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;

public partial class DatoPersonal : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
         if (!IsPostBack){              
                  Page.Form.Attributes.Add("enctype", "multipart/form-data");
                  Buscar();
         }
        validarol();
        
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.LBhv);
    }

    /*Valida si es de rol profesor para que le muestre que puede subir la hoja de vida*/
    private void validarol()
    {
        string rol = Session["rol"].ToString().Trim();
        String[] ciclo = rol.Split(' ');
        foreach (var cadena in ciclo){
            if (cadena.Equals("DOC") || cadena.Equals("EXT")){
                HVdoc.Visible = true;
                RolP.Value = cadena;
                BuscarHV();
                break;
            }else {
                RolP.Value = "";
                HVdoc.Visible = false;
                Lhv.Visible = false;
                LBhv.Visible = false;
            }
        }
    }

    private void BuscarHV(){
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "";

            if (RolP.Value.Equals("DOC")) {
               sql= "select Prof_Documento from profesor where Usu_Username='" + Session["id"] + "'";
            } else {
               sql= "select ext_documento from externo where usu_username='" + Session["id"] + "'";
            }
             
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){
                if (drc1.IsDBNull(0)){
                    Lhv.Visible = false;
                    LBhv.Visible = false;
                    Linfo.Text = "Debe subir la hoja de vida para que la información se encuentre actualizada.";
                } else{
                    Lhv.Visible = true;
                    LBhv.Visible = true;
                    Verificar.Value = "NO";
                }
            }
            drc1.Close();
        }
    }

    /*Metodo para buscar la  informacion del usuario*/
    private void Buscar()
    {
        int id = Int32.Parse(Session["id"].ToString());
        string sql = "SELECT USU_NOMBRE,USU_APELLIDO, USU_DIRECCION,USU_CORREO, USU_TELEFONO FROM USUARIO WHERE USU_USERNAME='" + id + "'";
        List<string> list = con.consulta(sql, 5, 1);
         TBnombre.Text = list[0];
         TBapellido.Text = list[1];
         TBdireccion.Text = list[2];
         TBcorreo.Text = list[3];
         TBtelefono.Text = list[4];
        Lanscod.Text = Session["id"].ToString();
    }

    /*Metodos para actualizar la informacion del usuario*/
    protected void Aceptar(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBapellido.Text) == true || string.IsNullOrEmpty(TBtelefono.Text) == true || string.IsNullOrEmpty(TBdireccion.Text) == true || string.IsNullOrEmpty(TBcorreo.Text) == true) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Los campos son obligatorios";
        } else{
            if (HVdoc.Visible) {
                if (FUdocumento.HasFile){
                    string fileExt = System.IO.Path.GetExtension(FUdocumento.FileName);
                    if (fileExt == ".pdf" || fileExt == ".doc" || fileExt == ".docx") {
                        List<string> list = con.FtpConexion();

                        if (Verificar.Value.Equals("NO")) {  EliminarHV(list[0], list[1]); }
                        string ruta = list[2] + "HV/";
                        bool existe = con.ExisteDirectorio(ruta);
                        if (!existe) {
                            con.crearcarpeta(ruta);
                            Guardar(ruta, list[0], list[1]);
                        } else { Guardar(ruta, list[0], list[1]);}
                    } else{
                        Linfo.ForeColor = System.Drawing.Color.Red;
                        Linfo.Text = "Formato no permitido, debe subir un archivo en PDF o un documento de Word";
                    }  
                } else {
                    string sql = "UPDATE USUARIO SET USU_NOMBRE='" + TBnombre.Text + "', USU_APELLIDO='" + TBapellido.Text + "', USU_TELEFONO='" + TBtelefono.Text + "', USU_DIRECCION='" + TBdireccion.Text + "', USU_CORREO='" + TBcorreo.Text + "'WHERE USU_USERNAME='" + Lanscod.Text + "'";
                    Ejecutar("Datos Modificados Satisfactoriamente", sql);
                }
            } else {
                string sql = "UPDATE USUARIO SET USU_NOMBRE='" + TBnombre.Text + "', USU_APELLIDO='" + TBapellido.Text + "', USU_TELEFONO='" + TBtelefono.Text + "', USU_DIRECCION='" + TBdireccion.Text + "', USU_CORREO='" + TBcorreo.Text + "'WHERE USU_USERNAME='" + Lanscod.Text + "'";
                Ejecutar("Datos Modificados Satisfactoriamente", sql);
            }   
        }
    }
    private void EliminarHV(string usuario, string pass){
        string ruta = "";
        string sql = "", sql2="";

        if (RolP.Value.Equals("DOC")) {
            sql = "select PROF_NOMARCHIVO, PROF_DOCUMENTO FROM PROFESOR WHERE USU_USERNAME = '" + Session["id"] + "'";
            sql2 = "update profesor set prof_nomarchivo= null,Prof_Documento= null, Prof_Tipo=null where usu_username='" + Session["id"] + "'";
        } else {
            sql = "select EXT_NOMARCHIVO, EXT_DOCUMENTO FROM EXTERNO WHERE USU_USERNAME = '" + Session["id"] + "'";
            sql2 = "update externo set ext_nomarchivo= null, ext_Documento= null, ext_Tipo=null where usu_username='" + Session["id"] + "'";
        }

        List<string> contenido = con.consulta(sql, 2, 0);
        ruta = contenido[1] + contenido[0];

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta);
        request.Method = WebRequestMethods.Ftp.DeleteFile;
        request.Credentials = new NetworkCredential(usuario, pass);
        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) { }

        Ejecutar("", sql2);
    }
    private void Guardar(string ruta, string usuario, string pass){
        string contentType = "";
        string filename = Lanscod.Text + FUdocumento.FileName;

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta + filename);
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential(usuario, pass);
        using (Stream fs = FUdocumento.PostedFile.InputStream){
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
                Linfo.Text = response.StatusDescription;
                response.Close();

                string query = "";
                if (RolP.Value.Equals("DOC")){
                    query = "update PROFESOR set prof_documento='" + ruta + "', prof_nomarchivo='" + filename + "',prof_tipo='" + contentType + "' where usu_username='" + Session["id"] + "'";
                } else {
                    query = "update EXTERNO set ext_documento='" + ruta + "', ext_nomarchivo='" + filename + "',ext_tipo='" + contentType + "' where usu_username='" + Session["id"] + "'";
                }
                Ejecutar("", query);
            }
        }
        string sql = "UPDATE USUARIO SET USU_NOMBRE='" + TBnombre.Text + "', USU_APELLIDO='" + TBapellido.Text + "', USU_TELEFONO='" + TBtelefono.Text + "', USU_DIRECCION='" + TBdireccion.Text + "', USU_CORREO='" + TBcorreo.Text + "'WHERE USU_USERNAME='" + Lanscod.Text + "'";
        Ejecutar("Datos Modificados Satisfactoriamente", sql);
        Verificar.Value = "NO";
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")) {
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        } else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        TBnombre.Text = "";
        TBapellido.Text = "";
        TBtelefono.Text = "";
        TBdireccion.Text = "";
        TBcorreo.Text = "";
        Linfo.Text = "";
    }

    /*Evento para descargar la hoja de vida*/
    protected void LBhv_Click(object sender, EventArgs e)
    {
        List<string> list = con.FtpConexion();
        string fileName = "", contentype = "", ruta = "";
        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);

        string sql = "";
        if (RolP.Value.Equals("DOC")){
            sql = "select PROF_NOMARCHIVO, PROF_DOCUMENTO, PROF_TIPO FROM PROFESOR WHERE USU_USERNAME='" + Session["id"] + "'";
        }else{
            sql = "select EXT_NOMARCHIVO, EXT_DOCUMENTO, EXT_TIPO FROM EXTERNO WHERE USU_USERNAME='" + Session["id"] + "'";
        }

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
        }
        catch (WebException a)
        {
            Linfo.Text = a.ToString();
        }
    }

}