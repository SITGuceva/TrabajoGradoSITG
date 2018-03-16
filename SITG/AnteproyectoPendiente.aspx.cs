using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AnteproyectoPendiente : System.Web.UI.Page
{
    Conexion con = new Conexion();
    String CodigoPropuesta; //Recoge el codigo de la propuesta para consultarla posteriormente
    String TituloPropuesta; //Recoge el titulo de la propuesta para consultarla posteriormente

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack)
        {
            ResultadoConsulta(); //consulta los anteproyectos pendientes
            formularioP.Visible = false; // guarda el codigo del anteproyecto a lo largo de toda la clase (Se oculta inicialmente)
        }
        ocultarElementos();
    }


    /*Metodo que oculta todos los elementos de la clase*/
    private void ocultarElementos()
    {
        MostrarDDLestadoP.Visible = false;
        BTterminar.Visible = false;
        BTcancelar.Visible = false;
        BTregresar.Visible = false;
        Linfo.Text = "";

    }

    /*Metodos que se encargan de generar tablas con el contenido deseado*/

    /*Tabla que carga la información del anteproyecto para descargar*/
    protected void GVconsultaPP2_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVconsultaPP2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {}

    /*Metodos que realizan la funcionalidad de consultar el anteproyecto*/
    protected void BuscarAnteproyecto()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select apro_codigo, anp_nombre, anp_fecha  from anteproyecto where apro_codigo='"+CodigoP.Text+"'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsultaPP2.DataSource = dataTable;
                }
                GVconsultaPP2.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }


    /*Consulta de los anteproyectos que estan pendientes del respectivo programa(comite) */

    protected void GVconsultaPP_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVconsultaPP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVconsultaPP.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    private void ResultadoConsulta()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select DISTINCT   an.apro_codigo, an.anp_nombre, an.anp_fecha, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director from estudiante e, anteproyecto an, comite c, PROFESOR d, solicitud_dir s, usuario u WHERE u.usu_username = s.usu_username and e.prop_codigo = s.prop_codigo and s.prop_codigo = an.apro_codigo and an.apro_codigo = e.prop_codigo and an.ant_evaluador = 'SIN ASIGNAR' and d.COM_CODIGO = c.COM_CODIGO and c.PROG_CODIGO = e.PROG_CODIGO and d.USU_USERNAME = '"+Session["id"]+"'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsultaPP.DataSource = dataTable;
                }
                GVconsultaPP.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
        }
    }

    // Metodo que recoge el titulo y el codigo de la propuesta en la tabla
    protected void GVconsultaPP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ConsultarPropuesta")
        {
            Consulta.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsultaPP.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene el codigo de propuesta en la tabla
            CodigoPropuesta = Metodo.Value;
            Metodo.Value = row.Cells[1].Text; //obtiene el titulo de la propuesta en la tabla
            TituloPropuesta = Metodo.Value;
            ResultadoContenidoP();

        }
    }

    /*Asigno a las variables globales el titulo y el codigo del anteproyecto*/
    private void ResultadoContenidoP()
    {
        TCodigoP.Text = "Código: ";
        CodigoP.Text = CodigoPropuesta;
        TTituloP.Text = "Título: ";
        TituloP.Text = TituloPropuesta;
        LlamarInfo();
    }

    /*Metodo que descarga la hoja de vida del profesor*/
    protected void DownloadFile2(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select PROF_DOCUMENTO, PROF_NOMARCHIVO, PROF_TIPO FROM PROFESOR WHERE USU_USERNAME='"+id+"'";

        OracleConnection conn = con.crearConexion();
        if (conn != null)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader())
                {
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

    /*Metodo que descarga el anteproyecto*/
    protected void DownloadFile(object sender, EventArgs e)
    {
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select ANP_DOCUMENTO, ANP_NOMARCHIVO, ANP_TIPO FROM ANTEPROYECTO WHERE APRO_CODIGO=" + CodigoP.Text + "";

        OracleConnection conn = con.crearConexion();
        if (conn != null)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader())
                {
                    drc1.Read();
                    contentype = drc1["ANP_TIPO"].ToString();
                    fileName = drc1["ANP_NOMARCHIVO"].ToString();
                    bytes = (byte[])drc1["ANP_DOCUMENTO"];

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

    // Metodo que recoge el titulo y el codigo de la propuesta en la tabla
    protected void GVConsultaContenidoP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ConsultarPropuesta")
        {
            Consulta.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsultaPP.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene el codigo de propuesta en la tabla
            CodigoPropuesta = Metodo.Value;
            Metodo.Value = row.Cells[1].Text; //obtiene el titulo de la propuesta en la tabla
            TituloPropuesta = Metodo.Value;
            ResultadoContenidoP();
            consulta2.Visible = true;
            BuscarAnteproyecto();

            DDLprofesores.Items.Clear();
        }
    }


    /*Metodos que sirven para buscar la informacion del profesor*/
    protected void InfProfesor(object sender, EventArgs e)
    {
        CargarInfoProfesor();
        GVinfprof.Visible = true;
    }
    private void CargarInfoProfesor()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select usu_username,usu_telefono, usu_direccion, usu_correo  from usuario  where usu_username='" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "'";

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

    /*Metodo que llama a los elementos despues del evento revisar al inicio*/
    private void LlamarInfo()
    {
        BuscarAnteproyecto();
        consulta2.Visible = true;
        DDLprofesores.Items.Clear();
        string sql = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido)from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
        DDLprofesores.Items.AddRange(con.cargardatos(sql));
        DDLprofesores.Items.Insert(0, "Seleccione un profesor");
        MostrarDDLestadoP.Visible = true;
        BTterminar.Visible = true;
        BTcancelar.Visible = true;


    }

    /*Metodo que asigna y cambia de estado un anteproyecto*/
    protected void terminar(object sender, EventArgs e)
    {

        if (DDLprofesores.Items[DDLprofesores.SelectedIndex].Value.ToString().Equals("Seleccione un profesor"))
        {
            Linfo.Text = "Seleccione primero un profesor";
            MostrarDDLestadoP.Visible = true;
            infoprofesor.Visible = false;
            BTterminar.Visible = true;
            BTcancelar.Visible = true;
        }
        else
        {
   
            string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
            string texto = "";
            string sql = "insert into evaluador (EVA_ID, EVA_FECHA,USU_USERNAME ,APRO_CODIGO) values (EVALUADORID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'"+ DDLprofesores.Items[DDLprofesores.SelectedIndex].Value+"', '" + CodigoP.Text + "')";
            string sql2 = "update anteproyecto set ant_evaluador='ASIGNADO' where apro_codigo='" + CodigoP.Text + "'";
            Ejecutar(texto, sql);
            Ejecutar(texto, sql2);
             MostrarDDLestadoP.Visible = false;
            CodigoP.Visible = false;
            consulta2.Visible = false;
            TCodigoP.Visible = false;
            BTterminar.Visible = false;
            BTcancelar.Visible = false;
            infoprofesor.Visible = false;
            Linfo.Text = "Se ha asignado el jurado para este proyecto satisfactoriamente";
            BTregresar.Visible = true;
            DDLprofesores.SelectedIndex = 0;

        }

    }

    /*Metodo que cancela el proceso de asignación de jurado*/
    protected void cancelar(object sender, EventArgs e)
    {
      
        MostrarDDLestadoP.Visible = false;
        BTterminar.Visible = false;
        BTcancelar.Visible = false;
        infoprofesor.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        DDLprofesores.SelectedIndex = 0;
        consulta2.Visible = false;


    }

    /*Metodo que regresa a la lista de anteproyectos pendientes*/
    protected void regresar(object sender, EventArgs e)
    {

        Linfo.Text = "";
        BTregresar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        consulta2.Visible = false;

    }
    /*Metodo que llama a la tabla profesor para ver su información*/
    protected void consultarprofesor(object sender, EventArgs e)
    {
        if (DDLprofesores.SelectedIndex.Equals(0))
        {
            Linfo.Text = "Seleccione primero un profesor";
            MostrarDDLestadoP.Visible = true;
            infoprofesor.Visible = false;
            BTterminar.Visible = true;
            BTcancelar.Visible = true;

        }
        else

            MostrarDDLestadoP.Visible = true;
            BTterminar.Visible = true;
            BTcancelar.Visible = true;
            CargarInfoProfesor();
            infoprofesor.Visible = true;

        }

    /*Metodo que se encarga de ejecutar los sql*/
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono"))
        {
            LlamarInfo();
        }

    }

}
