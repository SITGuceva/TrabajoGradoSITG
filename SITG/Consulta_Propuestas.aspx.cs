using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Consulta_Propuestas : Conexion
{
    Conexion con = new Conexion();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        Ingreso.Visible = true;
    }

    /*Evento del boton buscar de la propuesta*/
    protected void Buscar(object sender, EventArgs e)
    {
        if (TBCodigoE.Text.Equals("") && TBCodigoP.Text.Equals("")){
            Linfo.Text = "Digite un criterio de busqueda o ambos para consultar";
            Linfo.Visible = true;
        } else if (!TBCodigoE.Text.Equals("") && !TBCodigoP.Text.Equals("")) {
            CargarPropuesta(2);
        } else if (TBCodigoP.Text.Equals("")){
            CargarPropuesta(1);
        } else if (TBCodigoE.Text.Equals(""))
        {
            CargarPropuesta(3);
        }
    }
  
    /*Metodos para la consulta de la propuesta*/
    protected void GVresulprop_RowDataBound(object sender, GridViewRowEventArgs e){ }
    public void CargarPropuesta(int crit)
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                if (crit.Equals(2)){
                    sql = "Select p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.sol_estado as Estado from propuesta p, solicitud_dir s, usuario u, estudiante e where e.usu_username='" + TBCodigoE.Text + "' and e.prop_codigo='" + TBCodigoP.Text + "' and u.usu_username = s.usu_username and e.prop_codigo = s.prop_codigo and s.prop_codigo=p.prop_codigo ";
                }else if (crit.Equals(1)){
                    sql = "Select p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.sol_estado as Estado from propuesta p, solicitud_dir s, usuario u, estudiante e where e.usu_username='" + TBCodigoE.Text + "' and u.usu_username = s.usu_username and e.prop_codigo = s.prop_codigo and s.prop_codigo=p.prop_codigo ";
                }else if (crit.Equals(3)) {
                    sql = "Select p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.sol_estado as Estado from propuesta p, solicitud_dir s, usuario u where s.prop_codigo='" + TBCodigoP.Text + "' and u.usu_username = s.usu_username and s.prop_codigo = p.prop_codigo ";
                }
               
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVresulprop.DataSource = dataTable;
                }
                GVresulprop.DataBind();
            }
            conn.Close();
        } catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }

        TResultado.Visible = true;
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









