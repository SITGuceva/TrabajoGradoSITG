using Oracle.DataAccess.Client;
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

        if (!IsPostBack)
        {
           
            TablaPeticiones.Visible = true;
            CargarTablaPeticiones();
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

    /*evento que cambia la pagina de la tabla*/
    protected void gvPeDir_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        CargarTablaPeticiones();// la consulta a la base de datos
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvPeDir_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    public void CargarTablaPeticiones()
    {
      
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT S.SOL_ID, S.SOL_FECHA, S.SOL_ESTADO, S.PROP_CODIGO, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director FROM SOLICITUD_DIR S, USUARIO U WHERE S.SOL_ESTADO='Pendiente' AND U.USU_USERNAME = S.USU_USERNAME";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvPeDir.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }

                gvPeDir.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }




    /*evento que cambia la pagina de la tabla*/
    protected void gvSysDatosTitulo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvSysDatosTitulo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    public void CargarTablaTitulo()
    {
        Regresar.Visible = true;
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select prop_titulo from propuesta where prop_codigo = '"+Metodo.Value+"'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvSysDatosTitulo.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                 
                }

                gvSysDatosTitulo.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }






    /*evento que cambia la pagina de la tabla*/
    protected void gvDatosEstudiantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvDatosEstudiantes_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    public void CargarTablaEstudiantes()
    {
 
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select CONCAT(CONCAT(u.usu_apellido, ' '), u.usu_nombre) as integrantes from estudiante e, usuario u  where e.prop_codigo = '" + Metodo.Value + "' and u.usu_username = e.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvDatosEstudiantes.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    
                }

                gvDatosEstudiantes.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }




    protected void gvPeDir_RowCommand(object sender, GridViewCommandEventArgs e)
    {

     
            if (e.CommandName == "Ver")
            {
            Regresar.Visible = true;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvPeDir.Rows[index];
            Metodo.Value = row.Cells[2].Text;


            TablaTitulo.Visible = true;
            TablaIntegrantes.Visible = true;
            CargarTablaEstudiantes();
            CargarTablaTitulo();
            
            TablaPeticiones.Visible = false;


            }

        if (e.CommandName == "Aprobar")
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvPeDir.Rows[index];
            Metodo.Value = row.Cells[2].Text;
            string sql = "", texto = "Propuesta Aprobada";
            sql = "update SOLICITUD_DIR set SOL_ESTADO='Aprobado' where PROP_CODIGO='"+Metodo.Value+"'";
            Ejecutar(texto, sql);
            CargarTablaPeticiones();


        }

        if (e.CommandName == "Rechazar")
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvPeDir.Rows[index];
            Metodo.Value = row.Cells[2].Text;
            string sql = "", texto = "Propuesta Rechazada";
            sql = "update SOLICITUD_DIR set SOL_ESTADO='Rechazado' where PROP_CODIGO='" + Metodo.Value + "'";
            Ejecutar(texto, sql);
            CargarTablaPeticiones();


        }

    }

    protected void regresar(object sender, EventArgs e)
    {
        Regresar.Visible = false;
        TablaTitulo.Visible = false;
        TablaPeticiones.Visible = true;
        TablaIntegrantes.Visible = false;
        CargarTablaPeticiones();

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
}









