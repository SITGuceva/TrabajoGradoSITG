using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CambiarEstadoP : Conexion
{
    Conexion con = new Conexion();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Ingreso.Visible = true;

        }

    }


    protected void Buscar(object sender, EventArgs e)
    {


        if (TBCodigoP.Text.Equals(""))
        {
            
                TablaResultado.Visible = true;
                CargarTablaPropuestaE();

         

        }

        if (TBCodigoE.Text.Equals(""))
        {
           

                TablaResultado.Visible = true;
            CargarTablaPropuestaP();

           

        }

        if (TBCodigoE.Text.Equals("") && TBCodigoP.Text.Equals(""))
        {

            Linfo.Text = "Digite un criterio de busqueda o ambos para consultar";
            Linfo.Visible = true;


        }

        if (!TBCodigoE.Text.Equals("") && !TBCodigoP.Text.Equals(""))
        {

         
                TablaResultado.Visible = true;
                CargarTablaPropuestaAmbas();
            

        }

    }



    /*evento que cambia la pagina de la tabla*/
    protected void gvTablaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvTablaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    public void CargarTablaPropuestaP()
    {

        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "Select p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.sol_estado as Estado from propuesta p, solicitud_dir s, usuario u where s.prop_codigo='" + TBCodigoP.Text + "' and u.usu_username = s.usu_username and s.prop_codigo = p.prop_codigo ";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvTablaResultado.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());

                }

                gvTablaResultado.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }


    public void CargarTablaPropuestaE()
    {

        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "Select p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.sol_estado as Estado from propuesta p, solicitud_dir s, usuario u, estudiante e where e.usu_username='" + TBCodigoE.Text + "' and u.usu_username = s.usu_username and e.prop_codigo = s.prop_codigo and s.prop_codigo=p.prop_codigo ";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvTablaResultado.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());

                }

                gvTablaResultado.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }




    public void CargarTablaPropuestaAmbas()
    {

        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "Select p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.sol_estado as Estado from propuesta p, solicitud_dir s, usuario u, estudiante e where e.usu_username='" + TBCodigoE.Text + "' and e.prop_codigo='" + TBCodigoP.Text + "' and u.usu_username = s.usu_username and e.prop_codigo = s.prop_codigo and s.prop_codigo=p.prop_codigo ";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvTablaResultado.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());

                }

                gvTablaResultado.DataBind();

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
        string sql = "select PROP_NOMARCHIVO, PROP_DOCUMENTO, PROP_TIPO FROM PROPUESTA WHERE PROP_CODIGO=" + id + "";

        OracleConnection conn = con.crearConexion();
        if (conn != null)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader())
                {
                    drc1.Read();

                    contentype = drc1["PROP_TIPO"].ToString();
                    fileName = drc1["PROP_NOMARCHIVO"].ToString();
                    bytes = (byte[])drc1["PROP_DOCUMENTO"];

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



    protected void gvTablaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {



        if (e.CommandName == "Aprobar")
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvTablaResultado.Rows[index];
            Metodo.Value = row.Cells[0].Text;
            string sql = "", texto = "Propuesta Aprobada";
            sql = "update PROPUESTA set PROP_ESTADO='Aprobado' where PROP_CODIGO='" + Metodo.Value + "'";
            Ejecutar(texto, sql);
            TablaResultado.Visible = false;



        }

        if (e.CommandName == "Rechazar")
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvTablaResultado.Rows[index];
            Metodo.Value = row.Cells[0].Text;
            string sql = "", texto = "Propuesta Rechazada";
            sql = "update PROPUESTA set PROP_ESTADO='Rechazado' where PROP_CODIGO='" + Metodo.Value + "'";
            Ejecutar(texto, sql);
            TablaResultado.Visible = false;


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

}









