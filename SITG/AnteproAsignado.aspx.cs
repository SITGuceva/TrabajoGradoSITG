using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AnteproAsignado : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "AnteproAsignado.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                ResultadoConsulta();
            }
        }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVconsultaAA);
    }

    /*Metodos que se encargan de la consulta de los anteproyectos le asignaron */
    protected void GVconsultaAA_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVconsultaAA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVconsultaAA.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    protected void GVconsultaAA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ConsultarAnteproyecto")
        {
            Consulta.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsultaAA.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene el codigo de propuesta en la tabla
            MostrarDDLestadoP.Visible = true;
            Terminar.Visible = true;
            Linfo.Text = "";
        }
    }
    private void ResultadoConsulta()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "select DISTINCT   A.Apro_Codigo,A.Anp_Nombre, A.Anp_Fecha, A.Anp_Estado, A.Anp_Aprobacion from anteproyecto a, estudiante e, PROFESOR d , evaluador r " +
                    "WHERE R.Usu_Username = '"+Session["id"]+"' and E.Prop_Codigo = A.Apro_Codigo and A.Anp_Estado = 'PENDIENTE'and A.Anp_Aprobacion='APROBADO' and D.Usu_Username = R.Usu_Username";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()) {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsultaAA.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVconsultaAA.DataBind();
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

        string sql = "select ANP_NOMARCHIVO, ANP_DOCUMENTO,ANP_TIPO FROM ANTEPROYECTO WHERE APRO_CODIGO=" + id + "";
        List<string> ante = con.consulta(sql, 3, 0);
        fileName = ante[0];
        ruta = ante[1];
        contentype = ante[2];

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
        } catch (WebException a) {
            Linfo.Text = a.ToString();
        }
    }

    /*Metodos para la consulta de las observaciones del anteproyecto*/
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla()
    {
        string sql = "";
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT AOBS_CODIGO, AOBS_DESCRIPCION FROM ANTE_OBSERVACION  WHERE APROP_CODIGO ='" + Metodo.Value + "' and AOBS_REALIZADA='EVALUADOR'";

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
        }
        catch (Exception ex) { }
    }

    /*Metodos que sirven para el modificar-eliminar de la tabla observaciones*/
    protected void GVobservacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string id = GVobservacion.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from ante_observacion where AOBS_CODIGO='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                cargarTabla();
            }
        }
    }
    protected void GVobservacion_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        GridViewRow row = (GridViewRow)GVobservacion.Rows[e.RowIndex];
        if (conn != null){
            TextBox observacion = (TextBox)row.Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVobservacion.Rows[e.RowIndex].Cells[0].Controls[0];
            string sql = "update ante_observacion set aobs_descripcion = '" + observacion.Text + "' where  aobs_codigo ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVobservacion.EditIndex = -1;
                cargarTabla();
            }
        }
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

        if (string.IsNullOrEmpty(TBdescripcion.Value) == true){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "No puede ingresar una observación en blanco.";
        }else{
            string descripcion2 = TBdescripcion.Value;
            string sql = "insert into ante_observacion (AOBS_CODIGO, AOBS_DESCRIPCION ,APROP_CODIGO, AOBS_FECHA,AOBS_REALIZADA) values (OBSANTEPID.nextval,'" + descripcion2.ToLower() + "', '" + Metodo.Value + "',TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'EVALUADOR')";
            Ejecutar("", sql);
            TBdescripcion.Value = "";
            Resultado.Visible = true;
            cargarTabla();
        }
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
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
        if (DDLestadoA.SelectedIndex.Equals(0)) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe calificar el anteproyecto.";
        }else{
            string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
            string sql = "update anteproyecto set anp_estado='" + DDLestadoA.Items[DDLestadoA.SelectedIndex].Value.ToString() + "' where apro_codigo='" + Metodo.Value + "'";
            Ejecutar("El anteproyecto ha sido revisado con exito, presione click en regresar para revisar otro.", sql);
            Resultado.Visible = false;
            MostrarAgregarObs.Visible = false;
            MostrarDDLestadoP.Visible = false;
            Terminar.Visible = false;
            IBregresar.Visible = true;
            Metodo.Value = "";
        }
    }
    protected void cancelar(object sender, EventArgs e)
    {
        Resultado.Visible = false;
        MostrarAgregarObs.Visible = false;
        MostrarDDLestadoP.Visible = false;
        Terminar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        DDLestadoA.SelectedIndex = 0;
        Metodo.Value = "";
        Linfo.Text = "";
    }
    protected void regresar(object sender, EventArgs e)
    {
        IBregresar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        Metodo.Value = "";
        Linfo.Text = "";
    }

}