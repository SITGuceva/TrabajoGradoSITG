using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GDirEst : Conexion
{
    Conexion con = new Conexion();

    int prop_codigo;




    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        string sql2 = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido)from profesor p, usuario u where u.usu_username=p.usu_username";
        DDLlista.Items.AddRange(con.cargardatos(sql2));

        RevisarExiste();

    }


    private void RevisarExiste()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT PROP_CODIGO FROM ESTUDIANTE WHERE USU_USERNAME ='" + Session["id"] + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                if (drc1.IsDBNull(0))
                {
                    LBSolicitar.Enabled = false;
                    LBSolicitar.ForeColor = System.Drawing.Color.Gray;


                }
                else
                {

                    LBSolicitar.Enabled = true;
                    LBSolicitar.ForeColor = System.Drawing.Color.Black;
                    prop_codigo= drc1.GetInt32(0);




                }
            }
            drc1.Close();
        }
    }




    protected void Aceptar(object sender, EventArgs e)
    {
                
                string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");

                string sql = "insert into solicitud_dir (SOL_ID, SOL_FECHA, SOL_ESTADO, PROP_CODIGO, USU_USERNAME) values(SOLICITUD_DIR_SEQUENCE.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), 'Pendiente', '"+prop_codigo+"','" + DDLlista.Items[DDLlista.SelectedIndex].Value + "')";

                string texto = "Datos modificados satisfactoriamente";
                Ejecutar(texto, sql);

    }


    protected void Solicitar(object sender, EventArgs e)
    {
        Solicitar2.Visible = true;
    }


    protected void  Consultar(object sender, EventArgs e)
    {
        consulta2.Visible = true;
        resultado.Visible = true;
        cargarTabla();
    }







    /*evento que cambia la pagina de la tabla*/
    protected void gvSysDatosConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvSysDatosConsulta_RowDataBound(object sender, GridViewRowEventArgs e)
    {
     

    }

    public void cargarTabla()
    {
   
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select s.sol_id, s.sol_fecha, s.sol_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director  from solicitud_dir s, usuario u where u.usu_username=s.usu_username and s.prop_codigo='" + prop_codigo+"'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvSysDatosConsulta.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }

                gvSysDatosConsulta.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }












    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono"))
        {
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }
        else
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }

    }



}