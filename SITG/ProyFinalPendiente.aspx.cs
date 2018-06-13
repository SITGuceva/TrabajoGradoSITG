using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;
using System.Net;

public partial class ProyFinalPendiente : Conexion
{
    Conexion con = new Conexion();
    string evaluador;
    string jurado1, jurado2, jurado3;
    int EvaAsignado,contadorjur,Eliminado;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null) {
            Response.Redirect("Default.aspx");
        }
        if (!Page.IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "ProyFinalPendiente.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            } else {
                BuscarDocumentos();
                Consulta.Visible = true;
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
    private void ExisteRol(string id)
    {
        string sql = " select u.USU_USERNAME from usuario_rol u where u.ROL_ID = 'JUR' and U.Usu_Username = '" + id + "'";
        List<string> list = con.consulta(sql, 1, 1);
        if (list.Count.Equals(0)) {
            sql = "insert into USUARIO_ROL (USUROL_ID,USU_USERNAME,ROL_ID) VALUES(USUARIOID.nextval, '" + id + "', 'JUR') ";
            Ejecutar("Se ha asignado el evaluador para el anteproyecto satisfactoriamente", sql);
        }
    }

    /*metodo que verifica quien es el evaluador*/
    private void AnteEvaluador()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select usu_username from evaluador where apro_codigo='"+Metodo.Value+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                evaluador = drc1.GetInt32(0).ToString();
            }
            drc1.Close();
        }
    }

    /*metodo que verifica estado jurado1*/
    private void Jur1()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select pf_jur1 from proyecto_final where ppro_codigo='" + Metodo.Value + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                jurado1 = drc1[0].ToString();
            }
            drc1.Close();
        }
    }

    /*metodo que verifica estado jurado2*/
    private void Jur2()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select pf_jur2 from proyecto_final where ppro_codigo='" + Metodo.Value + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                jurado2 = drc1[0].ToString();
            }
            drc1.Close();
        }
    }

    /*Pestaña de consultar proyecto*/
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
        string sql = "Select * from programa";
        DDLprogramas.Items.Clear();
        DDLprogramas.Items.AddRange(con.cargardatos(sql));
        DDLprogramas.Items.Insert(0, "Seleccione un programa");
    }

    protected void asignarproy(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        Jurado1.Visible = false;
        Jurado2.Visible = false;
        Jurado3.Visible = false;
        IBregresar.Visible = false;
        infoprofesor.Visible = false;
        Mostrarprog.Visible = false;
        trabajosdegrado.Visible = false;
        Mostrarjurados.Visible = false;
        IBregresarCon.Visible= false;
        
    }



    /*Metodo que verifica si  el evaluador del anteproyecto ya fue asignado*/
    private void EvaluadorAsignado()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select * from jurado where usu_username='"+evaluador+"' and ppro_codigo='"+Metodo.Value+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                EvaAsignado = 1;
            }
            drc1.Close();
        }
    }

    /*Metodo que verifica si  el evaluador fue eliminado*/
    private void RevisarEliminado()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select * from proyecto_final where pf_jur1='ELIMINADO' and ppro_codigo='" + Metodo.Value + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                Eliminado = 1;
                
            }
            drc1.Close();
        }


    }

        /*Metodo que verifica cantidad de jurado asignados en el proyecto seleccionado*/
        private void CantidadJur()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select count(*) from jurado where ppro_codigo='"+Metodo.Value+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {

                contadorjur = drc1.GetInt32(0);
                contadorjur=contadorjur+1;
            }
            drc1.Close();
        }
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
                string sql = "select PPRO_CODIGO, PF_TITULO, PF_FECHA from proyecto_final where (pf_jur1='PENDIENTE' or pf_jur2='PENDIENTE' or pf_jur3='PENDIENTE') and pf_aprobacion='APROBADO'";

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
        if (e.CommandName == "Asignar")
        {
            string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
            Consulta.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsulta.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene id del trabajo final
            Mostrarprof.Visible = true;
            Jur1();
            AnteEvaluador(); //consultamos el evaluador del anteproyecto
            if (jurado1 == "ASIGNADO")
            {
                EvaluadorAsignado();//consultamos si el evaluador del anteproyecto ya fue asignado como jurado
            }
            RevisarEliminado();
            Consulta.Visible = false;
            //si el evaludor del anteproyecto no ha sido asignado entre al if
            if (EvaAsignado != 1 && Eliminado !=1 && jurado1 != "ASIGNADO")
            {
                string texto = "";
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + evaluador + "', '" + Metodo.Value + "', '1')";
                Ejecutar(texto, sql);
                string texto2 = "";
                string sql2 = "update proyecto_final set pf_jur1='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar(texto2, sql2);
                ExisteRol(evaluador);
            }
           
            cargarJurado1();
            Jurado1.Visible = true;
            cargarJurado2();
            Jurado2.Visible = true;
            cargarJurado3();
            Jurado3.Visible = true;
            ProfDisponible();
            IBregresar.Visible = true;
            DDLprofesores.Visible = true;
            BTconsultar.Visible = true;

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
                string sql = "select j.jur_id,  j.usu_username,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as nombre, u.usu_correo, j.ppro_codigo from jurado j, usuario u where j.jur_num='1' and ppro_codigo='" + Metodo.Value + "' and u.usu_username = j.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVjurado1.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;

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
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string id = GVjurado1.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from jurado where jur_id='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                cargarJurado1();
                Eliminado = 1;
                ProfDisponible();
                string texto3 = "";
                string sql3 = "update proyecto_final set pf_jur1='ELIMINADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar(texto3, sql3);
            }
        }
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
                string sql = "select j.jur_id, j.usu_username,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as nombre, u.usu_correo, j.ppro_codigo from jurado j, usuario u where j.jur_num='2' and ppro_codigo='" + Metodo.Value + "' and u.usu_username = j.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVjurado2.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;

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
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string id = GVjurado2.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from jurado where jur_id='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                cargarJurado2();
                ProfDisponible();
                string texto3 = "";
                string sql3 = "update proyecto_final set pf_jur2='PENDIENTE' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar(texto3, sql3);
            }
        }
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
               string  sql = "select j.jur_id,j.usu_username, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as nombre, u.usu_correo, j.ppro_codigo from jurado j, usuario u where j.jur_num='3' and ppro_codigo='" + Metodo.Value + "' and u.usu_username = j.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVjurado3.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;

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
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string id = GVjurado3.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from jurado where jur_id='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                cargarJurado3();
                ProfDisponible();
                string texto3 = "";
                string sql3 = "update proyecto_final set pf_jur3='PENDIENTE' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar(texto3, sql3);
            }
        }
    }














  
    private void CargarTablatg()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select distinct pro.ppro_codigo, pro.pf_titulo, pro.pf_fecha from estudiante e, programa p, proyecto_final pro  where e.prop_codigo = pro.ppro_codigo and e.prog_codigo='" + DDLprogramas.Items[DDLprogramas.SelectedIndex].Value + "' and pf_jur1='ASIGNADO' and pf_jur2='ASIGNADO' and pf_jur3='ASIGNADO'";
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

        protected void consultarprograma(object sender, EventArgs e)
    {
      
            CargarTablatg();
            trabajosdegrado.Visible = true;
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
                string sql = "select CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as jurados from usuario u, jurado j  where j.usu_username=u.usu_username and j.ppro_codigo='" + Metodo2.Value + "'";
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
        if (DDLprofesores.SelectedIndex.Equals(0))
        {
            Linfo.Text = "Seleccione un profesor.";
            infoprofesor.Visible = false;
        }
        else
        {
            CargarInfoProfesor();
            infoprofesor.Visible = true;
            BTconsultar.Enabled = false;
            Linfo.Text = "";
        }

    }
    protected void ProfDisponible()
    {
        if (Eliminado == 1)
        {
            string sql = "Select distinct u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
            DDLprofesores.Items.Clear();
            DDLprofesores.Items.AddRange(con.cargardatos(sql));
            DDLprofesores.Items.Insert(0, "Seleccione un profesor");
        }
        else
        {
            string sql = "Select distinct u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) from profesor p, usuario u, jurado j where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO' and u.usu_username not in (select j.usu_username from jurado j where u.usu_username = j.usu_username and j.ppro_codigo='" + Metodo.Value + "')";
            DDLprofesores.Items.Clear();
            DDLprofesores.Items.AddRange(con.cargardatos(sql));
            DDLprofesores.Items.Insert(0, "Seleccione un profesor");
        }
      
       
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
    protected void regresar(object sender, EventArgs e)
    {
        BuscarDocumentos();
        Consulta.Visible = true;
        IBregresar.Visible = false;
        DDLprofesores.Visible = false;
        BTconsultar.Visible = false;
        infoprofesor.Visible = false;
        Jurado1.Visible = false;
        Jurado2.Visible = false;
        Jurado3.Visible = false;
        Linfo.Text = "";

    }
    protected void cancelarasignar(object sender, EventArgs e)
    {
        infoprofesor.Visible = false;
        BTconsultar.Enabled = true;
        Linfo.Text = "";
        DDLprofesores.SelectedIndex = 0;
    }

    /*Metodo que se utiliza para asignar jurado*/
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
    protected void asignarjurado(object sender, EventArgs e)
    {
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        CantidadJur();
        Jur1();
        Jur2();
        if (contadorjur <= 3) {
            if (contadorjur == 1)
            {
                  ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "', '" + contadorjur + "')";
                Ejecutar("", sql);
                infoprofesor.Visible = false;
                BTconsultar.Enabled = true;
                DDLprofesores.SelectedIndex = 0;
                cargarJurado1();
                Jurado1.Visible = true;
                string sql2 = "update proyecto_final set pf_jur1='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar("Jurado asignado correctamente.", sql2);
                ProfDisponible();

            }
            if (contadorjur == 2 && jurado1 == "ELIMINADO")
            {
                ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "', '1')";
                Ejecutar("", sql);
                infoprofesor.Visible = false;
                BTconsultar.Enabled = true;
                DDLprofesores.SelectedIndex = 0;
                cargarJurado1();
                Jurado1.Visible = true;
                string sql2 = "update proyecto_final set pf_jur1='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar("Jurado asignado correctamente.", sql2);
                ProfDisponible();
            }else if (contadorjur == 2 && jurado1 != "ELIMINADO")
                {
                    ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                    string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "',  '" + contadorjur + "' )";
                    Ejecutar("", sql);
                    infoprofesor.Visible = false;
                    BTconsultar.Enabled = true;
                    DDLprofesores.SelectedIndex = 0;
                    cargarJurado2();
                    Jurado2.Visible = true;
                    string sql2 = "update proyecto_final set pf_jur2='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                    Ejecutar("Jurado asignado correctamente.", sql2);
                    ProfDisponible();

            }

            else if (contadorjur == 3 && jurado2 == "PENDIENTE")
            {
                ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "',  '2' )";
                Ejecutar("", sql);
                infoprofesor.Visible = false;
                BTconsultar.Enabled = true;
                DDLprofesores.SelectedIndex = 0;
                cargarJurado2();
                Jurado2.Visible = true;
                string sql2 = "update proyecto_final set pf_jur2='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar("Jurado asignado correctamente.", sql2);
                ProfDisponible();

            }
        
        else if (contadorjur == 3 && jurado1 == "ELIMINADO") {
                ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "', '1')";
                Ejecutar("", sql);
                infoprofesor.Visible = false;
                BTconsultar.Enabled = true;
                Linfo.Text = "";
                DDLprofesores.SelectedIndex = 0;
                cargarJurado1();
                Jurado1.Visible = true;
                string sql3 = "update proyecto_final set pf_jur1='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar("Jurado asignado correctamente.", sql3);
                ProfDisponible();
            }
            else if (contadorjur == 3 && jurado1 != "ELIMINADO")
            {
                ExisteRol(DDLprofesores.Items[DDLprofesores.SelectedIndex].Value);
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "', '" + contadorjur + "')";
                Ejecutar("", sql);
                infoprofesor.Visible = false;
                BTconsultar.Enabled = true;
                Linfo.Text = "";
                DDLprofesores.SelectedIndex = 0;
                cargarJurado3();
                Jurado3.Visible = true;
                string sql3 = "update proyecto_final set pf_jur3='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar("Jurado asignado correctamente.", sql3);
                ProfDisponible();
            }

        }
    }

}