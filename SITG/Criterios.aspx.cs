using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Criterios : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Criterios.aspx");
            if (valida.Equals("false")) {
                Response.Redirect("MenuPrincipal.aspx");
            }
        }
    }

    /*Metodos de crear-consultar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;       
        Linfo.Text = "";
        ConsultaCrit.Visible = false;
    }   
    protected void Consultar(object sender, EventArgs e)
    {
       
        Ingreso.Visible = false;
        ConsultaCrit.Visible = true;
        Linfo.Text = "";
        ResultadoConsulta();
    }

    /*Metodos que realizan el guardar */
    protected void Aceptar(object sender, EventArgs e)
    {
        string sql = "", texto = "";
        if (Ingreso.Visible){
            if (string.IsNullOrEmpty(TBnom.Text) == true || string.IsNullOrEmpty(TBtipo.Text)==true){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else{
                sql = "insert into CRITERIOS (CRIT_CODIGO,CRIT_NOMBRE,CRIT_TIPO) VALUES(criteriosid.nextval, '" + TBnom.Text + "', '" + TBtipo.Text + "')";
                texto = "Datos guardados satisfactoriamente";
                Ejecutar(texto, sql);             
            }
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
        TBnom.Text = "";
        TBtipo.Text = "";
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        TBnom.Text = "";
        TBtipo.Text = "";
    }

    /*Metodos que realizan la consulta con el modificar e inhabilitar*/
    private void ResultadoConsulta()
    {     
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "SELECT CRIT_CODIGO, CRIT_NOMBRE, CRIT_TIPO,CRIT_ESTADO FROM CRITERIOS ORDER BY CRIT_CODIGO";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVcriterios.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVcriterios.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVcriterios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVcriterios.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    protected void GVcriterios_RowDataBound(object sender, GridViewRowEventArgs e){}
    protected void GVcriterios_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            DropDownList combo = GVcriterios.Rows[e.RowIndex].FindControl("estado") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox nombre = (TextBox)GVcriterios.Rows[e.RowIndex].Cells[1].Controls[0];
            TextBox porcentaje = (TextBox)GVcriterios.Rows[e.RowIndex].Cells[2].Controls[0];
            TextBox codigo = (TextBox)GVcriterios.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update criterios set crit_nombre = '" + nombre.Text + "', crit_porcentaje='"+porcentaje.Text+"', crit_estado='" + estado + "' where  crit_codigo ='" + codigo.Text + "'";
             cmd = new OracleCommand(sql, conn);
             cmd.CommandType = CommandType.Text;
             using (OracleDataReader reader = cmd.ExecuteReader())
             {
                 GVcriterios.EditIndex = -1;
                 ResultadoConsulta();
             }
        }
    }
    protected void GVcriterios_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVcriterios.EditIndex = e.NewEditIndex;
        ResultadoConsulta();
        GVcriterios.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVcriterios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVcriterios.EditIndex = -1;
        ResultadoConsulta();
    }
}