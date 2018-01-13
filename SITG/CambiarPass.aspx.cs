using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CambiarPass : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void Aceptar(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TBpassactual.Text) == true || string.IsNullOrEmpty(TBpassnueva.Text) == true || string.IsNullOrEmpty(TBpassnueva2.Text) == true)
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Los campos son obligatorios";
        }
        else { 
            if (TBpassnueva.Text.Equals(TBpassnueva2.Text))
            {
                string pass = con.GetMD5(TBpassactual.Text);
                string passnueva = con.GetMD5(TBpassnueva.Text);       
                string sql = "UPDATE USUARIO SET USU_CONTRASENA='"+passnueva+"' WHERE USU_USERNAME='"+Session["id"].ToString()+"' AND USU_CONTRASENA='"+pass+"'";
                string info = con.IngresarBD(sql);
                if (info.Equals("Funciono")){
                    Linfo.ForeColor = System.Drawing.Color.Green;
                    Linfo.Text = "La contraseña ha sido actualizada satisfactoriamente";
                    TBpassactual.Text = "";
                    TBpassnueva.Text = "";
                    TBpassnueva2.Text = "";
                }
                else{
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Contraseña incorrecta";
                    TBpassactual.Text = "";
                }

            }else{
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Las contraseñas no coinciden";
                TBpassnueva.Text = "";
                TBpassnueva2.Text = "";
            }
        }
    }

    protected void Limpiar(object sender, EventArgs e)
    {
        TBpassactual.Text = "";
        TBpassnueva.Text = "";
        TBpassnueva2.Text = "";
        Linfo.Text = "";
    }


}