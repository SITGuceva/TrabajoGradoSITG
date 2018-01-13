using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class Conexion : System.Web.UI.Page
{
    string info = "";

    public Conexion() { }

    public OracleConnection crearConexion() {
        OracleConnection conn = new OracleConnection();
        string id = "sitg";
        string pwd = "1234";
        conn.ConnectionString = "Data Source=PRUEBA ;"
        + "User Id=" + id + "; Password=" + pwd + ";";
        try
        {
            conn.Open();
        }
        catch (Exception ex)
        {
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
                                list.Add(drc1.GetInt32(i).ToString());
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