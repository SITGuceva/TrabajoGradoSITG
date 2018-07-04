using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Consulta_Anteproyecto : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Consulta_Anteproyecto.aspx");
            if (valida.Equals("false")) {
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                DDLconsultaPrograma.Items.Clear();
                string sql = "SELECT P.Prog_Codigo, P.Prog_Nombre FROM PROGRAMA p, FACULTAD f, DECANO d WHERE F.Fac_Codigo=P.Fac_Codigo and D.Fac_Codigo = F.Fac_Codigo and D.Usu_Username='" + Session["id"] + "'";
                DDLconsultaPrograma.Items.AddRange(con.cargardatos(sql));
                DDLconsultaPrograma.Items.Insert(0, "Seleccione");
                DDLconsultaLinea.Items.Insert(0, "Seleccione");
                DDLconsultaTema.Items.Insert(0, "Seleccione");
            }
        }
    }

    /*Evento del boton buscar de la propuesta*/
    protected void buscar(object sender, EventArgs e)
    {
        if (!DDLconsultaTema.SelectedIndex.Equals(0)) {
            CargarAnteproyecto(1);
        }else if (!DDLconsultaPrograma.SelectedIndex.Equals(0) && !DDLconsultaLinea.SelectedIndex.Equals(0)){
            CargarAnteproyecto(2);
        }else {
            TResultado.Visible = false;
            Linfo.Text = "Seleccione una programa y una línea de profundización";
        }
    }

    /*Metodos para la consulta de la propuesta*/
    protected void GVresulant_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void CargarAnteproyecto(int crit)
    {
        string sql = "";
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                if (crit.Equals(2)){
                    sql = "Select distinct a.apro_codigo, a.anp_nombre, a.anp_fecha,Initcap(A.anp_Estado) as Estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as revisor from anteproyecto a, programa p, estudiante e,  lin_investigacion l, usuario u , evaluador r where p.prog_codigo = e.prog_codigo and p.prog_codigo = '" + DDLconsultaPrograma.Items[DDLconsultaPrograma.SelectedIndex].Value.ToString() + "' and l.linv_codigo='" + DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value.ToString() + "' and u.usu_username = r.Usu_Username and r.Apro_Codigo = a.Apro_Codigo  order by A.Anp_Fecha";
                } else if (crit.Equals(1)){
                    sql = "Select distinct a.apro_codigo, a.anp_nombre, a.anp_fecha,Initcap(a.anp_Estado) as Estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as revisor from anteproyecto a,propuesta p, programa b, estudiante e, tema t, lin_investigacion l, usuario u ,evaluador r where b.prog_codigo = e.prog_codigo  and b.prog_codigo = '" + DDLconsultaPrograma.Items[DDLconsultaPrograma.SelectedIndex].Value.ToString() + "' and l.linv_codigo = '" + DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value.ToString() + "' and t.tem_codigo = '" +  DDLconsultaTema.Items[DDLconsultaTema.SelectedIndex].Value.ToString() + "' and t.tem_codigo = p.tem_codigo and l.linv_codigo = t.linv_codigo  and u.usu_username  =r.Usu_Username and p.Prop_Codigo = a.Apro_Codigo and r.Apro_Codigo = a.Apro_Codigo  order by A.Anp_Fecha";
                }

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVresulant.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVresulant.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
        TResultado.Visible = true;
    }
    protected void DDLconsultaPrograma_SelectedIndexChanged(object sender, EventArgs e)/*evento del ddl para cuando selecciona un item*/
    {
        TResultado.Visible = false;
        Linfo.Text = "";
        if (DDLconsultaPrograma.SelectedIndex.Equals(0)){
            DDLconsultaLinea.Items.Clear();
            DDLconsultaLinea.Items.Insert(0, "Seleccione");
            DDLconsultaTema.Items.Clear();
            DDLconsultaTema.Items.Insert(0, "Seleccione");
        } else{
            DDLconsultaLinea.Items.Clear();
            string sql = "SELECT LINV_CODIGO, LINV_NOMBRE FROM LIN_INVESTIGACION WHERE PROG_CODIGO='" + DDLconsultaPrograma.Items[DDLconsultaPrograma.SelectedIndex].Value.ToString() + "'";
            DDLconsultaLinea.Items.AddRange(con.cargardatos(sql));
            DDLconsultaLinea.Items.Insert(0, "Seleccione");
            Linfo.Text = "";
        }
    }
    protected void DDLconsultaLinea_SelectedIndexChanged(object sender, EventArgs e)/*evento del ddl para cuando selecciona un item*/
    {
        TResultado.Visible = false;
        Linfo.Text = "";
        if (DDLconsultaLinea.SelectedIndex.Equals(0)) {
            DDLconsultaTema.Items.Clear();
            DDLconsultaTema.Items.Insert(0, "Seleccione");
        } else {
            DDLconsultaTema.Items.Clear();
            string sql3 = "SELECT TEM_CODIGO, TEM_NOMBRE FROM TEMA WHERE LINV_CODIGO='" + DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value.ToString() + "'";
            DDLconsultaTema.Items.AddRange(con.cargardatos(sql3));
            DDLconsultaTema.Items.Insert(0, "Seleccione");
            Linfo.Text = "";
        }
    }
    protected void DDLconsultaTema_SelectedIndexChanged(object sender, EventArgs e)
    {
        TResultado.Visible = false;
        Linfo.Text = "";
    }

}