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
        Resultado.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }
    protected void Modificar(object sender, EventArgs e){
        DDLcodigo2.Items.Clear();
        string sql2 = "SELECT USU_USERNAME FROM USUARIO";
        DDLcodigo2.Items.AddRange(con.cargarDDLid(sql2));
        Actualizar.Visible = true;
        Ingreso.Visible = false;
        Resultado.Visible = false;
        Eliminar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }
    protected void Consultar(object sender, EventArgs e){
        cargarTabla();
        Resultado.Visible = true;
        Botones.Visible = false;
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;  
    }
    protected void Inhabilitar(object sender, EventArgs e){
        DDLcodigo.Items.Clear();
        string sql1 = "SELECT USU_USERNAME FROM USUARIO";
        DDLcodigo.Items.AddRange(con.cargarDDLid(sql1));
        Eliminar.Visible = true;
        Ingreso.Visible = false;
        Resultado.Visible = false;
        Actualizar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }

    //METODO ACEPTAR(GUARDAR) REALIZA LAS OPERACIONES DE CREAR, MODIFICAR, INHABILITAR
    protected void Aceptar(object sender, EventArgs e){ 
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        if (Ingreso.Visible){
            guardar();
        }else if (Actualizar.Visible){
            if (string.IsNullOrEmpty(DDLcodigo2.Text) == true || string.IsNullOrEmpty(TBcontra2.Text) == true || string.IsNullOrEmpty(TBnombre2.Text) == true || string.IsNullOrEmpty(TBapellido2.Text) == true || string.IsNullOrEmpty(TBtelefono2.Text) == true || string.IsNullOrEmpty(TBdireccion2.Text) == true || string.IsNullOrEmpty(TBcorreo2.Text) == true){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else{
                string pass = con.GetMD5(TBcontra2.Text);
                sql = "UPDATE USUARIO SET USU_CONTRASENA = '" + pass + "', USU_NOMBRE ='" + TBnombre2.Text + "', USU_APELLIDO ='" + TBapellido2.Text + "', USU_TELEFONO ='" + TBtelefono2.Text + "', USU_DIRECCION ='" + TBdireccion2.Text + "', USU_CORREO ='" + TBcorreo2.Text + "', USU_FMODIFICACION = TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS')  WHERE USU_USERNAME = '" + DDLcodigo2.Items[DDLcodigo2.SelectedIndex].Value.ToString() + "'";
                texto = "Datos modificados satisfactoriamente";
                Ejecutar(texto, sql);
            }
        }else if (Eliminar.Visible){
             sql = "UPDATE USUARIO SET USU_ESTADO = '" + DDLestado.Items[DDLestado.SelectedIndex].Value.ToString() + "', USU_FMODIFICACION = TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS') WHERE USU_USERNAME = '" + DDLcodigo.Items[DDLcodigo.SelectedIndex].Value.ToString() + "'";
             texto = "Usuario se ha puesto "+ DDLestado.Items[DDLestado.SelectedIndex].Value.ToLower() + " satisfactoriamente";
             Ejecutar(texto, sql);
        }
     }    
    private void guardar()
    {
        int cod;
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        if (string.IsNullOrEmpty(TBcodigo.Text) == true || string.IsNullOrEmpty(TBcontra.Text) == true || string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBapellido.Text) == true || string.IsNullOrEmpty(TBtelefono.Text) == true || string.IsNullOrEmpty(TBdireccion.Text) == true || string.IsNullOrEmpty(TBcorreo.Text) == true) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Los campos son obligatorios";
        } else {
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
                if (rol.Equals("EST"))
                {
                    sql = "insert into ESTUDIANTE (EST_SEMESTRE, USU_USERNAME, PROG_CODIGO) VALUES ('" + TBsemestre.Text + "','" + cod + "', '"+ DDLprograma.Items[DDLprograma.SelectedIndex].Value.ToString() + "' )";
                    texto = "Datos guardados satisfactoriamente";
                    Ejecutar(texto, sql);
                    DDLprograma.SelectedIndex = 0;
                }
                else if (rol.Equals("DOC"))
                {
                    sql = "insert into PROFESOR (USU_USERNAME) VALUES ('" + cod + "' )";
                    texto = "Datos guardados satisfactoriamente";
                    Ejecutar(texto, sql);
                }
            }
            DDLrol.SelectedIndex = 0;
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
        TBcontra2.Text = "";
        TBnombre2.Text = "";
        TBapellido2.Text = "";
        TBtelefono2.Text = "";
        TBdireccion2.Text = "";
        TBcorreo2.Text = "";
        Testudiante.Visible = false;
        DDLrol.SelectedIndex = 0;
       
    }

    //METODOS QUE REALIZAN LAS OPERACIONES DE CONSULTA   
    protected void GVusuario_PageIndexChanging(object sender, GridViewPageEventArgs e)/*evento que cambia la pagina de la tabla*/
    {
        GVusuario.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVusuario_RowDataBound(object sender, GridViewRowEventArgs e) { } /*evento que se llama cuando llenga las columnas*/
    public void cargarTabla()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT USU_USERNAME,USU_NOMBRE,USU_APELLIDO,USU_TELEFONO,USU_DIRECCION,USU_CORREO,USU_FCREACION, USU_FMODIFICACION,USU_ESTADO  FROM USUARIO ";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVusuario.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
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


    protected void consultausuario(object sender, EventArgs e)
    {
     //////////////////////////////////////////PROBLEMAAAAAAS //////////////////////////////////  
        string sql = "SELECT USU_CONTRASENA,USU_NOMBRE,USU_APELLIDO, USU_DIRECCION,USU_CORREO, USU_TELEFONO FROM USUARIO WHERE USU_USERNAME='" + DDLcodigo2.Items[DDLcodigo2.SelectedIndex].Value.ToString() + "'";
        List<string> list = con.consulta(sql, 6, 1);
        TBcontra2.Text = list[0];
        TBnombre2.Text = list[1];
        TBapellido2.Text = list[2];
        TBdireccion2.Text = list[3];
        TBcorreo2.Text = list[4];
        TBtelefono2.Text = list[5];
        
    }
}