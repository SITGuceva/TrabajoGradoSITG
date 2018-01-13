using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Nuevo(object sender, EventArgs e)
    {
        Ingreso.Visible = true;
        LBacpetar.ForeColor = System.Drawing.Color.Black;
        LBacpetar.Enabled = true;
        LBcancelar.ForeColor = System.Drawing.Color.Black;
        LBcancelar.Enabled = true;
        Consulta.Visible = false;
        Resultado.Visible = false;
    }

    protected void Editar(object sender, EventArgs e)
    {
        LBacpetar.ForeColor = System.Drawing.Color.Black;
        LBacpetar.Enabled = true;
        LBcancelar.ForeColor = System.Drawing.Color.Black;
        LBcancelar.Enabled = true;
        Consulta.Visible = true;
        Ingreso.Visible = true;
        Resultado.Visible = false;
    }

    protected void Consultar(object sender, EventArgs e)
    {
        LBacpetar.ForeColor = System.Drawing.Color.LightGray;
        LBacpetar.Enabled = false;
        LBcancelar.ForeColor = System.Drawing.Color.Black;
        LBcancelar.Enabled = true;
        Consulta.Visible = false;
        Ingreso.Visible = false;
        Resultado.Visible = true;
        cargarTabla();
    }

    protected void Cancelar(object sender, EventArgs e)
    {

        LBacpetar.ForeColor = System.Drawing.Color.LightGray;
        LBacpetar.Enabled = false;
        LBcancelar.ForeColor = System.Drawing.Color.LightGray;
        LBcancelar.Enabled = false;
        Consulta.Visible = false;
        Ingreso.Visible = false;
        Resultado.Visible = false;
    }

    protected void Buscar(object sender, EventArgs e)
    {
        cargarTabla();
    }
    /*evento que cambia la pagina de la tabla*/
    protected void gvSysRol_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSysRol.PageIndex = e.NewPageIndex;
        cargarTabla();// la consulta a la base de datos

    }

    /*evento que se llama cuando llenga las columnas*/
    protected void gvSysRol_RowDataBound(object sender, GridViewRowEventArgs e) { }

    private void hola() { }


    public void cargarTabla()
    {
        /*     string sql = "";
           List<ListItem> list = new List<ListItem>();
            try
             {
                 OracleConnection conn = crearConexion();
                 OracleCommand cmd = null;
                if (conn != null)
                 {
                     //Valido que las fechas no estén vacías
                     if (string.IsNullOrEmpty(tbFechaIni.Text) == false && string.IsNullOrEmpty(tbFechaFin.Text) == false)
                     {
                         int cupon = string.IsNullOrEmpty(tbCupon.Text) ? 0 : Convert.ToInt32(tbCupon.Text);
                         int entidad = string.IsNullOrEmpty(tbEntidad.Text) ? 0 : Convert.ToInt32(tbEntidad.Text);
                         DateTime fechaini = DateTime.Parse(tbFechaIni.Text);
                         DateTime fechafin = DateTime.Parse(tbFechaFin.Text);
                         /*Valida que no este chequeado el filtro de horas*/
        /* if (!cbHora.Checked)
         {
             sql = "SELECT PROCESO,COD_ENTI, ID_PAGO, CUPON, TIP_CUP, VALOR, FECHA_PAGO, FECHA_INICIO, FECHA_FIN " +
                 "FROM LOGWS_REC_EMC WHERE CUPON = DECODE(" + cupon + ",0,CUPON," + cupon + ")" +
                     " AND   TRUNC(to_date(FECHA_FIN)) BETWEEN TRUNC(TO_DATE(:fechaini)) AND TRUNC(TO_DATE(:fechafin))" +
                     " AND COD_ENTI = DECODE('" + entidad + "', 0, COD_ENTI, '" + entidad + "') ";
         }
         cmd = new OracleCommand(sql, conn);
         cmd.CommandType = CommandType.Text;
         //las fechas hay que pasarlas como parametros para que reconosca el formato date
         cmd.Parameters.Add("fechaini", OracleDbType.Date).Value = fechaini;
         cmd.Parameters.Add("fechafin", OracleDbType.Date).Value = fechafin;
         using (OracleDataReader reader = cmd.ExecuteReader())
         {*/
        DataTable dataTable = new DataTable();
        // dataTable.Load(reader);
        gvSysRol.DataSource = dataTable;
        // int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
        //Label7.Text = "Cantidad de filas encontradas: " + cantfilas;
        //  }

        gvSysRol.DataBind();

        /*   }
           conn.Close();
       }
   }
   catch (Exception ex)
   {
       //label resultado
       Label7.Text = "Error al cargar la lista: " + ex.Message;
   }*/
    }
}