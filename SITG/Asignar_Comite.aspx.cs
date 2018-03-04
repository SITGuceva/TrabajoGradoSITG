using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

public partial class Asignar_Comite : Conexion
{
    Conexion con = new Conexion();


    protected void Page_Load(object sender, EventArgs e)
    {

        Ingreso.Visible = true;
        Roles.Visible = false;

    }


    /*Metodo que llama a la interfaz asignar comite*/
    protected void LIAsignar(object sender, EventArgs e)
    {
        Linfo.Visible = false;
        Ingreso.Visible = true;
        Consultar.Visible = false;
        Miembros.Visible = false;

    }
    /*Metodo que llama a la interfaz consultar comite*/
    protected void LIConsultar(object sender, EventArgs e)
    {
        Linfo.Visible = false;
        string sql = "SELECT COM_CODIGO, COM_NOMBRE FROM COMITE WHERE COM_ESTADO='ACTIVO'";
        DDLcom2.Items.AddRange(con.cargardatos(sql));
        Consultar.Visible = true;
        Ingreso.Visible = false;

    }


    /*Metodo que llama a cargarTabla2 el cual muestra la información de un usuario*/
    protected void Buscar_usuario(object sender, EventArgs e)
    {

        Resultado.Visible = false;
        Resultado2.Visible = true;
        cargarTabla2();


    }


    /*Metodo que llama a cargarTabla3 el cual muestra los miembros de un comite*/
    protected void BuscarComite(object sender, EventArgs e)
    {
        Ingreso.Visible = false;
        Miembros.Visible = true;
        cargarTabla3();


    }

    /*Metodo que recibe dos parametros para ingresar en la base de datos*/
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono"))
        {
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
            Linfo.Visible = true;
      
            Resultado2.Visible = true;
            cargarTabla2();
        }
        else
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }

    }

    /*evento que cambia la pagina de la tabla*/
    protected void gvComites_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComites.PageIndex = e.NewPageIndex;
        cargarTabla();// la consulta a la base de datos
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvComites_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }


    /*Esta tabla consulta si un profesor ya tiene comite*/
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
                sql = "Select P.USU_USERNAME, C.COM_NOMBRE from COMITE C, PROFESOR P where C.COM_CODIGO=P.COM_CODIGO and p.usu_username='" + TBcodigo.Text + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvComites.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    
                    if (cantfilas > 0)
                    {
                        Linfo.Text = "Este usuario ya tiene un comite asignado, para agregarlo a un nuevo comite este usuario debe ser eliminado del comite al que pertenece actualmente";
                        Linfo.Visible = true;
                        Roles.Visible = false;
                        Resultado.Visible = false;
                        Resultado2.Visible = false;
                    }
                    else
                    {
                        Roles.Visible = true;
                        Resultado.Visible = true;

                    }
                }

                gvComites.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*evento que cambia la pagina de la tabla*/
    protected void gvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvComites.PageIndex = e.NewPageIndex;
        cargarTabla();// la consulta a la base de datos
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }

    /*Esta tabla consulta los datos de un profesor*/
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
                sql = "select CONCAT(CONCAT(usu_nombre, ' '), usu_apellido) as usuario from usuario where usu_username = '" + TBcodigo.Text + "' and  usu_estado='ACTIVO' ";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvUsuario.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                   

                    if (cantfilas > 0)
                    {

                        Resultado.Visible = true;
                        cargarTabla();
                        string sql2 = "SELECT COM_CODIGO, COM_NOMBRE FROM COMITE WHERE COM_ESTADO='ACTIVO'";
                        DDLcom.Items.AddRange(con.cargardatos(sql2));

                    }
                    else
                    {
                        Linfo.Visible = false;
                    }
                }
                

                gvUsuario.DataBind();
               

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }



    /*evento que cambia la pagina de la tabla*/
    protected void gvMiembros_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvMiembros.PageIndex = e.NewPageIndex;
        cargarTabla3();// la consulta a la base de datos

    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvMiembros_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    /*Esta tabla consulta los miembros de un comite*/
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
                sql = "Select u.usu_username, CONCAT(CONCAT(usu_nombre, ' '), usu_apellido) as miembros from profesor p, usuario u where p.usu_username = u.usu_username and p.com_codigo='"+ DDLcom2.Items[DDLcom2.SelectedIndex].Value.ToString() + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvMiembros.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }

                gvMiembros.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*Este metodo permite obtener el valor la primera celda del cargarTabla3 y lo asigna en un sql para modificarlo*/
    protected void gvMiembros_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string id = gvMiembros.Rows[e.RowIndex].Cells[0].Text;

            // Label lbldeleteID = (Label)gvSysRol.Rows[e.RowIndex].FindControl("lblstId");
            string sql = "update profesor set com_codigo=null where usu_username='" + id + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                Ingreso.Visible = false;
                cargarTabla3();
                // BindData();
            }
        }
    }


    /*Este metodo permite agregar un usuario a un comite*/
    protected void AgregarComite (object sender, EventArgs e)
    {

        string sql = "", texto = "Usuario agregar correctamente al comite";
        sql = "update  profesor set com_codigo='"+ DDLcom.Items[DDLcom.SelectedIndex].Value.ToString() + "' where usu_username='"+TBcodigo.Text+"'";
        Ejecutar(texto, sql);

    }
}




