using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PropuestaAsignada : Conexion
{
    Conexion con = new Conexion();
    int propcodigo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { 
            cargarTabla2();
        }
        TBdescripcion.Enabled = false;
        BtCancelar.Visible = false;
        BtCancelar.Enabled = false;
        BTagregar.Enabled = false;
        Ingreso.Visible = true;
        Label1.Visible = false;
        Label2.Visible = false;
        TBcodigo.Visible = false;
        TBdescripcion.Visible = false;
        Resultado2.Visible = true;
        
        BTagregar.Visible = false;
        BtCancelar.Visible = false;
        



    }

    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
       // cargarTabla2();
    }

    protected void Agregar_observacion(object sender, EventArgs e)
    {
        Resultado2.Visible = false;
  
       

        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string sql = "", texto = "sql registrado correctamente";
        sql = "insert into observacion (OBS_CODIGO, OBS_DESCRIPCION, OBS_REALIZADA ,PROP_CODIGO) values (OBSERVACIONPROP.nextval,'" + TBdescripcion.Text + "','Director', '"+Metodo.Value+"')";
        Ejecutar(texto, sql);
        TBdescripcion.Text = "";
        Resultado.Visible = true;
        cargarTabla();

    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select PROP_NOMARCHIVO, PROP_DOCUMENTO, PROP_TIPO FROM PROPUESTA WHERE PROP_CODIGO='"+id+"'";
       
        OracleConnection conn = con.crearConexion();
        if (conn != null)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader())
                {
                    Linfo.Text = sql;
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

    protected void cancelar(object sender, EventArgs e)
    {

      
        TBdescripcion.Enabled = false;
        BtCancelar.Visible = false;
        BTagregar.Enabled = false;
        Resultado.Visible = false;
        Resultado2.Visible = true;

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
    protected void gvSysRol_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        BTagregar.Enabled = true;
        TBdescripcion.Enabled = true;
    }


    public void cargarTabla()
    {
        BtCancelar.Visible = true;
        BTagregar.Visible = true;
        TBdescripcion.Visible = true;
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT OBS_CODIGO, OBS_DESCRIPCION FROM OBSERVACION  WHERE PROP_CODIGO ='"+ Metodo.Value + "'";

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
    protected void gvSysAsignados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        BTagregar.Enabled = true;
        TBdescripcion.Enabled = true;
        gvSysRol.PageIndex = e.NewPageIndex;
        cargarTabla();// la consulta a la base de datos
    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvSysAsignados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        BtCancelar.Visible = true;
        BtCancelar.Enabled = true;
        BTagregar.Enabled = true;
        TBdescripcion.Enabled = true;
    }

    public void cargarTabla2()
    {

        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select distinct p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado  from propuesta p, usuario u, solicitud_dir s where s.USU_USERNAME='" + Session["id"] + "' and p.PROP_CODIGO = s.prop_codigo and sol_estado='Aprobado'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvSysAsignados.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    // Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }

                gvSysAsignados.DataBind();

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
        Resultado2.Visible = false;

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
        Resultado2.Visible = false;
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
        Resultado2.Visible = false;
        //NewEditIndex property used to determine the index of the row being edited.  
        int indice = gvSysRol.EditIndex = e.NewEditIndex;
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
        Resultado2.Visible = false;
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        gvSysRol.EditIndex = -1;
        cargarTabla();
    }


    protected void gvSysAsignados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
 
        if (e.CommandName == "agregar")
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvSysAsignados.Rows[index];
            Metodo.Value = row.Cells[0].Text;


            Resultado2.Visible = false;
            Resultado.Visible=true;
            cargarTabla();
          
        }
    }
}









