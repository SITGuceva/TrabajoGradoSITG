using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Roles : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e){
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Roles.aspx");
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
    protected void Consultar(object sender, EventArgs e){
        cargarTabla();
        Resultado.Visible = true;      
        Ingreso.Visible = false;      
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e) {
        Linfo.Text = "";
        TBid.Text = "";
        TBnombre.Text = "";
    }

    /*Metodos que se utilizan para guardar*/
    protected void Aceptar(object sender, EventArgs e) {
        string sql = "";
        string texto = "";
        if (Ingreso.Visible) {
            if (string.IsNullOrEmpty(TBid.Text) == true || string.IsNullOrEmpty(TBnombre.Text) == true) {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else {
                sql = "insert into ROL (ROL_ID,ROL_NOMBRE) VALUES('" + TBid.Text + "', '" + TBnombre.Text + "')";
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
        TBid.Text = "";
        TBnombre.Text = "";
    }

    /*Metodos que se utilizan para la consulta*/
    protected void GVrol_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVrol.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVrol_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla(){
        try{
           OracleConnection conn = con.crearConexion();
           OracleCommand cmd = null;
           if (conn != null){                   
             string sql = "SELECT ROL_ID,ROL_NOMBRE, ROL_ESTADO FROM ROL ";      
             cmd = new OracleCommand(sql, conn);
             cmd.CommandType = CommandType.Text;    
             using (OracleDataReader reader = cmd.ExecuteReader()){
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                GVrol.DataSource = dataTable;
                int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
             }
             GVrol.DataBind();
           }
           conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVrol_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            DropDownList combo = GVrol.Rows[e.RowIndex].FindControl("estado") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox nombre = (TextBox)GVrol.Rows[e.RowIndex].Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVrol.Rows[e.RowIndex].Cells[0].Controls[0];
          
            string sql = "update rol set rol_nombre = '" + nombre.Text + "', rol_estado='" + estado + "' where  rol_id ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVrol.EditIndex = -1;
                cargarTabla();
            }
        }
    }
    protected void GVrol_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVrol.EditIndex = e.NewEditIndex;
        cargarTabla();
        GVrol.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVrol_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVrol.EditIndex = -1;
        cargarTabla();
    }

}