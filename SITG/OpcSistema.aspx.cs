using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OpcSistema : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e){
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        string sql = "SELECT CATS_ID, CATS_NOMBRE FROM CATEGORIA_SISTEMA";
        DDLcategoria.Items.AddRange(con.cargardatos(sql));
    }

    protected void Crear(object sender, EventArgs e) {
        Ingreso.Visible = true;
        DDLcategoria.Items.Clear();
        string sql = "SELECT CATS_ID, CATS_NOMBRE FROM CATEGORIA_SISTEMA";
        DDLcategoria.Items.AddRange(con.cargardatos(sql));
        Resultado.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }

    protected void Modificar(object sender, EventArgs e){
        DDLid.Items.Clear();
        string sql2 = "SELECT OPCS_ID FROM OPCION_SISTEMA";
        DDLid.Items.AddRange(con.cargarDDLid(sql2));
        DDLcategoria2.Items.Clear();
        string sql = "SELECT CATS_ID, CATS_NOMBRE FROM CATEGORIA_SISTEMA";
        DDLcategoria2.Items.AddRange(con.cargardatos(sql));
        Actualizar.Visible = true;
        Ingreso.Visible = false;
        Resultado.Visible = false;
        Eliminar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }

    protected void Consultar(object sender, EventArgs e){
        cargarTabla();
        Resultado.Visible = true;
        Botones.Visible = false;
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
    }

    protected void Inhabilitar(object sender, EventArgs e){
        DDLid2.Items.Clear();
        string sql1 = "SELECT OPCS_ID, OPCS_NOMBRE FROM OPCION_SISTEMA";
        DDLid2.Items.AddRange(con.cargardatos(sql1));
        Eliminar.Visible = true;
        Ingreso.Visible = false;
        Resultado.Visible = false;
        Actualizar.Visible = false;
        Botones.Visible = true;
        Linfo.Text = "";
    }

     protected void Limpiar(object sender, EventArgs e){
        Linfo.Text = "";
        TBid.Text = "";
        TBnombre.Text = "";
        TBurl.Text = "";
        TBnombre2.Text = "";
        TBurl2.Text = "";
    }
    protected void Aceptar(object sender, EventArgs e){          
        string sql = "";
        string texto = "";
        if (Ingreso.Visible)
        {
            if (string.IsNullOrEmpty(TBid.Text) == true || string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBurl.Text) == true)
            {
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }
            else
            {
                int id = Int32.Parse(TBid.Text);
                sql = "insert into OPCION_SISTEMA (OPCS_ID,OPCS_NOMBRE,CATS_ID,OPCS_URL) VALUES('" + id + "', '" + TBnombre.Text + "', '" + DDLcategoria.Items[DDLcategoria.SelectedIndex].Value.ToString() + "', '" + TBurl.Text + "')";

                texto = "Datos guardados satisfactoriamente";
                Ejecutar(texto, sql);
            }
        }
        else if (Actualizar.Visible){
             if (string.IsNullOrEmpty(TBnombre2.Text) == true || string.IsNullOrEmpty(TBurl2.Text) == true) {
                   Linfo.ForeColor = System.Drawing.Color.Red;
                   Linfo.Text = "Los campos son obligatorios";
               }else{
                sql = "UPDATE OPCION_SISTEMA SET OPCS_NOMBRE= '" + TBnombre2.Text + "', CATS_ID = '" + DDLcategoria2.Items[DDLcategoria2.SelectedIndex].Value.ToString() + "', OPCS_URL ='" + TBurl2.Text + "'  WHERE OPCS_ID = '" + DDLid.Items[DDLid.SelectedIndex].Value.ToString() + "'";
                texto = "Datos modificados satisfactoriamente";
                Ejecutar(texto, sql);
            }
            }
        else if (Eliminar.Visible)
        {
       
            sql = "UPDATE OPCION_SISTEMA SET OPCS_ESTADO = '" + DDLestado.Items[DDLestado.SelectedIndex].Value.ToString() + "' WHERE OPCS_ID = '" + DDLid2.Items[DDLid2.SelectedIndex].Value.ToString() + "'";
            texto = "Opcion se ha puesto " + DDLestado.Items[DDLestado.SelectedIndex].Value.ToLower() + " satisfactoriamente";
            Ejecutar(texto, sql);
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

        TBid.Text = "";
        TBnombre.Text = "";
        TBurl.Text = "";
        TBnombre2.Text = "";
        TBurl2.Text = "";
    }


    protected void gvSysRol_PageIndexChanging(object sender, GridViewPageEventArgs e){
        gvSysRol.PageIndex = e.NewPageIndex;
        cargarTabla();
    }

    protected void gvSysRol_RowDataBound(object sender, GridViewRowEventArgs e) { }

    public void cargarTabla(){
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT DISTINCT O.OPCS_ID, O.OPCS_NOMBRE, C.CATS_NOMBRE, O.OPCS_URL, O.OPCS_ESTADO FROM OPCION_SISTEMA O, CATEGORIA_SISTEMA C WHERE O.CATS_ID = C.CATS_ID ORDER BY O.OPCS_ID ";

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
}
