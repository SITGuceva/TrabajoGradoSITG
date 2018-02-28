using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Evaluar_Criterio : System.Web.UI.Page
{
    Conexion con = new Conexion();
    int porcentaje;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if(!IsPostBack) { 
            CargarCriterios();
            string sql = "SELECT reu_codigo FROM reunion WHERE reu_estado='ACTIVO'";
            DDLreunion.Items.AddRange(con.cargarDDLid(sql));
            string sql2= "select DISTINCT   p.PROP_CODIGO,p.PROP_TITULO from estudiante e, PROPUESTA p, comite c, PROFESOR p " +
                    "WHERE p.PROP_CODIGO = e.PROP_CODIGO and p.PROP_ESTADO = 'Pendiente' and p.COM_CODIGO = c.COM_CODIGO" +
                    " and c.PROG_CODIGO = e.PROG_CODIGO and p.USU_USERNAME = '" + Session["id"] + "'";
            DDLpropuesta.Items.AddRange(con.cargardatos(sql2));

        }
    }

    /*Metodos de crear-modificar-consultar-inhabilitar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        
        Metodo.Value = "";
        Linfo.Text = "";

        CargarCriterios();


    }

    protected void Consultar(object sender, EventArgs e)
    {
        
        
        Metodo.Value = "2";
        
        
        Ingreso.Visible = false;
        
        Linfo.Text = "";
    }
    
   
    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        if (!Ingreso.Visible)
        {
           
        }
        else
        {
           
        }
    }

    
    /*Metodos que se utilizan para guardar-actualizar-inhabilitar*/
    protected void Aceptar(object sender, EventArgs e)
    {
        string sql = "", texto = "";
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");

        sql = "insert into REVISION_PROPUESTA (REV_CODIGO,REV_FECHA,REV_ESTADO,PROP_CODIGO,REU_CODIGO,REV_CRITERIOS,REV_RECOMENDACION) " +
            "VALUES(revisionid.nextval, TO_DATE('"+fecha+ "', 'YYYY-MM-DD HH24:MI:SS'),'PRUEBA', '" + DDLpropuesta.Items[DDLpropuesta.SelectedIndex].Value.ToString() + "', '" + DDLreunion.Items[DDLreunion.SelectedIndex].Value.ToString() + "','90%','"+TBrecomendacion.Text+"')";
        texto = "Datos guardados satisfactoriamente";
        
        Ejecutar(texto, sql);
                  
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

    /*Metodos que se utilizan para la consulta*/
 

    public void CargarCriterios()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT crit_codigo, crit_nombre, crit_porcentaje FROM criterios WHERE crit_estado= 'ACTIVO'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVevaluarcrit.DataSource = dataTable;
                }
                GVevaluarcrit.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    protected void GVevaluarcrit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVevaluarcrit.PageIndex = e.NewPageIndex;
        CargarCriterios();
    }

    protected void GVevaluarcrit_RowDataBound(object sender, GridViewRowEventArgs e) {}

    

    protected void CBcumplio_CheckedChanged(object sender, EventArgs e)
    {
        /*foreach (GridViewRow row in GVevaluarcrit.Rows)
        {
            CheckBox check = row.FindControl("CBcumplio") as CheckBox;

            if (check.Checked)
            {

                //Ex.:
                int variable = Convert.ToInt32(row.Cells[1].Text);
                Linfo.Text = "es" + variable;
                //Cualquier codigo aqui
            }
        }*/
       
        foreach (GridViewRow row in GVevaluarcrit.Rows)
        {

            CheckBox check = row.FindControl("CBcumplio") as CheckBox;

            

            if (check.Checked)
            {
               string x= row.Cells[2].Text;
                porcentaje += Convert.ToInt32( x);
                Linfo.Text = "funciona" + porcentaje;
           

            }
        }

    }

    
}