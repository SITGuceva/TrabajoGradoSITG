using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Facultad : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack) {
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Facultad.aspx");
            if (valida.Equals("false")) {
                Response.Redirect("MenuPrincipal.aspx");
            }
        }
    }

    /*Metodos de crear-consultar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Resultado.Visible = false;
        Linfo.Text = "";
    }
    protected void Consultar(object sender, EventArgs e)
    {
        cargarTabla();
        Resultado.Visible = true;
        Ingreso.Visible = false;
        Linfo.Text = "";
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        TBnombre.Text = "";               
    }

    /*Metodos que se utilizan para guardar-actualizar-inhabilitar*/
    protected void Aceptar(object sender, EventArgs e)
    {
        string sql = "";
        string texto = "";
        if (Ingreso.Visible){
            if (string.IsNullOrEmpty(TBnombre.Text) == true ){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else{
                sql = "insert into FACULTAD (FAC_CODIGO,FAC_NOMBRE) VALUES(facultadid.nextval, '" + TBnombre.Text + "')";
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
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "SELECT FAC_CODIGO,FAC_NOMBRE, FAC_ESTADO  FROM FACULTAD ";

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
    protected void GVfac_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVfac.EditIndex = -1;
        cargarTabla();
    }
    protected void GVfac_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVfac.EditIndex = e.NewEditIndex;
        cargarTabla();
        GVfac.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVfac_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            DropDownList combo = GVfac.Rows[e.RowIndex].FindControl("estado") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox nombre = (TextBox)GVfac.Rows[e.RowIndex].Cells[1].Controls[0];           
            TextBox codigo = (TextBox)GVfac.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update facultad set fac_nombre = '" + nombre.Text + "',fac_estado='" + estado + "' where  fac_codigo ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVfac.EditIndex = -1;

                sql = "update programa set prog_estado = '" + estado + "' where fac_codigo = '" + codigo.Text + "'";
                Ejecutar("", sql);

                cargarTabla();
            }
        }
    }

}