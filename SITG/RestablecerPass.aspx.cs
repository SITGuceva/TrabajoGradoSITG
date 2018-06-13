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
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "CambiarPass.aspx");
            if (valida.Equals("false"))
            {
                Response.Redirect("MenuPrincipal.aspx");
            }
        }
        LBcancelar.Visible = false;
    }

    

    private void CargarTabladatos()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select CONCAT(CONCAT(usu_nombre, ' '), usu_apellido) as nombre, usu_correo, usu_telefono from usuario where usu_username='"+TBcodigo.Text+"'";
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

























    protected void Restablecer(object sender, EventArgs e)
    {
        string pass = con.GetMD5(TBcodigo.Text);
        string sql = "Update usuario set usu_contrasena='"+pass+"' where usu_username='"+TBcodigo.Text+"'";
        Ejecutar("", sql);
    }


    protected void Aceptar(object sender, EventArgs e)
    {
        Mostrardatos.Visible = true;
        CargarTabladatos();
        TBcodigo.Enabled = false;
        LBacpetar.Visible = false;
        LBcancelar.Visible = true;
        LBrestablecer.Visible = true;
        Linfo.Text = "";


    }

    protected void Limpiar(object sender, EventArgs e)
    {
        Mostrardatos.Visible = false;
        TBcodigo.Enabled = true;
        LBacpetar.Visible = true;
        LBcancelar.Visible = false;
        LBrestablecer.Visible = false;


    }

    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        texto = "Contraseña restablecida correctamente";
        Linfo.ForeColor = System.Drawing.Color.Green;
        Linfo.Text = texto;
        Mostrardatos.Visible = false;
        TBcodigo.Enabled = true;
        TBcodigo.Text = "";
        LBacpetar.Visible = true;
        LBcancelar.Visible = false;
        LBrestablecer.Visible = false;


    }


}