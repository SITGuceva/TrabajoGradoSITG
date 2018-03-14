using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;

public partial class Anteproyecto : Conexion
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

        RevisarAprobadoProp(); // llama metodo que verifica si la propuesta fue aprobada
       
        if (!Page.IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
    }

    /* metodo que verifica si una propuesta fue aprobada y habilita el boton de subir propuesta*/
    private void RevisarAprobadoProp()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select p.prop_estado, p.prop_codigo, p.prop_titulo, s.sol_estado from propuesta p, estudiante e, solicitud_dir s where e.usu_username='" + Session["id"] + "' and s.prop_codigo=e.prop_codigo and p.prop_codigo = e.prop_codigo";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                string estado_propuesta = drc1.GetString(0).ToString();
                string estado_director = drc1.GetString(3).ToString();
                codprop = drc1.GetInt32(1).ToString();
                 titulo = drc1.GetString(2).ToString();

                if (estado_propuesta.Equals("APROBADO") && estado_director.Equals("APROBADO"))
                {
                    
                    RevisarAprobadoAnte(); // llama metodo que verifica si el antepryecto fue aprobado
                }
                else
                {
                    LBSubir_propuesta.Enabled = false;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Gray;
                }
            }
            drc1.Close();
        }
    }

    /* Metodo que verifica si un anteproyecto fue aprobado o rechazado*/
    private void RevisarAprobadoAnte()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select an.ant_estado, an.ant_aprobacion from anteproyecto an, estudiante e where e.usu_username='" + Session["id"] + "' and an.apro_codigo = e.prop_codigo";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                string estado_jurado = drc1.GetString(0).ToString();
                string estado_director = drc1.GetString(1).ToString();
            

                if (estado_jurado.Equals("RECHAZADO")||estado_director.Equals("RECHAZADO"))
                {
                   
                    LBSubir_propuesta.Enabled = true;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Black;

                }
                else
                {
                    LBSubir_propuesta.Enabled = false;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Gray;
                }
            }
            drc1.Close();
        }
    }


    /*Metodos que manejar la interfaz del subir-consultar*/
    protected void Subir_propuesta(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Consulta.Visible = false;
        Observaciones.Visible = false;
        Linfo.Text = "";
    }
    protected void Consulta_propuesta(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        Ingreso.Visible = false;
        Observaciones.Visible = false;
        Linfo.Text = "";
        BuscarAnteproyecto();
    }


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
                            string query = "insert into anteproyecto values (:apro_codigo ,:anp_nombre , :Data, :anp_nomarchivo, :anp_tipo, :anp_fecha ,'PENDIENTE', 'PENDIENTE')";
                            using (OracleCommand cmd = new OracleCommand(query))
                            {
                                cmd.Connection = conn;
                                cmd.Parameters.Add(":apro_codigo",codprop);
                                cmd.Parameters.Add(":anp_nombre", titulo);
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
                Linfo.ForeColor = System.Drawing.Color.Green;
                Linfo.Text = "Formato guardado satisfatoriamente";
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
                string sql = "select an.apro_codigo, an.anp_nombre, an.ant_aprobacion, an.ant_estado, an.anp_fecha  from anteproyecto an, estudiante e where an.apro_codigo = e.prop_codigo and e.usu_username ='" + Session["id"] + "'";

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

    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql ="select ANP_DOCUMENTO, ANP_NOMARCHIVO, ANP_TIPO FROM ANTEPROYECTO WHERE APRO_CODIGO=" + id + "";

        OracleConnection conn = con.crearConexion();
        if (conn != null){ 
            using (OracleCommand cmd = new OracleCommand(sql, conn)){
                cmd.CommandText = sql;             
                 using (OracleDataReader drc1 = cmd.ExecuteReader()){
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
    protected void GVconsulta_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "buscar")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsulta.Rows[index];
            ResultadoObservaciones();
            Observaciones.Visible = true;
        }
    }
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        ResultadoObservaciones();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void ResultadoObservaciones()
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
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVobservacion.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }





}