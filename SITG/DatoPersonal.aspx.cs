using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatoPersonal : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }else
        {
            if (!IsPostBack)
            {
                Buscar();
            }
        } 
    }

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


    protected void Aceptar(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBapellido.Text) == true || string.IsNullOrEmpty(TBtelefono.Text) == true || string.IsNullOrEmpty(TBdireccion.Text) == true || string.IsNullOrEmpty(TBcorreo.Text) == true)
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Los campos son obligatorios";
        }
        else
        {
            string sql = "UPDATE USUARIO SET USU_NOMBRE='"+TBnombre.Text+"', USU_APELLIDO='"+TBapellido.Text+"', USU_TELEFONO='"+TBtelefono.Text+"', USU_DIRECCION='"+TBdireccion.Text+"', USU_CORREO='"+TBcorreo.Text+"'WHERE USU_USERNAME='"+Lanscod.Text+"'";
            string info = con.IngresarBD(sql);
            if (info.Equals("Funciono")){
                Linfo.ForeColor = System.Drawing.Color.Green;
                Linfo.Text = "Datos Modificados Satisfactoriamente";
                //Session["Usuario"] = TBnombre.Text + " " + TBapellido.Text;
                Buscar();
            }else{
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = info;
            }
        }
    }

    protected void Limpiar(object sender, EventArgs e)
    {
        TBnombre.Text = "";
        TBapellido.Text = "";
        TBtelefono.Text = "";
        TBdireccion.Text = "";
        TBcorreo.Text = "";
        Linfo.Text = "";
    }

}