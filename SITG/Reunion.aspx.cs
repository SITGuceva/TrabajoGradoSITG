using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Calendario : System.Web.UI.Page
{
   
   Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        comite();
        
    }

    /*Metodos de crear-modificar-consultar-inhabilitar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Metodo.Value = "";
        Ingreso.Visible = true;
        Linfo.Text = "";
        comite();
        Consulta.Visible = false;
    }
    protected void LBConsultar_Click(object sender, EventArgs e) {
        Ingreso.Visible = false;
        Linfo.Text = "";
        Metodo.Value = "";
        GVconsulta.Visible = false;
        Consulta.Visible = true;
        DDLmes.SelectedIndex = 0;
    }

    /*Metodos que se utilizan para crear reunión*/
    private void comite()
    {
        string sql = "";
        sql = "select c.COM_NOMBRE,c.COM_CODIGO from comite c, profesor p where p.COM_CODIGO = c.COM_CODIGO and p.USU_USERNAME = '" + Session["id"] + "'";
        List<string> list = con.consulta(sql, 2, 1);
        Rcomite.Text = list[0];
        Metodo.Value = list[1];
    }
    protected void Aceptar(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TBfecha.Text) == true)
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe Elegir una fecha.";
        }else {
            string sql= "insert into REUNION (REU_CODIGO,REU_FPROP,COM_CODIGO) VALUES(reunionid.nextval,TO_DATE( '"+TBfecha.Text+ "', 'DD-MM-YYYY HH24:MI:SS'),'" + Metodo.Value+"')";
            Ejecutar("Reunión creada satisfactoriamente", sql);
            TBfecha.Text = "";
        }
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")) {
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }else {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }
    protected void Ifecha_Click(object sender, ImageClickEventArgs e){
        if (Cfecha.Visible == false) {
            Cfecha.Visible = true;
        } else{
            Cfecha.Visible = false;
        }
    }
    protected void Cfecha_SelectionChanged(object sender, EventArgs e) {
        TBfecha.Text = Cfecha.SelectedDate.ToShortDateString();
        Cfecha.Visible = false;
    }
    protected void Limpiar(object sender, EventArgs e)
    {
        TBfecha.Text = "";
        Linfo.Text = "";
    }

    /*Metodos que se utilizan para consultar y dar inicio a la reunión*/
    private void CargarReunion(){
        string sql = "";
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "select r.reu_codigo, r.reu_fprop, r.reu_estado from reunion r,comite c, profesor p where Reu_Estado='PENDIENTE' and TO_CHAR(reu_fprop,'MM')='"+ DDLmes.Items[DDLmes.SelectedIndex].Value + "' and  P.Com_Codigo=C.Com_Codigo and P.Usu_Username='" + Session["id"] + "' order by  r.reu_codigo";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsulta.DataSource = dataTable;
                   // int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                   // Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVconsulta.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVconsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVconsulta.PageIndex = e.NewPageIndex;
        CargarReunion();
    }  
    protected void GVconsulta_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVconsulta_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "iniciar") {
            string fecha = DateTime.Now.ToString("dd/MM/yyyy, HH:mm:ss");
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsulta.Rows[index];
            Linfo.Text = "";
            string sql = "update reunion set reu_freal= TO_DATE( '" + fecha + "', 'DD-MM-YYYY HH24:MI:SS') , reu_estado='ACTIVO' where reu_codigo='" + row.Cells[0].Text+"'";
            Ejecutar("Se ha dado inicio a la reunión nº " + row.Cells[0].Text, sql);
            CargarReunion();   
        }
    }
    protected void Bbuscar_Click(object sender, EventArgs e)
    {
        if (DDLmes.SelectedIndex.Equals(0)) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe elegir un parametro.";
            GVconsulta.Visible = false;
        }else{
            Linfo.Text = "";
            CargarReunion();
            GVconsulta.Visible = true;
        }
    }
    protected void DDLmes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLmes.SelectedIndex.Equals(0)){
            Linfo.Text = "";
            GVconsulta.Visible = false;
        }
    }


}