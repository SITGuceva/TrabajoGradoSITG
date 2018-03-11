using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConsultarAReunion : System.Web.UI.Page
{
    Conexion con = new Conexion();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            DDLprog.Items.Clear();
            string sql = "SELECT PROG_CODIGO,PROG_NOMBRE FROM PROGRAMA WHERE PROG_ESTADO='ACTIVO'" ;
            DDLprog.Items.AddRange(con.cargardatos(sql));
            DDLprog.Items.Insert(0, "Seleccione");
        }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVactas);
    }

    /*Metodos que se utilizan para la consulta de las actas por un rango de fecha*/
    protected void IBdesde_Click(object sender, ImageClickEventArgs e)
    {
        if (Cdesde.Visible == false)
        {
            Cdesde.Visible = true;
        }else {
            Cdesde.Visible = false;
        }
    }
    protected void IBhasta_Click(object sender, ImageClickEventArgs e)
    {
        if (Chasta.Visible == false){
            Chasta.Visible = true;
        }else {
            Chasta.Visible = false;
        }
    }
    protected void Cdesde_SelectionChanged(object sender, EventArgs e)
    {
        TBdesde.Text = Cdesde.SelectedDate.ToShortDateString();
        Cdesde.Visible = false;
    }
    protected void Chasta_SelectionChanged(object sender, EventArgs e)
    {
        TBhasta.Text = Chasta.SelectedDate.ToShortDateString();
        Chasta.Visible = false;
    }
    protected void BbuscarAct_Click(object sender, EventArgs e)
    {
        if(DDLprog.SelectedIndex.Equals(0)) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe elegir un programa"; 
        } else if (string.IsNullOrEmpty(TBdesde.Text) == true){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe elegir una fecha de inicio";
        } else { 
            CargarActas();
        }
    }
    private void CargarActas()
    {
        string fhasta;
        if (string.IsNullOrEmpty(TBhasta.Text) == true){
            fhasta = DateTime.Now.ToString("dd/MM/yyyy");
            TBhasta.Text = fhasta;
        } else{
            fhasta = TBhasta.Text;
        }
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                string sql = "SELECT r.REU_CODIGO, TO_CHAR(r.REU_FREAL,'DD/MM/YY') AS FECHA from reunion r" +
                    " where r.REU_FREAL BETWEEN TO_DATE('" + TBdesde.Text + "', 'DD/MM/YYYY')  and TO_DATE('" + fhasta + "', 'DD/MM/YYYY')  AND r.REU_ESTADO='Finalizada'" +
                    "and  r.COM_CODIGO='"+ DDLprog.Items[DDLprog.SelectedIndex].Value.ToString() + "' ORDER BY r.REU_CODIGO";
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVactas.DataSource = dataTable;
                    GVactas.Visible = true;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVactas.DataBind();
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
        string sql = "select REU_NOMARCHIVO, REU_ACTA, REU_TIPO FROM REUNION WHERE REU_CODIGO=" + id + "";

        OracleConnection conn = con.crearConexion();
        if (conn != null)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader())
                {
                    drc1.Read();
                    contentype = drc1["REU_TIPO"].ToString();
                    fileName = drc1["REU_NOMARCHIVO"].ToString();
                    bytes = (byte[])drc1["REU_ACTA"];

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
    protected void GVactas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVactas.PageIndex = e.NewPageIndex;
        CargarActas();
    }
    protected void GVactas_RowDataBound(object sender, GridViewRowEventArgs e) { }

    protected void DDLprog_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLprog.SelectedIndex.Equals(0)){
            Linfo.Text = "";
            GVactas.Visible = false;
            TBdesde.Text = "";
            TBhasta.Text = "";
        }else {
            Linfo.Text = "";
            GVactas.Visible = false;
        }
    }
}