using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Consulta_ProyectoFinal : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Consulta_ProyectoFinal.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            } else{
                DDLconsultaPrograma.Items.Clear();
                string sql = "SELECT PROG_CODIGO, PROG_NOMBRE FROM PROGRAMA";
                DDLconsultaPrograma.Items.AddRange(con.cargardatos(sql));
                DDLconsultaPrograma.Items.Insert(0, "Seleccione Programa");
                DDLconsultaLinea.Items.Insert(0, "Seleccione Linea");
                DDLconsultaTema.Items.Insert(0, "Seleccione Tema");
            }
        }
    }

    /*Evento del boton buscar de la propuesta*/
    protected void buscar(object sender, EventArgs e)
    {
        if (!DDLconsultaTema.SelectedIndex.Equals(0))
        {
            CargarProyectoF(1);
        }
        else if (!DDLconsultaPrograma.SelectedIndex.Equals(0) && !DDLconsultaLinea.SelectedIndex.Equals(0))
        {
            CargarProyectoF(2);
        }
        else
        {
            TResultado.Visible = false;
            Linfo.Text = "Seleccione una programa y una linea de profundización";
        }
    }

    /*Metodos para la consulta de la propuesta*/
    protected void GVresulpro_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void CargarProyectoF(int crit)
    {
        string sql = "";
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                if (crit.Equals(2))
                {
                    sql = "Select F.Ppro_Codigo, F.Pf_Titulo,F.Pf_Fecha,F.Pf_Estado from proyecto_final f,propuesta p, programa pro, estudiante e, tema t, lin_investigacion l " +
                        "where pro.prog_codigo = e.prog_codigo and pro.prog_codigo = '" + DDLconsultaPrograma.Items[DDLconsultaPrograma.SelectedIndex].Value.ToString() + "' and l.linv_codigo = '" + DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value.ToString() + "'and t.tem_codigo = p.tem_codigo and l.linv_codigo = t.linv_codigo  and P.Prop_Codigo = F.Ppro_Codigo";
                } else if (crit.Equals(1))
                {
                    sql = "Select F.Ppro_Codigo, F.Pf_Titulo,F.Pf_Fecha,F.Pf_Estado from proyecto_final f,propuesta p, programa pro, estudiante e, tema t, lin_investigacion l " +
                        "where pro.prog_codigo = e.prog_codigo and pro.prog_codigo = '" + DDLconsultaPrograma.Items[DDLconsultaPrograma.SelectedIndex].Value.ToString() + "' and l.linv_codigo = '" + DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value.ToString() + "' and t.tem_codigo = '" + DDLconsultaTema.Items[DDLconsultaTema.SelectedIndex].Value.ToString() + "' and t.tem_codigo = p.tem_codigo and l.linv_codigo = t.linv_codigo and P.Prop_Codigo = F.Ppro_Codigo";
                }

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVresulpro.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVresulpro.DataBind();
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
        if (DDLconsultaPrograma.SelectedIndex.Equals(0))
        {
            DDLconsultaLinea.Items.Clear();
            DDLconsultaLinea.Items.Insert(0, "Seleccione");
            DDLconsultaTema.Items.Clear();
            DDLconsultaTema.Items.Insert(0, "Seleccione");            
        }
        else
        {
            DDLconsultaLinea.Items.Clear();
            string sql = "SELECT LINV_CODIGO, LINV_NOMBRE FROM LIN_INVESTIGACION WHERE PROG_CODIGO='" + DDLconsultaPrograma.Items[DDLconsultaPrograma.SelectedIndex].Value.ToString() + "'";
            DDLconsultaLinea.Items.AddRange(con.cargardatos(sql));
            DDLconsultaLinea.Items.Insert(0, "Seleccione Linea");
            Linfo.Text = "";
        }
    }
    protected void DDLconsultaLinea_SelectedIndexChanged(object sender, EventArgs e)/*evento del ddl para cuando selecciona un item*/
    {
        TResultado.Visible = false;
        Linfo.Text = "";
        if (DDLconsultaLinea.SelectedIndex.Equals(0))
        {
            DDLconsultaTema.Items.Clear();
            DDLconsultaTema.Items.Insert(0, "Seleccione");
        }
        else
        {
            DDLconsultaTema.Items.Clear();
            string sql3 = "SELECT TEM_CODIGO, TEM_NOMBRE FROM TEMA WHERE LINV_CODIGO='" + DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value.ToString() + "'";
            DDLconsultaTema.Items.AddRange(con.cargardatos(sql3));
            DDLconsultaTema.Items.Insert(0, "Seleccione Tema");
            Linfo.Text = "";
        }
    }

}