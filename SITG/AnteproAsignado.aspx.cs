﻿using Oracle.DataAccess.Client;
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
        scriptManager.RegisterPostBackControl(this.GVrevisado);
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
                string sql = "select DISTINCT   A.Apro_Codigo,A.Anp_Nombre, A.Anp_Fecha, InitCap(A.Anp_Estado) as estado, InitCap(A.Anp_Aprobacion) as aprobacion from anteproyecto a, evaluador r WHERE r.Usu_Username = '" + Session["id"] + "' and r.Apro_Codigo = a.Apro_Codigo  and a.Anp_Estado = 'PENDIENTE' and a.Anp_Aprobacion='APROBADO'";               
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()) {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsultaAA.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
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
                sql = "SELECT a.AOBS_CODIGO, a.AOBS_DESCRIPCION FROM ANTE_OBSERVACION a, EVALUADOR e WHERE a.APROP_CODIGO ='" + Metodo.Value + "' and a.AOBS_REALIZADA='EVALUADOR' and e.APRO_CODIGO = a.APROP_CODIGO and e.USU_USERNAME = '" + Session["id"] + "'";  

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
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
            string sql = "update ante_observacion set aobs_descripcion = '" + observacion.Text + "' where  aobs_codigo ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myconfirmbox", "myconfirmbox();", true); 
        }
    }
    protected void btnDummy_Click(object sender, EventArgs e){
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        Ejecutar("", "update evaluador set eva_fenvio= TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS') where apro_codigo='"+Metodo.Value+"' and usu_username= '"+ Session["id"] + "'");
        string sql = "update anteproyecto set anp_estado='" + DDLestadoA.Items[DDLestadoA.SelectedIndex].Value.ToString() + "' where apro_codigo='" + Metodo.Value + "'";
        Ejecutar("El anteproyecto ha sido revisado con exito, presione click en regresar para revisar otro.", sql);

        Resultado.Visible = false;
        MostrarAgregarObs.Visible = false;
        MostrarDDLestadoP.Visible = false;
        Terminar.Visible = false;
        IBregresar.Visible = true;
        Metodo.Value = "";
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
    }
    protected void regresar(object sender, EventArgs e)
    {
        IBregresar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        Metodo.Value = "";
    }

    /*Eventos que manejan lo que se muestra*/
    protected void LBconsultar_Click(object sender, EventArgs e)
    {
        Revisados.Visible = true;
        ConsultarRevisados();
        Consulta.Visible = false;
        MostrarDDLestadoP.Visible = false;
        MostrarAgregarObs.Visible = false;
        Resultado.Visible = false;
        Terminar.Visible = false;
        Linfo.Text = "";
        Metodo.Value = "";
    }
    protected void LBPendiente_Click(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        ResultadoConsulta();
        MostrarDDLestadoP.Visible = false;
        MostrarAgregarObs.Visible = false;
        Resultado.Visible = false;
        Terminar.Visible = false;
        Revisados.Visible = false;
        Metodo.Value = "";
    }

    /*Metodos para el consultar los anteproyectos ya asignados*/
    private void ConsultarRevisados()
    {
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select Distinct A.Apro_Codigo, A.Anp_Nombre, Initcap(A.Anp_Aprobacion) as aprobacion, Initcap(A.Anp_Estado) as estado, TO_CHAR( e.eva_fecha, 'dd/mm/yyyy') as FASIGNADO, TO_CHAR( e.eva_frta, 'dd/mm/yyyy') as FRPTA, TO_CHAR( e.eva_fenvio, 'dd/mm/yyyy') as FENVIO from anteproyecto a, evaluador e where A.Apro_Codigo = E.Apro_Codigo and E.Usu_Username = '" + Session["id"] + "' order by FASIGNADO";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVrevisado.DataSource = dataTable;
                }
                GVrevisado.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVrevisado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "buscar"){
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVrevisado.Rows[index];
            Metodo.Value = row.Cells[0].Text;
            cargarTabla();
            Resultado.Visible = true; 
        }
    }
    protected void GVrevisado_RowDataBound(object sender, GridViewRowEventArgs e){}
    protected void GVrevisado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVrevisado.PageIndex = e.NewPageIndex;
        ConsultarRevisados();
    }

}