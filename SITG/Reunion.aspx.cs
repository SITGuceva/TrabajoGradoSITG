using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Calendario : System.Web.UI.Page
{
   
   Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        
    }

    /*Metodos de crear-modificar-consultar-inhabilitar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Botones.Visible = true;
        Linfo.Text = "";
    }

    /*Metodos que se utilizan para guardar-actualizar-inhabilitar*/
    protected void Aceptar(object sender, EventArgs e)
    {
        string sql = "", sql2 = "";
        sql2= "select c.COM_CODIGO from comite c, profesor p where p.COM_CODIGO = c.COM_CODIGO and p.USU_USERNAME = '"+Session["id"]+"'";
        List<string> list = con.consulta(sql2, 1, 1);
       
       string com= list[0];
        string fecha = DateTime.Now.ToString("dd/MM/yyyy, HH:mm:ss");
        sql = "insert into REUNION (REU_CODIGO,REU_FPROP,COM_CODIGO, REU_ESTADO) VALUES(reunionid.nextval,TO_DATE( '"+fecha+ "', 'DD-MM-YYYY HH24:MI:SS'),'" + com+"', 'ACTIVO')";
        Ejecutar("Datos guardados satisfactoriamente", sql);
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

}