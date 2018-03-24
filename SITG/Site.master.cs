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
        List<ListItem> categoria = new List<ListItem>();
        List<String[]> Opciones = new List<String[]>();
        foreach (var cadena in ciclo) {
            try{
                OracleConnection conn = con.crearConexion();
                if (conn != null){
                    string sql = "SELECT UNIQUE c.CATS_NOMBRE, c.CATS_ICONO, c.CATS_ESTADO  FROM OPCION_ROL r, OPCION_SISTEMA s, CATEGORIA_SISTEMA  c WHERE r.ROL_ID = '" + cadena + "' and s.OPCS_ID= r.OPCS_ID and s.CATS_ID = c.CATS_ID";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader drc1 = cmd.ExecuteReader();
                    if (drc1.HasRows){
                        while (drc1.Read()){
                            if (drc1.GetString(2).Equals("ACTIVO")) {
                                String cat = drc1.GetString(0).Replace(" ", "");
                                bool existe = false;
                                if (categoria.Count.Equals(0)) {
                                    categoria.Add(new ListItem(drc1[1].ToString(), cat));
                                }else{
                                    for (int i = 0; i < categoria.Count; i++){
                                        if (categoria[i].Value.Equals(cat)){
                                            existe = true;
                                            break;
                                        }else{ existe = false;}
                                    }
                                    if (existe.Equals(false)) { categoria.Add(new ListItem(drc1[1].ToString(), cat)); }
                                }
                                string sql1 = "SELECT s.OPCS_NOMBRE, s.OPCS_URL, s.OPCS_ESTADO FROM  OPCION_ROL r,OPCION_SISTEMA s, CATEGORIA_SISTEMA  c WHERE  r.ROL_ID = '" + cadena + "' AND s.OPCS_ID = r.OPCS_ID  AND s.CATS_ID = c.CATS_ID AND c.CATS_NOMBRE ='" + drc1.GetString(0) + "'";
                                OracleCommand cmd1 = new OracleCommand(sql1, conn);
                                cmd1.CommandType = CommandType.Text;
                                OracleDataReader drc2 = cmd1.ExecuteReader();
                                if (drc2.HasRows){
                                    while (drc2.Read()) {
                                        if (drc2.GetString(2).Equals("ACTIVO")){
                                            string[] subopc = new string[3];
                                            subopc[0] = drc2[0].ToString();
                                            subopc[1] = drc2[1].ToString();
                                            subopc[2] = cat;
                                            Opciones.Add(subopc);
                                        }
                                    }
                                }
                                drc2.Close();
                            }
                        }
                    }
                    drc1.Close();
                }
            }catch (Exception ex){ Response.Write(ex.StackTrace);}
        }


        for (int m = 0; m < categoria.Count; m++){
            PHprueba.Controls.Add(new LiteralControl("<li data-toggle=\"collapse\" onclick=\"ocultartodo()\" data-target=\"#" + categoria[m].Value + "\" class=\"collapsed\">"));
            PHprueba.Controls.Add(new LiteralControl("<a href=\"#\">" + categoria[m].Value + "<i class=\"fa fa-sort-desce\"></i>  <span class=\"sub_icon " + categoria[m].Text + "\" ></span></a>"));
            PHprueba.Controls.Add(new LiteralControl("<ul class=\"nav nav-second-level\" name=\"opciones\" id =\"" + categoria[m].Value + "\">"));
            for (int h = 0; h < Opciones.Count; h++) {
                if (Opciones[h][2].Contains(categoria[m].Value)) {
                    PHprueba.Controls.Add(new LiteralControl("<li><a href=" + Opciones[h][1] + ">" + Opciones[h][0] + "</a></li>"));
                    PHprueba.Controls.Add(new LiteralControl("<li class=\"divider\"></li>"));
                }
            }
            PHprueba.Controls.Add(new LiteralControl("</ul>"));
            PHprueba.Controls.Add(new LiteralControl("</li>"));
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

