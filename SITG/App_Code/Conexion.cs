using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;

public class Conexion : System.Web.UI.Page
{
    string info = "";

    public Conexion() { }

    public string Validarurl(int id, string url )
    {
        string returnvalue = "";
        try {
            OracleConnection conn = crearConexion();
            OracleCommand cmd = new OracleCommand();
            
            if (conn != null){
                cmd.Connection = conn;
                cmd.CommandText = "validaurl";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("returnVal", OracleDbType.Varchar2);
                cmd.Parameters["returnVal"].Direction = ParameterDirection.ReturnValue;
                cmd.Parameters["returnVal"].Size = 255;

                cmd.Parameters.Add("userName", OracleDbType.Int32).Value =id;
                cmd.Parameters.Add("ruta", OracleDbType.Varchar2).Value = url;

                cmd.ExecuteNonQuery();
                returnvalue = cmd.Parameters["returnVal"].Value.ToString();
            }
            conn.Close();
        }catch (Exception ex){
           return "Error al cargar la lista: " + ex.Message;
        }
        return returnvalue;
    }

    public List<string> FtpConexion()
    {
        List<string> list = new List<string>();
        string server = "ASUS VX199H";
        list.Add(server);
        string password = "cole1";
    
        list.Add(password);
        string url = "ftp://192.168.1.7/";
        list.Add(url);

        return list;
    }

    public void crearcarpeta(string ruta)
    {
        FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(ruta);
        ftpReq.Method = WebRequestMethods.Ftp.MakeDirectory;
        ftpReq.Credentials = new NetworkCredential("ASUS VX199H", "cole1");
        FtpWebResponse ftpResp = (FtpWebResponse)ftpReq.GetResponse();
    }

    public bool ExisteDirectorio(string ruta)
    {
        bool bExiste = true;
        try {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta);
            request.Credentials = new NetworkCredential("ASUS VX199H", "cole1");
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse respuesta = (FtpWebResponse)request.GetResponse();
        } catch (WebException ex) {
            if (ex.Response != null) {
                FtpWebResponse respuesta = (FtpWebResponse)ex.Response;
                if (respuesta.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable){
                    bExiste = false;
                }
            }
        }
        return bExiste;
    }

    public OracleConnection crearConexion() {
        OracleConnection conn = new OracleConnection();
        string id = "sitg";
        string pwd = "1234";
        conn.ConnectionString = "Data Source=PRUEBA ;" + "User Id=" + id + "; Password=" + pwd + ";";
        try {
            conn.Open();
        }catch (Exception ex) {
            Response.Write("Fallo!!" + ex);
        }
        return conn;
    }

    public string IngresarBD(string sentencia) {       ///Este metodo puede guardar, actualizar en la bd                   
        try {
            OracleConnection conn = crearConexion();
            if (conn != null)
            {
               string sql = sentencia;
               OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;              
                int count = cmd.ExecuteNonQuery();
                info = "es" + count;
                if (count != 0)
                {
                    info = "Funciono";
                }else{
                    info = "Error al guardar";
                }
                conn.Close();
                
            }
        } catch (Exception ex) {
            if (ex.Message.StartsWith("ORA-00001")) {
                info = "Error al guardar, el dato ya existe";
            } else {
                info = "Error al guardar los datos: " + ex.Message;
            }
        }
        return info;
    }

    public ListItem[] cargardatos(string sentencia) { // carga datos de la forma nombre- id
        List<ListItem> list = new List<ListItem>();
        try {
            OracleConnection conn = crearConexion();
            if (conn != null) {
                string sql = sentencia;
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
              
                while (dr.Read()) {
                  list.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
                
            }
            conn.Close();
        }
        catch (Exception ex) {
            Response.Write("Error al cargar la lista: " + ex.Message);
        }
        return list.ToArray();
    }

    public ListItem[] cargarDDLid(string setencia) { // carga solo el id
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = crearConexion();
            if (conn != null)
            {
                string sql = setencia;
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new ListItem(dr[0].ToString()));
                }
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Response.Write("Error al cargar la lista: " + ex.Message);
        }
        return list.ToArray();
    }

    public List<string> consulta(string secuencia, int cant, int enteros){ //metodo de consulta almacena en la lista valores enteros y cadena
        List<string> list = new List<string>();
        try{
            OracleConnection conn = crearConexion();
            if (conn != null){
                string sql = secuencia;
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader drc1 = cmd.ExecuteReader();
                if (drc1.HasRows){
                    while (drc1.Read()){
                        for (int i = 0; i < cant; i++){
                            if (i >= cant - enteros){
                                list.Add(drc1.GetInt64(i).ToString());
                                enteros--;
                            }else{
                                list.Add(drc1.GetString(i).ToString());
                            }
                        }
                    }
                }
                drc1.Close();
            }
        }catch (Exception ex){
            Response.Write("Error al cargar la lista: " + ex.StackTrace);
        }
        return list;
    }

    public string GetMD5(string str)
    {
        MD5 md5 = MD5CryptoServiceProvider.Create();
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] stream = null;
        StringBuilder sb = new StringBuilder();
        stream = md5.ComputeHash(encoding.GetBytes(str));
        for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
        return sb.ToString();
    }

}