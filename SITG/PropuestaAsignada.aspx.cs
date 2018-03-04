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
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        } else if (!IsPostBack) { 
            CargarPropuestas();
        }
        
    }

  
    /*Evento del boton seleccionar nueva propuesta*/
    protected void Nueva(object sender, EventArgs e)
    {
        Ingreso.Visible = false;
        ResultadoObservacion.Visible = false;
        ResultadoPropuesta.Visible = true;
        CargarPropuestas();  
    }

    /*Metodos para agregar observaciones y descargar el documento*/
    protected void Agregar_observacion(object sender, EventArgs e)
    {
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string sql  = "insert into observacion (OBS_CODIGO, OBS_DESCRIPCION, OBS_REALIZADA ,PROP_CODIGO) values (OBSERVACIONPROP.nextval,'" + TBdescripcion.Text + "','Director', '"+Metodo.Value+"')";
        Ejecutar("Observacion ingresada correctamente", sql);
        TBdescripcion.Text = "";
        CargarObservaciones();
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
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        } else  {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }

    }


    /*Metodos para la consulta de las propuestas asignadas al director*/
    protected void GVpropuesta_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVpropuesta.PageIndex = e.NewPageIndex;
        CargarPropuestas();
    }
    protected void GVpropuesta_RowDataBound(object sender, GridViewRowEventArgs e){}
    protected void GVpropuesta_RowCommand(object sender, GridViewCommandEventArgs e)
    { 
        if (e.CommandName == "agregar"){
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVpropuesta.Rows[index];
            Metodo.Value = row.Cells[0].Text;
            Ingreso.Visible = true;
            ResultadoPropuesta.Visible = false;
            ResultadoObservacion.Visible=true;
            CargarObservaciones();
            
        }
    }
    public void CargarPropuestas()
    {
       string sql = "";
        List<ListItem> list = new List<ListItem>();
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                sql = "select distinct p.prop_codigo, p.prop_titulo, p.prop_fecha, p.prop_estado  from propuesta p, usuario u, solicitud_dir s where s.USU_USERNAME='" + Session["id"] + "' and p.PROP_CODIGO = s.prop_codigo and sol_estado='Aprobado'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVpropuesta.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVpropuesta.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*Metodos para la consulta-modificar-eliminar de las observaciones */
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        CargarObservaciones();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) {}
    protected void GVobservacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string id = GVobservacion.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from observacion where OBS_CODIGO='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                CargarObservaciones();
            }
        }
    }
    protected void GVobservacion_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        GridViewRow row = (GridViewRow)GVobservacion.Rows[e.RowIndex];
        if (conn != null){
            TextBox observacion = (TextBox)row.Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVobservacion.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update observacion set obs_descripcion = '" + observacion.Text + "' where  obs_codigo ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVobservacion.EditIndex = -1;
                CargarObservaciones();
            }
        }
    }
    protected void GVobservacion_RowEditing(object sender, GridViewEditEventArgs e)
    {      
        int indice = GVobservacion.EditIndex = e.NewEditIndex;
        CargarObservaciones();
        GVobservacion.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVobservacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVobservacion.EditIndex = -1;
        CargarObservaciones();
    }
    public void CargarObservaciones()
    {
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
               string sql = "SELECT OBS_CODIGO, OBS_DESCRIPCION FROM OBSERVACION  WHERE PROP_CODIGO ='"+ Metodo.Value + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVobservacion.DataBind();
            }
            conn.Close();
        }catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
}









