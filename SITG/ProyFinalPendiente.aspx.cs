﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;

public partial class ProyFinalPendiente : Conexion
{
    Conexion con = new Conexion();
    string evaluador;
    int EvaAsignado;
    int contadorjur;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!Page.IsPostBack)
        {
            BuscarDocumentos();
            Consulta.Visible = true;
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        

    }

    /*Evento que se encargar de mostrar el formulario y llamar a cada evento*/
    protected void GVconsulta_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Asignar")
        {
            string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
            Consulta.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsulta.Rows[index];
            Metodo.Value = row.Cells[0].Text; //obtiene id del trabajo final
            Mostrarprof.Visible = true;
            AnteEvaluador(); //consultamos el evaluador del anteproyecto
            EvaluadorAsignado();//consultamos si el evaluador del anteproyecto ya fue asignado como jurado
            Consulta.Visible = false;
            //si el evaludor del anteproyecto no ha sido asignado entre al if
            if (EvaAsignado != 1)
            {
                string texto = "";
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + evaluador + "', '" + Metodo.Value + "', '1')";
                Ejecutar(texto, sql);
                string texto2 = "";
                string sql2 = "update proyecto_final set pf_jur1='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar(texto2, sql2);
            }
            cargarJurado1();
            Jurado1.Visible = true;
            cargarJurado2();
            Jurado2.Visible = true;
            cargarJurado3();
            Jurado3.Visible = true;
            ProfDisponible();
            IBregresar.Visible = true;
            DDLprofesores.Visible = true;
            BTconsultar.Visible = true;

        }
    }



    /*metodos que verifican*/



    /*metodo que verifica quien es el evaluador*/
    private void AnteEvaluador()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select usu_username from evaluador where apro_codigo='"+Metodo.Value+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                evaluador = drc1.GetInt32(0).ToString();
            }
            drc1.Close();
        }
    }

    /*Metodo que verifica si  el evaluador del anteproyecto ya fue asignado*/
    private void EvaluadorAsignado()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select * from jurado where usu_username='"+evaluador+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                EvaAsignado = 1;
            }
            drc1.Close();
        }
    }

    /*Metodo que verifica cantidad de jurado asignados en el proyecto seleccionado*/
    private void CantidadJur()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select count(*) from jurado where ppro_codigo='"+Metodo.Value+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {

                contadorjur = drc1.GetInt32(0);
                contadorjur=contadorjur+1;
            }
            drc1.Close();
        }
    }




    /*Metodo que consulta los proyectos finales que falta por asignar jurados*/
    protected void BuscarDocumentos()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select PPRO_CODIGO, PF_TITULO, PF_FECHA from proyecto_final where (pf_jur1='PENDIENTE' or pf_jur2='PENDIENTE' or pf_jur3='PENDIENTE') and pf_aprobacion='APROBADO'";

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



    /*Metodos que consultan la informacion de cada jurado*/


    /*Informacion jurado 1 (evaluador)*/
    protected void GVjurado1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVjurado1.PageIndex = e.NewPageIndex;
        cargarJurado1();
    }
    protected void GVjurado1_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarJurado1()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select j.jur_id, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as nombre, u.usu_correo, j.ppro_codigo from jurado j, usuario u where j.jur_num='1' and ppro_codigo='" + Metodo.Value + "' and u.usu_username = j.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVjurado1.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;

                }
                GVjurado1.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }


    /*Informacion jurado 2*/
    protected void GVjurado2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVjurado2.PageIndex = e.NewPageIndex;
        cargarJurado2();
    }
    protected void GVjurado2_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarJurado2()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select j.jur_id, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as nombre, u.usu_correo, j.ppro_codigo from jurado j, usuario u where j.jur_num='2' and ppro_codigo='" + Metodo.Value + "' and u.usu_username = j.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVjurado2.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;

                }
                GVjurado2.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*Metodo que elimina al jurado 2 en caso de ser necesario*/
    protected void GVjurado2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string id = GVjurado2.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from jurado where jur_id='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                cargarJurado2();
                ProfDisponible();
                string texto3 = "";
                string sql3 = "update proyecto_final set pf_jur2='PENDIENTE' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar(texto3, sql3);
            }
        }
    }


    /*Informacion jurado 3*/
    protected void GVjurado3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVjurado3.PageIndex = e.NewPageIndex;
        cargarJurado3();
    }
    protected void GVjurado3_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void cargarJurado3()
    {
        string sql = "";
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "select j.jur_id, CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as nombre, u.usu_correo, j.ppro_codigo from jurado j, usuario u where j.jur_num='3' and ppro_codigo='" + Metodo.Value + "' and u.usu_username = j.usu_username";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVjurado3.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.ForeColor = System.Drawing.Color.Red;

                }
                GVjurado3.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*Metodo que elimina al jurado 3 en caso de ser necesario*/
    protected void GVjurado3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string id = GVjurado3.Rows[e.RowIndex].Cells[0].Text;
            string sql = "Delete from jurado where jur_id='" + id + "'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                cargarJurado3();
                ProfDisponible();
                string texto3 = "";
                string sql3 = "update proyecto_final set pf_jur3='PENDIENTE' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar(texto3, sql3);
            }
        }
    }


    /*Metodos que se encargan de consultar la informacion de un profesor seleccionado*/
    protected void InfProfesor(object sender, EventArgs e)
    {
        CargarInfoProfesor();
        GVinfprof.Visible = true;
    }
    private void CargarInfoProfesor()
    {
        List<ListItem> list = new List<ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select usu_username,usu_telefono, usu_direccion, usu_correo  from usuario  where usu_username='" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "'";
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
    protected void GVinfprof_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void consultarprofesor(object sender, EventArgs e)
    {
        if (DDLprofesores.SelectedIndex.Equals(0))
        {
            Linfo.Text = "Seleccione un profesor.";
            infoprofesor.Visible = false;
        }
        else
        {
            CargarInfoProfesor();
            infoprofesor.Visible = true;
            BTconsultar.Enabled = false;
            Linfo.Text = "";
        }

    }


    /*Metodos que trae los profesores al drop down list*/
    protected void ProfDisponible()
    {   
        string sql = "Select distinct u.usu_username ,CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) from profesor p, usuario u, jurado j where u.usu_username=p.usu_username and u.USU_ESTADO='ACTIVO' and u.usu_username not in (select j.usu_username from jurado j where u.usu_username = j.usu_username and j.ppro_codigo='"+Metodo.Value+"')";
        DDLprofesores.Items.Clear();
        DDLprofesores.Items.AddRange(con.cargardatos(sql));
        DDLprofesores.Items.Insert(0, "Seleccione un profesor");
    }

    /*Metodo cancelar asignar jurado y regresar a la ventana principal de asignación*/

    protected void regresar(object sender, EventArgs e)
    {
        BuscarDocumentos();
        Consulta.Visible = true;
        IBregresar.Visible = false;
        DDLprofesores.Visible = false;
        BTconsultar.Visible = false;
        Jurado1.Visible = false;
        Jurado2.Visible = false;
        Jurado3.Visible = false;
        Linfo.Text = "";

    }

    protected void cancelarasignar(object sender, EventArgs e)
    {
        infoprofesor.Visible = false;
        BTconsultar.Enabled = true;
        Linfo.Text = "";
        DDLprofesores.SelectedIndex = 0;

    }

    



    /*Aquí iria las descargas*/
    protected void DescargaHV(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select PROF_DOCUMENTO, PROF_NOMARCHIVO, PROF_TIPO FROM PROFESOR WHERE USU_USERNAME='" + id + "'";
        OracleConnection conn = con.crearConexion();
        if (conn != null)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader())
                {
                    drc1.Read();
                    contentype = drc1["PROF_TIPO"].ToString();
                    fileName = drc1["PROF_NOMARCHIVO"].ToString();
                    bytes = (byte[])drc1["PROF_DOCUMENTO"];
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




    /*Metodos que inserta o actualiza informacion en la base de datos*/

    /*Metodo que inserta en la base de datos*/
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

    /*Metodo que inserta en la tabla jurado*/
    protected void asignarjurado(object sender, EventArgs e)
    {
        string fecha = DateTime.Now.ToString("yyyy/MM/dd, HH:mm:ss");
        string texto = "";
        CantidadJur();
        if (contadorjur <= 3)
        {
            if (contadorjur == 2)
            {
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "', '" + contadorjur + "')";
                Ejecutar(texto, sql);
                infoprofesor.Visible = false;
                BTconsultar.Enabled = true;
                Linfo.Text = "";
                DDLprofesores.SelectedIndex = 0;
                cargarJurado2();
                Jurado2.Visible = true;
                string texto2 = "";
                string sql2 = "update proyecto_final set pf_jur2='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar(texto2, sql2);
                ProfDisponible();
            }
            if (contadorjur == 3)
            {
                string sql = "insert into jurado (jur_id, jur_fecha, usu_username, ppro_codigo, jur_num) values (JURADOID.nextval,TO_DATE( '" + fecha + "', 'YYYY-MM-DD HH24:MI:SS'),'" + DDLprofesores.Items[DDLprofesores.SelectedIndex].Value + "', '" + Metodo.Value + "', '" + contadorjur + "')";
                Ejecutar(texto, sql);
                infoprofesor.Visible = false;
                BTconsultar.Enabled = true;
                Linfo.Text = "";
                DDLprofesores.SelectedIndex = 0;
                cargarJurado3();
                Jurado3.Visible = true;
                string texto3 = "";
                string sql3 = "update proyecto_final set pf_jur3='ASIGNADO' where ppro_codigo='" + Metodo.Value + "' ";
                Ejecutar(texto3, sql3);
                ProfDisponible();
            }

        }


    }


}