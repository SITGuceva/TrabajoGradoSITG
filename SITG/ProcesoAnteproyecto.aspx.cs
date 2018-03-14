using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcesoAnteproyecto : System.Web.UI.Page
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
            ResultadoConsulta();
        }
        ocultarElementos();

    }

    private void ocultarElementos()
    {

        MostrarDDLestadoP.Visible = false;
        LinkDescarga.Visible = false;
        BTterminar.Visible = false;
        BTcancelar.Visible = false;
        BTregresar.Visible = false;
        Linfo.Visible = false;
        Linfo2.Visible = false;
    }

    /*Metodos que se encargan de la consulta de las propuestas que esten pendientes del respectivo programa(comite) */

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
                string sql = "select DISTINCT an.apro_codigo, an.anp_nombre, an.anp_fecha from estudiante e, anteproyecto an, solicitud_dir s, usuario u WHERE  an.apro_codigo = e.PROP_CODIGO and s.prop_codigo = an.apro_codigo and an.ant_aprobacion = 'PENDIENTE' and  s.usu_username='"+Session["id"]+"'";

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


    protected void GVConsultaContenidoP_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVConsultaContenidoP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ResultadoContenidoP();
    }
    private void ResultadoContenidoP()
    {
        TCodigoP.Text = "Código: ";
        CodigoP.Text = CodigoPropuesta;
        TTituloP.Text = "Título: ";
        TituloP.Text = TituloPropuesta;
        LlamarInfo();
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select ANP_DOCUMENTO, ANP_NOMARCHIVO, ANP_TIPO FROM ANTEPROYECTO WHERE APRO_CODIGO=" +CodigoP.Text+ "";

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
           
           
            DDLestadoP.Items.Clear();
        }
    }

    private void LlamarInfo()
    {

        InformacionP.Visible = true;
        MostrarDDLestadoP.Visible = true;
        BTterminar.Visible = true;
        BTcancelar.Visible = true;
        LinkDescarga.Visible = true;

    }


    /*Metodos para la consulta de las observaciones*/
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT AOBS_CODIGO, AOBS_DESCRIPCION FROM ANTE_OBSERVACION  WHERE APROP_CODIGO ='" + CodigoP.Text + "' and AOBS_REALIZADA ='DIRECTOR'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                }
                GVobservacion.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {

        }
    }

    /*Metodos que sirven para el modificar-eliminar de la tabla observaciones*/
    protected void GVobservacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string id = GVobservacion.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from ante_observacion where AOBS_CODIGO='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                LlamarInfo();
                cargarTabla();
            }
        }
    }
    protected void GVobservacion_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        GridViewRow row = (GridViewRow)GVobservacion.Rows[e.RowIndex];
        if (conn != null)
        {

            TextBox observacion = (TextBox)row.Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVobservacion.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update ante_observacion set aobs_descripcion = '" + observacion.Text + "' where  aobs_codigo ='" + codigo.Text + "'";


            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVobservacion.EditIndex = -1;
                LlamarInfo();
                cargarTabla();
            }
        }
    }
    protected void GVobservacion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVobservacion.EditIndex = e.NewEditIndex;
        LlamarInfo();
        cargarTabla();
        GVobservacion.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVobservacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVobservacion.EditIndex = -1;
        LlamarInfo();
        cargarTabla();
    }


    protected void MostrarObservaciones(object sender, EventArgs e)
    {
        MostrarDDLestadoP.Visible = true;
        cargarTabla();
        Resultado.Visible = true;
        MostrarAgregarObs.Visible = true;
        LinkDescarga.Visible = true;
        BTterminar.Visible = true;
        BTcancelar.Visible = true;



    }

    protected void Agregar_observacion(object sender, EventArgs e)
    {


        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");

        if (string.IsNullOrEmpty(TBdescripcion.Text) == true)
        {
            LlamarInfo();
        }
        else
        {
            LlamarInfo();
            string descripcion2 = TBdescripcion.Text;
            string texto = "Se agrego la observacion correctamente";
            string sql = "insert into ante_observacion (AOBS_CODIGO, AOBS_DESCRIPCION, APROP_CODIGO ,AOBS_FECHA, AOBS_REALIZADA) values (ANTEOBSERVACIONID.nextval,'" + descripcion2.ToLower() + "','"+ CodigoP.Text + "',TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'DIRECTOR')";
            Ejecutar(texto, sql);
            TBdescripcion.Text = "";
            Resultado.Visible = true;
            cargarTabla();


        }
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono"))
        {
            LlamarInfo();
        }

    }


    protected void terminar(object sender, EventArgs e)
    {

        if (DDLestadoP.Items[DDLestadoP.SelectedIndex].Value.ToString().Equals("Calificar Anteproyecto"))
        {
            Linfo2.Visible = true;
            MostrarDDLestadoP.Visible = true;
            BTterminar.Visible = true;
            BTcancelar.Visible = true;
        }
        else
        {
            string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
            string texto = "";
            string sql = "update anteproyecto set ant_aprobacion ='" + DDLestadoP.Items[DDLestadoP.SelectedIndex].Value.ToString() + "' where apro_codigo='" + CodigoP.Text + "'";
            Ejecutar(texto, sql);

            Resultado.Visible = false;
            MostrarAgregarObs.Visible = false;
            MostrarDDLestadoP.Visible = false;
            InformacionP.Visible = false;
            LinkDescarga.Visible = true;
            BTterminar.Visible = false;
            BTcancelar.Visible = false;
            Linfo.Visible = true;
            BTregresar.Visible = true;

        }

    }

    protected void cancelar(object sender, EventArgs e)
    {
        Resultado.Visible = false;
        MostrarAgregarObs.Visible = false;
        MostrarDDLestadoP.Visible = false;
        InformacionP.Visible = false;
        BTterminar.Visible = false;
        BTcancelar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;
        DDLestadoP.SelectedIndex = 0;

    }

    protected void regresar(object sender, EventArgs e)
    {

        Linfo.Visible = false;
        BTregresar.Visible = false;
        ResultadoConsulta();
        Consulta.Visible = true;

    }
}
