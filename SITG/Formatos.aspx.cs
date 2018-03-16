using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Formatos : System.Web.UI.Page
{
    Conexion con = new Conexion();
    protected int widestData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        widestData = 0;
        if (!IsPostBack){
           Page.Form.Attributes.Add("enctype", "multipart/form-data"); 
         }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVformatos);
    }

    /*Metodos de crear-consultar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Linfo.Text = "";
        ConsultaFormat.Visible = false;
    }
    protected void Consultar(object sender, EventArgs e)
    {
        Ingreso.Visible = false;
        ConsultaFormat.Visible = true;
        Linfo.Text = "";
        ResultadoConsulta();
    }

    /*Metodos que realizan el guardar */
    protected void Aceptar(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TBnom.Text) == true)
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Los campos son obligatorios";
        }else{
            if (FUdocumento.HasFile) {
                string filename = Path.GetFileName(FUdocumento.PostedFile.FileName);
                string contentType = FUdocumento.PostedFile.ContentType;
                using (Stream fs = FUdocumento.PostedFile.InputStream){
                    using (BinaryReader br = new BinaryReader(fs)) {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        OracleConnection conn = con.crearConexion();
                        if (conn != null) {
                            string query = "insert into FORMATO values (formatoid.nextval ,:For_titulo , :Data, :For_nomarchivo, :For_tipo)";
                            using (OracleCommand cmd = new OracleCommand(query)) {
                                cmd.Connection = conn;
                                cmd.Parameters.Add(":For_titulo", TBnom.Text);
                                cmd.Parameters.Add(":Data", bytes);
                                cmd.Parameters.Add(":For_documento", filename);
                                cmd.Parameters.Add(":For_tipo", contentType);
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                // Response.Redirect(Request.Url.AbsoluteUri);   
                Linfo.ForeColor = System.Drawing.Color.Green;
                Linfo.Text = "Formato guardado satisfatoriamente";
            } else{
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Debe elegir un archivo";
            }
        }
    }
   
    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";
         TBnom.Text = "";
    }

    /*Metodos que realizan la consulta con el modificar e inhabilitar*/
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select FOR_NOMARCHIVO, FOR_DOCUMENTO, FOR_TIPO FROM FORMATO WHERE FOR_ID=" + id + "";

        OracleConnection conn = con.crearConexion();
        if (conn != null){ 
            using (OracleCommand cmd = new OracleCommand(sql, conn)){
                cmd.CommandText = sql;             
                 using (OracleDataReader drc1 = cmd.ExecuteReader()){
                     drc1.Read();
                     contentype = drc1["FOR_TIPO"].ToString();
                     fileName = drc1["FOR_NOMARCHIVO"].ToString();
                     bytes = (byte[])drc1["FOR_DOCUMENTO"];
                    
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
    private void ResultadoConsulta()
    {
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "SELECT FOR_ID, FOR_TITULO FROM FORMATO ORDER BY FOR_ID";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVformatos.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVformatos.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVformatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVformatos.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    protected void GVformatos_RowDataBound(object sender, GridViewRowEventArgs e) {
        /* System.Data.DataRowView drv;
         drv = (System.Data.DataRowView)e.Row.DataItem;
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             if (drv != null)
             {
                 String catName = drv[1].ToString();
                 Response.Write(catName + "/");

                 int catNameLen = catName.Length;
                 if (catNameLen > widestData)
                 {
                     widestData = catNameLen;
                     GVformatos.Columns[2].ItemStyle.Width =widestData * 10;
                     GVformatos.Columns[2].ItemStyle.Wrap = false;
                 }

             }
         }*/
    }
    protected void GVformatos_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            TextBox nombre = (TextBox)GVformatos.Rows[e.RowIndex].Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVformatos.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update formato set for_titulo = '" + nombre.Text + "' where  for_id ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){
                GVformatos.EditIndex = -1;
                ResultadoConsulta();
            }
        }
    }
    protected void GVformatos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVformatos.EditIndex = e.NewEditIndex;
        ResultadoConsulta();
        GVformatos.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVformatos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVformatos.EditIndex = -1;
        ResultadoConsulta();
    }
    protected void GVformatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string id = GVformatos.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from FORMATO where FOR_ID='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){
                ResultadoConsulta();
            }
        }
    }


  
}