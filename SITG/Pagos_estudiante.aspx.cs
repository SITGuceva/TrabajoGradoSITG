using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;
using System.Net;

public partial class Pagos_Estudiante : Conexion
{
    Conexion con = new Conexion();
    string codprop;
    string titulo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null) {
            Response.Redirect("Default.aspx");
        }
        if (!Page.IsPostBack){
            string valida = con.Validarurl(Convert.ToInt32(Session["id"]), "Pagos_Estudiante.aspx");
            if (valida.Equals("false")){
                Response.Redirect("MenuPrincipal.aspx");
            }else{
                BuscarPago();
            }
        }
   
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVconsulta);
    }
    
    /*Metodos que realizan la funcionalidad de consultar el documento de los pagos subido por el usuario*/
    protected void BuscarPago()
    {
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select p.pag_id, p.pag_nombre, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as estudiante ,p.pag_fecha, INITCAP(p.pag_estado) as estado  from pagos p, estudiante e, usuario u where pag_estado = 'PENDIENTE' and E.Usu_Username = U.Usu_Username and P.Usu_Username= E.Usu_Username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsulta.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVconsulta.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*Metodos que descarga el pago subido*/
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        List<string> list = con.FtpConexion();
        string fileName = "", contentype = "", ruta = "";

        WebClient request = new WebClient();
        request.Credentials = new NetworkCredential(list[0], list[1]);

        string sql = "select PAG_NOMARCHIVO,PAG_DOCUMENTO, PAG_TIPO FROM PAGOS WHERE PAG_ID=" + id + "";
        List<string> pago = con.consulta(sql, 3, 0);
        fileName = pago[0];
        ruta = pago[1];
        contentype = pago[2];

        try {
            byte[] bytes = request.DownloadData(ruta + fileName);
            string fileString = System.Text.Encoding.UTF8.GetString(bytes);
            Console.WriteLine(fileString);
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentype;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        } catch (WebException a) {
            Linfo.Text = a.ToString();
        }
    }

    /*Metodos que se encargan obtener el codigo del pago para luego cambiar el estado*/
    protected void GVconsulta_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Revisar")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsulta.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene id del pago
            Consulta.Visible = false;
            MostrarDDLestadoP.Visible = true;
            Linfo.Text = "";
        }
    }

    /*Evento de los botones*/
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
    protected void Bcancelar_Click(object sender, EventArgs e)
    {
        MostrarDDLestadoP.Visible = false;
        BuscarPago();
        Consulta.Visible = true;
        DDLestadoP.SelectedIndex = 0;
        TAdescripcion.Value = "";
        Metodo.Value = "";
    }
    protected void guardar(object sender, EventArgs e)
    {
        string sql;
        if (string.IsNullOrEmpty(TAdescripcion.Value) == false)
        {
            sql = "update pagos set pag_observacion = '" + TAdescripcion.Value + "', pag_estado = '" + DDLestadoP.Items[DDLestadoP.SelectedIndex].Value.ToString() + "' where pag_id = '" + Metodo.Value + "'";
            Ejecutar("Se verificó el pago correctamente", sql);
        }
        else
        {
            sql = "update pagos set pag_estado='" + DDLestadoP.Items[DDLestadoP.SelectedIndex].Value.ToString() + "' where pag_id='" + Metodo.Value + "'";
            Ejecutar("Se verificó el pago correctamente", sql);
        }
        IBregresar.Visible = true;
        MostrarDDLestadoP.Visible = false;
        Metodo.Value = "";
    }
    protected void regresar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        BuscarPago();
        Consulta.Visible = true;
        IBregresar.Visible = false;
        Metodo.Value = "";
    }

}