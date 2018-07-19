using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

public partial class Categoria : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e) {
        if (Session["Usuario"] == null) {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Categoria.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }
        }
    }

    /*Metodos de crear-consultar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e) {
        Ingreso.Visible = true;
        Resultado.Visible = false;
        Linfo.Text = "";
    }
    protected void Consultar(object sender, EventArgs e)
    {
        cargarTabla();
        Resultado.Visible = true;
        Ingreso.Visible = false;
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        TBid.Text = "";
        TBnombre.Text = "";
        TBicono.Text = "";
    }

    /*Metodos que se utilizan para guardar*/
    protected void Aceptar(object sender, EventArgs e)
    {
        string sql = "";
        string texto = "";
        if (Ingreso.Visible)
        {
            if (string.IsNullOrEmpty(TBid.Text) == true || string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBicono.Text) == true)
            {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }
            else
            {
                sql = "insert into CATEGORIA_SISTEMA (CATS_ID,CATS_NOMBRE,CATS_ICONO) VALUES('" + TBid.Text + "', '" + TBnombre.Text + "', '" + TBicono.Text + "')";
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

        TBid.Text = "";
        TBnombre.Text = "";
        TBicono.Text = "";
    }

    /*evento que cambia la pagina de la tabla*/
    protected void GVcategoria_PageIndexChanging(object sender, GridViewPageEventArgs e){
        GVcategoria.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    /*evento que se llama cuando llenga las columnas*/
    protected void GVcategoria_RowDataBound(object sender, GridViewRowEventArgs e) { }

    /*Metodos que se utilizan para la consulta*/
    public void cargarTabla(){
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT CATS_ID,CATS_NOMBRE, CATS_ICONO, CATS_ESTADO  FROM CATEGORIA_SISTEMA ";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVcategoria.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVcategoria.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVcategoria_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVcategoria.EditIndex = -1;
        cargarTabla();
    }
    protected void GVcategoria_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVcategoria.EditIndex = e.NewEditIndex;
        cargarTabla();
        GVcategoria.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVcategoria_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            DropDownList combo = GVcategoria.Rows[e.RowIndex].FindControl("estado") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox nombre = (TextBox)GVcategoria.Rows[e.RowIndex].Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVcategoria.Rows[e.RowIndex].Cells[0].Controls[0];
            TextBox icono = (TextBox)GVcategoria.Rows[e.RowIndex].Cells[2].Controls[0];

            string sql = "update categoria_sistema set cats_nombre = '" + nombre.Text + "', cats_icono='" + icono.Text + "', cats_estado='" + estado + "' where  cats_id ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVcategoria.EditIndex = -1;
                sql = "update opcion_sistema set opcs_estado = '" + estado + "' where cats_id = '" + codigo.Text + "'";
                Ejecutar("", sql);
                cargarTabla();
            }
        }
        conn.Close();
    }
}