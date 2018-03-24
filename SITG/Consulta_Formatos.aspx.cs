using Oracle.DataAccess.Client;

using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Consulta_Formatos : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Consulta_Formatos.aspx");
            if (valida.Equals("false")) {
                Response.Redirect("MenuPrincipal.aspx");
            }
        }      
        ResultadoConsulta();
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVformatos);
    }

    /*Metodos que realizan la consulta y descarga el documento*/
    protected void DownloadFile(object sender, EventArgs e)
    {
        List<string> list = con.FtpConexion();
        int id = int.Parse((sender as LinkButton).CommandArgument);
        string fileName = "", contentype = "", ruta = "";
        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);
        string sql = "select FOR_NOMARCHIVO, FOR_DOCUMENTO, FOR_TIPO FROM FORMATO WHERE FOR_ID=" + id + "";

        OracleConnection conn = con.crearConexion();
        if (conn != null) {
            using (OracleCommand cmd = new OracleCommand(sql, conn)) {
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader()) {
                    drc1.Read();
                    contentype = drc1["FOR_TIPO"].ToString();
                    fileName = drc1["FOR_NOMARCHIVO"].ToString();
                    ruta = drc1["FOR_DOCUMENTO"].ToString();

                    try {
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
                    }catch (WebException l){
                        Linfo.Text = l.ToString();
                    }
                }
            }
        }
    }
    private void ResultadoConsulta()
    {
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
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
    protected void GVformatos_RowDataBound(object sender, GridViewRowEventArgs e){}

}