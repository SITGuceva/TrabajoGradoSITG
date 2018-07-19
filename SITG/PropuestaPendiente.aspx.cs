using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PropuestaPendiente : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "PropuestaPendiente.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                ResultadoConsulta();
            }
        }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVConsultaContenidoP);
    }

    /*Metodos que se encargan de la consulta de las propuestas que esten pendientes del respectivo programa(comite) */
    protected void GVconsultaPP_RowDataBound(object sender, GridViewRowEventArgs e){}
    protected void GVconsultaPP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVconsultaPP.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    protected void GVconsultaPP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ConsultarPropuesta")
        {
            Consulta.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsultaPP.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene el codigo de propuesta en la tabla
            ResultadoContenidoP();
            ConsultaContenidoP.Visible = true;
            DDLconsultaReunion.Items.Clear();
            string sql = "SELECT r.REU_CODIGO, r.REU_CODIGO FROM REUNION r, profesor p WHERE r.COM_CODIGO=p.COM_CODIGO AND p.usu_username='" + Session["id"] + "' AND r.REU_ESTADO='ACTIVO'";
            DDLconsultaReunion.Items.AddRange(con.cargardatos(sql));
            DDLconsultaReunion.Items.Insert(0, "Seleccione Reunion");
            DDLconsultaReunion.Visible = true;
            Linfo.Text = "";
        }
    }
    private void ResultadoConsulta()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "select DISTINCT   p.PROP_CODIGO,p.PROP_TITULO, InitCap(p.PROP_ESTADO) as pestado, p.PROP_FECHA, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, InitCap(s.dir_estado) as Estado  " +
                    "from estudiante e, PROPUESTA p,  PROFESOR d, director s, usuario u " +
                    "WHERE u.usu_username = s.usu_username and e.prop_codigo = s.prop_codigo and s.prop_codigo=p.prop_codigo and p.PROP_CODIGO = e.PROP_CODIGO and p.PROP_ESTADO = 'PENDIENTE' and d.COM_CODIGO = e.PROG_CODIGO and d.USU_USERNAME = '"+ Session["id"] + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsultaPP.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas ;
                }
                GVconsultaPP.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /* Metodos muestran la info especifica de una propuesta*/
    protected void GVConsultaContenidoP_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVConsultaContenidoP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVConsultaContenidoP.PageIndex = e.NewPageIndex;
        ResultadoContenidoP();
    }
    private void ResultadoContenidoP()
    {
        MostrarDDLReunion.Visible = true;
        MostrarDDLestadoP.Visible = true;
        Terminar.Visible = true;
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select p.PROP_CODIGO,p.PROP_TITULO, l.LINV_NOMBRE, t.TEM_NOMBRE from propuesta p, lin_investigacion l, tema t where t.LINV_CODIGO = l.LINV_CODIGO and t.TEM_CODIGO = p.TEM_CODIGO and p.PROP_CODIGO = '" + Metodo.Value + "'";

                 cmd = new OracleCommand(sql, conn);
                 cmd.CommandType = CommandType.Text;
                 using (OracleDataReader reader = cmd.ExecuteReader()){
                     DataTable dataTable = new DataTable();
                     dataTable.Load(reader);
                     GVConsultaContenidoP.DataSource = dataTable;
                 }
                 GVConsultaContenidoP.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
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

        string sql = "select PROP_NOMARCHIVO, PROP_DOCUMENTO, PROP_TIPO FROM PROPUESTA WHERE PROP_CODIGO=" + id + "";
        List<string> prop = con.consulta(sql, 3, 0);
        fileName = prop[0];
        ruta = prop[1];
        contentype = prop[2];
        try{
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
        } catch (WebException a) {
            Linfo.Text = a.ToString();
        }
    }

    /*Metodos para la consulta de las observaciones que ha hecho el comite*/
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla()
    {
        string sql = "";
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT OBS_CODIGO, OBS_DESCRIPCION FROM OBSERVACION  WHERE PROP_CODIGO ='"+ Metodo.Value + "' and OBS_REALIZADA!='DIRECTOR'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                }
                GVobservacion.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.StackTrace;
        }
    }

    /*Metodos que sirven para el modificar-eliminar de la tabla observaciones*/
    protected void GVobservacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string id = GVobservacion.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from observacion where OBS_CODIGO='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                cargarTabla();
            }
        }
        conn.Close();
    }
    protected void GVobservacion_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        GridViewRow row = (GridViewRow)GVobservacion.Rows[e.RowIndex];
        if (conn != null){
            TextBox observacion = (TextBox)row.Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVobservacion.Rows[e.RowIndex].Cells[0].Controls[0];
            string sql = "update observacion set obs_descripcion = '" + observacion.Text + "' where  obs_codigo ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){
                GVobservacion.EditIndex = -1;
                cargarTabla();
            }
        }
        conn.Close();
    }
    protected void GVobservacion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVobservacion.EditIndex = e.NewEditIndex;
        cargarTabla();
        GVobservacion.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVobservacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVobservacion.EditIndex = -1;
        cargarTabla();
    }
    protected void MostrarObservaciones(object sender, EventArgs e)
    {
        Linfo.Text = "";
        cargarTabla();
        Resultado.Visible = true;
        MostrarAgregarObs.Visible = true;
    }
    protected void Agregar_observacion(object sender, EventArgs e)
    {      
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");

        if (string.IsNullOrEmpty(TBdescripcion.Value) == true) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "No puede ingresar una observación en blanco.";
        } else {
            string descripcion2=TBdescripcion.Value;
            string sql = "insert into observacion (OBS_CODIGO, OBS_DESCRIPCION, OBS_REALIZADA ,PROP_CODIGO, OBS_FECHA) values (OBSPROPID.nextval,'" + descripcion2.ToLower() + "','COMITE', '" + Metodo.Value + "',TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'))";
            Ejecutar("", sql);
            TBdescripcion.Value = "";
            Resultado.Visible = true;
            cargarTabla();
            Linfo.Text = "";
        }
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")) {
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }

    /*Eventos de los botones cancelar, regresar y terminar revision */
    protected void terminar(object sender, EventArgs e)
    {
        if (DDLconsultaReunion.SelectedIndex.Equals(0)) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe elegir una reunión.";
        } else if( DDLestadoP.SelectedIndex.Equals(0)){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe calificar la propuesta.";
        } else  {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myconfirmbox", "myconfirmbox();", true);
        }
    }
    protected void btnDummy_Click(object sender, EventArgs e) {
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string sql2 = "update propuesta set prop_estado='" + DDLestadoP.Items[DDLestadoP.SelectedIndex].Value.ToString() + "' where prop_codigo='" + Metodo.Value + "'";
        Ejecutar("", sql2);
        string sql = "insert into revision_propuesta (REV_FECHA, REV_ESTADO, PROP_CODIGO, REU_CODIGO) values (TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLestadoP.Items[DDLestadoP.SelectedIndex].Value.ToString() + "','" + Metodo.Value + "', '" + DDLconsultaReunion.Items[DDLconsultaReunion.SelectedIndex].Value.ToString() + "')";
        Ejecutar("La propuesta ha sido revisada con exito, presione click en regresar para revisar otra propuesta", sql);

        Resultado.Visible = false;
        ConsultaContenidoP.Visible = false;
        MostrarDDLReunion.Visible = false;
        MostrarAgregarObs.Visible = false;
        MostrarDDLestadoP.Visible = false;
        Terminar.Visible = false;
        IBregresar.Visible = true;
        Metodo.Value = "";
    }
    protected void cancelar(object sender, EventArgs e)
    {
        Resultado.Visible = false;
        ConsultaContenidoP.Visible = false;
        MostrarDDLReunion.Visible = false;
        MostrarAgregarObs.Visible = false;
        MostrarDDLestadoP.Visible = false;
        Terminar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        DDLestadoP.SelectedIndex = 0;
        DDLconsultaReunion.SelectedIndex = 0;
        Metodo.Value = "";
        Linfo.Text = "";
    }
    protected void regresar(object sender, EventArgs e)
    {
        IBregresar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        Metodo.Value = "";
    }
}
