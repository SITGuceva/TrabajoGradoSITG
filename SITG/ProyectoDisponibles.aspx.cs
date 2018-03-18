using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProyectoDisponibles : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null) {
            Response.Redirect("Default.aspx");
        }
        Consultaproyectos.Visible = true;
        ResultadoConsulta();
    }

    /*Metodos que realizan la consulta proyectos disponibles*/
    private void ResultadoConsulta()
    {
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "select P.Proy_Id,L.Lprof_Nombre, T.Tem_Nombre ,P.Proy_Nombre, P.Proy_Descripcion, P.Proy_Cantest,P.Proy_Fecha , CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as creador from proyectos p, lin_profundizacion l, programa p ,estudiante e, tema t , usuario u " +
                    "where T.Tem_Codigo = P.Tem_Codigo and T.Lprof_Codigo = L.Lprof_Codigo and P.Usu_Username = U.Usu_Username  and P.Proy_Estado = 'DISPONIBLE' and P.Prog_Codigo = E.Prog_Codigo and E.Usu_Username = '"+Session["id"]+ "' and L.Prog_Codigo = P.Prog_Codigo order by P.Proy_Id";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVproyectos.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVproyectos.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVproyectos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVproyectos.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    protected void GVproyectos_RowDataBound(object sender, GridViewRowEventArgs e) { }


}