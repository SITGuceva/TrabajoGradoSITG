using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OpcSistema : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e){
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        DDLcategoria.Items.Clear();
        string sql = "SELECT CATS_ID, CATS_NOMBRE FROM CATEGORIA_SISTEMA";
        DDLcategoria.Items.AddRange(con.cargardatos(sql));
    }

    /*Metodos de crear-consultar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e) {
        Ingreso.Visible = true;
        DDLcategoria.Items.Clear();
        string sql = "SELECT CATS_ID, CATS_NOMBRE FROM CATEGORIA_SISTEMA";
        DDLcategoria.Items.AddRange(con.cargardatos(sql));
        Resultado.Visible = false;
        Busqueda.Visible = false;
        Linfo.Text = "";
    }
    protected void Consultar(object sender, EventArgs e){
        DDLcat.Items.Clear();
        string sql = "SELECT CATS_ID, CATS_NOMBRE FROM CATEGORIA_SISTEMA";
        DDLcat.Items.AddRange(con.cargardatos(sql));
        Resultado.Visible = false;
        Busqueda.Visible = true;
        Ingreso.Visible = false;
        Linfo.Text = "";
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e){
        Linfo.Text = "";      
        TBnombre.Text = "";
        TBurl.Text = "";
    }

    /*Metodos que se utilizan para guardar*/
    protected void Aceptar(object sender, EventArgs e){          
        string sql = "";
        string texto = "";
        if (Ingreso.Visible)
        {
            if (string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBurl.Text) == true)
            {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }
            else
            {
                sql = "insert into OPCION_SISTEMA (OPCS_ID,OPCS_NOMBRE,CATS_ID,OPCS_URL) VALUES(OPCSID.nextval, '" + TBnombre.Text + "', '" + DDLcategoria.Items[DDLcategoria.SelectedIndex].Value + "', '" + TBurl.Text + "')";

                texto = "Datos guardados satisfactoriamente";
                Ejecutar(texto, sql);
            }
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
        TBnombre.Text = "";
        TBurl.Text = "";
    }

    /*Metodos que se utilizan para la consulta*/
    protected void Buscar(object sender, EventArgs e)
    {
        cargarTabla();
        Resultado.Visible = true;
    }
    protected void GVSysRol_PageIndexChanging(object sender, GridViewPageEventArgs e){
        GVSysRol.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVSysRol_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla(){
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT DISTINCT O.OPCS_ID, O.OPCS_NOMBRE, O.OPCS_URL, O.OPCS_ESTADO FROM OPCION_SISTEMA O, CATEGORIA_SISTEMA C WHERE O.CATS_ID = C.CATS_ID AND C.CATS_ID='"+ DDLcat.Items[DDLcat.SelectedIndex].Value.ToString() + "'ORDER BY O.OPCS_ID ";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVSysRol.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVSysRol.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVSysRol_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVSysRol.EditIndex = -1;
        cargarTabla();
    }
    protected void GVSysRol_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVSysRol.EditIndex = e.NewEditIndex;
        cargarTabla();
        GVSysRol.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVSysRol_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            DropDownList combo = GVSysRol.Rows[e.RowIndex].FindControl("estado") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox nombre = (TextBox)GVSysRol.Rows[e.RowIndex].Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVSysRol.Rows[e.RowIndex].Cells[0].Controls[0];
            TextBox url = (TextBox)GVSysRol.Rows[e.RowIndex].Cells[2].Controls[0];

            string sql = "update opcion_sistema set opcs_nombre = '" + nombre.Text + "', opcs_url='" + url.Text + "', opcs_estado='" + estado + "' where  opcs_id ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVSysRol.EditIndex = -1;
                cargarTabla();
            }
        }
    }
}
