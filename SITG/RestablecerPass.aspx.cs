using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;


public partial class RestablecerPass : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack)
        {
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "RestablecerPass.aspx");
            if (valida.Equals("false"))
            {
                Response.Redirect("MenuPrincipal.aspx");
            }
        }
    }

    /*Consultar usuario*/
    private void CargarTabladatos()
    {
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "select CONCAT(CONCAT(usu_nombre, ' '), usu_apellido) as nombre, usu_correo, usu_telefono from usuario where usu_username='"+TBcodigo.Text+"' ";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVdatos.DataSource = dataTable;
                }
                GVdatos.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVdatos_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void Aceptar(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TBcodigo.Text) == true)
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe ingresar código del usuario.";
        }else {
            TBcodigo.Enabled = false;
            Bacpetar.Visible = false;
            Bcancelar.Visible = true; 
            List<string> usuario = con.consulta("select usu_username from usuario where usu_username='"+TBcodigo.Text+"' and usu_estado='ACTIVO'",1,1);
            if (usuario.Count > 0){
                Mostrardatos.Visible = true;
                CargarTabladatos();
                Linfo.Text = "";
            } else {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "¡El usuario con el código ingresado no existe o se encuentra inactivo! ";
            }
        }
    }

    /*Metodo para poner nuevamente la contraseña*/
    protected void Restablecer(object sender, EventArgs e)
    {
        string pass = con.GetMD5(TBcodigo.Text);
        string sql = "Update usuario set usu_contrasena='"+pass+"' where usu_username='"+TBcodigo.Text+"'";
        Ejecutar("Contraseña restablecida correctamente", sql);
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
        borrar();

    }

    /*Metodo de borrar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        borrar();
    }
    private void borrar()
    {
        Mostrardatos.Visible = false;
        TBcodigo.Enabled = true;
        TBcodigo.Text = "";
        Bacpetar.Visible = true;
        Bcancelar.Visible = false;
    }

}