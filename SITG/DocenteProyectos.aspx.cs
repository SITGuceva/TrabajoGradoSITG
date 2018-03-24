using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class DocenteProyectos : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack) {
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "DocenteProyectos.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                DDLprograma.Items.Clear();
                string sql = "SELECT PROG_CODIGO, PROG_NOMBRE FROM PROGRAMA WHERE PROG_ESTADO='ACTIVO' ORDER BY PROG_CODIGO";
                DDLprograma.Items.AddRange(con.cargardatos(sql));
                DDLprograma.Items.Insert(0, "Seleccione");
                DDLlprof.Items.Insert(0, "Seleccione");
                DDLtema.Items.Insert(0, "Seleccione");
            }
        }
    }

    /*Metodos de crear-consultar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        Linfo.Text = "";
        Consultaproyectos.Visible = false;
        DDLprograma.Items.Clear();
        string sql = "SELECT PROG_CODIGO, PROG_NOMBRE FROM PROGRAMA WHERE PROG_ESTADO='ACTIVO' ORDER BY PROG_CODIGO";
        DDLprograma.Items.AddRange(con.cargardatos(sql));
        DDLprograma.Items.Insert(0, "Seleccione");
        DDLlprof.Items.Insert(0, "Seleccione");
        DDLtema.Items.Insert(0, "Seleccione");
    }
    protected void Consultar(object sender, EventArgs e)
    {
        Ingreso.Visible = false;
        Consultaproyectos.Visible = true;
        Linfo.Text = "";
        ResultadoConsulta();
    }

    /*Metodos que realizan el guardar */
    protected void Aceptar(object sender, EventArgs e)
    {
        string sql = "", texto = "";
        if (Ingreso.Visible){
            if (string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBcant.Text) == true || string.IsNullOrEmpty(TBdescripcion.Value) == true) {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            } else {
                string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
                sql = "insert into PROYECTOS (PROY_ID,PROY_NOMBRE,PROY_DESCRIPCION, PROY_CANTEST, PROY_FECHA, USU_USERNAME, TEM_CODIGO) " +
                    "VALUES(proyectoid.nextval, '"+ TBnombre.Text+"', '"+TBdescripcion.Value+"', '"+TBcant.Text+ "', TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), '" + Session["id"]+"', '"+ DDLtema.Items[DDLtema.SelectedIndex].Value.ToString() + "')";
                texto = "Datos guardados satisfactoriamente";
                Ejecutar(texto, sql);
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
        TBnombre.Text = "";
        TBcant.Text = "";
        TBdescripcion.Value = "";
        DDLtema.SelectedIndex = 0;
        DDLlprof.SelectedIndex = 0;
        DDLprograma.SelectedIndex = 0;
    }
    protected void DDLlprof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLlprof.SelectedIndex.Equals(0))
        {
            DDLtema.Items.Clear();
            DDLtema.Items.Insert(0, "Seleccione");
        }
        else
        {
            DDLtema.Items.Clear();
            string sql = "SELECT TEM_CODIGO, TEM_NOMBRE FROM TEMA WHERE TEM_ESTADO='ACTIVO' AND LINV_CODIGO='" + DDLlprof.Items[DDLlprof.SelectedIndex].Value.ToString() + "'";
            DDLtema.Items.AddRange(con.cargardatos(sql));
            DDLtema.Items.Insert(0, "Seleccione");
        }
    }
    protected void DDLconsultaPrograma_SelectedIndexChanged(object sender, EventArgs e)/*evento del ddl para cuando selecciona un item*/
    {
        if (DDLprograma.SelectedIndex.Equals(0))
        {
            DDLlprof.Items.Clear();
            DDLlprof.Items.Insert(0, "Seleccione");
            DDLtema.Items.Clear();
            DDLtema.Items.Insert(0, "Seleccione");
        }
        else
        {
            DDLlprof.Items.Clear();
            string sql = "SELECT LINV_CODIGO, LINV_NOMBRE FROM LIN_INVESTIGACION WHERE PROG_CODIGO='" + DDLprograma.Items[DDLprograma.SelectedIndex].Value.ToString() + "' and LINV_ESTADO='ACTIVO' ORDER BY LINV_CODIGO";          
            DDLlprof.Items.AddRange(con.cargardatos(sql));
            DDLlprof.Items.Insert(0, "Seleccione");
            Linfo.Text = "";
        }
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        TBnombre.Text = "";
        TBcant.Text = "";
        TBdescripcion.Value = "";
        DDLlprof.SelectedIndex = 0;
        DDLtema.SelectedIndex = 0;
        DDLprograma.SelectedIndex = 0;
    }

    /*Metodos que realizan la consulta con el modificar e inhabilitar*/
    private void ResultadoConsulta()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select P.Proy_Id,L.Linv_Nombre, T.Tem_Nombre ,P.Proy_Nombre, P.Proy_Descripcion, P.Proy_Cantest,P.Proy_Fecha ,P.Proy_Estado  from proyectos p, lin_investigacion l, tema t where T.Tem_Codigo = P.Tem_Codigo and T.Linv_Codigo = L.Linv_Codigo and P.Usu_Username = '" + Session["id"]+"'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVproyectos.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVproyectos.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVproyectos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVproyectos.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    protected void GVproyectos_RowDataBound(object sender, GridViewRowEventArgs e){}
    protected void GVproyectos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVproyectos.EditIndex = -1;
        ResultadoConsulta();
    }
    protected void GVproyectos_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
            DropDownList combo = GVproyectos.Rows[e.RowIndex].FindControl("estado") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox nombre = (TextBox)GVproyectos.Rows[e.RowIndex].Cells[3].Controls[0];
            TextBox descripcion = (TextBox)GVproyectos.Rows[e.RowIndex].Cells[4].Controls[0];
            TextBox cantest = (TextBox)GVproyectos.Rows[e.RowIndex].Cells[5].Controls[0];
            TextBox codigo = (TextBox)GVproyectos.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update proyectos set proy_nombre = '" + nombre.Text + "', proy_descripcion='" + descripcion.Text + "',proy_cantest = '" + cantest.Text + "', proy_fecha=TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'), proy_estado='" + estado + "' where  proy_id ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVproyectos.EditIndex = -1;
                ResultadoConsulta();
            }
        }
    }
    protected void GVproyectos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVproyectos.EditIndex = e.NewEditIndex;
        ResultadoConsulta();
        GVproyectos.Rows[indice].Cells[0].Enabled = false;
        GVproyectos.Rows[indice].Cells[1].Enabled = false;
        GVproyectos.Rows[indice].Cells[2].Enabled = false;
    }

}