using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    Conexion con = new Conexion();

    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    protected void Page_Init(object sender, EventArgs e)
    {
        
        // El código siguiente ayuda a proteger frente a ataques XSRF
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Utilizar el token Anti-XSRF de la cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generar un nuevo token Anti-XSRF y guardarlo en la cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Establecer token Anti-XSRF
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validar el token Anti-XSRF
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Error de validación del token Anti-XSRF.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] != null){
            if (!IsPostBack){
                logged.Visible = true;
                Lwelcome.Text += Session["Usuario"].ToString().ToUpper();          
            }
            sidebarwrapper.Visible = true;
            menu_dinamico();
        }else{
            sidebarwrapper.Visible = false;
            logged.Visible = false;
        }
    }

    private void menu_dinamico(){
        string rol = Session["rol"].ToString().Trim();
        String[] ciclo = rol.Split(' ');
        foreach (var cadena in ciclo){
            try{
                OracleConnection conn = con.crearConexion();
                if (conn != null){
                    string sql = "SELECT UNIQUE c.CATS_NOMBRE, c.CATS_ICONO, c.CATS_ESTADO  FROM OPCION_ROL r, OPCION_SISTEMA s, CATEGORIA_SISTEMA  c WHERE r.ROL_ID = '" + cadena + "' and s.OPCS_ID= r.OPCS_ID and s.CATS_ID = c.CATS_ID";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader drc1 = cmd.ExecuteReader();
                    if (drc1.HasRows){
                        while (drc1.Read()) {
                            if (drc1.GetString(2).Equals("ACTIVO")){
                                String cat = drc1.GetString(0).Replace(" ", "");
                                PHprueba.Controls.Add(new LiteralControl("<li data-toggle=\"collapse\" data-target=\"#" + cat + "\" class=\"collapsed\">"));
                                PHprueba.Controls.Add(new LiteralControl("<a href=\"#\">" + drc1.GetString(0) + "<i class=\"fa fa-sort-desce\"></i>  <span class=\"sub_icon " + drc1.GetString(1) + "\" ></span></a>"));

                                string sql1 = "SELECT s.OPCS_NOMBRE, s.OPCS_URL, s.OPCS_ESTADO FROM  OPCION_ROL r,OPCION_SISTEMA s, CATEGORIA_SISTEMA  c WHERE  r.ROL_ID = '" + cadena + "' AND s.OPCS_ID = r.OPCS_ID  AND s.CATS_ID = c.CATS_ID AND c.CATS_NOMBRE ='" + drc1.GetString(0) + "'";
                                OracleCommand cmd1 = new OracleCommand(sql1, conn);
                                cmd1.CommandType = CommandType.Text;
                                OracleDataReader drc2 = cmd1.ExecuteReader();

                                if (drc2.HasRows){
                                    PHprueba.Controls.Add(new LiteralControl("<ul class=\"sub-menu collapse\" id=\"" + cat + "\">"));
                                    while (drc2.Read()) {
                                        if (drc2.GetString(2).Equals("ACTIVO")){
                                           PHprueba.Controls.Add(new LiteralControl("<li><a href=" + drc2.GetString(1) + ">" + drc2.GetString(0) + "</a></li>"));
                                           PHprueba.Controls.Add(new LiteralControl("<li class=\"divider\"></li>"));
                                        }
                                    }
                                    PHprueba.Controls.Add(new LiteralControl("</ul>"));
                                }else {
                                    PHprueba.Controls.Add(new LiteralControl("<li><a>No tiene opciones</a></li>"));
                                }
                                drc2.Close();
                                PHprueba.Controls.Add(new LiteralControl("</li>"));
                            }
                        }
                    }else{
                       PHprueba.Controls.Add(new LiteralControl("<li><a>No tiene opciones</a></li>"));
                    }
                    drc1.Close();
                }
            }catch (Exception ex){
                Response.Write(ex.StackTrace);
            }
        }
    }

    protected void Evento_salir(object sender, EventArgs e){
        Session["Usuario"] = null;
        Session["rol"] = null;
        Session["id"] = null;
        sidebarwrapper.Visible = false;
        Response.Redirect("Default.aspx");
    }

}

