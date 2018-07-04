using Oracle.DataAccess.Client;
using System;
using System.Data;
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
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "ProcesoEST.aspx");
            if (valida.Equals("false")){
              Response.Redirect("MenuPrincipal.aspx");
            }else{
                LBpropuesta.Visible = false;
                LBanteproyecto.Visible = false;
                LBproyectofinal.Visible = false;
            }
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
        TablaResultado2.Visible = false;
        TablaResultado3.Visible = false;
        LBpropuesta.Visible = false;
        LBanteproyecto.Visible = false;
        LBproyectofinal.Visible = false;
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
                CargarAnteproyecto();
                CargarProyectoFinal();
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
        TablaResultado2.Visible = true;
        TablaResultado3.Visible = true;
        BTnueva.Visible = true;
        BTbuscar.Visible = false;
        TBCodigoE.Enabled = false;
        Linfo.Text = "";
    }

    /*Metodos que se utilizan para la consulta de la propuesta*/
    public void CargarPropuesta()
    {
        LBpropuesta.Visible = true;
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
               string sql = "Select p.prop_codigo, p.prop_titulo, p.prop_fecha,INITCAP(p.prop_estado) as estado,  CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, INITCAP(s.dir_estado) as direstado from propuesta p, director s, usuario u, estudiante e, profesor pro where pro.usu_username='" + Session["id"]+"' and pro.com_codigo =e.prog_codigo and e.usu_username='"+TBCodigoE.Text+"' and u.usu_username = s.usu_username and e.prop_codigo = s.prop_codigo and s.prop_codigo=p.prop_codigo";
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

    /*Metodos que se utilizan para la consulta del anteproyecto*/
    public void CargarAnteproyecto()
    {
        LBanteproyecto.Visible = true;
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "Select P.APRO_CODIGO, P.ANP_NOMBRE, P.ANP_FECHA, INITCAP(P.ANP_APROBACION) as aprobacion ,INITCAP(P.ANP_ESTADO) as estado,CONCAT(CONCAT(o.usu_nombre, ' '), o.usu_apellido) as revisor from anteproyecto p, usuario o, estudiante e, profesor d, evaluador r " +
                             "where d.usu_username = '" + Session["id"] + "' and d.com_codigo = e.prog_codigo  and e.usu_username = '" + TBCodigoE.Text + "' and e.prop_codigo = p.apro_codigo and r.Usu_Username = o.Usu_Username and r.Apro_Codigo = e.Prop_Codigo";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()) {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVanteproyecto.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                }
                GVanteproyecto.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
        Comprobado();
    }
    protected void GVanteproyecto_RowDataBound(object sender, GridViewRowEventArgs e) { }

    /*Metodos que se utilizan para la consulta del proyecto final*/
    public void CargarProyectoFinal()
    {
        LBproyectofinal.Visible = true;
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "Select distinct p.ppro_codigo, p.pf_titulo, p.pf_fecha, INITCAP(p.pf_aprobacion) as aprobacion, InitCap(p.pf_jur1) as jur1, InitCap(p.pf_jur2)  as jur2, InitCap(p.pf_jur3) as jur3, INITCAP(p.pf_estado) as estado from proyecto_final p, estudiante e, profesor d " +
                             " where d.usu_username = '"+Session["id"]+"' and d.com_codigo = e.prog_codigo and e.usu_username = '"+TBCodigoE.Text+"' and e.prop_codigo = p.ppro_codigo";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVproyectofinal.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                }
                GVproyectofinal.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
        Comprobado();
    }
    protected void GVproyectofinal_RowDataBound(object sender, GridViewRowEventArgs e) { }

}









