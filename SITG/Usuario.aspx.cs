using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Estudiante : Conexion
{
    Conexion con = new Conexion();
    string sql = "", texto = "";

    protected void Page_Load(object sender, EventArgs e){
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
    }

    //METODOS DE CREAR, MODIFICAR, CONSULTAR, INHABILITAR ME MANEJAN QUE SE HACE VISIBLE EN EL  FRONTED
    protected void Crear(object sender, EventArgs e){
        Ingreso.Visible = true;
        ConsultarUsuario.Visible = false;
        Resultado.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";

    }
    protected void Consultar(object sender, EventArgs e){
        ConsultarUsuario.Visible = true;
        Botones.Visible = false;
        Ingreso.Visible = false;
       
    }
    //METODO ACEPTAR(GUARDAR) REALIZA LAS OPERACIONES DE CREAR, MODIFICAR, INHABILITAR
    protected void Aceptar(object sender, EventArgs e){ 
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        if (Ingreso.Visible)
        {
            int cod;
            if (string.IsNullOrEmpty(TBcodigo.Text) == true || string.IsNullOrEmpty(TBcontra.Text) == true || string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBapellido.Text) == true || string.IsNullOrEmpty(TBtelefono.Text) == true || string.IsNullOrEmpty(TBdireccion.Text) == true || string.IsNullOrEmpty(TBcorreo.Text) == true)
            {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }
            else
            {
                string rol = DDLrol.Items[DDLrol.SelectedIndex].Value.ToString();
                string pass = con.GetMD5(TBcontra.Text);
                cod = Int32.Parse(TBcodigo.Text);
                sql = "insert into USUARIO (USU_USERNAME,USU_CONTRASENA,USU_NOMBRE,USU_APELLIDO,USU_TELEFONO,USU_DIRECCION,USU_CORREO,USU_FCREACION, USU_FMODIFICACION) VALUES('" + TBcodigo.Text + "', '" + pass + "', '" + TBnombre.Text + "', '" + TBapellido.Text + "', '" + TBtelefono.Text + "', '" + TBdireccion.Text + "', '" + TBcorreo.Text + "', TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'))";
                texto = "Datos guardados satisfactoriamente";
                Ejecutar(texto, sql);

                if (rol.Equals("NULL"))
                {
                    Linfo.ForeColor = System.Drawing.Color.Green;
                    Linfo.Text += "Datos guardados satisfactoriamente";
                }
                else
                {
                    sql = "insert into USUARIO_ROL (USUROL_ID,USU_USERNAME,ROL_ID) VALUES (USUARIOID.nextval,'" + cod + "','" + rol + "')";
                    texto = "Datos guardados satisfactoriamente";
                    Ejecutar(texto, sql);
                    string sql2 = "";
                    if (rol.Equals("EST"))
                    {
                        sql2 = "insert into ESTUDIANTE (EST_SEMESTRE, USU_USERNAME, PROG_CODIGO) VALUES ('" + TBsemestre.Text + "','" + cod + "', '" + DDLprograma.Items[DDLprograma.SelectedIndex].Value.ToString() + "' )";
                        texto = "Datos guardados satisfactoriamente";
                        Ejecutar(texto, sql2);
                        DDLprograma.SelectedIndex = 0;
                    }
                    else if (rol.Equals("DOC"))
                    {
                        sql2 = "insert into PROFESOR (USU_USERNAME) VALUES ('" + cod + "' )";
                        texto = "Datos guardados satisfactoriamente";
                        Ejecutar(texto, sql2);
                    }
                }
                DDLrol.SelectedIndex = 0;
            }
        }
    }
    protected void DDLrol_SelectedIndexChanged(object sender, EventArgs e)/*evento del ddl para cuando selecciona un item*/
    {
        string rol = DDLrol.Items[DDLrol.SelectedIndex].Value.ToString();
        if (rol.Equals("EST"))
        {
            Testudiante.Visible = true;
            string sql = "SELECT PROG_CODIGO, PROG_NOMBRE FROM PROGRAMA WHERE PROG_ESTADO='ACTIVO'";
            DDLprograma.Items.AddRange(con.cargardatos(sql));
        } else if (rol.Equals("NULL")){
            Testudiante.Visible = false;
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
        borrardatos();
    }
    //EVENTO DEL BOTON ACEPTAR 
    protected void Limpiar(object sender, EventArgs e){
        Linfo.Text = "";
        borrardatos();
    }
    private void borrardatos()/*limpia los campos*/
    {
        TBcodigo.Text = "";
        TBcontra.Text = "";
        TBnombre.Text = "";
        TBapellido.Text = "";
        TBtelefono.Text = "";
        TBdireccion.Text = "";
        TBcorreo.Text = "";
        Testudiante.Visible = false;
        DDLrol.SelectedIndex = 0;
       
    }
    //METODOS QUE REALIZAN LAS OPERACIONES DE CONSULTA   




    //////////////////////////////////////////////////////////////////////////////////////////////////
    /*Metodos para la consulta de las observaciones*/
    protected void GVusuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVusuarios.PageIndex = e.NewPageIndex;
        LlamarTablaRoles();
    }
    protected void GVusuarios_RowDataBound(object sender, GridViewRowEventArgs e) { }

    protected void LlamarTablaRoles()/*Tabla para la consulta*/
    {

        string sql = "";
      
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {

                if (DDLconsulta.Items[DDLconsulta.SelectedIndex].Value.ToString().Equals("TODOS"))
                {
                    sql = "SELECT * FROM USUARIO";
                }
                else
                {

                    sql = "SELECT U.USU_USERNAME, U.USU_NOMBRE, U.USU_APELLIDO, U.USU_TELEFONO, U.USU_DIRECCION, U.USU_CORREO, U.USU_ESTADO FROM USUARIO U, USUARIO_ROL UR WHERE U.USU_USERNAME=UR.USU_USERNAME AND UR.ROL_ID='" + DDLconsulta.Items[DDLconsulta.SelectedIndex].Value.ToString() + "'";

                }
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVusuarios.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVusuarios.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }

    }


    /*Metodos que sirven para el modificar-eliminar de la tabla observaciones*/
    protected void GVusuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        GridViewRow row = (GridViewRow)GVusuarios.Rows[e.RowIndex];
        if (conn != null)
        {
            DropDownList combo = GVusuarios.Rows[e.RowIndex].FindControl("estado") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox codigo = (TextBox)GVusuarios.Rows[e.RowIndex].Cells[0].Controls[0];
            TextBox nombre = (TextBox)GVusuarios.Rows[e.RowIndex].Cells[1].Controls[0];
            TextBox apellido = (TextBox)GVusuarios.Rows[e.RowIndex].Cells[2].Controls[0];
            TextBox telefono = (TextBox)GVusuarios.Rows[e.RowIndex].Cells[3].Controls[0];
            TextBox direccion = (TextBox)GVusuarios.Rows[e.RowIndex].Cells[4].Controls[0];

            string sql = "update usuario set usu_nombre = '" + nombre.Text + "', usu_apellido='"+apellido.Text+"', usu_telefono='"+telefono.Text+"', usu_direccion='"+direccion.Text+"', usu_estado='"+estado+"' where  usu_username ='" + codigo.Text + "'";
            Linfo.Text = sql;

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVusuarios.EditIndex = -1;
                LlamarTablaRoles();
            }
        }
    }
    protected void GVusuarios_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVusuarios.EditIndex = e.NewEditIndex;
        LlamarTablaRoles();
        GVusuarios.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVusuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVusuarios.EditIndex = -1;
        LlamarTablaRoles();
    }

    protected void DDLconsulta_SelectedIndexChanged(object sender, EventArgs e)/*evento del ddl para cuando selecciona un item*/
    {
        Resultado.Visible = true;
        LlamarTablaRoles();
    }



}