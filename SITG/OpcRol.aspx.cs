using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class OpcRol : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e) {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }else{
            if (!IsPostBack){
                string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "OpcRol.aspx");
                if (valida.Equals("false")) {
                    Response.Redirect("MenuPrincipal.aspx");
                }else { 
                    DDLrolbuscar.Items.Clear();
                    string sql = "SELECT ROL_ID, ROL_NOMBRE FROM ROL";
                    DDLrolbuscar.Items.AddRange(con.cargardatos(sql));
                }
            }
        }
   
    }   

    /*Metodos de crear-consultar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e) {
        DDLrolbuscar.Items.Clear();
        string sql = "SELECT ROL_ID, ROL_NOMBRE FROM ROL";
        DDLrolbuscar.Items.AddRange(con.cargardatos(sql));
        Metodo.Value = "1";
        Linfo.Text = "";
        Ingreso.Visible = false;
        Busqueda.Visible = true;
        Resultado.Visible = false;
        Ltitulo.Text = "Asignar Opción al Rol";
    }
    protected void Consultar(object sender, EventArgs e)
    {
        DDLrolbuscar.Items.Clear();
        string sql = "SELECT ROL_ID, ROL_NOMBRE FROM ROL";
        DDLrolbuscar.Items.AddRange(con.cargardatos(sql));
        Metodo.Value = "2";
        Linfo.Text = "";
        Ingreso.Visible = false;
        Busqueda.Visible = true;
        Resultado.Visible = false;
        Ltitulo.Text = "Consultar Opciones del rol";
    }

    /*Evento del boton buscar*/
    protected void Buscar(object sender, EventArgs e){      
        if (Metodo.Value.Equals("1")){
            CargarOpciones();
            Ingreso.Visible = true;
        }else if (Metodo.Value.Equals("2"))
        {
            Resultado.Visible = true;
            cargarTabla();
        }
    }
    
    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Ingreso.Visible = false;
        Busqueda.Visible = true;
        Resultado.Visible = false;
        Linfo.Text = "";   
    }

    /*Metodos que se utilizan para guardar*/
    protected void Aceptar(object sender, EventArgs e){
        string sql = "";
        string texto = "";
        Linfo.Text = "estos son todos" + Metodo.Value;
        GVasignaopc.Enabled = false;
        if (Ingreso.Visible){
            String[] ciclo = Metodo.Value.Split(' ');

            for(int i=0; i< ciclo.Length-1; i++)
            {
                 sql = "insert into OPCION_ROL (OPCROL_ID,OPCS_ID,ROL_ID) VALUES(opcrid.nextval, '" + ciclo[i] + "', '" + DDLrolbuscar.Items[DDLrolbuscar.SelectedIndex].Value.ToString() + "')";
                 texto = "Datos guardados satisfactoriamente";
                 Ejecutar(sql, texto);
            }
            CargarOpciones();
            GVasignaopc.Enabled = true;
        }
    }
    private void Ejecutar( string sql,string texto)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }
    public void CargarOpciones()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "SELECT O.OPCS_ID,O.OPCS_NOMBRE FROM OPCION_SISTEMA O WHERE O.OPCS_ESTADO='ACTIVO' AND O.OPCS_ID NOT IN(SELECT R.OPCS_ID FROM OPCION_ROL R WHERE  R.ROL_ID = '" + DDLrolbuscar.Items[DDLrolbuscar.SelectedIndex].Value.ToString() + "') ORDER BY O.OPCS_ID";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()) {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVasignaopc.DataSource = dataTable;
                }
                GVasignaopc.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void CBeligio_CheckedChanged(object sender, EventArgs e)
    {
        string codigos="";
        foreach (GridViewRow row in GVasignaopc.Rows)
        {
            CheckBox check = row.FindControl("CBeligio") as CheckBox;
            if (check.Checked){
                codigos += row.Cells[0].Text + " ";
                Metodo.Value = codigos;
            }
        }
        
    }
    protected void GVasignaopc_RowDataBound(object sender, GridViewRowEventArgs e){}
    protected void GVasignaopc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVasignaopc.PageIndex = e.NewPageIndex;
        CargarOpciones();
    }

     /*Metodos que se utilizan para la consulta*/
    protected void GVopcrol_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string id = GVopcrol.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from opcion_rol where opcrol_id='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){
                cargarTabla();
            }
        }
        conn.Close();
    }
    protected void GVopcrol_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVopcrol.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVopcrol_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
               string sql = "SELECT DISTINCT C.OPCROL_ID,O.OPCS_ID,O.OPCS_NOMBRE  FROM OPCION_SISTEMA O, OPCION_ROL C WHERE O.OPCS_ID = C.OPCS_ID AND C.ROL_ID = '"+ DDLrolbuscar.Items[DDLrolbuscar.SelectedIndex].Value.ToString() + "' ORDER BY O.OPCS_ID ";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVopcrol.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVopcrol.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }       
    }


}