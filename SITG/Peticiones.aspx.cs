using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PeticionDir : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
    }

    /*Metodos que manejan la parte del fronted*/
    protected void LBPeticion_Dir_Click(object sender, EventArgs e)
    {
        TPeticiones.Visible = true;
        CargarTablaPeticiones();
        Linfo.Text = "";
        PetiEstudiante.Visible = false;
        ConsultaPeti.Visible = false;
        Tipo.Value = "";
    }
    protected void LBPeticion_Est_Click(object sender, EventArgs e)
    {
        TPeticiones.Visible = false;
        Linfo.Text = "";
        PetiEstudiante.Visible = true;
        ConsultaPeti.Visible = false;
        Tipo.Value = "";
    }
   
    /*Metodos que se utilizan para consultar las peticiones de director pendientes*/
    protected void GVpeticion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVpeticion.PageIndex = e.NewPageIndex;
        CargarTablaPeticiones();
    }
    protected void GVpeticion_RowDataBound(object sender, GridViewRowEventArgs e){}
    public void CargarTablaPeticiones()
    {   
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT S.SOL_ID, S.SOL_FECHA, S.SOL_ESTADO, S.PROP_CODIGO, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director, u.usu_username " +
                    "FROM SOLICITUD_DIR S, USUARIO U, estudiante e, comite c, PROFESOR p " +
                    "WHERE S.SOL_ESTADO = 'Pendiente' AND U.USU_USERNAME = S.USU_USERNAME and S.PROP_CODIGO = e.PROP_CODIGO  and p.COM_CODIGO = c.COM_CODIGO " +
                    "and c.PROG_CODIGO = e.PROG_CODIGO and p.USU_USERNAME = '"+ Session["id"] + "'";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVpeticion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVpeticion.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVpeticion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        string sql = ""; 
        GridViewRow row;

        if (e.CommandName == "Ver"){
            Regresar.Visible = true;
            CargarInfprof(index);
            Tinfprof.Visible = true;
            TPeticiones.Visible = false;
            Linfo.Text = "";

        }else  if (e.CommandName == "Aprobar"){
            row = GVpeticion.Rows[index];  
            HiddenField idprof = row.FindControl("Director") as HiddenField;            
            sql = "update SOLICITUD_DIR set SOL_ESTADO='Aprobado' where PROP_CODIGO='"+ row.Cells[2].Text + "'";
            Ejecutar("Solicitud de Director Aprobada", sql);   
            ExisteRol(idprof.Value);
            CargarTablaPeticiones();
        } else if (e.CommandName == "Rechazar"){
            row = GVpeticion.Rows[index];
            sql = "update SOLICITUD_DIR set SOL_ESTADO='Rechazado' where PROP_CODIGO='" + row.Cells[2].Text + "'";
            Ejecutar("Solicitud de Director Rechazada", sql);
            CargarTablaPeticiones();
        }

    }
    private void ExisteRol(string idprof)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null) {
            string sql = "select USU_USERNAME from usuario_rol where ROL_ID='DIR' and USU_USERNAME='"+idprof+"'";
            cmd = new OracleCommand(sql, conn);  
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
           
            if (!drc1.HasRows){
                AsignarRol(idprof);
            }
            drc1.Close();
        }
    }
    private void AsignarRol(string idprof)
    { 
        string sql = "insert into USUARIO_ROL (USUROL_ID,USU_USERNAME,ROL_ID) VALUES(USUARIOID.nextval, '" + idprof + "', 'DIR')";
        Ejecutar("Guardo", sql);
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
     
    /*Evento del boton regresar*/
    protected void regresar(object sender, EventArgs e)
    {
        Regresar.Visible = false;
        TPeticiones.Visible = true;
        CargarTablaPeticiones();
        Tinfprof.Visible = false;
    }

    /*Metodos que se utilizan para consultar la informacion adicional del director*/
    protected void GVinfprof_RowDataBound(object sender, GridViewRowEventArgs e){}
    public void CargarInfprof(int cod)
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select usu_telefono, usu_direccion, usu_correo  from usuario  where usu_username='" +cod+ "'";

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

    /*Metodos que se utilian para consulta - modificacion de las peticiones del estudiante*/
    protected void Bbuscar_Click(object sender, EventArgs e)
    {
        if (DDLsol.SelectedIndex.Equals(0))
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe escoger algun tipo de peticion";
        } else {
            ConsultaPeti.Visible = true;
            PeticionEstudiante();
            Tipo.Value = DDLsol.SelectedIndex.ToString();
        }       
    }
    private void PeticionEstudiante()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select DISTINCT s.SOLE_ID, s.SOLE_FECHA, s.SOLE_MOTIVO, p.PROP_TITULO,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as ESTUDIANTE,s.SOLE_ESTADO " +
                    "from solicitud_est s, propuesta p, usuario u, estudiante e where s.PROP_CODIGO = p.PROP_CODIGO  and u.USU_USERNAME = s.USU_USERNAME and s.SOLE_TIPO = '"+ DDLsol.Items[DDLsol.SelectedIndex].Text + "'  and s.SOLE_ESTADO='Pendiente' and e.PROG_CODIGO " +
                    " in (select  c.PROG_CODIGO from profesor p, comite c where p.USU_USERNAME = '" + Session["id"] + "' and c.COM_CODIGO = p.COM_CODIGO ) order by s.sole_id";

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
    protected void GVconsulta_RowDataBound(object sender, GridViewRowEventArgs e){ }
    protected void DDLsol_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLsol.SelectedIndex.Equals(0))
        {
            ConsultaPeti.Visible = false;
        }
    }
    protected void GVconsulta_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVconsulta.EditIndex = -1;
        PeticionEstudiante();
    }
    protected void GVconsulta_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indice = GVconsulta.EditIndex = e.NewEditIndex;
        PeticionEstudiante();
        GVconsulta.Rows[indice].Cells[0].Enabled = false;
        GVconsulta.Rows[indice].Cells[1].Enabled = false;
        GVconsulta.Rows[indice].Cells[2].Enabled = false;
        GVconsulta.Rows[indice].Cells[3].Enabled = false;
        GVconsulta.Rows[indice].Cells[4].Enabled = false;
    }
    protected void GVconsulta_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            DropDownList combo = GVconsulta.Rows[e.RowIndex].FindControl("estado") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox codigo = (TextBox) GVconsulta.Rows[e.RowIndex].Cells[0].Controls[0];
            string sql="";

            if (estado.Equals("Aceptada")) {
                int caso = Convert.ToInt32(Tipo.Value);
                switch (caso){
                    case 1: sql = "update estudiante set prop_codigo = 0 where  EXISTS (select 1 from estudiante e, SOLICITUD_EST s where e.PROP_CODIGO=s.PROP_CODIGO and s.SOLE_ID='" + codigo.Text + "')";
                            break;
                    case 2: sql = "update estudiante set prop_codigo = 0 where usu_username =(select solicitud_est.USU_USERNAME as estudiante from solicitud_est where SOLE_ID = '" + codigo.Text + "') ";
                            break;
                    case 3: sql = "update estudiante set prop_codigo = (select PROP_CODIGO as propuesta from solicitud_est where SOLE_ID = '" + codigo.Text + "') " +
                                  "where usu_username = (select solicitud_est.USU_USERNAME as estudiante from solicitud_est where SOLE_ID = '" + codigo.Text + "')";
                            break;
                    default: break;
                }
                Ejecutar("", sql);
            }
            
            sql = "update solicitud_est set sole_estado='" + estado + "' where  sole_id ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){
                GVconsulta.EditIndex = -1;
                PeticionEstudiante();
            }
        }
    }
}









