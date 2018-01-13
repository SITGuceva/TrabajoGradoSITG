using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Docente : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        string sql = "SELECT USU_USERNAME FROM USUARIO";
        DDLusuario.Items.AddRange(con.cargarDDLid(sql));       
        string sql2 = "SELECT ROL_ID, ROL_NOMBRE FROM ROL WHERE ROL_ESTADO='ACTIVO'";
        DDLrol.Items.AddRange(con.cargardatos(sql2));
    }

    /*Metodos de crear-modificar-consultar-inhabilitar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        DDLusuario.Items.Clear();
        string sql = "SELECT USU_USERNAME FROM USUARIO";
        DDLusuario.Items.AddRange(con.cargarDDLid(sql));
        DDLrol.Items.Clear();
        string sql2 = "SELECT ROL_ID, ROL_NOMBRE FROM ROL WHERE ROL_ESTADO='ACTIVO'";
        DDLrol.Items.AddRange(con.cargardatos(sql2));
        Metodo.Value = "";
        Linfo.Text="";
        Botones.Visible = true;
        Resultado.Visible = false;
        Eliminar.Visible = false;
        Actualizar.Visible = false;
        Tablebuscar.Visible = false;
        Tableinfo.Visible = false;
    }
    protected void Modificar(object sender, EventArgs e)
    {
        DDLcodigo2.Items.Clear();
        string sql2 = "SELECT UNIQUE u.USU_USERNAME FROM USUARIO u, USUARIO_ROL ur where ur.USU_USERNAME = u.USU_USERNAME";
        DDLcodigo2.Items.AddRange(con.cargarDDLid(sql2));
        Linfo.Text = "";
        Tablebuscar.Visible = true;
        Tableinfo.Visible = false;
        Metodo.Value = "1";
        Botones.Visible = false;
        Resultado.Visible = false;
        Eliminar.Visible = false;
        Ingreso.Visible = false;
        Actualizar.Visible = false;
    }
    protected void Consultar(object sender, EventArgs e)
    {
        DDLcodigo2.Items.Clear();
        string sql2 = "SELECT UNIQUE u.USU_USERNAME FROM USUARIO u, USUARIO_ROL ur where ur.USU_USERNAME = u.USU_USERNAME";
        DDLcodigo2.Items.AddRange(con.cargarDDLid(sql2));
        Tableinfo.Visible = false;
        Tablebuscar.Visible = true;
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
        DDLcodigo2.Items.Clear();
        string sql2 = "SELECT UNIQUE  u.USU_USERNAME FROM USUARIO u, USUARIO_ROL ur where ur.USU_USERNAME = u.USU_USERNAME";
        DDLcodigo2.Items.AddRange(con.cargarDDLid(sql2));
        Tableinfo.Visible = false;
        Tablebuscar.Visible = true;
        Metodo.Value = "3";
        Botones.Visible = false;
        Resultado.Visible = false;
        Eliminar.Visible = false;
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Linfo.Text = "";
    }
    
    /*Metodo que se utiliza para la busqueda*/
    protected void Buscar(object sender, EventArgs e)
    {
        string sql = "SELECT USU_NOMBRE, USU_APELLIDO FROM USUARIO WHERE USU_USERNAME='" + DDLcodigo2.Items[DDLcodigo2.SelectedIndex].Value.ToString() + "'";
        List<string> list = con.consulta(sql, 2, 0);
        Lansusu.Text = list[0] + " " + list[1];
        Tableinfo.Visible = true;

        if (Metodo.Value.Equals("1")){
            DDLrolactu.Items.Clear();
            string sql1 = "SELECT  UR.USUROL_ID,R.ROL_NOMBRE FROM  USUARIO_ROL UR, ROL R WHERE UR.ROL_ID = R.ROL_ID AND UR.USU_USERNAME = '"+ DDLcodigo2.Items[DDLcodigo2.SelectedIndex].Value.ToString() + "' ORDER BY UR.USUROL_ID";
            DDLrolactu.Items.AddRange(con.cargardatos(sql1));

            DDLroles.Items.Clear();
            string sql2 = "SELECT ROL_ID, ROL_NOMBRE FROM ROL WHERE ROL_ESTADO='ACTIVO'";
            DDLroles.Items.AddRange(con.cargardatos(sql2));

            Actualizar.Visible = true;
            Botones.Visible = true;
        }else if (Metodo.Value.Equals("2") ){
            cargarTabla();
            Resultado.Visible = true;
        }else if (Metodo.Value.Equals("3")){
            DDLrolelim.Items.Clear();
            string sql1 = "SELECT  UR.USUROL_ID,R.ROL_NOMBRE FROM  USUARIO_ROL UR, ROL R WHERE UR.ROL_ID = R.ROL_ID AND UR.USU_USERNAME = '" + DDLcodigo2.Items[DDLcodigo2.SelectedIndex].Value.ToString() + "' ORDER BY UR.USUROL_ID";
            DDLrolelim.Items.AddRange(con.cargardatos(sql1));
            Eliminar.Visible = true;
            Botones.Visible = true;
        }
    }
   
    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {        
        Linfo.Text = "";
        TBcodigo.Text = "";
        Tableinfo.Visible = false;
        Lansusu.Text = "";
        Eliminar.Visible = false;
        Actualizar.Visible = false;
        if (!Ingreso.Visible){
            Botones.Visible = false;
        }else{
            Botones.Visible = true;
        }
    }

    /*Metodos que se utilizan para guardar-actualizar-inhabilitar*/
    protected void Aceptar(object sender, EventArgs e){
        string sql = "";
        string texto = "";
        if (Ingreso.Visible){
            if (string.IsNullOrEmpty(TBcodigo.Text) == true){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else{
                sql = "insert into USUARIO_ROL (USUROL_ID,USU_USERNAME,ROL_ID) VALUES('" + TBcodigo.Text + "', '" + DDLusuario.Items[DDLusuario.SelectedIndex].Value.ToString() + "', '" + DDLrol.Items[DDLrol.SelectedIndex].Value.ToString() + "')";
                texto = "Datos guardados satisfactoriamente";
                Ejecutar(texto, sql);
            }
        }else if (Actualizar.Visible){
                sql = "UPDATE USUARIO_ROL SET ROL_ID='"+DDLroles.Items[DDLroles.SelectedIndex].Value.ToString()+"'  WHERE USUROL_ID='"+DDLrolactu.Items[DDLrolactu.SelectedIndex].Value.ToString()+"'";
                texto = "Datos modificados satisfactoriamente";
                Ejecutar(texto, sql);            
        } else if (Eliminar.Visible){
            sql = "UPDATE USUARIO_ROL SET USUROL_ESTADO = '" + DDLestado.Items[DDLestado.SelectedIndex].Value.ToString() + "' WHERE USUROL_ID = '" + DDLrolelim.Items[DDLrolelim.SelectedIndex].Value.ToString() + "'";
            texto = "EL rol del usuario se ha puesto "+ DDLestado.Items[DDLestado.SelectedIndex].Value.ToLower() + " satisfactoriamente";
            Ejecutar(texto, sql);
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
        TBcodigo.Text = "";  
    }

   /*Metodos que se utilizan para la consulta*/
    protected void GVasigrol_PageIndexChanging(object sender, GridViewPageEventArgs e){ 
        GVasigrol.PageIndex = e.NewPageIndex;
        cargarTabla();
    }  
    protected void GVasigrol_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla(){
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT UR.USUROL_ID, R.ROL_ID,R.ROL_NOMBRE, UR.USUROL_ESTADO FROM  USUARIO_ROL UR, ROL R WHERE UR.ROL_ID = R.ROL_ID  AND UR.USU_USERNAME = '"+ DDLcodigo2.Items[DDLcodigo2.SelectedIndex].Value.ToString() + "' ORDER BY UR.USUROL_ID";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVasigrol.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVasigrol.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

}