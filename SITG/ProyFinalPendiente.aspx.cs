using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;
using System.Net;

public partial class ProyFinalPendiente : Conexion{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e) {
        if (Session["Usuario"] == null) {
            Response.Redirect("Default.aspx");
        }
        if (!Page.IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "ProyFinalPendiente.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            } else {
                BuscarDocumentos();
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVconsulta);
        scriptManager.RegisterPostBackControl(this.GVjurado1);
        scriptManager.RegisterPostBackControl(this.GVjurado2);
        scriptManager.RegisterPostBackControl(this.GVjurado3);
        scriptManager.RegisterPostBackControl(this.GVinfprof);
    }

    /*metodos que verifica si existe el rol */
    private void ExisteRol(string id){
        string sql = " select u.USU_USERNAME from usuario_rol u where u.ROL_ID = 'JUR' and U.Usu_Username = '" + id + "'";
        List<string> list = con.consulta(sql, 1, 1);
        if (list.Count.Equals(0)) {
            sql = "insert into USUARIO_ROL (USUROL_ID,USU_USERNAME,ROL_ID) VALUES(USUARIOID.nextval, '" + id + "', 'JUR') ";
            Ejecutar("", sql);
        }
    }

    /*Pestañas que manejan lo grafico del proyecto*/
    protected void consultarproy(object sender, EventArgs e)
    {
        Consulta.Visible = false;
        Jurado1.Visible = false;
        Jurado2.Visible = false;
        Jurado3.Visible = false;
        trabajosdegrado.Visible = false;
        IBregresar.Visible = false;
        infoprofesor.Visible = false;
        Mostrarprof.Visible = false;
        Mostrarprog.Visible = true;
        string sql = "SELECT P.Prog_Codigo, P.Prog_Nombre FROM PROGRAMA p, FACULTAD f, DECANO d WHERE F.Fac_Codigo = P.Fac_Codigo and D.Fac_Codigo = F.Fac_Codigo and D.Usu_Username = '" + Session["id"] + "' and P.Prog_Estado = 'ACTIVO'";
        DDLprogramas.Items.Clear();
        DDLprogramas.Items.AddRange(con.cargardatos(sql));
        DDLprogramas.Items.Insert(0, "Seleccione Programa");
    }
    protected void asignarproy(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        BuscarDocumentos();
        Jurado1.Visible = false;
        Jurado2.Visible = false;
        Jurado3.Visible = false;
        IBregresar.Visible = false;
        infoprofesor.Visible = false;
        Mostrarprog.Visible = false;
        trabajosdegrado.Visible = false;
        Mostrarjurados.Visible = false;
        IBregresarCon.Visible= false;
        Linfo.Text = "";
        Metodo.Value = "";
    }

    /*Metodo que verifica cantidad de jurado asignados en el proyecto seleccionado*/
    private int CantidadJur()
    {
        int cont = 0;
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string sql = "select count(*) from jurado where ppro_codigo='"+Metodo.Value+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows) {
                cont = drc1.GetInt32(0);
               // cont++; PENDIENTE
            }
            drc1.Close();
        }
        conn.Close();

        return cont;
    }
   
    /*Metodo que consulta el evaluador y verifica si el evaluador del anteproyecto ya fue asignado al proyecto f*/
    private string EvaluadorAsignado()
    {
        List<string> evaluador = con.consulta("select usu_username from evaluador where apro_codigo='" + Metodo.Value + "'",1,1);
        string evasignado = null;
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string sql = "select * from jurado where usu_username='" + evaluador[0] + "' and ppro_codigo='" + Metodo.Value + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){
                evasignado ="si";
            }else {
                evasignado = evaluador[0];
            }
            drc1.Close();
        }
        conn.Close();

        return evasignado;
    }

    /*Metodo que consulta los proyectos finales que falta por asignar jurados*/
    protected void BuscarDocumentos()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select F.Ppro_Codigo, F.Pf_Titulo, F.Pf_Fecha from proyecto_final f, estudiante e where (F.Pf_Jur1='PENDIENTE' or F.Pf_Jur2='PENDIENTE' or F.Pf_Jur3='PENDIENTE' or F.pf_jur1='ELIMINADO') and F.Pf_Aprobacion='APROBADO' and E.Prop_Codigo= F.Ppro_Codigo" +
                     " and E.Prog_Codigo IN(select P.Prog_Codigo from programa p, facultad f, decano d where F.Fac_Codigo= P.Fac_Codigo and D.Fac_Codigo= F.Fac_Codigo and D.Usu_Username= '"+ Session["id"] + "')";

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
    protected void GVconsulta_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Asignar") {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsulta.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene id del trabajo final

            List < string > estadojur1 = con.consulta("select pf_jur1 from proyecto_final where ppro_codigo='" + Metodo.Value + "'", 1, 0);
            string evaluador = EvaluadorAsignado();//consultamos si el evaluador del anteproyecto ya fue asignado como jurado

            if (estadojur1[0].Equals("PENDIENTE") && evaluador !="si") {
                string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
                string fentrega = System.DateTime.Now.AddMonths(1).Date.ToString("yyyy/MM/dd, HH:mm:ss");
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num, jur_frpta) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + evaluador + "', '" + Metodo.Value + "', '1',TO_DATE( '" + fentrega + "', 'YYYY-MM-DD HH24:MI:SS'))";
                Ejecutar("", sql);
                sql = "update proyecto_final set pf_jur1='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar("", sql);
                ExisteRol(evaluador);
                enviarmensaje(evaluador, 1);
            }

            Consulta.Visible = false;

            cargarJurado1();
            Jurado1.Visible = true;
            cargarJurado2();
            Jurado2.Visible = true;
            cargarJurado3();
            Jurado3.Visible = true;

            ProfDisponible();
            Mostrarprof.Visible = true;

            IBregresar.Visible = true;
        }
    }
    protected void DescargaPF(object sender, EventArgs e)
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
        }catch (WebException a) {
            Linfo.Text = a.ToString();
        }
    }

    /*Metodos que consultan la informacion de cada jurado*/

    /*Informacion jurado 1 (evaluador)*/
    protected void GVjurado1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVjurado1.PageIndex = e.NewPageIndex;
        cargarJurado1();
    }
    protected void GVjurado1_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarJurado1()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "select j.jur_id,  j.usu_username,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as nombre, u.usu_correo, j.ppro_codigo, j.jur_fecha, j.jur_frpta from jurado j, usuario u where j.jur_num='1' and ppro_codigo='" + Metodo.Value + "' and u.usu_username = j.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVjurado1.DataSource = dataTable;
                }
                GVjurado1.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVjurado1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string codigo = GVjurado1.Rows[e.RowIndex].Cells[1].Text;
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string id = GVjurado1.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from jurado where jur_id='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()) {
                cargarJurado1();
                Mostrarprof.Visible = true;
                BTconsultar.Enabled = true;
                Ejecutar("", "update proyecto_final set pf_jur1='ELIMINADO' where ppro_codigo='" + Metodo.Value + "' ");
                ProfDisponible();
            }
        }
        conn.Close();
        enviarmensaje(codigo, 2);
    }

    /*Informacion jurado 2*/
    protected void GVjurado2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVjurado2.PageIndex = e.NewPageIndex;
        cargarJurado2();
    }
    protected void GVjurado2_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarJurado2()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select j.jur_id, j.usu_username,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as nombre, u.usu_correo, j.ppro_codigo, j.jur_fecha, j.jur_frpta from jurado j, usuario u where j.jur_num='2' and ppro_codigo='" + Metodo.Value + "' and u.usu_username = j.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVjurado2.DataSource = dataTable;
                }
                GVjurado2.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVjurado2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string codigo = GVjurado2.Rows[e.RowIndex].Cells[1].Text;
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string id = GVjurado2.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from jurado where jur_id='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){
                cargarJurado2();
                Mostrarprof.Visible = true;
                BTconsultar.Enabled = true;
                Ejecutar("", "update proyecto_final set pf_jur2='PENDIENTE' where ppro_codigo='" + Metodo.Value + "' ");
                ProfDisponible();
            }
        }
        conn.Close();
        enviarmensaje(codigo, 2);
    }

    /*Informacion jurado 3*/
    protected void GVjurado3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVjurado3.PageIndex = e.NewPageIndex;
        cargarJurado3();
    }
    protected void GVjurado3_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarJurado3()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
               string  sql = "select j.jur_id,j.usu_username, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as nombre, u.usu_correo, j.ppro_codigo, j.jur_fecha, j.jur_frpta from jurado j, usuario u where j.jur_num='3' and ppro_codigo='" + Metodo.Value + "' and u.usu_username = j.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()) {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVjurado3.DataSource = dataTable;
                }
                GVjurado3.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVjurado3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string codigo = GVjurado3.Rows[e.RowIndex].Cells[1].Text;
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string id = GVjurado3.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from jurado where jur_id='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){
                cargarJurado3();
                Mostrarprof.Visible = true;
                BTconsultar.Enabled = true;
               Ejecutar("", "update proyecto_final set pf_jur3='PENDIENTE' where ppro_codigo='" + Metodo.Value + "' ");
                ProfDisponible();
            }
        }
        conn.Close();
        enviarmensaje(codigo, 2);
    }

   /*Proceso para consultar los jurados ya asignados*/
    private void CargarTablatg()
    {
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select distinct pro.ppro_codigo, pro.pf_titulo, pro.pf_fecha from estudiante e, programa p, proyecto_final pro  where e.prop_codigo = pro.ppro_codigo and e.prog_codigo='" + DDLprogramas.Items[DDLprogramas.SelectedIndex].Value + "' and pf_jur1!='PENDIENTE' and pf_jur2!='PENDIENTE' and pf_jur3!='PENDIENTE' order by pro.pf_fecha";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVtg.DataSource = dataTable;
                }
                GVtg.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVtg_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVtg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Ver")
        {
            Consulta.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVtg.Rows[index];
            Metodo2.Value = row.Cells[0].Text; //obtiene id del trabajo final
            CargarTablajurados();
            Mostrarjurados.Visible = true;
            trabajosdegrado.Visible = false;
            Mostrarprog.Visible = true;
            IBregresarCon.Visible = true;

        }
    }
    protected void consultarprograma(object sender, EventArgs e){
        if (DDLprogramas.SelectedIndex.Equals(0))
        {
            Linfo.Text = "Se requiere que seleccione un programa.";
        }
        else {
            CargarTablatg();
            trabajosdegrado.Visible = true;
        }
    }
    protected void DDLprogramas_SelectedIndexChanged(object sender, EventArgs e)
    {
        trabajosdegrado.Visible = false;
        Mostrarjurados.Visible = false;
        IBregresarCon.Visible = false;
        Linfo.Text = "";
    }
    protected void regresarCon(object sender, EventArgs e)
    {
        Mostrarjurados.Visible = false;
        trabajosdegrado.Visible = true;
        DDLprogramas.Visible = true;
        BTconsultarp.Visible = true;
        IBregresarCon.Visible = false;

    }
    private void CargarTablajurados()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as jurados, j.jur_fecha, j.jur_frpta, j.jur_fenvio from usuario u, jurado j  where j.usu_username=u.usu_username and j.ppro_codigo='" + Metodo2.Value + "'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVmjurados.DataSource = dataTable;
                }
                GVmjurados.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVmjurados_RowDataBound(object sender, GridViewRowEventArgs e) { }

    /*Metodos que se encargan de consultar la informacion de un profesor seleccionado*/
    protected void InfProfesor(object sender, EventArgs e)
    {
        CargarInfoProfesor();
        GVinfprof.Visible = true;
    }
    private void CargarInfoProfesor()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select usu_username,usu_telefono, usu_direccion, usu_correo  from usuario  where usu_username='" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "'";
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
    protected void GVinfprof_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void consultarprofesor(object sender, EventArgs e)
    {
        if (DDLprofesores.SelectedIndex.Equals(0)) {
            Linfo.Text = "Seleccione un profesor.";
            infoprofesor.Visible = false;
        }else{
            CargarInfoProfesor();
            infoprofesor.Visible = true;
            BTconsultar.Enabled = false;
            Linfo.Text = "";
        }

    }
    protected void ProfDisponible()
    {
        List<string> estadojurados = con.consulta("select pf_jur1, pf_jur2, pf_jur3 from proyecto_final where ppro_codigo='" + Metodo.Value + "'", 3, 0);

        string sql = "";

        if (estadojurados[0].Equals("ELIMINADO") && estadojurados[1].Equals("PENDIENTE") && estadojurados[2].Equals("PENDIENTE")) {
            sql = "Select distinct u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
        } else {
            sql = "Select distinct u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) from profesor p, usuario u, jurado j where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO' and u.usu_username not in (select j.usu_username from jurado j where u.usu_username = j.usu_username and j.ppro_codigo='" + Metodo.Value + "')";
        }
        DDLprofesores.Items.Clear();
        DDLprofesores.Items.AddRange(con.cargardatos(sql));
        DDLprofesores.Items.Insert(0, "Seleccione un profesor");
    }
    protected void DescargaHV(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        List<string> list = con.FtpConexion();
        string fileName = "", contentype = "", ruta = "";
        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);
        string sql = "select PROF_NOMARCHIVO, PROF_DOCUMENTO, PROF_TIPO FROM PROFESOR WHERE USU_USERNAME='" +id + "'";
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
        }catch (WebException a) {
            Linfo.Text = a.ToString();
        }
    }

    /*Metodo cancelar asignar jurado y regresar a la ventana principal de asignación*/
    protected void regresar(object sender, EventArgs e)  {
        BuscarDocumentos();
        Consulta.Visible = true;

        IBregresar.Visible = false;
        Mostrarprof.Visible = false;
        infoprofesor.Visible = false;
        Jurado1.Visible = false;
        Jurado2.Visible = false;
        Jurado3.Visible = false;
        Linfo.Text = "";
        Metodo.Value = "";
    }
    protected void cancelarasignar(object sender, EventArgs e)///PENDIENTE
    {
        infoprofesor.Visible = false;
        BTconsultar.Enabled = true;
        Linfo.Text = "";
        DDLprofesores.SelectedIndex = 0;
    }

    /*Metodo que se utiliza para asignar jurados*/
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
    protected void asignarjurado(object sender, EventArgs e)
    {
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string fentrega = System.DateTime.Now.AddMonths(1).Date.ToString("yyyy/MM/dd, HH:mm:ss");
        int cant= CantidadJur();
        List<string> estadojurados = con.consulta("select pf_jur1, pf_jur2, pf_jur3 from proyecto_final where ppro_codigo='" + Metodo.Value + "'",3,0);

        if (cant < 3) {
            if (cant == 0) { 

                ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num, jur_frpta) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "', '1',TO_DATE( '" + fentrega + "', 'YYYY-MM-DD HH24:MI:SS'))";
                Ejecutar("", sql);

                string sql2 = "update proyecto_final set pf_jur1='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar("Jurado asignado correctamente.", sql2);

                Ocultas(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                cargarJurado1();
            } else if (cant == 1 &&  estadojurados[0] !="ELIMINADO" && estadojurados[1].Equals("PENDIENTE")) {

                ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num, jur_frpta) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "', '2',TO_DATE( '" + fentrega + "', 'YYYY-MM-DD HH24:MI:SS'))";
                Ejecutar("", sql);
                
                string sql2 = "update proyecto_final set pf_jur2='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar("Jurado asignado correctamente.", sql2);

                Ocultas(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                cargarJurado2();
            } else if (cant == 2 && estadojurados[0] != "ELIMINADO" && estadojurados[2].Equals("PENDIENTE")){

                ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num, jur_frpta) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "',  '3',TO_DATE( '" + fentrega + "', 'YYYY-MM-DD HH24:MI:SS') )";
                Ejecutar("", sql);
                
                string sql2 = "update proyecto_final set pf_jur3='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar("Jurado asignado correctamente.", sql2);

                Ocultas(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                cargarJurado3();
            }else if (cant == 2 && estadojurados[0] != "ELIMINADO" && estadojurados[1].Equals("PENDIENTE")){

                ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num, jur_frpta) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "',  '2',TO_DATE( '" + fentrega + "', 'YYYY-MM-DD HH24:MI:SS') )";
                Ejecutar("", sql);

                string sql2 = "update proyecto_final set pf_jur2='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar("Jurado asignado correctamente.", sql2);

                Ocultas(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                cargarJurado2();
            }else if (estadojurados[0].Equals("ELIMINADO")){

                ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num, jur_frpta) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "',  '1' ,TO_DATE( '" + fentrega + "', 'YYYY-MM-DD HH24:MI:SS'))";
                Ejecutar("", sql);

                string sql2 = "update proyecto_final set pf_jur1='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar("Jurado asignado correctamente.", sql2);

                Ocultas(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                cargarJurado1(); 
            }
        } else {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Ya se ha asignado todos los jurados.";
            Mostrarprof.Visible = false;
            infoprofesor.Visible = false;
        }

    }
    private void Ocultas(string id){
        infoprofesor.Visible = false;
        enviarmensaje(id, 1);

        BTconsultar.Enabled = true;
        DDLprofesores.SelectedIndex = 0;
        ProfDisponible();
    }

    //Metodo  que envia el mensaje de asignacion o eliminacion de la asignacion al evaluador
    private void enviarmensaje(string id, int opc)
    {
        List<string> correo = con.consulta("select usu_correo from usuario where usu_username='" + id + "'", 1, 0);
        string msj = "", asunto = "";
        if (correo.Count.Equals(0))
        {
            Linfo.Text = "El profesor no tiene un correo para enviarle la alerta";
        }
        else
        {
            if (opc.Equals(1))
            {
                asunto = "ASIGNADO A JURADO";
                msj = "Ha sido asignado como jurado de proyecto final con código: " + Metodo.Value + " para saber mas de dicha información por favor ingresar a SITG";
            }
            else if (opc.Equals(2))
            {
                asunto = "ELIMINADO DE JURADO";
                msj = "Ha sido desasignado como jurado del proyecto final con código: " + Metodo.Value + "";
            }

            con.EnviarCorreo(correo[0], asunto, msj);
        }
    }
}