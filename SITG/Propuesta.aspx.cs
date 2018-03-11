using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;

public partial class Propuesta : Conexion
{
    Conexion con = new Conexion();
    System.Data.DataTable table;
    System.Data.DataRow row;
    string codprop;
    int prop_codigo;

    protected void Page_Load(object sender, EventArgs e) {
       if (Session["Usuario"] == null){
                Response.Redirect("Default.aspx");
       }
        RevisarExiste();
        RevisarRechaza();
        SolicitudHecha();

        string sql = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido)from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
        DDLlista.Items.AddRange(con.cargardatos(sql));

        if (!Page.IsPostBack){
            table = new System.Data.DataTable();
            table.Columns.Add("CODIGO", typeof(System.String));
            table.Columns.Add("INTEGRANTES", typeof(System.String));
            Session.Add("Tabla", table);
        }
    }
  
    /*Metodos de consulta que se necesitan hacer antes de cargar la pagina*/
    private void RevisarExiste(){
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "SELECT PROP_CODIGO FROM ESTUDIANTE WHERE USU_USERNAME ='" + Session["id"] + "' and PROP_CODIGO!=0";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){
                if (drc1.IsDBNull(0)){
                    LBSubir_propuesta.Enabled = true;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Black;

                    LBSolicitar.Enabled = false;
                    LBSolicitar.ForeColor = System.Drawing.Color.Gray;
                } else{
                    LBSubir_propuesta.Enabled = false;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Gray;

                    LBSolicitar.Enabled = true;
                    LBSolicitar.ForeColor = System.Drawing.Color.Black;
                    prop_codigo = drc1.GetInt32(0);
                }
            }
            drc1.Close();
        }
    }
    private void RevisarRechaza(){
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT p.PROP_ESTADO, p.PROP_CODIGO FROM ESTUDIANTE e, PROPUESTA p WHERE e.USU_USERNAME='"+ Session["id"] + "' and p.PROP_CODIGO = e.PROP_CODIGO";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                string estado=drc1.GetString(0).ToString();    
                codprop= drc1.GetInt32(1).ToString();

                if (estado.Equals("Rechazado")){
                    LBSubir_propuesta.Enabled = true;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Black;
                    Metodo.Value = "ACTUALIZA";
                }
                else {
                    LBSubir_propuesta.Enabled = false;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Gray;
                }                
            }
            drc1.Close();
        }
    }

    /*Metodo para saber si ya se tiene una solicitud y depende del estado se habilita la opcion*/
    private void SolicitudHecha()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string sql = "SELECT SOL_ESTADO FROM SOLICITUD_DIR WHERE PROP_CODIGO ='" + prop_codigo + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows) {
                if (drc1.IsDBNull(0)){
                    LBSolicitar.Enabled = true;
                    LBSolicitar.ForeColor = System.Drawing.Color.Black;
                }else {
                    string estado = drc1.GetString(0);
                    if (estado.Equals("Rechazado")){
                        LBSolicitar.Enabled = true;
                        LBSolicitar.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        LBSolicitar.Enabled = false;
                        LBSolicitar.ForeColor = System.Drawing.Color.Gray;
                    }
                }
            }
            drc1.Close();
        }
    }

    /*Evento que envia la solicitud*/
    protected void SolicitarDir(object sender, EventArgs e)
    {
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string sql = "insert into solicitud_dir (SOL_ID, SOL_FECHA, SOL_ESTADO, PROP_CODIGO, USU_USERNAME) values(SOLICITUDID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), 'Pendiente', '" + prop_codigo + "','" + DDLlista.Items[DDLlista.SelectedIndex].Value + "')";
        string texto = "Solicitud realizada correctamente";
        Ejecutar(texto, sql);
        Solicitar.Visible = false;
    }

    /*Metodos que manejar la interfaz del subir-consultar- solicitar dir- consultar dir*/
    protected void Subir_propuesta(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Consulta.Visible = false;
        Observaciones.Visible = false;
        Solicitar.Visible = false;
        ConsultaSolicitud.Visible = false;
        Linfo.Text = "";
        DDLlprof.Items.Clear();
        string sql = "SELECT LPROF_CODIGO, LPROF_NOMBRE FROM LIN_PROFUNDIZACION WHERE LPROF_ESTADO='ACTIVO' AND PROG_CODIGO IN" +
            " ( SELECT E.PROG_CODIGO FROM PROGRAMA P, ESTUDIANTE E WHERE P.PROG_CODIGO = E.PROG_CODIGO AND E.USU_USERNAME = '" + Session["id"] + "')";
        DDLlprof.Items.AddRange(con.cargardatos(sql));
        DDLlprof.Items.Insert(0, "Seleccione");
        DDLtema.Items.Insert(0, "Seleccione");
    }
    protected void Consulta_propuesta(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        Ingreso.Visible = false;
        Observaciones.Visible = false;
        Solicitar.Visible = false;
        ConsultaSolicitud.Visible = false;
        Linfo.Text = "";
        BuscarPropuesta(); 
    }
    protected void LBSolicitar_Click(object sender, EventArgs e)
    {
        Solicitar.Visible = true;
        ConsultaSolicitud.Visible = false;
        Consulta.Visible = false;
        Ingreso.Visible = false;
        Observaciones.Visible = false;
        Linfo.Text = "";
        DDLlista.Items.Clear();
        string sql = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido)from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
        DDLlista.Items.AddRange(con.cargardatos(sql));
    }
    protected void Consultar(object sender, EventArgs e) {
         Consulta.Visible = false;
         Ingreso.Visible = false;
         Observaciones.Visible = false;
         Solicitar.Visible = false;
         ConsultaSolicitud.Visible = true;
         cargarTabla();
         Linfo.Text="";
     }

    /*Evento del boton limpiar - cancelar*/
    protected void Cancelar(object sender, EventArgs e)
    {      
        Linfo.Text = "";
        TAnombre.Value = "";
        TAjusti.Value = "";
        TAobj.Value = "";
        TAblib.Value = "";
        RespInte.Visible = false;
        TBcodint.Text = "";
        Bintegrante.Enabled = true;
        DDLlprof.SelectedIndex = 0;
        DDLtema.SelectedIndex = 0;
        TBcodint.Enabled = true;
        // table.Clear();
    }
    protected void Limpiar(object sender, EventArgs e)
    {
        GVinfprof.Visible = false;
        Linfo.Text = "";
        Tbotones2.Visible = false;
        DDLlista.Enabled = true;
        Bconsulta.Enabled = true;
        DDLlista.Items.Clear();
        string sql = "Select u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido)from profesor p, usuario u where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO'";
        DDLlista.Items.AddRange(con.cargardatos(sql));
        table = (System.Data.DataTable)(Session["Tabla"]);
        table.Clear();
    }

    /*Metodos que realizan la funcionalidad del subir propuesta*/
    protected void Guardar(object sender, EventArgs e)
    {
        string fecha;
        if (Metodo.Value.Equals("ACTUALIZA"))
        {
            fecha = DateTime.Now.ToString("yyyy/MM/dd");
            OracleConnection conn = crearConexion();
            if (conn != null){
                string sql = "update  propuesta set prop_titulo='"+TAnombre.Value+"' ,prop_fecha=TO_DATE( '" + fecha + "', 'YYYY-MM-DD'),prop_estado= 'Pendiente',prop_justificacion='"+TAjusti.Value+ "',prop_objetivos= '"+TAobj.Value+ "', prop_bibliografia='"+ TAblib.Value + "' where prop_codigo='"+ codprop + "'";

            }
        }else{
            table = (System.Data.DataTable)(Session["Tabla"]);
            DataRow[] currentRows = table.Select(null, null, DataViewRowState.CurrentRows);
            fecha = DateTime.Now.ToString("yyyy/MM/dd");
            OracleConnection conn = crearConexion();
            if (conn != null){
                string sql = "insert into propuesta (prop_codigo,prop_titulo,prop_justificacion, prop_objetivos, prop_bibliografia,tem_codigo, prop_fecha)values (propuestaid.nextval ,'"+TAnombre.Value+"' ,'"+TAjusti.Value+"', '"+TAobj.Value+"', '"+TAblib.Value+ "','" + DDLtema.Items[DDLtema.SelectedIndex].Value.ToString() + "', TO_DATE( '" + fecha + "', 'YYYY-MM-DD'))";
                Ejecutar("1", sql);
                if (Verificador.Value.Equals("Funciono")){
                    sql = "UPDATE ESTUDIANTE SET PROP_CODIGO = propuestaid.currval  WHERE USU_USERNAME = '" + Session["id"] + "'";
                    string texto = "Se subio la propuesta correctamente";
                    Ejecutar(texto, sql);

                    foreach (DataRow row in currentRows){
                        sql = "UPDATE ESTUDIANTE SET PROP_CODIGO=propuestaid.currval WHERE USU_USERNAME ='"+ row["CODIGO"] + "'";
                        texto = "Se subio la propuesta correctamente";
                        Ejecutar(texto, sql);
                    }
                    table.Rows.Clear();
                    GVagreinte.DataSource = table;
                    GVagreinte.Visible = false;
                }          
            }
        }
    }
    private void Ejecutar(string texto, string sql){
      string info = con.IngresarBD(sql);
      if (info.Equals("Funciono")){
            if (texto.Equals("1")) {
                Verificador.Value = "Funciono";
            }else{
                Linfo.ForeColor = System.Drawing.Color.Green;
                Linfo.Text = texto;
            } 
      }else{
         Linfo.ForeColor = System.Drawing.Color.Red;
         Linfo.Text = info;
      }
    }
    protected void AgregarIntegrante(object sender, EventArgs e)
    {
        String user = Session["id"].ToString();
        if (TBcodint.Text.Equals(user)){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "El usuario no se puede agregar porque es ";
            TBcodint.Text = "";
            Bintegrante.Visible = true;
            TBcodint.Enabled = true;
            RespInte.Visible = false;
            Bnueva.Visible = false;
        } else{
            table = (System.Data.DataTable)(Session["Tabla"]);
            row = table.NewRow();
            row["CODIGO"] = TBcodint.Text;
            row["INTEGRANTES"] = Rnombre.Text;

            table.Rows.Add(row);
            GVagreinte.DataSource = table;
            GVagreinte.DataBind();
        }  
    }
    protected void Bintegrante_Click(object sender, EventArgs e)
    {
        if (Bintegrante.Visible){
            if(string.IsNullOrEmpty(TBcodint.Text) == true) {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Ingrese codigo del estudiante!!";
            } else{
                string sql = "SELECT CONCAT(CONCAT(u.usu_apellido, ' '), u.usu_nombre) as integrantes  FROM USUARIO u, ESTUDIANTE e WHERE e.USU_USERNAME= '" + TBcodint.Text + "' and u.USU_USERNAME = e.USU_USERNAME ";
                List<string> list = con.consulta(sql, 1, 0);
                if (list.Count == 0){
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Codigo del estudiante no existe!!";
                    TBcodint.Text = "";
                    Bintegrante.Enabled = true;
                }else{                
                    Rnombre.Text =  list[0];
                    RespInte.Visible = true;
                    Bintegrante.Visible = false;
                    TBcodint.Enabled = false;
                    Bnueva.Visible = true;
                }
            }
        }else if (Bnueva.Visible){
            Rnombre.Text = "";
            RespInte.Visible = false;
            Bintegrante.Visible = true;
            Bnueva.Visible = false;
            TBcodint.Text = "";
            TBcodint.Enabled = true;
        }
    }
    protected void DDLlprof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLlprof.SelectedIndex.Equals(0)){
            DDLtema.Items.Clear();
            DDLtema.Items.Insert(0, "Seleccione");
        }else{
            DDLtema.Items.Clear();
            string sql = "SELECT TEM_CODIGO, TEM_NOMBRE FROM TEMA WHERE TEM_ESTADO='ACTIVO' AND LPROF_CODIGO='" + DDLlprof.Items[DDLlprof.SelectedIndex].Value.ToString() + "'";
            DDLtema.Items.AddRange(con.cargardatos(sql));
            DDLtema.Items.Insert(0, "Seleccione");
        }
    }

    /*Metodos que realizan la funcionalidad de consultar la propuesta*/
    protected void BuscarPropuesta()
    { 
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select p.PROP_CODIGO,p.PROP_TITULO, p.PROP_ESTADO, p.PROP_FECHA  from propuesta p, estudiante e where p.PROP_CODIGO = e.PROP_CODIGO and e.USU_USERNAME ='" + Session["id"] + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsulta.DataSource = dataTable;
                }
                GVconsulta.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVconsulta_RowCommand(object sender, GridViewCommandEventArgs e)
    {     
        if (e.CommandName == "buscar"){
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsulta.Rows[index];
            ResultadoObservaciones();
            Observaciones.Visible = true;
        }
    }

    /*Metodo para consultar las observaciones de la propuesta*/
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        ResultadoObservaciones();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void ResultadoObservaciones()
    {
        string sql = "";
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT DISTINCT O.OBS_CODIGO, O.OBS_DESCRIPCION, O.OBS_REALIZADA FROM OBSERVACION O, ESTUDIANTE E WHERE E.PROP_CODIGO=O.PROP_CODIGO AND E.USU_USERNAME='" + Session["id"] + "' ORDER BY O.OBS_CODIGO";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVobservacion.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*Metodo para consultar la propuesta completa*/
    protected void GVdocumento_RowDataBound(object sender, GridViewRowEventArgs e){}
    protected void Bpropuesta_Click(object sender, EventArgs e)
    {
        int id = int.Parse((sender as Button).CommandArgument);
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
               string sql = "select p.PROP_JUSTIFICACION, p.PROP_OBJETIVOS, l.LPROF_NOMBRE, t.TEM_NOMBRE  ,p.PROP_BIBLIOGRAFIA  from propuesta p, lin_profundizacion l, tema t " +
                    "where t.LPROF_CODIGO = l.LPROF_CODIGO and t.TEM_CODIGO = p.TEM_CODIGO  and p.PROP_CODIGO = '"+id+"'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVdocumento.DataSource = dataTable;                    
                }
                GVdocumento.DataBind();
            }
            conn.Close();
        } catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
        Documento.Visible = true;
        
    }

    /*Metodos que sirven para buscar la informacion del profesor*/
    protected void InfProfesor(object sender, EventArgs e)
    {
        DDLlista.Enabled = false;
        Bconsulta.Enabled = false;
        Tbotones2.Visible = true;
        CargarInfoProfesor();
        GVinfprof.Visible = true;
    }
    private void CargarInfoProfesor()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select usu_telefono, usu_direccion, usu_correo  from usuario  where usu_username='" + DDLlista.Items[DDLlista.SelectedIndex].Value + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVinfprof.DataSource = dataTable;
                }
                GVinfprof.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVinfprof_RowDataBound(object sender, GridViewRowEventArgs e) { }

    /*Metodos que sirven para la consulta de las solicitudes realizadas*/
    protected void GVsolicitud_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVsolicitud.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVsolicitud_RowDataBound(object sender, GridViewRowEventArgs e) { }
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
                sql = "select s.sol_id, s.sol_fecha, s.sol_estado, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director  from solicitud_dir s, usuario u where u.usu_username=s.usu_username and s.prop_codigo='" + prop_codigo + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVsolicitud.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVsolicitud.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

}