using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DocumentosCom : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack) {
            DDLconsultaLinea.Items.Clear();
            string sql = "SELECT l.LPROF_CODIGO, l.LPROF_NOMBRE FROM LIN_PROFUNDIZACION l, programa p, comite c, profesor d where d.USU_USERNAME = '" + Session["id"] + "' and d.COM_CODIGO = c.COM_CODIGO and c.PROG_CODIGO = p.PROG_CODIGO and l.PROG_CODIGO = p.PROG_CODIGO";
            DDLconsultaLinea.Items.AddRange(con.cargardatos(sql));
            DDLconsultaLinea.Items.Insert(0, "Seleccione Linea");
        }
    }

    protected void LBpropuesta_Click(object sender, EventArgs e) {
        Ingreso.Visible =true;
        Linfo.Text = "";
    }
    protected void LBanteproyecto_Click(object sender, EventArgs e){
        Ingreso.Visible = false;
        TResultado.Visible = false;
        Linfo.Text = "";
    }
    protected void LBproyecto_Click(object sender, EventArgs e)
    {
        Ingreso.Visible = false;
        TResultado.Visible = false;
        Linfo.Text = "";
    }
   
    /*Metodos para la consulta de la propuesta*/
    protected void Btbuscar_Click(object sender, EventArgs e)
    {
        if(!DDLconsultaLinea.SelectedIndex.Equals(0) && !DDLestado.SelectedIndex.Equals(0)){
            CargarPropuesta(1);
        }else if (!DDLestado.SelectedIndex.Equals(0)){
            CargarPropuesta(2);
        }else{
            TResultado.Visible = false;
            Linfo.Text = "Debe seleccionar un parametro";
        }
    } 
    protected void GVresulprop_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void CargarPropuesta(int crit)
    {
        string sql = "";
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                if (crit.Equals(2))
                {
                    sql = "select p.PROP_CODIGO,p.PROP_TITULO, l.LPROF_NOMBRE, t.TEM_NOMBRE,TO_CHAR( p.PROP_FECHA, 'dd/mm/yyyy') as FECHA ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.sol_estado as Estado from propuesta p, estudiante e, lin_profundizacion l, tema t, solicitud_dir s, usuario u where t.LPROF_CODIGO = l.LPROF_CODIGO and t.TEM_CODIGO = p.TEM_CODIGO and p.PROP_CODIGO = e.PROP_CODIGO and p.PROP_ESTADO = '" + DDLestado.Items[DDLestado.SelectedIndex].Text.ToUpper() + "' and u.USU_USERNAME = s.USU_USERNAME and s.PROP_CODIGO = p.PROP_CODIGO";
                }else if (crit.Equals(1))
                {
                   sql = "Select P.Prop_Codigo,P.Prop_Titulo, L.Lprof_Nombre, T.Tem_Nombre,To_Char( P.Prop_Fecha, 'dd/mm/yyyy') As Fecha ,Concat(Concat(U.Usu_Nombre, ' '), U.Usu_Apellido) As Director, S.Sol_Estado As Estado From Propuesta P, Estudiante E, Lin_Profundizacion L, Tema T, Solicitud_Dir S, Usuario U Where T.Lprof_Codigo = L.Lprof_Codigo " +
                        "And T.Tem_Codigo = P.Tem_Codigo And P.Prop_Codigo = E.Prop_Codigo And L.Lprof_Codigo = '"+ DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value + "' And P.Prop_Estado = '"+ DDLestado.Items[DDLestado.SelectedIndex].Text.ToUpper() + "' And U.Usu_Username = S.Usu_Username And S.Prop_Codigo = P.Prop_Codigo";
                }
                Linfo.Text = sql;
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVresulprop.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVresulprop.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
        TResultado.Visible = true;
    }
  
 

}