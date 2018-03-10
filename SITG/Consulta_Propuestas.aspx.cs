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
        if (!IsPostBack){ 
            DDLconsultaPrograma.Items.Clear();
            string sql = "SELECT PROG_CODIGO, PROG_NOMBRE FROM PROGRAMA";
            DDLconsultaPrograma.Items.AddRange(con.cargardatos(sql));
            DDLconsultaPrograma.Items.Insert(0, "Seleccione Programa");
            DDLconsultaLinea.Items.Insert(0, "Seleccione Linea");
            DDLconsultaTema.Items.Insert(0, "Seleccione Tema");
        }
    }

    /*Evento del boton buscar de la propuesta*/
    protected void Buscar(object sender, EventArgs e)
    {
       
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
                if (crit.Equals(2))
                {
                    sql = "Select p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.sol_estado as Estado from propuesta p, programa pro, estudiante e, tema t, lin_profundizacion l, solicitud_dir s, usuario u where pro.prog_codigo=e.prog_codigo and pro.prog_codigo='"+DDLconsultaPrograma.Items[DDLconsultaPrograma.SelectedIndex].Value.ToString()+"' and l.lprof_codigo='"+ DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value.ToString() + "' and t.tem_codigo = p.tem_codigo and l.lprof_codigo =t.lprof_codigo  and  u.usu_username = s.usu_username and e.prop_codigo = s.prop_codigo and s.prop_codigo=p.prop_codigo  ";
                }
                if (crit.Equals(1))
                {
                    sql = "Select p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.sol_estado as Estado from propuesta p, programa pro, estudiante e, tema t, lin_profundizacion l, solicitud_dir s, usuario u where pro.prog_codigo=e.prog_codigo and pro.prog_codigo='" + DDLconsultaPrograma.Items[DDLconsultaPrograma.SelectedIndex].Value.ToString() + "' and l.lprof_codigo='" + DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value.ToString() + "' and t.tem_codigo='"+ DDLconsultaTema.Items[DDLconsultaTema.SelectedIndex].Value.ToString() + "' and t.tem_codigo = p.tem_codigo and l.lprof_codigo =t.lprof_codigo  and  u.usu_username = s.usu_username and e.prop_codigo = s.prop_codigo and s.prop_codigo=p.prop_codigo  ";
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

    protected void DDLconsultaPrograma_SelectedIndexChanged(object sender, EventArgs e)/*evento del ddl para cuando selecciona un item*/
    {
        if (DDLconsultaPrograma.SelectedIndex.Equals(0))
        {
            DDLconsultaLinea.Items.Clear();
            DDLconsultaLinea.Items.Insert(0, "Seleccione");
            DDLconsultaTema.Items.Clear();
            DDLconsultaTema.Items.Insert(0, "Seleccione");
        }
        else{
            DDLconsultaLinea.Items.Clear();
            string sql = "SELECT LPROF_CODIGO, LPROF_NOMBRE FROM LIN_PROFUNDIZACION WHERE PROG_CODIGO='" + DDLconsultaPrograma.Items[DDLconsultaPrograma.SelectedIndex].Value.ToString() + "'";
            DDLconsultaLinea.Items.AddRange(con.cargardatos(sql));
            DDLconsultaLinea.Items.Insert(0, "Seleccione Linea");
            Linfo.Text = "";
        }
    }

    protected void DDLconsultaLinea_SelectedIndexChanged(object sender, EventArgs e)/*evento del ddl para cuando selecciona un item*/
    {
        if (DDLconsultaLinea.SelectedIndex.Equals(0))
        {
            DDLconsultaTema.Items.Clear();
            DDLconsultaTema.Items.Insert(0, "Seleccione");
        }
        else
        {
            DDLconsultaTema.Items.Clear();
            string sql3 = "SELECT TEM_CODIGO, TEM_NOMBRE FROM TEMA WHERE LPROF_CODIGO='" + DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value.ToString() + "'";
            DDLconsultaTema.Items.AddRange(con.cargardatos(sql3));
            DDLconsultaTema.Items.Insert(0, "Seleccione Tema");
            Linfo.Text = "";
        }
    
       
  
    }



    protected void buscar(object sender, EventArgs e)
    {

        if (DDLconsultaTema.SelectedIndex.Equals(0))
        {
            CargarPropuesta(2);
        }

        if (!DDLconsultaTema.SelectedIndex.Equals(0) && !DDLconsultaLinea.SelectedIndex.Equals(0) )
        {
            CargarPropuesta(1);
        }

        if (DDLconsultaLinea.SelectedIndex.Equals(0))
        {
            TResultado.Visible = false;
            Linfo.Text = "Seleccione una programa y una linea de profundización";
        }





    }



}









