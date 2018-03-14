using System;
using System.Data;
using System.Web.UI;
using Oracle.DataAccess.Client;
using System.Security.Cryptography;
using System.Text;

public partial class _Default : Page{

    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e){
        if (Session["Usuario"] != null )
        {
            Response.Redirect("~/MenuPrincipal.aspx");
        }
    }

    protected void LogIn(object sender, EventArgs e){
        string estado = "", username = "", rol="";
        int id = 0;
        try{
            OracleConnection conn = con.crearConexion();         
            string pass = GetMD5(Password.Text);
            int usuario = Convert.ToInt32(UserName.Text);
            if (conn != null){            
                string sql = "select  UR.ROL_ID,U.USU_NOMBRE, U.USU_APELLIDO, U.USU_USERNAME, U.USU_ESTADO from USUARIO_ROL UR, USUARIO U  WHERE U.USU_USERNAME='" + usuario + "' AND U.USU_CONTRASENA='" + pass + "' AND U.USU_USERNAME=UR.USU_USERNAME ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows){
                    while (dr.Read()){                       
                        rol += dr.GetString(0) +" " ;
                        username = dr.GetString(1)+" "+dr.GetString(2);
                        id = dr.GetInt32(3);
                        estado = dr.GetString(4);
                    }
                    if (estado.Equals("ACTIVO"))
                    {
                        Session["rol"] = rol;
                        Session["id"] = id;
                        Session["usuario"] = username;
                        Response.Redirect("~/MenuPrincipal.aspx", false);
                    }
                    else
                    {
                        Session["usuario"] = null;
                        Session["rol"] = null;
                        Session["id"] = null;
                        Lerror.Text = "El usuario no se encuentra activo.";
                    }
                    
                }else{                   
                    Lerror.Text = "Usuario o contraseña incorrecta";
                    UserName.Text = "";
                }
            }
        }catch (Exception ex){
           Lerror.Text=ex.StackTrace;          
        }
    }

    public string GetMD5(string str){
        MD5 md5 = MD5CryptoServiceProvider.Create();
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] stream = null;
        StringBuilder sb = new StringBuilder();
        stream = md5.ComputeHash(encoding.GetBytes(str));
        for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
        return sb.ToString();
    }
}



