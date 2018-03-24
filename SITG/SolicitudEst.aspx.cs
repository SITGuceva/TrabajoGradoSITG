using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GDirEst : Conexion
{
    Conexion con = new Conexion();
    int prop_codigo;
    DataTable table;
    DataRow row;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null) {
            Response.Redirect("Default.aspx");
        }
        RevisarExiste();
        if (!Page.IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "SolicitudEst.aspx");
            if (valida.Equals("false")) {
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                table = new System.Data.DataTable();
                table.Columns.Add("CODIGO", typeof(System.String));
                table.Columns.Add("INTEGRANTES", typeof(System.String));
                Session.Add("Tabla", table);
            }
        }
    }

    /*Metodos de consulta que se necesitan hacer antes de cargar la pagina*/
    private void RevisarExiste()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "SELECT PROP_CODIGO FROM ESTUDIANTE WHERE USU_USERNAME ='" + Session["id"] + "' and PROP_CODIGO!=0";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){
                if (drc1.IsDBNull(0)){
                    LBSolicitar.Enabled = false;
                    LBSolicitar.ForeColor = System.Drawing.Color.Gray;
                }else{
                    LBSolicitar.Enabled = true;
                    LBSolicitar.ForeColor = System.Drawing.Color.Black;
                    prop_codigo = drc1.GetInt32(0);
                }
            }
            drc1.Close();
        }
    }

    /*Metodos que manejar la interfaz del solicitar - consultar*/
    protected void LBSolicitar_Click(object sender, EventArgs e)
    {
        Linfo.Text = "";
        TipoSolicitud.Visible = true;
        Integrante.Visible = false;
        Tbotones.Visible = true;
        Validar.Value = "";
        ConsultaSol.Visible = false;
    }
    protected void LBConsultar_Click(object sender, EventArgs e)
    {
        Linfo.Text = "";
        TipoSolicitud.Visible = false;
        Integrante.Visible = false;
        Tbotones.Visible = false;
        Validar.Value = "";
        ConsultaSol.Visible = true;
        CargarSolicitudes();
    }

    /*Evento que maneja el boton limpiar*/
    protected void Blimpiar_Click(object sender, EventArgs e)
    {
        Linfo.Text = "";
        limpiar();
    }
    private void limpiar()
    {
        TAdescrip.Value = "";
        TBcodint.Text = "";
        DDLsol.SelectedIndex = 0;
        Integrante.Visible = false;
        table = (System.Data.DataTable)(Session["Tabla"]);
        table.Clear();
        Validar.Value = "";
    }

    /*Metodos que realizan la funcionalidad del solicitar algun tipo de peticion*/
    protected void Bsolicitar_Click(object sender, EventArgs e)
    {
        table = (System.Data.DataTable)(Session["Tabla"]);
        DataRow[] currentRows = table.Select(null, null, DataViewRowState.CurrentRows);
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string sql="";

        if (DDLsol.SelectedIndex.Equals(0)){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe escoger algun tipo de peticion";
        }else if (string.IsNullOrEmpty(TAdescrip.Value) == true){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe escribir un motivo";
        } else if(DDLsol.SelectedIndex.Equals(3)){
            foreach (DataRow row in currentRows){
                sql = "insert into solicitud_est (sole_id, sole_fecha, sole_motivo,sole_tipo, prop_codigo, usu_username) " +
                "values (SOLIESTID.nextval, TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + TAdescrip.Value + "','" + DDLsol.Items[DDLsol.SelectedIndex].Text + "','" + prop_codigo + "','" + row["CODIGO"] + "')";
                Ejecutar("Solicitud realizada correctamente!!", sql);
            }
            table.Rows.Clear();
            GVagreinte.DataSource = table;
            GVagreinte.Visible = false;
        } else { 
            sql = "insert into solicitud_est (sole_id, sole_fecha, sole_motivo,sole_tipo, prop_codigo, usu_username) " +
                "values (SOLIESTID.nextval, TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'"+TAdescrip.Value+"','" + DDLsol.Items[DDLsol.SelectedIndex].Text + "','"+prop_codigo+"','"+ Session["id"] + "')";
            Ejecutar("Solicitud realizada correctamente!!", sql); 
        }

        limpiar();
    }
    private void Ejecutar(string texto, string sql){
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }
    protected void DDLsol_SelectedIndexChanged(object sender, EventArgs e)
    {
        Linfo.Text = "";
        if (DDLsol.SelectedIndex.Equals(3)){
            Integrante.Visible = true;
        }else{
            Integrante.Visible = false;
        }
    }
    protected void AgregarInt_Click(object sender, EventArgs e)
    {
        String user = Session["id"].ToString();
        if (TBcodint.Text.Equals(user)){
            borrar();
            Linfo.Text = "El usuario no se puede agregar porque es ";
        }else  if (Validar.Value.Equals("0")) { 
                table = (System.Data.DataTable)(Session["Tabla"]);
                row = table.NewRow();
                row["CODIGO"] = TBcodint.Text;
                row["INTEGRANTES"] = Rnombre.Text;

                table.Rows.Add(row);
                GVagreinte.DataSource = table;
                GVagreinte.DataBind();
        }else{
            borrar();
            Linfo.Text = "El estudiante no se puede agregar porque ya tiene una propuesta asignada!!";                
        }
    }
    private void borrar(){
        Linfo.ForeColor = System.Drawing.Color.Red;
        TBcodint.Text = "";
        Bintegrante.Visible = true;
        TBcodint.Enabled = true;
        RespInte.Visible = false;
        Bnueva.Visible = false;
        Validar.Value = "";
    }
    protected void Bintegrante_Click(object sender, EventArgs e)
    {
        if (Bintegrante.Visible){
            if (string.IsNullOrEmpty(TBcodint.Text) == true){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Ingrese codigo del estudiante!!";
            }else{
                string sql = "SELECT CONCAT(CONCAT(u.usu_apellido, ' '), u.usu_nombre) as integrantes,  e.PROP_CODIGO   FROM USUARIO u, ESTUDIANTE e WHERE e.USU_USERNAME= '" + TBcodint.Text + "' and u.USU_USERNAME = e.USU_USERNAME ";
                List<string> list = con.consulta(sql, 2, 1);
                if (list.Count == 0){
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Codigo del estudiante no existe!!";
                    TBcodint.Text = "";
                    Bintegrante.Enabled = true;
                }else {
                    Rnombre.Text = list[0];
                    RespInte.Visible = true;
                    Bintegrante.Visible = false;
                    TBcodint.Enabled = false;
                    Bnueva.Visible = true;
                    Validar.Value= list[1];
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

    /*Metodos que funcionan para la consulta de las solicitudes*/
    protected void GVconsulta_RowDataBound(object sender, GridViewRowEventArgs e){}
    private void CargarSolicitudes()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "SELECT SOLE_ID, SOLE_FECHA, SOLE_MOTIVO, SOLE_TIPO, SOLE_ESTADO FROM  SOLICITUD_EST WHERE PROP_CODIGO='"+ prop_codigo+ "' ORDER BY SOLE_ID";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsulta.DataSource = dataTable;
                }
                GVconsulta.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }


}