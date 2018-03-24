using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProyFinalAsignado : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "ProyFinalAsignado.aspx");
            if (valida.Equals("false")) {
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                CargarPoyectosAsignado();
            }
        }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVpfasignado);
    }
 
    /*Metodos para consultar los proyectos asignados del jurado*/
    protected void GVpfasignado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVpfasignado.PageIndex = e.NewPageIndex;
        CargarPoyectosAsignado();
    }
    protected void GVpfasignado_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVpfasignado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ProyectoF")
        {
            Consulta.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVpfasignado.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene el codigo de propuesta en la tabla
            Terminar.Visible = true;
            Linfo.Text = "";
            criterios.Visible = true;
            CargarCriterios();
        }
    }
    private void CargarPoyectosAsignado()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select DISTINCT  P.Ppro_Codigo, P.Pf_Titulo, P.Pf_Fecha, P.Pf_Estado, P.Pf_Aprobacion from proyecto_final p, estudiante e, profesor d , jurado j " +
                    "WHERE J.Usu_Username = '"+Session["id"]+"' and E.Prop_Codigo = P.Ppro_Codigo and P.Pf_Estado = 'PENDIENTE'and P.Pf_Aprobacion = 'APROBADO' and D.Usu_Username = J.Usu_Username and J.Ppro_Codigo = P.Ppro_Codigo and J.Jur_Revisado = 'PENDIENTE'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVpfasignado.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVpfasignado.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        List<string> list = con.FtpConexion();
        string fileName = "", contentype = "", ruta = "";

        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);

        string sql = "select PF_NOMARCHIVO, PF_DOCUMENTO, PF_TIPO FROM PROYECTO_FINAL WHERE PPRO_CODIGO=" + id + "";
        List<string> pf = con.consulta(sql, 3, 0);
        fileName = pf[0];
        ruta = pf[1];
        contentype = pf[2];

        try {
            byte[] bytes = request.DownloadData(ruta + fileName);
            string fileString = System.Text.Encoding.UTF8.GetString(bytes);
            Console.WriteLine(fileString);
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentype;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }catch (WebException a){
            Linfo.Text = a.ToString();
        }
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

    /*Eventos de los botones cancelar, regresar y terminar revision */
    protected void terminar(object sender, EventArgs e)
    {
        if (DDLestado.SelectedIndex.Equals(0)) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe calificar el anteproyecto.";
        } else {
            guardarCriterios(); 
        }
    }
    private void guardarCriterios()
    {
        foreach (GridViewRow row in GVcriterios.Rows){
            CheckBox check = row.FindControl("CBcumplio") as CheckBox;
            if (!check.Checked){
                int id = Convert.ToInt32(row.Cells[0].Text);
                string sql = "insert into evalua_criterios values (REVCRITID.nextval,'No','"+id+"', '"+Metodo.Value+"')";
                Ejecutar("", sql);
            }
        }
        if(string.IsNullOrEmpty(TBbs.Value) == false){
            string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
            string sql = "insert into pf_observacion (PFOBS_CODIGO, PFOBS_DESCRIPCION, PPRO_CODIGO ,PFOBS_FECHA, PFOBS_REALIZADA) values (OBSPROYFID.nextval,'" + TBbs.Value.ToLower() + "','" + Metodo.Value + "',TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), 'JURADO')";
            Ejecutar("", sql);
        }
        CambiaEstado();
    }
    private void CambiaEstado()
    {
        string revision = "update jurado set jur_revisado='REVISADO' where ppro_codigo='" + Metodo.Value + "' and usu_username='" + Session["id"] + "'";
        Ejecutar("", revision);

        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string sql = "select Jur_Num from jurado where Usu_Username = '" + Session["id"] + "' and Ppro_Codigo = '" + Metodo.Value + "'";
        List<string> list = con.consulta(sql, 1, 1);
        int num = Convert.ToInt32(list[0]);

        string sentencia = "";
        if (num.Equals(1)) {
            sentencia = "update proyecto_final set Pf_Jur1='" + DDLestado.Items[DDLestado.SelectedIndex].Value.ToString() + "' where Ppro_Codigo='" + Metodo.Value + "'";
        } else if (num.Equals(2)) {
            sentencia = "update proyecto_final set Pf_Jur2='" + DDLestado.Items[DDLestado.SelectedIndex].Value.ToString() + "' where Ppro_Codigo='" + Metodo.Value + "'";
        }else if (num.Equals(3)){
            sentencia = "update proyecto_final set Pf_Jur3='" + DDLestado.Items[DDLestado.SelectedIndex].Value.ToString() + "' where Ppro_Codigo='" + Metodo.Value + "'";
        }

        Ejecutar("El proyecto ha sido revisado con exito, presione click en regresar para revisar otro.", sentencia);

        Terminar.Visible = false;
        IBregresar.Visible = true;
        Metodo.Value = "";
        criterios.Visible = false;
    }
    protected void cancelar(object sender, EventArgs e)
    {      
        Terminar.Visible = false;
        CargarPoyectosAsignado();
        Consulta.Visible = true;
        DDLestado.SelectedIndex = 0;
        Metodo.Value = "";
        criterios.Visible = false;
    }
    protected void regresar(object sender, EventArgs e)
    {
        IBregresar.Visible = false;
        CargarPoyectosAsignado();
        Consulta.Visible = true;
        Metodo.Value = "";
    }

    /*Metodos que manejan los criterios*/
    public void CargarCriterios()
    {
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
               string sql = "SELECT crit_codigo, crit_nombre, crit_tipo FROM criterios WHERE crit_estado= 'ACTIVO' order by crit_codigo";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()) {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVcriterios.DataSource = dataTable;
                }
                GVcriterios.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVcriterios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVcriterios.PageIndex = e.NewPageIndex;
        CargarCriterios();
    }
    protected void GVcriterios_RowDataBound(object sender, GridViewRowEventArgs e){}
   
}