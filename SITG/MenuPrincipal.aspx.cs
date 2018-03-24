using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MenuPrincipal : Page
{
    Conexion con = new Conexion();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }

       /* EST.Visible = true;
        ADM.Visible = false;
        estudianterol.Visible = false;
        profesorrol.Visible = false;
        comiterol.Visible = false;
        directorrol.Visible = false;*/

        consultaroles();
    }


    protected void consultaroles() {
        
        string rol = Session["rol"].ToString().Trim();
        String[] ciclo = rol.Split(' ');
        // label.Text = ciclo.Length+ "es";
        // foreach (var cadena in ciclo)
        string nombre = "";
       for(int i=0; i<ciclo.Length; i++) {
            //label.Text += ciclo[i] + "jaja";


            if (ciclo[i].Equals("EST"))
            {
                nombre = "Estudiante";
            }
            if (ciclo[i].Equals("DOC"))
            {
                nombre = "Docente";
            }
            if (ciclo[i].Equals("COM"))
            {
                nombre = "Comité";
            }
            if (ciclo[i].Equals("DIR"))
            {
                nombre = "Director";
            }
            if (ciclo[i].Equals("JUR"))
            {
                nombre = "Jurado";
            }
            if (ciclo[i].Equals("EVA"))
            {
                nombre = "Evaluador";
            }
            if (ciclo[i].Equals("DEC"))
            {
                nombre = "Decana";
            }
            Notificaciones.Controls.Add(new LiteralControl("<li style='background-color:black'><a href =\"#"+ciclo[i]+ "\" data-toggle=\"tab\" style=\"color:gray;\" > " +nombre+"</a></li>"));
            
            /*   if (cadena.Equals("EST"))
               {

                   estudianterol.Visible = true;
               }
               if (cadena.Equals("DOC"))
               {
                   profesorrol.Visible = true;
               }

               if (cadena.Equals("COM"))
               {

                   comiterol.Visible = true;
               }*/
        }
    }



}

