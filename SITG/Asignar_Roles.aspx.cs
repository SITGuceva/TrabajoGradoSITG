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
    string codigo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
       
        ConsultarR.Visible = false;


    }

    /*Metodo que llama a la interfaz de asignar rol*/
    protected void Crear(object sender, EventArgs e)
    {
        
        CargarDDLRoles();
        ConsultarR.Visible = false;
        Ingreso.Visible = true;
        Resultado.Visible = false;
        Resultado2.Visible = false;
        AgregarRol.Visible = false;

    }
    protected void CargarDDLRoles()
    {
        DDLroles.Items.Clear();
        string sql2 = "SELECT R.ROL_ID, R.ROL_NOMBRE FROM ROL R WHERE  R.ROL_ID NOT IN(SELECT U.ROL_ID FROM USUARIO_ROL U WHERE  U.USU_USERNAME = '" + TBcodigo.Text + "') ORDER BY R.ROL_ID";
        DDLroles.Items.AddRange(con.cargardatos(sql2));
        int i = DDLroles.Items.Count;
        if (i == 0)
        {
            AgregarRol.Visible = false;
            Linfo.Text = "El usuario ya tiene todos los roles posibles";
        }
        else{
            AgregarRol.Visible = true;
            Linfo.Text = "";
        }
    }

        /*Metodo que se encargar de llamar a la tabla resultado de las consultas en asignar-roles*/
        protected void Consultar(object sender, EventArgs e)
    {
        
  /**/
        
        ConsultarR.Visible = false;
        codigo = TBcodigo.Text;
        
        cargarTabla3();
        
       
    }

    /*Metodo que llama a la interfaz de consultar usuario-rol (consultar los miembros de un rol)*/

    protected void ConsultarR2(object sender, EventArgs e)
    {
        DDLroles2.Items.Clear();
        string sql = "SELECT ROL_ID, ROL_NOMBRE FROM ROL WHERE ROL_ESTADO='ACTIVO'";
        DDLroles2.Items.AddRange(con.cargardatos(sql));
        Ingreso.Visible = false;
       
        ConsultarR.Visible = true;
        Resultado2.Visible = false;
        Resultado.Visible = false;
        Resultado3.Visible = false;
        AgregarRol.Visible = false;
        Linfo.Text = "";
    }





    /*evento que cambia la pagina de la tabla*/
    protected void GVasigrol_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {


        GVasigrol.PageIndex = e.NewPageIndex;
        cargarTabla();// la consulta a la base de datos
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void GVasigrol_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        
    }

    /*Tabla que carga los roles que tiene un usuario*/
    public void cargarTabla()
    {
     
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select  ur.usurol_id,u.usu_estado, r.rol_nombre from usuario_rol ur, rol r, usuario u where u.usu_username = ur.usu_username and r.rol_id = ur.rol_id and u.usu_username='" + TBcodigo.Text + "' and u.usu_estado='ACTIVO'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVasigrol.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                   
                }

                GVasigrol.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }







    /*evento que cambia la pagina de la tabla*/
    protected void GVrolusuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {


        GVasigrol.PageIndex = e.NewPageIndex;
        cargarTabla2();// la consulta a la base de datos
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void GVrolusuario_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }

    /*Tabla que los usuarios que pertenecen a un rol en especifico puesto en la interfaz de consultar roles*/
    public void cargarTabla2()
    {

        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as Usuario, u.usu_estado from usuario_rol ur, rol r, usuario u where u.usu_username = ur.usu_username and r.rol_id = ur.rol_id and ur.rol_id='" + DDLroles2.Items[DDLroles2.SelectedIndex].Value.ToString() + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVrolusuario.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                 
                }

                GVrolusuario.DataBind();
                ConsultarR.Visible = true;

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }







    /*evento que cambia la pagina de la tabla*/
    protected void GVnombre_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {


        GVnombre.PageIndex = e.NewPageIndex;
        cargarTabla3();// la consulta a la base de datos
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void GVnombre_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }

    /*Tabla que carga el nombre del usuario consultado*/
    public void cargarTabla3()
    {

        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT CONCAT(CONCAT(usu_nombre, ' '), usu_apellido) as Usuario from usuario where usu_username='"+codigo+"'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVnombre.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    if (cantfilas == 0)
                    {
                        Resultado3.Visible = true;
                        Resultado.Visible = false;
                        Linfo.Text = "";

                    }
                    else
                    {
                        CargarDDLRoles();
                        Resultado3.Visible = true;
                        cargarTabla();
                        Resultado.Visible = true;
                    }

                }

                GVnombre.DataBind();
             

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }





    /*Metodo que se encarga de insertar el rol asignado a la base de datos*/
    protected void InsertarRol(object sender, EventArgs e)
    {

       


      OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select USU_USERNAME from usuario_rol where ROL_ID='"+ DDLroles.Items[DDLroles.SelectedIndex].Value.ToString()+"' and USU_USERNAME='" + TBcodigo.Text + "'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader drc1 = cmd.ExecuteReader();

                if (!drc1.HasRows)
                {
                    string sql2 = "", texto = "Rol asignado correctamente";
                    sql2 = "insert into USUARIO_ROL (USUROL_ID,USU_USERNAME,ROL_ID) VALUES(UROLID.nextval,'" + TBcodigo.Text + "','" + DDLroles.Items[DDLroles.SelectedIndex].Value.ToString() + "')";
                    Ejecutar(texto, sql2);
                    cargarTabla();
                }
                drc1.Close();
            }
        
    }

    /*Metodo que se llama para ejecutar el sql de insertar rol*/
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono"))
        {
           
           
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
            DDLroles.Items.Clear();
            CargarDDLRoles();




        }
        else
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }

    }

    /*Metodo que llama al resultado de cargartabla2*/

    protected void ConsultarRoles(object sender, EventArgs e)
    {
        Resultado2.Visible = true;
        cargarTabla2();


    }


    /*Este metodo permite eliminar una celda de una tabla*/
    protected void GVasigrol_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string id = GVasigrol.Rows[e.RowIndex].Cells[0].Text;

       
            string sql = "Delete from usuario_rol where USUROL_ID='" + id + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {

               

                cargarTabla();
                DDLroles.Items.Clear();
                CargarDDLRoles();

            }
        }
    }





}