using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;

public partial class Pagos : Conexion
{
    Conexion con = new Conexion();
    string codprop;
    string titulo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!Page.IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        revisarpago();
        pagorechazado();
    }

    /* Metodo que verifica si un pago esta pendiente o aprobado, si se cumple se inhabilita subir pago*/
    private void revisarpago()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select * from pagos where usu_username = '"+Session["id"]+"' and pag_estado='PENDIENTE' or pag_estado='APROBADO'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                LBSubir_pago.Enabled = false;
                LBSubir_pago.ForeColor = System.Drawing.Color.Gray;
            }
            drc1.Close();
        }
    }

    /* Metodo que verifica si el pago fue rechazado*/
    private void pagorechazado()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select * from pagos where usu_username = '" + Session["id"] + "' and pag_estado='rechazado'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                LBSubir_pago.Enabled = true;
                LBSubir_pago.ForeColor = System.Drawing.Color.Black;
            }
            drc1.Close();
        }
    }

    /*Metodos que manejan la interfaz del subir-consultar*/
    protected void Subir_pago(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Consulta.Visible = false;
        Linfo.Text = "";
    }

    //metodo que llama a la tabla pagos
    protected void Consulta_pago(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        Ingreso.Visible = false;
        Linfo.Text = "";
        BuscarPago();
    }

    //metodo que sube los pagos(documento);
    protected void Guardar(object sender, EventArgs e)
    {
        DateTime fecha = DateTime.Today;
        if (FUdocumento.HasFile)
        {
            string filename = Path.GetFileName(FUdocumento.PostedFile.FileName);
            string contentType = FUdocumento.PostedFile.ContentType;
            using (Stream fs = FUdocumento.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    OracleConnection conn = con.crearConexion();
                    if (conn != null)
                    {
                        string query = "insert into pagos values (PAGOSID.nextval ,:pag_nombre , :Data, :pag_nomarchivo, :pag_tipo, :pag_fecha ,'PENDIENTE', '"+Session["id"]+"')";
                        using (OracleCommand cmd = new OracleCommand(query))
                        {
                            cmd.Connection = conn;
                            cmd.Parameters.Add(":anp_nombre", TBtitulo.Text);
                            cmd.Parameters.Add(":Data", bytes);
                            cmd.Parameters.Add(":anp_nomarchivo", filename);
                            cmd.Parameters.Add(":anp_tipo", contentType);
                            cmd.Parameters.Add(":anp_fecha", fecha);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
            }
            // Response.Redirect(Request.Url.AbsoluteUri);
            LBSubir_pago.Enabled = false;
            LBSubir_pago.ForeColor = System.Drawing.Color.Gray;
            Ingreso.Visible = false;
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = "Pago guardado satisfatoriamente, cuando sean verificados podrá subir el proyecto final";
            TBtitulo.Text = "";
        }
        else
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe elegir un archivo";
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

    /*Metodos que realizan la funcionalidad de consultar el documento de los pagos subido por el usuario*/
    protected void BuscarPago()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select * from pagos where usu_username = '" + Session["id"] + "'";

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

    /*Metodos que descarga el pago subido*/
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select PAG_DOCUMENTO, PAG_NOMARCHIVO, PAG_TIPO FROM PAGOS WHERE USU_USERNAME=" + id + "";

        OracleConnection conn = con.crearConexion();
        if (conn != null)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader())
                {
                    drc1.Read();
                    contentype = drc1["PAG_TIPO"].ToString();
                    fileName = drc1["PAG_NOMARCHIVO"].ToString();
                    bytes = (byte[])drc1["PAG_DOCUMENTO"];

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