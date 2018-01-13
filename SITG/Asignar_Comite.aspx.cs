using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Asignar_Comite : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        string sql = "SELECT u.USU_USERNAME FROM USUARIO u, PROFESOR p WHERE u.USU_ESTADO='ACTIVO' and u.USU_USERNAME = p.USU_USERNAME and p.COM_CODIGO is NULL";
        CBLusuario.Items.AddRange(con.cargarDDLid(sql));
        string sql2 = "SELECT COM_CODIGO, COM_NOMBRE FROM COMITE WHERE COM_ESTADO='ACTIVO'";
        DDLcom.Items.AddRange(con.cargardatos(sql2));
    }

    /*Metodos de crear-modificar-consultar-inhabilitar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        CBLusuario.Items.Clear();
        string sql = "SELECT u.USU_USERNAME FROM USUARIO u, PROFESOR p WHERE u.USU_ESTADO='ACTIVO' and u.USU_USERNAME = p.USU_USERNAME and p.COM_CODIGO is NULL";
        CBLusuario.Items.AddRange(con.cargarDDLid(sql));
        DDLcom.Items.Clear();
        string sql2 = "SELECT COM_CODIGO, COM_NOMBRE FROM COMITE WHERE COM_ESTADO='ACTIVO'";
        DDLcom.Items.AddRange(con.cargardatos(sql2));
        Metodo.Value = "";
        Linfo.Text = "";
        Botones.Visible = true;
        Eliminar.Visible = false;
        Actualizar.Visible = false;
        Buscar.Visible = false;
        Resultado.Visible = false;
    }
    protected void Modificar(object sender, EventArgs e)
    {
        DDLcom2.Items.Clear();
        string sql2 = "SELECT COM_CODIGO, COM_NOMBRE FROM COMITE WHERE COM_ESTADO='ACTIVO'";
        DDLcom2.Items.AddRange(con.cargardatos(sql2));
        Linfo.Text = "";
        Buscar.Visible = true;
        Metodo.Value = "1";
        Botones.Visible = false;   
        Eliminar.Visible = false;
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Resultado.Visible = false;
    }
    protected void Consultar(object sender, EventArgs e)
    {
        DDLcom2.Items.Clear();
        string sql2 = "SELECT COM_CODIGO, COM_NOMBRE FROM COMITE WHERE COM_ESTADO='ACTIVO'";
        DDLcom2.Items.AddRange(con.cargardatos(sql2));
        Buscar.Visible = true;
        Metodo.Value = "2";
        Botones.Visible = false;
        Resultado.Visible = false;
        Eliminar.Visible = false;
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Linfo.Text = "";
    }
    protected void Inhabilitar(object sender, EventArgs e)
    {
        DDLcom2.Items.Clear();
        string sql2 = "SELECT COM_CODIGO, COM_NOMBRE FROM COMITE WHERE COM_ESTADO='ACTIVO'";
        DDLcom2.Items.AddRange(con.cargardatos(sql2));
        Buscar.Visible = true;
        Metodo.Value = "3";
        Botones.Visible = false;    
        Eliminar.Visible = false;
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Resultado.Visible = false;
        Linfo.Text = "";
    }

    /*Metodo que se utiliza para la busqueda*/
    protected void BuscarProfe(object sender, EventArgs e)
    {
        string codcom = DDLcom2.Items[DDLcom2.SelectedIndex].Value.ToString();
        if (Metodo.Value.Equals("1")){          
            DDLcom3.Items.Clear();
            string sql1 = "SELECT COM_CODIGO, COM_NOMBRE FROM COMITE WHERE COM_ESTADO = 'ACTIVO' and COM_CODIGO != '"+ codcom + "'";
            DDLcom3.Items.AddRange(con.cargardatos(sql1));

            CBLusuario2.Items.Clear();
            string sql2 = "SELECT u.USU_USERNAME FROM USUARIO u, PROFESOR p WHERE u.USU_ESTADO='ACTIVO' and u.USU_USERNAME = p.USU_USERNAME and p.COM_CODIGO='"+ codcom + "'";
            CBLusuario2.Items.AddRange(con.cargarDDLid(sql2));

            Actualizar.Visible = true;
            Botones.Visible = true;
        }else if (Metodo.Value.Equals("2")){
            cargarTabla();
            Resultado.Visible = true;
        }
        else if (Metodo.Value.Equals("3"))
        {
            CBLusuario3.Items.Clear();
            string sql1 = "SELECT u.USU_USERNAME FROM USUARIO u, PROFESOR p WHERE u.USU_ESTADO='ACTIVO' and u.USU_USERNAME = p.USU_USERNAME and p.COM_CODIGO='" + codcom + "'";
            CBLusuario3.Items.AddRange(con.cargarDDLid(sql1));
            Eliminar.Visible = true;
            Botones.Visible = true;
        }
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        if (!Ingreso.Visible){
            Botones.Visible = false;
            Actualizar.Visible = false;
        }
        else{
            Botones.Visible = true;
            DDLcom.Items.Clear();
            CBLusuario.Items.Clear();
           
        }
    }

    protected void hola()
    {
       //fgfgggkkk
    }
    /*Metodos que se utilizan para guardar-actualizar-inhabilitar*/
    protected void Aceptar(object sender, EventArgs e)
    {
        string sql = "",texto = "";
        int cant=0;
        if (Ingreso.Visible){
            for (int i = 0; i < CBLusuario.Items.Count; i++){
                if (CBLusuario.Items[i].Selected)
                {
                    sql = "UPDATE PROFESOR SET COM_CODIGO='" + DDLcom.Items[DDLcom.SelectedIndex].Value.ToString() + "'  WHERE USU_USERNAME='" + CBLusuario.Items[i].Text + "'";
                    texto = "Datos guardados satisfactoriamente";
                    Ejecutar(texto, sql);
                    cant++;
                }   
            }
            if (cant == 0){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Seleccione por lo menos un docente!!";
            }

        }else if (Actualizar.Visible){
            for (int i = 0; i < CBLusuario2.Items.Count; i++)
            {
                if (CBLusuario2.Items[i].Selected)
                {
                    sql = "UPDATE PROFESOR SET COM_CODIGO='" + DDLcom3.Items[DDLcom3.SelectedIndex].Value.ToString() + "'  WHERE USU_USERNAME='" + CBLusuario2.Items[i].Text + "'";
                    texto = "Datos guardados satisfactoriamente";
                    Ejecutar(texto, sql);
                    cant++;
                }
            }
            if (cant == 0)
            {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Seleccione por lo menos un docente!!";
            }
        } else if (Eliminar.Visible){
            for (int i = 0; i < CBLusuario3.Items.Count; i++)
            {
                if (CBLusuario3.Items[i].Selected)
                {
                    sql = "UPDATE PROFESOR SET COM_CODIGO= null  WHERE USU_USERNAME='" + CBLusuario3.Items[i].Text + "'";
                    texto = "Datos guardados satisfactoriamente";
                    Ejecutar(texto, sql);
                    cant++;
                }
            }
            if (cant == 0)
            {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Seleccione por lo menos un docente!!";
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
        
        
    }

    /*Metodos que se utilizan para la consulta*/
    protected void GVasigcom_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVasigcom.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVasigcom_RowDataBound(object sender, GridViewRowEventArgs e) { }
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
                sql = "SELECT p.USU_USERNAME, u.USU_NOMBRE, u.USU_APELLIDO FROM USUARIO u, PROFESOR p WHERE u.USU_USERNAME = p.USU_USERNAME AND p.COM_CODIGO='" + DDLcom2.Items[DDLcom2.SelectedIndex].Value.ToString() + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVasigcom.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVasigcom.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
}