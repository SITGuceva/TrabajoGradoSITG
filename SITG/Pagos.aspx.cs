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
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!Page.IsPostBack){
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        AnteAprobado();
        revisarpago();
      
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVconsulta);
    }

    /*Metodo que verifica que el anteproyecto fue aprobado.*/
    private void AnteAprobado()  {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string sql = "select A.Apro_Codigo from anteproyecto a , estudiante e where a.apro_codigo = E.Prop_Codigo and E.Usu_Username = '"+Session["id"]+"' and a.ant_estado = 'APROBADO' and a.ant_aprobacion = 'APROBADO'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows) {
                LBSubir_pago.Enabled = true;
                LBSubir_pago.ForeColor = System.Drawing.Color.Black;
            }else{
                LBSubir_pago.Enabled = false;
                LBSubir_pago.ForeColor = System.Drawing.Color.Gray;
            }
            drc1.Close();
        }
    }

    /* Metodo que verifica si el estado del pago*/
    private void revisarpago()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "select Pag_Estado from pagos where usu_username = '" + Session["id"] + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                string estado = drc1[0].ToString();
                if (estado.Equals("RECHAZADO")){
                    LBSubir_pago.Enabled = true;
                    LBSubir_pago.ForeColor = System.Drawing.Color.Black;
                } else{
                    LBSubir_pago.Enabled = false;
                    LBSubir_pago.ForeColor = System.Drawing.Color.Gray;
                }
            }else {
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
    protected void Consulta_pago(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        Ingreso.Visible = false;
        Linfo.Text = "";
        BuscarPago();
    }

    /*Evento del boton guardar*/
    protected void Guardar(object sender, EventArgs e)
    {
        DateTime fecha = DateTime.Today;
        if (FUdocumento.HasFile) {
            string filename = Path.GetFileName(FUdocumento.PostedFile.FileName);
            string contentType = FUdocumento.PostedFile.ContentType;
            using (Stream fs = FUdocumento.PostedFile.InputStream){
                using (BinaryReader br = new BinaryReader(fs)){
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    OracleConnection conn = con.crearConexion();
                    if (conn != null) {
                        string query = "insert into pagos (PAG_ID, PAG_NOMBRE, PAG_DOCUMENTO, PAG_NOMARCHIVO, PAG_TIPO, PAG_FECHA, USU_USERNAME) values (PAGOSID.nextval ,:pag_nombre , :Data, :pag_nomarchivo, :pag_tipo, :pag_fecha , '"+Session["id"]+"')";
                        using (OracleCommand cmd = new OracleCommand(query)){
                            cmd.Connection = conn;
                            cmd.Parameters.Add(":anp_nombre", TBtitulo.Text);
                            cmd.Parameters.Add(":Data", bytes);
                            cmd.Parameters.Add(":pag_nomarchivo", filename);
                            cmd.Parameters.Add(":pag_tipo", contentType);
                            cmd.Parameters.Add(":pag_fecha", fecha);
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
        } else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe elegir un archivo";
        }
    }
 
    /*Metodos que realizan la funcionalidad de consultar el documento de los pagos subido por el usuario*/
    protected void BuscarPago()
    {
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "select * from pagos where usu_username = '" + Session["id"] + "'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())  {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsulta.DataSource = dataTable;
                }
                GVconsulta.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select PAG_DOCUMENTO, PAG_NOMARCHIVO, PAG_TIPO FROM PAGOS WHERE PAG_ID=" + id + "";
        OracleConnection conn = con.crearConexion();
        if (conn != null) {
            using (OracleCommand cmd = new OracleCommand(sql, conn)) {
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader()){
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

    /*Evento del boton limpiar*/
    protected void Blimpiar_Click(object sender, EventArgs e)
    {
        Linfo.Text = "";
        TBtitulo.Text = "";
    }
}