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

    protected void Page_Load(object sender, EventArgs e) {
       if (Session["Usuario"] == null){
                Response.Redirect("Default.aspx");
       }
        RevisarExiste();
        RevisarRechaza();
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

                }else{
                    LBSubir_propuesta.Enabled = false;
                    LBSubir_propuesta.ForeColor = System.Drawing.Color.Gray;
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

    /*Metodos que manejar la interfaz del subir-consultar*/
    protected void Subir_propuesta(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Consulta.Visible = false;
        Observaciones.Visible = false;      
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
        Linfo.Text = "";
        BuscarPropuesta();      
    }

    /*Metodos que realizan la funcionalidad del subir propuesta*/
    protected void Limpiar(object sender, EventArgs e)
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
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select PROP_NOMARCHIVO, PROP_DOCUMENTO, PROP_TIPO FROM PROPUESTA WHERE PROP_CODIGO="+id+"";

        OracleConnection conn = crearConexion();
        if (conn != null)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader())
                {
                    drc1.Read();

                    contentype = drc1["PROP_TIPO"].ToString();
                    fileName = drc1["PROP_NOMARCHIVO"].ToString();
                    bytes = (byte[])drc1["PROP_DOCUMENTO"];

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    Response.ContentType = contentype;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                }
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
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
}