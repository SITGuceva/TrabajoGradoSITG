using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Programa : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Programa.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }           
        }
        string sql = "SELECT FAC_CODIGO, FAC_NOMBRE FROM FACULTAD WHERE FAC_ESTADO='ACTIVO'";
        DDLfacultad.Items.AddRange(con.cargardatos(sql));
    }

    /*Metodos de crear-consultar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        DDLfacultad.Items.Clear();
        string sql = "SELECT FAC_CODIGO, FAC_NOMBRE FROM FACULTAD WHERE FAC_ESTADO='ACTIVO'";
        DDLfacultad.Items.AddRange(con.cargardatos(sql));
        Resultado.Visible = false;
        BuscarF.Visible = false;      
        Linfo.Text = "";
    }    
    protected void Consultar(object sender, EventArgs e)
    {
        DDLfacultad1.Items.Clear();
        string sql = "SELECT FAC_CODIGO, FAC_NOMBRE FROM FACULTAD WHERE FAC_ESTADO='ACTIVO'";
        DDLfacultad1.Items.AddRange(con.cargardatos(sql));
        BuscarF.Visible = true;
        Resultado.Visible = false;
        Ingreso.Visible = false;
        Linfo.Text = "";
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";      
        TBnombre.Text = "";
        TBsemestre.Text = "";
    }

    /*Metodos que se utilizan para guardar-actualizar-inhabilitar*/
    protected void Aceptar(object sender, EventArgs e)
    {
        string sql = "", sql2="";
        string texto = "";
        string nombre = TBnombre.Text;
        if (Ingreso.Visible){
            if (string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBsemestre.Text) == true){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else{
                sql = "insert into PROGRAMA (PROG_CODIGO,PROG_NOMBRE,FAC_CODIGO,PROG_SEMESTRE) VALUES(programaid.nextval, '" + nombre+ "', '" + DDLfacultad.Items[DDLfacultad.SelectedIndex].Value.ToString() + "', '" + TBsemestre.Text + "')";
                texto = "1";
                Ejecutar(texto, sql);

                if (Verificador.Value.Equals("Funciono")) {
                    sql2 = "insert into COMITE (COM_CODIGO, COM_NOMBRE) VALUES (programaid.currval, '" + nombre + "',)";
                    texto= "Datos guardados satisfactoriamente"; 
                    Ejecutar(texto, sql2);
                }                
            }
            
        }        
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
            if (texto.Equals("1")){
                Verificador.Value = "Funciono";
            }else{
                Linfo.ForeColor = System.Drawing.Color.Green;
                Linfo.Text = texto;
            }       
        }else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
        TBnombre.Text = "";
        TBsemestre.Text = "";
        DDLfacultad.Items.Clear();
        string sql2 = "SELECT FAC_CODIGO, FAC_NOMBRE FROM FACULTAD WHERE FAC_ESTADO='ACTIVO'";
        DDLfacultad.Items.AddRange(con.cargardatos(sql2));
    }

    /*Metodos que se utilizan para la consulta*/
    protected void Buscar(object sender, EventArgs e){
        Resultado.Visible = true;
        cargarTabla();
    }
    protected void GVprog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVprog.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVprog_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "SELECT DISTINCT P.PROG_CODIGO, P.PROG_NOMBRE, P.PROG_SEMESTRE, P.PROG_ESTADO FROM PROGRAMA P, FACULTAD F WHERE P.FAC_CODIGO = F.FAC_CODIGO AND F.FAC_CODIGO='"+ DDLfacultad1.Items[DDLfacultad1.SelectedIndex].Value.ToString()+ "' ORDER BY P.PROG_CODIGO ";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVprog.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVprog.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVprog_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            DropDownList combo = GVprog.Rows[e.RowIndex].FindControl("estado") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox nombre = (TextBox)GVprog.Rows[e.RowIndex].Cells[1].Controls[0];
            TextBox semestre= (TextBox)GVprog.Rows[e.RowIndex].Cells[2].Controls[0];
            TextBox codigo = (TextBox)GVprog.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update programa set prog_nombre = '" + nombre.Text + "', prog_semestre='"+semestre.Text+"',prog_estado='" + estado + "' where  prog_codigo ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVprog.EditIndex = -1;
                
                sql= "update comite set com_nombre = '" + nombre.Text + "', com_estado = '" + estado + "' where prog_codigo = '" + codigo.Text + "'";
                Ejecutar("", sql);

                cargarTabla();
            }
        }
    }
    protected void GVprog_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVprog.EditIndex = e.NewEditIndex;
        cargarTabla();
        GVprog.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVprog_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVprog.EditIndex = -1;
        cargarTabla();
    }

}