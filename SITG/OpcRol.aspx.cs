using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OpcRol : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e) {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
       else
        {
            if (!IsPostBack)
            {
                DDLrolbuscar.Items.Clear();
                string sql = "SELECT ROL_ID, ROL_NOMBRE FROM ROL";
                    DDLrolbuscar.Items.AddRange(con.cargardatos(sql));
            }
        }
   
    }

    protected void Buscar(object sender, EventArgs e){      
        if (Metodo.Value.Equals("1")){
            Ingreso.Visible = true;
            Botones.Visible = true;
            DDLopcs.Items.Clear();
            string sql = "SELECT O.OPCS_ID,O.OPCS_NOMBRE FROM OPCION_SISTEMA O WHERE O.OPCS_ID NOT IN (SELECT R.OPCS_ID FROM OPCION_ROL R WHERE  R.ROL_ID='" + DDLrolbuscar.Items[DDLrolbuscar.SelectedIndex].Value.ToString() + "') ORDER BY O.OPCS_ID";
            DDLopcs.Items.AddRange(con.cargardatos(sql));
        }
        else if (Metodo.Value.Equals("2"))
        {
            DDLansopcs.Items.Clear();
            string sql = "SELECT R.OPCROL_ID, S.OPCS_NOMBRE FROM OPCION_ROL R, OPCION_SISTEMA S WHERE  R.ROL_ID='" + DDLrolbuscar.Items[DDLrolbuscar.SelectedIndex].Value.ToString() + "' AND S.OPCS_ID = R.OPCS_ID ORDER BY R.OPCS_ID";
            DDLansopcs.Items.AddRange(con.cargardatos(sql));
            Lansid.Text = DDLansopcs.Items[DDLansopcs.SelectedIndex].Value.ToString();

            DDLopactu.Items.Clear();
            string sql1 = "SELECT O.OPCS_ID,O.OPCS_NOMBRE FROM OPCION_SISTEMA O WHERE O.OPCS_ID NOT IN (SELECT R.OPCS_ID FROM OPCION_ROL R WHERE  R.ROL_ID='" + DDLrolbuscar.Items[DDLrolbuscar.SelectedIndex].Value.ToString() + "') ORDER BY O.OPCS_ID";
            DDLopactu.Items.AddRange(con.cargardatos(sql1));          
            Actualizar.Visible = true;
            Botones.Visible = true;
        }
        else if (Metodo.Value.Equals("3"))
        {
            Resultado.Visible = true;
            cargarTabla();
        }
        else if (Metodo.Value.Equals("4"))
        {
            DDLopcrol.Items.Clear();
            string sql = "SELECT O.OPCROL_ID, S.OPCS_NOMBRE  FROM OPCION_ROL O, OPCION_SISTEMA S WHERE S.OPCS_ID = O.OPCS_ID AND O.ROL_ID='" + DDLrolbuscar.Items[DDLrolbuscar.SelectedIndex].Value.ToString() + "' ORDER BY O.OPCROL_ID";
            DDLopcrol.Items.AddRange(con.cargardatos(sql));
            Eliminar.Visible = true;
            Botones.Visible = true;
        }    
    }

    protected void Crear(object sender, EventArgs e) {
        DDLrolbuscar.Items.Clear();
        string sql = "SELECT ROL_ID, ROL_NOMBRE FROM ROL";
        DDLrolbuscar.Items.AddRange(con.cargardatos(sql));
        Metodo.Value = "1";
        Linfo.Text = "";
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
        Resultado.Visible = false;
        Botones.Visible = false;
    }

    protected void Modificar(object sender, EventArgs e)
    {
        DDLrolbuscar.Items.Clear();
        string sql = "SELECT ROL_ID, ROL_NOMBRE FROM ROL";
        DDLrolbuscar.Items.AddRange(con.cargardatos(sql));
        Metodo.Value = "2";
        Linfo.Text = "";
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
        Resultado.Visible = false;
        Botones.Visible = false;
    }

    protected void Consultar(object sender, EventArgs e)
    {
        DDLrolbuscar.Items.Clear();
        string sql = "SELECT ROL_ID, ROL_NOMBRE FROM ROL";
        DDLrolbuscar.Items.AddRange(con.cargardatos(sql));
        Metodo.Value = "3";
        Linfo.Text = "";
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
        Resultado.Visible = false;
        Botones.Visible = false;
    }

    protected void Inhabilitar(object sender, EventArgs e)
    {
        DDLrolbuscar.Items.Clear();
        string sql = "SELECT ROL_ID, ROL_NOMBRE FROM ROL";
        DDLrolbuscar.Items.AddRange(con.cargardatos(sql));
        Metodo.Value = "4";
        Linfo.Text = "";
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
        Resultado.Visible = false;
        Botones.Visible = false;
    }

    protected void Limpiar(object sender, EventArgs e)
    {
        Ingreso.Visible = false;
        Actualizar.Visible = false;
        Eliminar.Visible = false;
        Resultado.Visible = false;
        Botones.Visible = false;
        Linfo.Text = "";   
    }
   
    protected void Aceptar(object sender, EventArgs e){
        string sql = "";
        string texto = "";
        if (Ingreso.Visible)
        {
            if (string.IsNullOrEmpty(TBid.Text) == true){
                Linfo.ForeColor = System.Drawing.Color.Red;
                Linfo.Text = "Los campos son obligatorios";
            }else{                      
                int id = Int32.Parse(TBid.Text);
                string op = DDLopcs.Items[DDLopcs.SelectedIndex].Value.ToString();
                int opcid = Int32.Parse(op);
                sql = "insert into OPCION_ROL (OPCROL_ID,OPCS_ID,ROL_ID) VALUES('" + id + "', '" + opcid + "', '" + DDLrolbuscar.Items[DDLrolbuscar.SelectedIndex].Value.ToString() + "')";
                texto = "Datos guardados satisfactoriamente";
                Ejecutar(sql, texto);
                DDLopcs.SelectedIndex = 1;
            }
        }else if (Actualizar.Visible){
         
            sql = "UPDATE OPCION_ROL SET OPCS_ID= '" + DDLopactu.Items[DDLopactu.SelectedIndex].Value.ToString() + "', ROL_ID = '" + DDLrolbuscar.Items[DDLrolbuscar.SelectedIndex].Value.ToString() + "'  WHERE OPCROL_ID = '" + DDLansopcs.Items[DDLansopcs.SelectedIndex].Value.ToString() + "'";
            texto = "Datos modificados satisfactoriamente";
            Ejecutar( sql,texto);
        }else if (Eliminar.Visible)
        {
            sql = "UPDATE OPCION_ROL SET OPCROL_ESTADO = '" + DDLestado.Items[DDLestado.SelectedIndex].Value.ToString() + "' WHERE OPCROL_ID = '" + DDLopcrol.Items[DDLopcrol.SelectedIndex].Value.ToString() + "'";
            texto = "La opcion del rol se ha puesto "+ DDLestado.Items[DDLestado.SelectedIndex].Value.ToLower() + " satisfactoriamente";
            Linfo.Text = sql;
            Ejecutar( sql,texto);
        }
    }

    private void Ejecutar( string sql,string texto)
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
    }


    protected void gvSysRol_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSysRol.PageIndex = e.NewPageIndex;
        cargarTabla();
    }

    protected void gvSysRol_RowDataBound(object sender, GridViewRowEventArgs e) { }

    public void cargarTabla()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT DISTINCT C.OPCROL_ID,O.OPCS_NOMBRE ,C.OPCROL_ESTADO  FROM OPCION_SISTEMA O, OPCION_ROL C WHERE O.OPCS_ID = C.OPCS_ID AND C.ROL_ID = '"+ DDLrolbuscar.Items[DDLrolbuscar.SelectedIndex].Value.ToString() + "' ORDER BY C.OPCROL_ID ";

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