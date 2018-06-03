using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Asignar_Comite : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack) {
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Asignar_Comite.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }
        }
    }

    /*Metodo que maneja el asignar-consultar en el fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Linfo.Text = "";
        Ingreso.Visible = true;
        Consultar.Visible = false;
        Miembros.Visible = false;
        TBcodigo.Text = "";
        Roles.Visible = false;
        ResultadoUsuario.Visible = false;
    }
    protected void Buscar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        DDLcom2.Items.Clear();
        string sql = "SELECT COM_CODIGO, COM_NOMBRE FROM COMITE WHERE COM_ESTADO='ACTIVO'";
        DDLcom2.Items.AddRange(con.cargardatos(sql));
        Consultar.Visible = true;
        Ingreso.Visible = false;
        ResultadoUsuario.Visible = false;
        ResultadoComite.Visible = false;
        Roles.Visible = false;
        Miembros.Visible = false;
    }

    /*Metodo que llama a cargarTabla2 el cual muestra la información de un usuario*/
    protected void Buscar_usuario(object sender, EventArgs e)
    {
        ResultadoUsuario.Visible = true;
        CargarUsuario();
    }
    private void Ejecutar(string texto, string sql){
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono"))
        {
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }
    protected void BuscarComite(object sender, EventArgs e)
    {
        Miembros.Visible = true;
        CargarMiembros();
    }   
    protected void AgregarComite(object sender, EventArgs e)
    {
        string sql = "", texto = "Usuario agregar correctamente al comite";
        sql = "update  profesor set com_codigo='" + DDLcom.Items[DDLcom.SelectedIndex].Value.ToString() + "' where usu_username='" + TBcodigo.Text + "'";
        Ejecutar(texto, sql);

        Roles.Visible = false;
        RevisarExiste();
    }

    /*Valida si el usuario ya esta en  la tabla usuario_rol*/
    private void RevisarExiste()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string sql = "SELECT USU_USERNAME FROM USUARIO_ROL WHERE ROL_ID ='COM' and USU_USERNAME='"+ TBcodigo.Text + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (!drc1.HasRows){
                sql = "insert into USUARIO_ROL (USUROL_ID,USU_USERNAME,ROL_ID) VALUES(USUARIOID.nextval,'" + TBcodigo.Text + "','COM')";
                Ejecutar("", sql);
            }
            drc1.Close();
        }
        CargarComite();
    }

    /*Metodo que sirven para la consulta del usuario*/
    protected void GVusuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVusuario.PageIndex = e.NewPageIndex;
        CargarUsuario();
    }
    protected void GVusuario_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void CargarUsuario()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as usuario from usuario u, profesor p where p.usu_username = '" + TBcodigo.Text + "' and u.usu_estado = 'ACTIVO' and u.USU_USERNAME = p.USU_USERNAME";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVusuario.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "";
                    if (cantfilas > 0)
                    {
                        Roles.Visible = true;
                        DDLcom.Items.Clear();
                        string sql2 = "SELECT COM_CODIGO, COM_NOMBRE FROM COMITE WHERE COM_ESTADO='ACTIVO'";
                        DDLcom.Items.AddRange(con.cargardatos(sql2));
                        CargarComite();
                        
                    }else{
                        ResultadoComite.Visible = false;
                       
                    }
                }
                GVusuario.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
   
    /*Metodos que sirven para la consulta de integrantes por comite*/
    protected void GVmiembros_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVmiembros.PageIndex = e.NewPageIndex;
        CargarMiembros();
    }
    protected void GVmiembros_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void CargarMiembros()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) { 
                string  sql = "Select u.usu_username, CONCAT(CONCAT(usu_nombre, ' '), usu_apellido) as miembros from profesor p, usuario u where p.usu_username = u.usu_username and p.com_codigo='" + DDLcom2.Items[DDLcom2.SelectedIndex].Value.ToString() + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVmiembros.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVmiembros.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVmiembros_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string id = GVmiembros.Rows[e.RowIndex].Cells[0].Text;
            string sql = "update profesor set com_codigo=null where usu_username='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                CargarMiembros();
                sql = "Delete from usuario_rol where USU_USERNAME='" + id + "' AND ROL_ID='COM'";
                Ejecutar("", sql);
            }
        }
    }

    /*Metodos que sirven para saber si el usuario tiene un comite*/
    protected void GVcomite_RowDataBound(object sender, GridViewRowEventArgs e) {}
    public void CargarComite()
    {
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "Select C.COM_NOMBRE from COMITE C, PROFESOR P where C.COM_CODIGO=P.COM_CODIGO and p.usu_username='"+TBcodigo.Text+"'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVcomite.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    
                    if (cantfilas > 0)
                    {
                        ResultadoComite.Visible = true;
                        Linfo.ForeColor = System.Drawing.Color.Red;
                        Linfo.Text = "Este usuario ya se encuentra en un comité, para agregarlo a un nuevo comité deberá ser eliminado del comité al que pertenece actualmente.";
                        Roles.Visible = false;                       
                  
                    } else {
                        Roles.Visible = true;
                        ResultadoComite.Visible = false;
                    }
                }

                GVcomite.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

}



