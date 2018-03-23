using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class DocumentosCom : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack) {
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "DocumentosCom.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                DDLconsultaLinea.Items.Clear();
                string sql = "SELECT l.LPROF_CODIGO, l.LPROF_NOMBRE FROM LIN_PROFUNDIZACION l, programa p, comite c, profesor d where d.USU_USERNAME = '" + Session["id"] + "' and d.COM_CODIGO = c.COM_CODIGO and c.PROG_CODIGO = p.PROG_CODIGO and l.PROG_CODIGO = p.PROG_CODIGO";
                DDLconsultaLinea.Items.AddRange(con.cargardatos(sql));
                DDLconsultaLinea.Items.Insert(0, "Seleccione Linea");
            }
        }
    }

    /*Eventos  que manejan el fronted*/
    protected void LBpropuesta_Click(object sender, EventArgs e) {
        Ingreso.Visible =true;
        ResultadoPropuesta.Visible = false;
        ResultadoAnteproyecto.Visible = false;
        ResultadoProyectoF.Visible = false;
        tipo.Value = "1";
        Linfo.Text = "";
        DDLconsultaLinea.SelectedIndex = 0;
        DDLestado.SelectedIndex = 0;
        Lprop.Visible = true;
        Lant.Visible = false;
        Lpf.Visible = false;
    }
    protected void LBanteproyecto_Click(object sender, EventArgs e){
        Ingreso.Visible = true;
        ResultadoPropuesta.Visible = false;
        ResultadoAnteproyecto.Visible = false;
        ResultadoProyectoF.Visible = false;
        tipo.Value= "2";
        Linfo.Text = "";
        DDLestado.SelectedIndex = 0;
        DDLconsultaLinea.SelectedIndex = 0;
        Lprop.Visible = false;
        Lant.Visible = true;
        Lpf.Visible = false;
    }
    protected void LBproyecto_Click(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        ResultadoPropuesta.Visible = false;
        ResultadoAnteproyecto.Visible = false;
        ResultadoProyectoF.Visible = false;
        tipo.Value = "3";
        Linfo.Text = "";
        DDLconsultaLinea.SelectedIndex = 0;
        DDLestado.SelectedIndex = 0;
        Lprop.Visible = false;
        Lant.Visible = false;
        Lpf.Visible = true;
    }
   
    /*Evento del boton buscar*/
    protected void Btbuscar_Click(object sender, EventArgs e)
    {
        if (tipo.Value.Equals("1")){
            consultaprop();
        }else if (tipo.Value.Equals("2")){
            consultaante();
        }else if (tipo.Value.Equals("3")) {
            consultapfinal();
        }    
    }

    /*Metodos para el proyecto*/
    private void consultapfinal()
    {
        if (!DDLconsultaLinea.SelectedIndex.Equals(0) && !DDLestado.SelectedIndex.Equals(0)){
            CargarProyectoF(1);
        }else if (!DDLestado.SelectedIndex.Equals(0)){
            CargarProyectoF(2);
        }else{
            ResultadoProyectoF.Visible = false;
            Linfo.Text = "Debe seleccionar un parametro";
        }
    }
    protected void GVproyectoF_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void CargarProyectoF(int crit)
    {
        string sql = "";
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                if (crit.Equals(2)){
                      sql = "select Distinct P.Ppro_Codigo, P.Pf_Titulo,TO_CHAR( P.Pf_Fecha, 'dd/mm/yyyy') as FECHA ,CONCAT(CONCAT(u.usu_nombre, ' '),u.usu_apellido) as director,  P.Pf_Aprobacion from proyecto_final p, estudiante e, solicitud_dir s, usuario u" +
                        " where P.Ppro_Codigo= e.PROP_CODIGO and P.Pf_Estado ='" + DDLestado.Items[DDLestado.SelectedIndex].Text.ToUpper() + "'and u.USU_USERNAME = s.USU_USERNAME  and s.Prop_Codigo= P.Ppro_Codigo";
                }else if (crit.Equals(1)) {
                    sql = "select Distinct P.Ppro_Codigo, P.Pf_Titulo,TO_CHAR( P.Pf_Fecha, 'dd/mm/yyyy') as FECHA ,CONCAT(CONCAT(u.usu_nombre, ' '),u.usu_apellido) as director,  P.Pf_Aprobacion from Propuesta n, Tema t, Lin_Profundizacion l, proyecto_final p, estudiante e, solicitud_dir s, usuario u " +
                        "Where  P.Ppro_Codigo= e.PROP_CODIGO and P.Pf_Estado = '" + DDLestado.Items[DDLestado.SelectedIndex].Text.ToUpper() + "' and u.USU_USERNAME = s.USU_USERNAME  and s.Prop_Codigo= P.Ppro_Codigo and T.Lprof_Codigo = L.Lprof_Codigo And T.Tem_Codigo = n.Tem_Codigo  And L.Lprof_Codigo = '" + DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value + "'";
                }
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVproyectoF .DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVproyectoF.DataBind();
            }
            conn.Close();
        } catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
        ResultadoProyectoF.Visible = true;
    }

    /*Metodos para el anteproyecto*/
    private void consultaante()
    {
        if (!DDLconsultaLinea.SelectedIndex.Equals(0) && !DDLestado.SelectedIndex.Equals(0)){
            CargarAnteproyecto(1);
        }else if (!DDLestado.SelectedIndex.Equals(0)){
            CargarAnteproyecto(2);
        }else{
            ResultadoAnteproyecto.Visible = false;
            Linfo.Text = "Debe seleccionar un parametro";
        }
    }
    protected void GVanteproyecto_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void CargarAnteproyecto(int crit)
    {
        string sql = "";
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                if (crit.Equals(2)){
                   sql = "select Distinct A.Apro_Codigo, A.Anp_Nombre,TO_CHAR( A.Anp_Fecha, 'dd/mm/yyyy') as FECHA ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director,  A.Ant_Aprobacion, CONCAT(CONCAT(o.usu_nombre, ' '), o.usu_apellido) as revisor from anteproyecto a, estudiante e, solicitud_dir s, usuario u, evaluador r, usuario o " +
                        " where A.Apro_Codigo = e.PROP_CODIGO and A.Ant_Estado = '" + DDLestado.Items[DDLestado.SelectedIndex].Text.ToUpper() + "' and u.USU_USERNAME = s.USU_USERNAME  and s.Prop_Codigo = a.Apro_Codigo and r.Usu_Username = o.Usu_Username and r.Apro_Codigo = e.Prop_Codigo";
                } else if (crit.Equals(1)){
                   sql = "Select Distinct A.Apro_Codigo, A.Anp_Nombre,TO_CHAR( A.Anp_Fecha, 'dd/mm/yyyy') as FECHA ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, A.Ant_Aprobacion, CONCAT(CONCAT(o.usu_nombre, ' '), o.usu_apellido) as revisor From Propuesta P, Estudiante E, Lin_Profundizacion L, Solicitud_Dir S, Usuario U,evaluador r, usuario o , Tema t, anteproyecto a " +
                        "Where T.Lprof_Codigo = L.Lprof_Codigo And T.Tem_Codigo = P.Tem_Codigo  And L.Lprof_Codigo = '" + DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value + "' and A.Apro_Codigo= e.PROP_CODIGO and A.Ant_Estado = '" + DDLestado.Items[DDLestado.SelectedIndex].Text.ToUpper() + "' and u.USU_USERNAME = s.USU_USERNAME  and s.Prop_Codigo= a.Apro_Codigo and r.Usu_Username = o.Usu_Username and r.Apro_Codigo = e.Prop_Codigo";
                }
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVanteproyecto.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVanteproyecto.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
        ResultadoAnteproyecto.Visible = true;
    }

    /*Metodos para la propuesta*/
    private void consultaprop()
    {
        if (!DDLconsultaLinea.SelectedIndex.Equals(0) && !DDLestado.SelectedIndex.Equals(0)) {
            CargarPropuesta(1);
        } else if (!DDLestado.SelectedIndex.Equals(0)){
            CargarPropuesta(2);
        } else {
            ResultadoPropuesta.Visible = false;
            Linfo.Text = "Debe seleccionar un parametro";
        }
    }
    protected void GVpropuesta_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void CargarPropuesta(int crit)
    {
        string sql = "";
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                if (crit.Equals(2)){
                    sql = "select Distinct p.PROP_CODIGO,p.PROP_TITULO, l.LPROF_NOMBRE, t.TEM_NOMBRE,TO_CHAR( p.PROP_FECHA, 'dd/mm/yyyy') as FECHA ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, s.sol_estado as Estado from propuesta p, estudiante e, lin_profundizacion l, tema t, solicitud_dir s, usuario u where t.LPROF_CODIGO = l.LPROF_CODIGO and t.TEM_CODIGO = p.TEM_CODIGO and p.PROP_CODIGO = e.PROP_CODIGO and p.PROP_ESTADO = '" + DDLestado.Items[DDLestado.SelectedIndex].Text.ToUpper() + "' and u.USU_USERNAME = s.USU_USERNAME and s.PROP_CODIGO = p.PROP_CODIGO";
                }else if (crit.Equals(1))
                {
                   sql = "Select Distinct P.Prop_Codigo,P.Prop_Titulo, L.Lprof_Nombre, T.Tem_Nombre,To_Char( P.Prop_Fecha, 'dd/mm/yyyy') As Fecha ,Concat(Concat(U.Usu_Nombre, ' '), U.Usu_Apellido) As Director, S.Sol_Estado As Estado From Propuesta P, Estudiante E, Lin_Profundizacion L, Tema T, Solicitud_Dir S, Usuario U Where T.Lprof_Codigo = L.Lprof_Codigo " +
                        "And T.Tem_Codigo = P.Tem_Codigo And P.Prop_Codigo = E.Prop_Codigo And L.Lprof_Codigo = '"+ DDLconsultaLinea.Items[DDLconsultaLinea.SelectedIndex].Value + "' And P.Prop_Estado = '"+ DDLestado.Items[DDLestado.SelectedIndex].Text.ToUpper() + "' And U.Usu_Username = S.Usu_Username And S.Prop_Codigo = P.Prop_Codigo";
                }
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVpropuesta.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVpropuesta.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
        ResultadoPropuesta.Visible = true;
    }

}