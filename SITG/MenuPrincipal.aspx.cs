using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Menu : Page{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null )
        {
            Response.Redirect("Default.aspx");
        }
    }

    /* serverUri parameter should start with the ftp:// scheme.
    if (serverUri.Scheme != Uri.UriSchemeFtp){
       return false;
    }*/
}
