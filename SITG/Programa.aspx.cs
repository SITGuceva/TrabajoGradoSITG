using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Programa : System.Web.UI.Page
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        string sql = "SELECT FAC_CODIGO, FAC_NOMBRE FROM FACULTAD WHERE FAC_ESTADO='ACTIVO'";
        DDLfacultad.Items.AddRange(con.cargardatos(sql));
    }

    /*Metodos de crear-modificar-consultar-inhabilitar que manejan la parte del fronted*/
    protected void Crear(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        DDLfacultad.Items.Clear();
        string sql = "SELECT FAC_CODIGO, FAC_NOMBRE FROM FACULTAD WHERE FAC_ESTADO='ACTIVO'";
        DDLfacultad.Items.AddRange(con.cargardatos(sql));
        Resultado.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }
    protected void Modificar(object sender, EventArgs e)
    {
        DDLid.Items.Clear();
        string sql2 = "SELECT PROG_CODIGO FROM PROGRAMA WHERE PROG_ESTADO='ACTIVO'";
        DDLid.Items.AddRange(con.cargarDDLid(sql2));
        DDLfacultad2.Items.Clear();
        string sql = "SELECT FAC_CODIGO, FAC_NOMBRE FROM FACULTAD WHERE FAC_ESTADO='ACTIVO'";
        DDLfacultad2.Items.AddRange(con.cargardatos(sql));
        Actualizar.Visible = true;
        Ingreso.Visible = false;
        Resultado.Visible = false;
        Eliminar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }
    protected void Consultar(object sender, EventArgs e)
    {
        cargarTabla();
        Resultado.Visible = true;
        Botones.Visible = false;
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
    }
    protected void Inhabilitar(object sender, EventArgs e)
    {
        DDLid2.Items.Clear();
        string sql1 = "SELECT PROG_CODIGO, PROG_NOMBRE FROM PROGRAMA";
        DDLid2.Items.AddRange(con.cargardatos(sql1));
        Eliminar.Visible = true;
        Ingreso.Visible = false;
        Resultado.Visible = false;
        Actualizar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }

    /*Evento del boton limpiar*/
    protected void Limpiar(object sender, EventArgs e)
    {
        Linfo.Text = "";
        TBid.Text = "";
        TBnombre.Text = "";
        TBsemestre.Text = "";
        TBnombre2.Text = "";
        TBsemestre2.Text = "";
    }

    /*Metodos que se utilizan para guardar-actualizar-inhabilitar*/
    protected void Aceptar(object sender, EventArgs e)
    {
        string sql = "", sql2="";
        string texto = "";
        string nombre = TBnombre.Text;
        if (Ingreso.Visible){
            if (string.IsNullOrEmpty(TBid.Text) == true || string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBsemestre.Text) == true){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else{
                int id = Int32.Parse(TBid.Text);
                sql = "insert into PROGRAMA (PROG_CODIGO,PROG_NOMBRE,FAC_CODIGO,PROG_SEMESTRE) VALUES('" + id + "', '" + nombre+ "', '" + DDLfacultad.Items[DDLfacultad.SelectedIndex].Value.ToString() + "', '" + TBsemestre.Text + "')";
                texto = "Datos guardados satisfactoriamente";
                Ejecutar(texto, sql);

                sql2 = "insert into COMITE (COM_CODIGO, COM_NOMBRE, PROG_CODIGO) VALUES (COMITEID.nextval, '"+nombre+"','"+id+"')";
                Ejecutar(texto, sql2);
            }
            
        }
        else if (Actualizar.Visible){
            if (string.IsNullOrEmpty(TBnombre2.Text) == true || string.IsNullOrEmpty(TBsemestre2.Text) == true){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else{
                nombre = TBnombre2.Text;
                sql = "UPDATE PROGRAMA SET PROG_NOMBRE= '" + nombre + "', FAC_CODIGO = '" + DDLfacultad2.Items[DDLfacultad2.SelectedIndex].Value.ToString() + "', PROG_SEMESTRE ='" + TBsemestre2.Text + "'  WHERE PROG_CODIGO = '" + DDLid.Items[DDLid.SelectedIndex].Value.ToString() + "'";
                texto = "Datos modificados satisfactoriamente";
                Ejecutar(texto, sql);
                sql2 = "UPDATE COMITE SET COM_NOMBRE= '" + nombre + "'  WHERE PROG_CODIGO = '" + DDLid.Items[DDLid.SelectedIndex].Value.ToString() + "'";;
                Ejecutar(texto, sql2);
            }
        }else if (Eliminar.Visible){
            sql = "UPDATE PROGRAMA SET PROG_ESTADO = '" + DDLestado.Items[DDLestado.SelectedIndex].Value.ToString() + "' WHERE PROG_CODIGO = '" + DDLid2.Items[DDLid2.SelectedIndex].Value.ToString() + "'";
            texto = "Programa se ha puesto " + DDLestado.Items[DDLestado.SelectedIndex].Value.ToLower() + " satisfactoriamente";
            Ejecutar(texto, sql);
            sql2= "UPDATE COMITE SET COM_ESTADO = '" + DDLestado.Items[DDLestado.SelectedIndex].Value.ToString() + "' WHERE PROG_CODIGO = '" + DDLid2.Items[DDLid2.SelectedIndex].Value.ToString() + "'";
            Ejecutar(texto, sql2);
        }
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")){
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        }else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
        TBid.Text = "";
        TBnombre.Text = "";
        TBsemestre.Text = "";
        TBnombre2.Text = "";
        TBsemestre2.Text = "";

    }

    /*Metodos que se utilizan para la consulta*/
    protected void GVprog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVprog.PageIndex = e.NewPageIndex;
        cargarTabla();
    }
    protected void GVprog_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarTabla()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                sql = "SELECT DISTINCT P.PROG_CODIGO, P.PROG_NOMBRE, F.FAC_NOMBRE, P.PROG_SEMESTRE, P.PROG_ESTADO FROM PROGRAMA P, FACULTAD F WHERE P.FAC_CODIGO = F.FAC_CODIGO ORDER BY P.PROG_CODIGO ";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVprog.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVprog.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
}