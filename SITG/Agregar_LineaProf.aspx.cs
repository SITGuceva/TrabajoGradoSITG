﻿using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Agregar_LineaProf : System.Web.UI.Page
{
    Conexion con = new Conexion();
    System.Data.DataTable table;
    System.Data.DataRow row;
    

    protected void Page_Load(object sender, EventArgs e)
    {
       if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        string sql = "SELECT PROG_CODIGO, PROG_NOMBRE FROM PROGRAMA WHERE PROG_ESTADO='ACTIVO'";
        DDLprog.Items.AddRange(con.cargardatos(sql));
        if (!Page.IsPostBack){
            table = new System.Data.DataTable();
            table.Columns.Add("TEMAS", typeof(System.String));
            Session.Add("Tabla", table);
        }
        
    }

    /*Metodos de crear-consultar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        DDLprog.Items.Clear();
        string sql = "SELECT PROG_CODIGO, PROG_NOMBRE FROM PROGRAMA WHERE PROG_ESTADO='ACTIVO'";
        DDLprog.Items.AddRange(con.cargardatos(sql)); 
        Linfo.Text = "";
        DIVBuscar.Visible = false;
        SolTema.Visible = false;
        SolLinProf.Visible = false;
    }
    protected void Consultar(object sender, EventArgs e)
    {
        DDLprog2.Items.Clear();
        string sql2 = "SELECT PROG_CODIGO, PROG_NOMBRE FROM PROGRAMA WHERE PROG_ESTADO='ACTIVO'";
        DDLprog2.Items.AddRange(con.cargardatos(sql2));
      
        DIVBuscar.Visible = true;        
        SolTema.Visible = false;
        SolLinProf.Visible = false;
        Ingreso.Visible = false;      
        Linfo.Text = "";
        Bbuscar.Visible = true;
        DDLprog2.Enabled = true;
        Bnueva.Visible = false;
    }

    /*Metodo que se utiliza para la busqueda del insertar tema*/
    protected void AgregarFila(object sender, EventArgs e)
    {
        table = (System.Data.DataTable)(Session["Tabla"]);
        row = table.NewRow();
        row["TEMAS"] = TBnomtema.Text;

        table.Rows.Add(row);
        GVagretema.DataSource = table;
        GVagretema.DataBind();
        TBnomtema.Text = "";
    }
  
    /*Metodos que se utilizan para guardar*/
    protected void Aceptar(object sender, EventArgs e)
    {
        table = (System.Data.DataTable)(Session["Tabla"]);
        DataRow[] currentRows = table.Select(null, null, DataViewRowState.CurrentRows);        
        string sql = "",texto = "";

        if (Ingreso.Visible){
            if (string.IsNullOrEmpty(TBnomlinea.Text) == true || currentRows.Length < 1){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else{
                sql = "insert into LIN_PROFUNDIZACION (LPROF_CODIGO,LPROF_NOMBRE,PROG_CODIGO) VALUES(lprofid.nextval, '" + TBnomlinea.Text + "', '" + DDLprog.Items[DDLprog.SelectedIndex].Value.ToString() + "')";
                texto = "Datos guardados satisfactoriamente";
                Ejecutar(texto, sql);

                foreach (DataRow row in currentRows){ 
                    sql = "insert into TEMA (TEM_CODIGO,TEM_NOMBRE,LPROF_CODIGO) VALUES(temaid.nextval, '" + row["TEMAS"] + "', lprofid.currval)";
                    texto = "Datos guardados satisfactoriamente";
                    Ejecutar(texto, sql);    
                }
                table.Rows.Clear();
                GVagretema.DataSource = table;
                GVagretema.Visible = false;
            }
        }
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
        TBnomlinea.Text = "";      
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e){
        Linfo.Text = "";
        TBnomlinea.Text = "";
    }

    /*Metodos que se utilizan para la consulta de la linea profundizacion*/
    protected void Buscar(object sender, EventArgs e)
    {
        cargarTabla();
        SolLinProf.Visible = true;
        Bbuscar.Visible = false;
        Bnueva.Visible = true;
        DDLprog2.Enabled = false;
    }
    protected void Nueva(object sender, EventArgs e)
    {
        Bbuscar.Visible = true;
        Bnueva.Visible = false;
        DDLprog2.Enabled = true;
        SolLinProf.Visible = false;
        SolTema.Visible = false;
        Linfo.Text = "";
        Lreslp.Text = "";
        TBagregt.Text = "";
    }
    public void cargarTabla()
    {
        string sql = "";
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
               
                string programa = DDLprog2.Items[DDLprog2.SelectedIndex].Value.ToString();               
                sql = "SELECT LPROF_CODIGO, LPROF_NOMBRE, LPROF_ESTADO FROM LIN_PROFUNDIZACION WHERE PROG_CODIGO='" + programa + "' ORDER BY LPROF_CODIGO";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVlineaprof.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVlineaprof.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVlineaprof_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVlineaprof.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVlineaprof_RowDataBound(object sender, GridViewRowEventArgs e) { } 
    protected void GVlineaprof_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;   
        if (conn != null){
            DropDownList combo = GVlineaprof.Rows[e.RowIndex].FindControl("estado") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox nombre = (TextBox)GVlineaprof.Rows[e.RowIndex].Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVlineaprof.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update lin_profundizacion set lprof_nombre = '" + nombre.Text + "', lprof_estado='" + estado + "' where  lprof_codigo ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader()){               
                GVlineaprof.EditIndex = -1;
                cargarTabla();
            }
        }
    }
    protected void GVlineaprof_RowEditing(object sender, GridViewEditEventArgs e)
    {       
        int indice = GVlineaprof.EditIndex = e.NewEditIndex;
        cargarTabla();
        GVlineaprof.Rows[indice].Cells[0].Enabled = false;
    }
    protected void GVlineaprof_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVlineaprof.EditIndex = -1;
        cargarTabla();
    }   
    protected void GVlineaprof_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "buscar"){
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVlineaprof.Rows[index];
            Metodo.Value = row.Cells[0].Text;
            ResultadoTemas();
            SolTema.Visible = true;
            SolLinProf.Visible = false;
            Lreslp.Text= row.Cells[1].Text;
        }
    }
    
    /*Metodos que se utilizan para la consulta de los temas*/

    protected void RegresarLP(object sender, EventArgs e) {
        SolTema.Visible = false;
        SolLinProf.Visible = true;
        cargarTabla();
        Lreslp.Text = "";
        TBagregt.Text = "";
    }
    protected void AgregarT(object sender, EventArgs e)
    {
       string sql = "insert into TEMA (TEM_CODIGO,TEM_NOMBRE,LPROF_CODIGO) VALUES(temaid.nextval, '" + TBagregt.Text+ "', '"+Metodo.Value+"')";
       string texto = "";    
       Ejecutar(texto, sql);
       ResultadoTemas();
    }     
    public void ResultadoTemas()
    {        
          try{
              OracleConnection conn = con.crearConexion();
              OracleCommand cmd = null;
              if (conn != null){
                  string sql = "SELECT T.TEM_CODIGO, T.TEM_NOMBRE, T.TEM_ESTADO FROM TEMA T, LIN_PROFUNDIZACION L WHERE T.LPROF_CODIGO = L.LPROF_CODIGO AND L.LPROF_CODIGO='" + Metodo.Value + "' ORDER BY  T.TEM_CODIGO";

                  cmd = new OracleCommand(sql, conn);
                  cmd.CommandType = CommandType.Text;
                  using (OracleDataReader reader = cmd.ExecuteReader())
                  {
                      DataTable dataTable = new DataTable();
                      dataTable.Load(reader);
                      GVtema.DataSource = dataTable;
                      int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                      Linfo.ForeColor = System.Drawing.Color.Red;
                      Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                  GVtema.DataBind();
              }
              conn.Close();
          }
          catch (Exception ex)
          {
              Linfo.Text = "Error al cargar la lista: " + ex.Message;
          }
    }
    protected void GVtema_RowDataBound(object sender, GridViewRowEventArgs e){}
    protected void GVtema_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVtema.PageIndex = e.NewPageIndex;
        ResultadoTemas();
    }
    protected void GVtema_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            DropDownList combo = GVtema.Rows[e.RowIndex].FindControl("estadotem") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox nombre = (TextBox)GVtema.Rows[e.RowIndex].Cells[1].Controls[0];
            TextBox codigo = (TextBox)GVtema.Rows[e.RowIndex].Cells[0].Controls[0];

            string sql = "update tema set tem_nombre = '" + nombre.Text + "', tem_estado='" + estado + "' where  tem_codigo ='" + codigo.Text + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                GVtema.EditIndex = -1;
                ResultadoTemas();
            }
        }
    }
    protected void GVtema_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        int indice = GVtema.EditIndex = e.NewEditIndex;
        ResultadoTemas();
        GVtema.Rows[indice].Cells[0].Enabled = false;  
    }
    protected void GVtema_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVtema.EditIndex = -1;
        ResultadoTemas();
    }
}