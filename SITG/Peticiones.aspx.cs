﻿using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PeticionDir : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVinfprof);
    }

    /*Metodos que manejan la parte del fronted*/
    protected void LBPeticion_Dir_Click(object sender, EventArgs e)
    {
        TPeticiones.Visible = true;
        CargarTablaPeticiones();
        Linfo.Text = "";
        PetiEstudiante.Visible = false;
        ConsultaPeti.Visible = false;
        Tipo.Value = "";
        IBregresar.Visible = false;
        Tinfprof.Visible = false;
    }
    protected void LBPeticion_Est_Click(object sender, EventArgs e)
    {
        TPeticiones.Visible = false;
        Linfo.Text = "";
        PetiEstudiante.Visible = true;
        ConsultaPeti.Visible = false;
        Tipo.Value = "";
        IBregresar.Visible = false;
        Tinfprof.Visible = false;
    }
   
    /*Metodos que se utilizan para consultar las peticiones de director pendientes*/
    protected void GVpeticion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVpeticion.PageIndex = e.NewPageIndex;
        CargarTablaPeticiones();
    }
    protected void GVpeticion_RowDataBound(object sender, GridViewRowEventArgs e){}
    public void CargarTablaPeticiones()
    {   
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT DISTINCT S.SOL_ID, S.SOL_FECHA, S.SOL_ESTADO, p.PROP_TITULO, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, u.usu_username  FROM SOLICITUD_DIR S, USUARIO U, estudiante e, propuesta p WHERE p.PROP_CODIGO = S.PROP_CODIGO  and S.SOL_ESTADO = 'PENDIENTE' AND U.USU_USERNAME = S.USU_USERNAME and S.PROP_CODIGO = e.PROP_CODIGO  and e.PROG_CODIGO in( " +
                    "select  p.PROG_CODIGO from  programa p, profesor d, comite c where c.PROG_CODIGO = p.PROG_CODIGO and c.COM_CODIGO = d.COM_CODIGO and d.USU_USERNAME = '"+Session["id"] +"')"; 
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVpeticion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVpeticion.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVpeticion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        string sql = ""; 
        GridViewRow row;

        if (e.CommandName == "Ver"){
            IBregresar.Visible = true;
            CargarInfprof(index);
            Tinfprof.Visible = true;
            TPeticiones.Visible = false;
            Linfo.Text = "";

        }  
        if (e.CommandName == "Aprobar") {
            row = GVpeticion.Rows[index]; 
            sql = "update SOLICITUD_DIR set SOL_ESTADO='APROBADO' where SOL_ID='" + row.Cells[0].Text + "'";
            Ejecutar("Solicitud de Director Aprobada", sql);
            ExisteRol(row.Cells[0].Text);
        }
        if (e.CommandName == "Rechazar"){
            row = GVpeticion.Rows[index];
            sql = "update SOLICITUD_DIR set SOL_ESTADO='RECHAZADO' where SOL_ID='" + row.Cells[0].Text + "'";
            Ejecutar("Solicitud de Director Rechazada", sql);
            CargarTablaPeticiones();
        }   
    }
    private void ExisteRol(string id)
    {
        string sql = " select u.USU_USERNAME from usuario_rol u , solicitud_dir s  where u.ROL_ID = 'DIR' and s.SOL_ID = '" + id + "' and u.USU_USERNAME = s.USU_USERNAME";
        List<string> list = con.consulta(sql, 1, 1);
        if (list.Count.Equals(0)){
            sql = " select USU_USERNAME from solicitud_dir  where SOL_ID = '" + id + "'";
            List<string> list2 = con.consulta(sql, 1, 1);
            sql = "insert into USUARIO_ROL (USUROL_ID,USU_USERNAME,ROL_ID) VALUES(USUARIOID.nextval, '" + list2[0] + "', 'DIR')";
            Ejecutar("", sql);
            Linfo.Text += sql;
            CargarTablaPeticiones();
        }else{
            CargarTablaPeticiones();
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
     
    /*Evento del boton regresar*/
    protected void regresar(object sender, EventArgs e)
    {
        IBregresar.Visible = false;
        TPeticiones.Visible = true;
        CargarTablaPeticiones();
        Tinfprof.Visible = false;
    }

    /*Metodos que se utilizan para consultar la informacion adicional del director*/
    protected void GVinfprof_RowDataBound(object sender, GridViewRowEventArgs e){}
    public void CargarInfprof(int cod)
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select CONCAT(CONCAT(usu_nombre, ' '), usu_apellido) as nombre, usu_telefono, usu_correo, usu_username from usuario  where usu_username='" + cod+ "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVinfprof.DataSource = dataTable;
                }
                GVinfprof.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select PROF_NOMARCHIVO, PROF_DOCUMENTO, PROF_TIPO FROM PROFESOR WHERE USU_USERNAME=" + id + "";

        OracleConnection conn = con.crearConexion();
        if (conn != null) {
            using (OracleCommand cmd = new OracleCommand(sql, conn)){
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader()){
                    drc1.Read();
                    contentype = drc1["PROF_TIPO"].ToString();
                    fileName = drc1["PROF_NOMARCHIVO"].ToString();
                    bytes = (byte[])drc1["PROF_DOCUMENTO"];

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentype;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

                }
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }

    }

    /*Metodos que se utilian para consulta - modificacion de las peticiones del estudiante*/
    protected void Bbuscar_Click(object sender, EventArgs e)
    {
        if (DDLsol.SelectedIndex.Equals(0))
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe escoger algun tipo de peticion";
        } else {
            ConsultaPeti.Visible = true;
            PeticionEstudiante();
            Tipo.Value = DDLsol.SelectedIndex.ToString();
        }       
    }
    private void PeticionEstudiante()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select DISTINCT s.SOLE_ID, s.SOLE_FECHA, s.SOLE_MOTIVO, p.PROP_TITULO,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as ESTUDIANTE,s.SOLE_ESTADO " +
                    "from solicitud_est s, propuesta p, usuario u, estudiante e where s.PROP_CODIGO = p.PROP_CODIGO  and u.USU_USERNAME = s.USU_USERNAME and s.SOLE_TIPO = '"+ DDLsol.Items[DDLsol.SelectedIndex].Text + "'  and s.SOLE_ESTADO='Pendiente' and e.PROG_CODIGO " +
                    " in (select  c.PROG_CODIGO from profesor p, comite c where p.USU_USERNAME = '" + Session["id"] + "' and c.COM_CODIGO = p.COM_CODIGO ) order by s.sole_id";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsulta.DataSource = dataTable;
                }
                GVconsulta.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVconsulta_RowDataBound(object sender, GridViewRowEventArgs e){ }
    protected void DDLsol_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLsol.SelectedIndex.Equals(0))
        {
            ConsultaPeti.Visible = false;
        }
    }
    protected void GVconsulta_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVconsulta.EditIndex = -1;
        PeticionEstudiante();
    }
    protected void GVconsulta_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVconsulta.EditIndex = e.NewEditIndex;
        PeticionEstudiante();
        GVconsulta.Rows[indice].Cells[0].Enabled = false;
        GVconsulta.Rows[indice].Cells[1].Enabled = false;
        GVconsulta.Rows[indice].Cells[2].Enabled = false;
        GVconsulta.Rows[indice].Cells[3].Enabled = false;
        GVconsulta.Rows[indice].Cells[4].Enabled = false;
    }
    protected void GVconsulta_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            DropDownList combo = GVconsulta.Rows[e.RowIndex].FindControl("estado") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox codigo = (TextBox) GVconsulta.Rows[e.RowIndex].Cells[0].Controls[0];
            string sql="";

            if (estado.Equals("Aceptada")) {
                int caso = Convert.ToInt32(Tipo.Value);
                switch (caso){
                    case 1: sql = "update estudiante set prop_codigo = 0 where  EXISTS (select 1 from estudiante e, SOLICITUD_EST s where e.PROP_CODIGO=s.PROP_CODIGO and s.SOLE_ID='" + codigo.Text + "')";
                            break;
                    case 2: sql = "update estudiante set prop_codigo = 0 where usu_username =(select solicitud_est.USU_USERNAME as estudiante from solicitud_est where SOLE_ID = '" + codigo.Text + "') ";
                            break;
                    case 3: sql = "update estudiante set prop_codigo = (select PROP_CODIGO as propuesta from solicitud_est where SOLE_ID = '" + codigo.Text + "') " +
                                  "where usu_username = (select solicitud_est.USU_USERNAME as estudiante from solicitud_est where SOLE_ID = '" + codigo.Text + "')";
                            break;
                    default: break;
                }
                Ejecutar("", sql);
            }
            
            sql = "update solicitud_est set sole_estado='" + estado + "' where  sole_id ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){
                GVconsulta.EditIndex = -1;
                PeticionEstudiante();
            }
        }
    }
}









