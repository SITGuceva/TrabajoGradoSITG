using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Observaciones : Conexion
{
    Conexion con = new Conexion();


    protected void Page_Load(object sender, EventArgs e)
    {
        TBdescripcion.Enabled = false;
        BtCancelar.Visible = false;
        BtCancelar.Enabled = false;
        BTagregar.Enabled = false;
        Ingreso.Visible = true;
    }

    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
    }

    protected void Agregar_observacion(object sender, EventArgs e)
    {
        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string sql = "", texto = "sql registrado correctamente";
        sql = "insert into observacion (OBS_CODIGO, OBS_DESCRIPCION, OBS_REALIZADA ,PROP_CODIGO) values (OBSERVACIONPROP.nextval,'" + TBdescripcion.Text + "','Comite', '" + TBcodigo.Text + "')";
        Ejecutar(texto, sql);
        cargarTabla();
        Resultado.Visible = true;
    }

    protected void Buscar_observacion(object sender, EventArgs e)
    {

        Linfo.Visible = true;

        if (string.IsNullOrEmpty(TBcodigo.Text) == true)
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Por favor digite el codigo de la propuesta que desea agregar observaciones";
        }

        if (string.IsNullOrEmpty(TBcodigo.Text) == false)
        {
            string sql = @"SELECT COUNT(*) FROM propuesta WHERE prop_codigo ='" + TBcodigo.Text + "'";
            string sql2 = @"SELECT PROP_TITULO FROM propuesta WHERE prop_codigo ='" + TBcodigo.Text + "'";

            using (OracleConnection conn = new OracleConnection("User ID=sitg;Password=1234;Data Source=PRUEBA"))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleCommand cmd2 = new OracleCommand(sql2, conn);
                object result = cmd.ExecuteScalar();
                object result2 = cmd2.ExecuteScalar();
                int resultado = Convert.ToInt32(result);
                string titulo = Convert.ToString(result2);
                if (resultado > 0) {
                    {
                        TBdescripcion.Enabled = true;
                        TBcodigo.Enabled = false;
                        BTagregar.Enabled = true;
                        cargarTabla2();
                        Resultado2.Visible = true;
                        cargarTabla();
                        Resultado.Visible = true;
                        cargarTabla3();
                        Resultado3.Visible = true;
                        BtCancelar.Visible = true;
                        BtCancelar.Enabled = true;
                        Btbuscar.Visible = false;

                        // Create new DataTable and DataSource objects.
                        DataTable table = new DataTable();
                        // Declare DataColumn and DataRow variables.
                        DataColumn column;
                        DataRow row;
                        DataView view;

                        // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.Int32");
                        column.ColumnName = "id";
                        table.Columns.Add(column);

                        // Create second column.
                        column = new DataColumn();
                        column.DataType = Type.GetType("System.String");
                        column.ColumnName = "item";
                        table.Columns.Add(column);

                        // Create new DataRow objects and add to DataTable.    
                        for (int i = 0; i < 10; i++)
                        {
                            row = table.NewRow();
                            row["id"] = i;
                            row["item"] = "item " + i.ToString();
                            table.Rows.Add(row);
                        }

                        // Create a DataView using the DataTable.
                        view = new DataView(table);
                        // Set a DataGrid control's DataSource to the DataView.
                        cargarTabla();
                        Resultado.Visible = true;

                    }
                }
                else
                {
                    Linfo.Visible = true;
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "La propuesta no existe, confirme el codigo ingresado";
                }
            }
        }
    }

    protected void cancelar(object sender, EventArgs e)
    {

        Ingreso.Visible = true;
        TBdescripcion.Enabled = false;
        TBcodigo.Enabled = true;
        BtCancelar.Visible = false;
        BTagregar.Enabled = false;
        Btbuscar.Visible = true;
        Resultado.Visible = false;
        Resultado2.Visible = false;
        Resultado3.Visible = false;
        Linfo.Visible = false;

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

    /*evento que cambia la pagina de la tabla*/
    protected void gvSysRol_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        BTagregar.Enabled = true;
        TBdescripcion.Enabled = true;
        gvSysRol.PageIndex = e.NewPageIndex;
        cargarTabla();// la consulta a la base de datos
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvSysRol_RowDataBound(object sender, GridViewRowEventArgs e) {

        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        BTagregar.Enabled = true;
        TBdescripcion.Enabled = true;
    }


    public void cargarTabla()
    {
        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        BTagregar.Enabled = true;
        TBdescripcion.Enabled = true;
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT OBS_CODIGO, OBS_DESCRIPCION FROM OBSERVACION  WHERE PROP_CODIGO ='" + TBcodigo.Text + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                  
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvSysRol.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }

                gvSysRol.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*evento que cambia la pagina de la tabla*/
    protected void gvSysDatosPropuesta_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        BTagregar.Enabled = true;
        TBdescripcion.Enabled = true;
        gvSysRol.PageIndex = e.NewPageIndex;
        cargarTabla();// la consulta a la base de datos
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvSysDatosPropuesta_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        BTagregar.Enabled = true;
        TBdescripcion.Enabled = true;
    }

    public void cargarTabla2()
    {
        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        BTagregar.Enabled = true;
        TBdescripcion.Enabled = true;
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select CONCAT(CONCAT(u.usu_apellido, ' '), u.usu_nombre) as integrantes from estudiante e, usuario u  where e.prop_codigo = '" + TBcodigo.Text + "' and u.usu_username = e.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvSysDatosPropuesta.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }

                gvSysDatosPropuesta.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }


    /*evento que cambia la pagina de la tabla*/
    protected void gvSysDatosTitulo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        BTagregar.Enabled = true;
        TBdescripcion.Enabled = true;
        gvSysRol.PageIndex = e.NewPageIndex;
        cargarTabla();// la consulta a la base de datos
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvSysDatosTitulo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        BTagregar.Enabled = true;
        TBdescripcion.Enabled = true;
    }

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
                sql = "select prop_titulo from propuesta where prop_codigo = '" + TBcodigo.Text + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvSysDatosTitulo.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }

                gvSysDatosTitulo.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }


    protected void gvSysRol_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
            string id = gvSysRol.Rows[e.RowIndex].Cells[0].Text;
           
           // Label lbldeleteID = (Label)gvSysRol.Rows[e.RowIndex].FindControl("lblstId");
                string sql = "Delete from observacion where OBS_CODIGO='" + id + "'";

               cmd = new OracleCommand(sql, conn);
               cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    cargarTabla();
                    // BindData();
                }
            }
        }


    protected void gvSysRol_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        GridViewRow row = (GridViewRow)gvSysRol.Rows[e.RowIndex];
        if (conn != null)
        {
          
            TextBox observacion = (TextBox)row.Cells[1].Controls[0];
            TextBox codigo = (TextBox)gvSysRol.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update observacion set obs_descripcion = '" + observacion.Text + "' where  obs_codigo ='" + codigo.Text + "'";
            Linfo.Text = sql;
            
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {

                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                gvSysRol.EditIndex = -1;
                //Call ShowData method for displaying updated data  

                cargarTabla();
            }
        }
    }

    protected void gvSysRol_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        int indice= gvSysRol.EditIndex = e.NewEditIndex;
        cargarTabla();
        gvSysRol.Rows[indice].Cells[0].Enabled = false;
        gvSysRol.Rows[indice].Cells[2].Enabled = false;


        /*if (combo != null)
        {
            combo.DataSource = DataAccess.GetAllPaises();
            combo.DataTextField = "descripcion";
            combo.DataValueField = "id";
            combo.DataBind();
        }

        combo.SelectedValue = Convert.ToString(row["pais"]);*/
    }

    protected void gvSysRol_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        gvSysRol.EditIndex = -1;
        cargarTabla();
    }




}









