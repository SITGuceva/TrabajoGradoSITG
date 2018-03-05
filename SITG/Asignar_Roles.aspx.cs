using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Asignar_Roles : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }      
    }

    /*Metodo que manejan el front-ed del crear-consultar*/
    protected void Crear(object sender, EventArgs e){
        CargarDDLRoles();
        Buscar.Visible = false;
        Ingreso.Visible = true;
        ResultadoUsuario.Visible = false;
        ResultadoUsuRol.Visible = false;
        ResultadoRoles.Visible = false;
        AgregarRol.Visible = false;
    }
    protected void Consultar(object sender, EventArgs e)
    {
        DDLroles2.Items.Clear();
        string sql = "SELECT ROL_ID, ROL_NOMBRE FROM ROL WHERE ROL_ESTADO='ACTIVO'";
        DDLroles2.Items.AddRange(con.cargardatos(sql));
        Ingreso.Visible = false;       
        Buscar.Visible = true;
        ResultadoRoles.Visible = false;
        ResultadoUsuario.Visible = false;
        ResultadoUsuRol.Visible = false;
        AgregarRol.Visible = false;
        Linfo.Text = "";
    }

    /*Metodos que se encargan de la asignacion del rol*/
    protected void CargarDDLRoles(){
        DDLroles.Items.Clear();
        string sql2 = "SELECT R.ROL_ID, R.ROL_NOMBRE FROM ROL R WHERE  R.ROL_ID NOT IN(SELECT U.ROL_ID FROM USUARIO_ROL U WHERE  U.USU_USERNAME = '" + TBcodigo.Text + "') and R.ROL_ESTADO='ACTIVO' ORDER BY R.ROL_ID";
        DDLroles.Items.AddRange(con.cargardatos(sql2));       
        if (DDLroles.Items.Count == 0){
            AgregarRol.Visible = false;
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "El usuario ya tiene todos los roles posibles";
        }else{
            AgregarRol.Visible = true;
            Linfo.Text = "";
        }
    }
    protected void InsertarRol(object sender, EventArgs e)
    {    
       string sql= "insert into USUARIO_ROL (USUROL_ID,USU_USERNAME,ROL_ID) VALUES(USUARIOID.nextval,'" + TBcodigo.Text + "','" + DDLroles.Items[DDLroles.SelectedIndex].Value.ToString() + "')";
       Ejecutar("Rol asignado correctamente", sql);
       CargarRoles();         
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono"))
        {
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
            CargarDDLRoles();
        }else {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }

    }
    protected void Nueva(object sender, EventArgs e)
    {
        Bconsultar.Visible = true;
        Bnueva.Visible = false;
        TBcodigo.Text = "";
        TBcodigo.Enabled = true;
        Linfo.Text = "";
        ResultadoUsuario.Visible = false;
        ResultadoRoles.Visible = false;
        AgregarRol.Visible = false;
    }

    /*Metodos que se utilizan para consultar los usuarios del rol*/
    protected void GVrolusuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVasigrol.PageIndex = e.NewPageIndex;
        CargarUsuRol();
    }
    protected void GVrolusuario_RowDataBound(object sender, GridViewRowEventArgs e) {}
    public void CargarUsuRol(){
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as Usuario, u.usu_estado from usuario_rol ur, rol r, usuario u where u.usu_username = ur.usu_username and r.rol_id = ur.rol_id and ur.rol_id='" + DDLroles2.Items[DDLroles2.SelectedIndex].Value.ToString() + "'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVrolusuario.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text= "Cantidad de filas encontradas: " + cantfilas;
                }
                GVrolusuario.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void ConsultarRoles(object sender, EventArgs e)
    {
        ResultadoUsuRol.Visible = true;
        CargarUsuRol();
    }

    /*Metodos que se utilizan para consultar la informacion del usuario*/
    protected void BuscarUsuario(object sender, EventArgs e)
    {
        CargarDatoUsuario();
        Bconsultar.Visible = false;
        Bnueva.Visible = true;
        TBcodigo.Enabled = false;
    }
    protected void GVnombre_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVnombre.PageIndex = e.NewPageIndex;
        CargarDatoUsuario();
    }  
    protected void GVnombre_RowDataBound(object sender, GridViewRowEventArgs e){}
    public void CargarDatoUsuario()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT CONCAT(CONCAT(usu_nombre, ' '), usu_apellido) as Usuario from usuario where usu_username='"+TBcodigo.Text+ "'  and usu_estado='ACTIVO'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVnombre.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    if (cantfilas == 0)
                    {
                        ResultadoUsuario.Visible = true;
                        ResultadoRoles.Visible = false;
                        AgregarRol.Visible = false;
                        Linfo.Text = "";
                    }else{
                        CargarDDLRoles();
                        ResultadoRoles.Visible = true;
                        CargarRoles();
                        ResultadoUsuario.Visible = true;
                    }
                }
               GVnombre.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
  
    /*Metodo que se utilizan para la consulta de roles del usuario y eliminarlos*/
    protected void GVasigrol_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string id = GVasigrol.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from usuario_rol where USUROL_ID='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                CargarRoles();
                CargarDDLRoles();
            }
        }
    }
    protected void GVasigrol_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVasigrol.PageIndex = e.NewPageIndex;
        CargarRoles();
    }
    protected void GVasigrol_RowDataBound(object sender, GridViewRowEventArgs e){}
    public void CargarRoles ()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                sql = "select  ur.usurol_id,u.usu_estado, r.rol_nombre from usuario_rol ur, rol r, usuario u where u.usu_username = ur.usu_username and r.rol_id = ur.rol_id and u.usu_username='" + TBcodigo.Text + "'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVasigrol.DataSource = dataTable;
                }
                GVasigrol.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

}