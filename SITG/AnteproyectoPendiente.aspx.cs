using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AnteproyectoPendiente : System.Web.UI.Page
{
    Conexion con = new Conexion();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "AnteproyectoPendiente.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }else {
                ResultadoConsulta(); //consulta los anteproyectos pendientes
            }
        }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVanteproy);
        scriptManager.RegisterPostBackControl(this.GVinfprof);
        scriptManager.RegisterPostBackControl(this.GVevaluador);
    }

    /*Tabla que carga la información del anteproyecto para descargar*/
    protected void GVanteproy_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVanteproy_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVanteproy.PageIndex = e.NewPageIndex;
        BuscarAnteproyecto();
    }
    protected void BuscarAnteproyecto()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select apro_codigo, anp_nombre  from anteproyecto where apro_codigo='"+Metodo.Value+"'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVanteproy.DataSource = dataTable;
                }
                GVanteproy.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void DescargaAnteProyecto(object sender, EventArgs e)
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
        } catch (WebException a){
            Linfo.Text = a.ToString();
        }
    }

    /*Consulta de los anteproyectos que estan pendientes del respectivo programa(comite) */
    protected void GVconsultaAP_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVconsultaAP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVconsultaAP.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    private void ResultadoConsulta()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select DISTINCT   an.apro_codigo, an.anp_nombre, an.anp_fecha from estudiante e, anteproyecto an, PROFESOR d WHERE an.apro_codigo = e.prop_codigo and an.anp_evaluador = 'SIN ASIGNAR' and d.COM_CODIGO = e.PROG_CODIGO  and An.Anp_Aprobacion = 'APROBADO' and d.USU_USERNAME = '" + Session["id"]+"'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsultaAP.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVconsultaAP.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVconsultaAP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Asignar") {
            Consulta.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsultaAP.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene el codigo de propuesta en la tabla
            BuscarAnteproyecto();
            InfoAnteproyecto.Visible = true;
            Mostrarprof.Visible = true;
            Terminar.Visible = true;
            MostrarDDLReunion.Visible = true;
            Linfo.Text = "";
            DDLprofesores.Items.Clear();
            string sql = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
            DDLprofesores.Items.AddRange(con.cargardatos(sql));
            DDLprofesores.Items.Insert(0, "Seleccione");
            DDLconsultaReunion.Items.Clear();
            string sql2 = "SELECT r.REU_CODIGO, r.REU_CODIGO FROM REUNION r,profesor p WHERE r.COM_CODIGO=p.com_codigo AND p.usu_username='" + Session["id"] + "' AND r.REU_ESTADO='ACTIVO'";
            DDLconsultaReunion.Items.AddRange(con.cargardatos(sql2));
            DDLconsultaReunion.Items.Insert(0, "Seleccione Reunion");
            DDLconsultaReunion.Visible = true;
        }
    }

    /*Metodos que sirven para buscar la informacion del profesor*/
    protected void DescargaHV(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        List<string> list = con.FtpConexion();
        string fileName = "", contentype = "", ruta = "";
        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);
        string sql = "select PROF_NOMARCHIVO, PROF_DOCUMENTO, PROF_TIPO FROM PROFESOR WHERE USU_USERNAME='" + id + "'";
        List<string> prof = con.consulta(sql, 3, 0);
        fileName = prof[0];
        ruta = prof[1];
        contentype = prof[2];
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
        }catch (WebException a){
            Linfo.Text = a.ToString();
        }
    }
    protected void InfProfesor(object sender, EventArgs e)
    {
        CargarInfoProfesor();
        GVinfprof.Visible = true;
    }
    private void CargarInfoProfesor()
    {
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
               string  sql = "select usu_username,usu_telefono, usu_direccion, usu_correo  from usuario  where usu_username='" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()) {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVinfprof.DataSource = dataTable;
                }
                GVinfprof.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVinfprof_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void consultarprofesor(object sender, EventArgs e)
    {
         if (DDLprofesores.SelectedIndex.Equals(0)){
            Linfo.Text = "Seleccione un profesor.";
            infoprofesor.Visible = false;
        }
        else
        {
            CargarInfoProfesor();
            infoprofesor.Visible = true;
        }
    }

    /*Eventos de los botones*/
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
            if (texto.Equals("1")) {
                Linfo.ForeColor = System.Drawing.Color.Green;
                Verificador.Value = "Funciono";
            }else {
                Linfo.ForeColor = System.Drawing.Color.Green;
                Linfo.Text = texto;
            }
        }else {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }
    protected void terminar(object sender, EventArgs e)
    {
        if (DDLconsultaReunion.SelectedIndex.Equals(0))
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Seleccione una reunión.";
            infoprofesor.Visible = false;
        } else if (DDLprofesores.SelectedIndex.Equals(0)){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Seleccione un profesor.";
            infoprofesor.Visible = false;
            Terminar.Visible = true;          
        }else{  
            string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
            string sql = "insert into evaluador (EVA_ID, EVA_FECHA,USU_USERNAME ,APRO_CODIGO, REU_CODIGO) values (EVALUADORID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'"+ DDLprofesores.Items[DDLprofesores.SelectedIndex].Value+"', '" + Metodo.Value + "', '" + DDLconsultaReunion.Items[DDLconsultaReunion.SelectedIndex].Value + "')";
            Ejecutar("1", sql);
            if (Verificador.Value.Equals("Funciono")){
                Verificador.Value = "";
                sql = "update anteproyecto set anp_evaluador='ASIGNADO' where apro_codigo='" + Metodo.Value + "'";
                Ejecutar("1", sql);
                if (Verificador.Value.Equals("Funciono"))
                {
                    Verificador.Value = "";
                    ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                }
            }
            quitar();
        }
    }
    private void quitar(){
        Mostrarprof.Visible = false;
        InfoAnteproyecto.Visible = true;
        Evaluador.Visible = true;
        CargarEvaluador();
       
       
        Terminar.Visible = false;
        infoprofesor.Visible = false;
        IBregresar.Visible = true;
        DDLprofesores.SelectedIndex = 0;
        DDLconsultaReunion.SelectedIndex = 0;
        MostrarDDLReunion.Visible = false;
        Verificador.Value = "";
    }

    /*Metodo cargar evaluador guardado*/
    private void CargarEvaluador()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select e.eva_id,  e.usu_username,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as nombre, u.usu_correo, e.apro_codigo from evaluador e, usuario u where  apro_codigo='" + Metodo.Value + "' and u.usu_username = e.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVevaluador.DataSource = dataTable;
                }
                GVevaluador.DataBind();
            }
            conn.Close();
        } catch (Exception ex){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVevaluador_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVevaluador_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string id = GVevaluador.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from evaluador where eva_id='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                CargarEvaluador();

                string sql3 = "update anteproyecto set anp_evaluador='SIN ASIGNAR' where apro_codigo='" + Metodo.Value + "' ";
                Ejecutar("", sql3);

                Mostrarprof.Visible = true;
                Terminar.Visible = true;
                MostrarDDLReunion.Visible = true;
                IBregresar.Visible = false;
                Linfo.Text = "";
                DDLprofesores.Items.Clear();
                string sql4 = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
                DDLprofesores.Items.AddRange(con.cargardatos(sql4));
                DDLprofesores.Items.Insert(0, "Seleccione");
                DDLconsultaReunion.Items.Clear();
                string sql2 = "SELECT r.REU_CODIGO, r.REU_CODIGO FROM REUNION r,profesor p WHERE r.COM_CODIGO=p.com_codigo AND p.usu_username='" + Session["id"] + "' AND r.REU_ESTADO='ACTIVO'";
                DDLconsultaReunion.Items.AddRange(con.cargardatos(sql2));
                DDLconsultaReunion.Items.Insert(0, "Seleccione Reunion");
                DDLconsultaReunion.Visible = true;
            }
        }
    }

    private void ExisteRol(string id)
    {
        string sql = " select u.USU_USERNAME from usuario_rol u where u.ROL_ID = 'EVA' and U.Usu_Username = '"+id+"'";
        List<string> list = con.consulta(sql, 1, 1);
        if (list.Count.Equals(0)) {
            sql = "insert into USUARIO_ROL (USUROL_ID,USU_USERNAME,ROL_ID) VALUES(USUARIOID.nextval, '" + id + "', 'EVA') ";
            Ejecutar("Se ha asignado el evaluador para el anteproyecto satisfactoriamente", sql);
        } else {
           Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = "Se ha asignado el evaluador para el anteproyecto satisfactoriamente";
        }
    }
    protected void cancelar(object sender, EventArgs e)
    {     
        Mostrarprof.Visible = false;
        Terminar.Visible = false;
        infoprofesor.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        DDLprofesores.SelectedIndex = 0;
        DDLconsultaReunion.SelectedIndex = 0;
        InfoAnteproyecto.Visible = false;
        MostrarDDLReunion.Visible = false;
        Verificador.Value = "";
        Metodo.Value = "";
    }
    protected void regresar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        IBregresar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        InfoAnteproyecto.Visible = false;
        Metodo.Value = "";
        Verificador.Value = "";
        Evaluador.Visible = false;
    }




   
}
