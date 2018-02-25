using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;

public partial class Propuesta : Conexion
{
    Conexion con = new Conexion();

    protected void Subir_propuesta(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Estado.Visible = false;
        Observaciones.Visible = false;
        DivBotones.Visible = true;
        Linfo.Text = "";
    }

    protected void Estado_propuesta(object sender, EventArgs e)
    {
        Estado.Visible = true;
        Ingreso.Visible = false;
        Observaciones.Visible = false;
        Linfo.Text = "";
        Consulta();      
    }

    protected void Observaciones_propuesta(object sender, EventArgs e)
    {
        Estado.Visible = false;
        Ingreso.Visible = false;
        Observaciones.Visible = true;
        cargarTabla();
        Linfo.Text = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       /* if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }*/
        Boolean prop = Convert.ToBoolean( Session["Propuesta"]);
        if (prop)
        {
            LBSubir_propuesta.Enabled = false;
            LBSubir_propuesta.ForeColor = System.Drawing.Color.Gray;
        }       
    }

    protected void Cancelar(object sender, EventArgs e)
    {
        Ingreso.Visible = false;
        Estado.Visible = false;
        Observaciones.Visible = false;
        DivBotones.Visible = false;
        Linfo.Text = "";
    }

    protected void Guardar(object sender, EventArgs e)
    {
        string filename = Path.GetFileName(FUdocumento.PostedFile.FileName);
        string contentType = FUdocumento.PostedFile.ContentType;
        DateTime fecha = DateTime.Today;
        int codigo= Convert.ToInt32(TBid.Text);

        using (Stream fs = FUdocumento.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                OracleConnection conn = crearConexion();
                if (conn != null)
                {
                    string query = "insert into propuesta values (:Prop_codigo ,:Prop_titulo ,:Prop_fecha, 'Pendiente', :Prop_documento, : Prop_nomarchivo, :Prop_tipo)";
                    using (OracleCommand cmd = new OracleCommand(query))
                    {
                        cmd.Connection = conn;
                        cmd.Parameters.Add(":Prop_codigo", codigo);
                        cmd.Parameters.Add(":Prop_titulo", TBnombre.Text);
                        cmd.Parameters.Add(":Prop_fecha", fecha);
                        cmd.Parameters.Add(":Data", bytes);
                        cmd.Parameters.Add(":Prop_nomarchivo", filename);
                        cmd.Parameters.Add(":Prop_tipo", contentType);

                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                string sql = "UPDATE ESTUDIANTE SET PROP_CODIGO = '" + codigo + "'   WHERE USU_USERNAME = '" + Session["id"] + "'";
                string texto = "Se subio la propuesta correctamente";
                Ejecutar(texto, sql);
                Session["Propuesta"] = true;
            }
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void Consulta()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select p.PROP_CODIGO,p.PROP_TITULO, p.PROP_ESTADO, p.PROP_FECHA  from propuesta p, estudiante e where p.PROP_CODIGO = e.PROP_CODIGO and e.USU_USERNAME ='" + Session["id"] + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVestado.DataSource = dataTable;
                }
                GVestado.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
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

    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select PROP_NOMARCHIVO, PROP_DOCUMENTO, PROP_TIPO FROM PROPUESTA WHERE PROP_CODIGO="+id+"";

        OracleConnection conn = crearConexion();
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

    protected void gvSysRol_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSysRol.PageIndex = e.NewPageIndex;
        cargarTabla();
    }

    protected void gvSysRol_RowDataBound(object sender, GridViewRowEventArgs e) { }

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
                sql = "SELECT DISTINCT O.OBS_CODIGO, O.OBS_DESCRIPCION, O.OBS_REALIZADA FROM OBSERVACION O, ESTUDIANTE E WHERE E.PROP_CODIGO=O.PROP_CODIGO AND E.USU_USERNAME='" + Session["id"] + "' ORDER BY O.OBS_CODIGO";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvSysRol.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                gvSysRol.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
}