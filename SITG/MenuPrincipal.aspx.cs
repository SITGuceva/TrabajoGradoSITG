
using System;
using System.Web.UI;

public partial class MenuPrincipal : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null )
        {
            Response.Redirect("Default.aspx");
        }  
    }

}