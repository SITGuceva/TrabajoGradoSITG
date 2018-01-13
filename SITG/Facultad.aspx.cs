using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Facultad : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
    }

    /*Metodos de crear-modificar-consultar-inhabilitar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Resultado.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }
    protected void Modificar(object sender, EventArgs e)
    {
        DDLid.Items.Clear();
        string sql2 = "SELECT FAC_CODIGO FROM FACULTAD WHERE FAC_ESTADO='ACTIVO'";
        DDLid.Items.AddRange(con.cargarDDLid(sql2));
        Actualizar.Visible = true;
        Ingreso.Visible = false;
        Resultado.Visible = false;
        Eliminar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }
    protected void Consultar(object sender, EventArgs e)
    {
        cargarTabla();
        Resultado.Visible = true;
        Botones.Visible = false;
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
    }
    protected void Inhabilitar(object sender, EventArgs e)
    {
        DDLid2.Items.Clear();
        string sql1 = "SELECT FAC_CODIGO, FAC_NOMBRE FROM FACULTAD";
        DDLid2.Items.AddRange(con.cargardatos(sql1));
        Eliminar.Visible = true;
        Ingreso.Visible = false;
        Resultado.Visible = false;
        Actualizar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        TBid.Text = "";
        TBnombre.Text = "";       
        TBnombre2.Text = "";   
    }

    /*Metodos que se utilizan para guardar-actualizar-inhabilitar*/
    protected void Aceptar(object sender, EventArgs e)
    {
        string sql = "";
        string texto = "";
        if (Ingreso.Visible){
            if (string.IsNullOrEmpty(TBid.Text) == true || string.IsNullOrEmpty(TBnombre.Text) == true ){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else{
                sql = "insert into FACULTAD (FAC_CODIGO,FAC_NOMBRE) VALUES('" + TBid.Text + "', '" + TBnombre.Text + "')";
                texto = "Datos guardados satisfactoriamente";
                Ejecutar(texto, sql);
            }
        }else if (Actualizar.Visible){
            if (string.IsNullOrEmpty(TBnombre2.Text) == true ){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else {  
                sql = "UPDATE FACULTAD SET FAC_NOMBRE='" + TBnombre2.Text + "' WHERE FAC_CODIGO ='" + DDLid2.Items[DDLid2.SelectedIndex].Value.ToString() + "'";
                texto = "Datos modificados satisfactoriamente";
                Ejecutar(texto, sql);
            }
        }else if (Eliminar.Visible){
            sql = "UPDATE FACULTAD SET FAC_ESTADO = '" + DDLestado.Items[DDLestado.SelectedIndex].Value.ToString() + "' WHERE FAC_CODIGO = '" + DDLid2.Items[DDLid2.SelectedIndex].Value.ToString() + "'";
            texto = "Facultad se ha puesto " + DDLestado.Items[DDLestado.SelectedIndex].Value.ToLower() + " satisfactoriamente";
            Ejecutar(texto, sql);
        }
    }
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
        TBid.Text = "";
        TBnombre.Text = "";
        TBnombre2.Text = "";
    }

    /*Metodos que se utilizan para la consulta*/
    protected void GVfac_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVfac.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVfac_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT FAC_CODIGO,FAC_NOMBRE, FAC_ESTADO  FROM FACULTAD ";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVfac.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVfac.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
}