using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcesoEST : Conexion
{
    Conexion con = new Conexion();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            Ingreso.Visible = true;
            LBpropuesta.Visible = false;
        }
    }

    /*Evento del boton nueva consulta*/
    protected void Nueva(object sender, EventArgs e)
    {
        TBCodigoE.Text = "";
        BTbuscar.Visible = true;
        BTnueva.Visible = false;
        TBCodigoE.Enabled = true;
        TablaResultado.Visible = false;
        LBpropuesta.Visible = false;
      //  Linfo.Text = "";
    }

    /*Evento del boton buscar*/
    protected void Buscar(object sender, EventArgs e)
    {
        if (TBCodigoE.Text.Equals(""))
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Digite un codigo de estudiante.";
        }
        else if (!TBCodigoE.Text.Equals("")){
            RevisarExiste();
        }
    }
    private void RevisarExiste()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string sql = "SELECT USU_USERNAME FROM ESTUDIANTE WHERE USU_USERNAME ='" + TBCodigoE.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows) {
               CargarPropuesta();
               Linfo.Text = "";
            } else{
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "El estudiante no se encuentra.";
                TBCodigoE.Text = "";
            }
            drc1.Close();
        }
    }
    private void Comprobado()
    {
        TablaResultado.Visible = true;
        BTnueva.Visible = true;
        BTbuscar.Visible = false;
        TBCodigoE.Enabled = false;
        Linfo.Text = "";
    }

    /*Metodos que se utilizan para la consulta y el modificar el estado*/
    public void CargarPropuesta()
    {
        LBpropuesta.Visible = true;
        string sql = "";
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
               
             
                sql = "Select p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado,  CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.sol_estado as Estado from propuesta p, solicitud_dir s, usuario u, estudiante e, comite com, profesor pro where pro.usu_username='"+Session["id"]+"' and pro.com_codigo = com.com_codigo and com.prog_codigo=e.prog_codigo and e.usu_username='"+TBCodigoE.Text+"' and u.usu_username = s.usu_username and e.prop_codigo = s.prop_codigo and s.prop_codigo=p.prop_codigo";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVgepropuesta.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                }
                GVgepropuesta.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
        Comprobado();
    }
    protected void GVgepropuesta_RowDataBound(object sender, GridViewRowEventArgs e) { }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }

}









