﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;
using iTextSharp.text.pdf;
using iTextSharp.text;

public partial class ProyectoFinal : Conexion
{
    Conexion con = new Conexion();
    int responsable;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null) {
            Response.Redirect("Default.aspx");
        }
        if (!Page.IsPostBack) {
           
            BloqueoSubir();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        RevisarProp(); //Recoge el codigo de la propuesta y el titulo
        ValidarPagos();

        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Bgenerar);
        scriptManager.RegisterPostBackControl(this.GVconsulta);

    }

    /*Validaciones*/
    private void BloqueoSubir()
    {
        LBSubir_pfinal.Enabled = false;
        LBConsulta_pfinal.Enabled = false;
        LBGenerar.Enabled = false;
        LBSubir_pfinal.ForeColor = System.Drawing.Color.Gray;
        LBConsulta_pfinal.ForeColor = System.Drawing.Color.Gray;
        LBGenerar.ForeColor = System.Drawing.Color.Gray; 
    }
    
    /*Metodo que valida el pago y el anteproyecto*/
    private void ValidarPagos()
    {
        List<System.Web.UI.WebControls.ListItem> list = new List<System.Web.UI.WebControls.ListItem>();
        try {
            OracleConnection conn = crearConexion();
            if (conn != null){
                string sql = "select p.pag_estado from pagos p, estudiante e, anteproyecto a where e.usu_username = p.usu_username and p.pag_estado = 'APROBADO' and a.apro_codigo = e.prop_codigo and a.ant_estado = 'APROBADO' and a.apro_codigo" +
                " in (select a.apro_codigo from estudiante e, anteproyecto a where e.prop_codigo = a.apro_codigo and e.usu_username = '" + Session["id"] + "')";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read()){
                    list.Add(new System.Web.UI.WebControls.ListItem(dr[0].ToString()));
                }
            }
            conn.Close();
        } catch (Exception ex) {
            Response.Write("Error al cargar la lista: " + ex.Message);
        }
        int aprobo=0;      

        if (list.Count.Equals(1)){
            if (list[0].ToString(). Equals("APROBADO")) {
                RevisarProyectoF();
            } else {
                Linfo.Text = "No ha enviado o no ha sido aprobado los pagos requeridos para subir el proyecto final.";
            }
        }else {
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Text.Equals("APROBADO")) {
                    aprobo++;
                }
            }
            if (aprobo.Equals(list.Count)){
                Linfo.Text = "";
                RevisarProyectoF();
            }else {
                Linfo.Text = "No ha enviado o no ha sido aprobados los pagos requeridos para subir el proyecto final los integrantes";
            }
        }
    }
  
    /* Metodo que verifica si ya subio un proyecto final y que el estado que le pone el director*/
    private void RevisarProyectoF()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "select Pf_Aprobacion from proyecto_final where ppro_codigo='"+Codigop.Value+"'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){

                if (drc1[0].Equals("RECHAZADO"))
                {
                    LBSubir_pfinal.Enabled = true;
                    LBSubir_pfinal.ForeColor = System.Drawing.Color.Black;
                    Metodo.Value = "ACTUALIZA";
                    responsable = 1;
                } else {
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "No puede subir otro proyecto final porque esta en proceso de revisión.";
                    CalificarProyectoF();
                }
                LBConsulta_pfinal.Enabled = true;
                LBConsulta_pfinal.ForeColor = System.Drawing.Color.Black;  
            } else {
                LBSubir_pfinal.Enabled = true;
                LBSubir_pfinal.ForeColor = System.Drawing.Color.Black;
            }
            drc1.Close();
        }
    }
    private void CalificarProyectoF()
    {
        string sql = "select Pf_Jur1, Pf_Jur2, Pf_Jur3 from proyecto_final where ppro_codigo='"+Codigop.Value+"'";
        List<string> list = con.consulta(sql, 3, 0);

        int aprobado = 0, rechazado = 0;
        string sql2="";

        for (int i=0; i < list.Count; i++)  {
            if (list[i].Equals("APROBADO")) {
                aprobado++;
            }else if (list[i].Equals("RECHAZADO")) {
                rechazado++;
            }
        }

        if (rechazado.Equals(3)){
            sql2 = "update proyecto_final set pf_estado='RECHAZADO' where ppro_codigo='" + Codigop.Value + "'";
        }
        else if (rechazado.Equals(2)){
            sql2 = "update proyecto_final set pf_estado='RECHAZADO' where ppro_codigo='" + Codigop.Value + "'";
        }
        else if (aprobado.Equals(2)) {
            sql2 = "update proyecto_final set pf_estado='APROBADO' where ppro_codigo='" + Codigop.Value + "'";
        }
        else if (aprobado.Equals(3)) {
            sql2 = "update proyecto_final set pf_estado='APROBADO' where ppro_codigo='" + Codigop.Value + "'";
        }

        if(string.IsNullOrEmpty(sql2) == false) {
            Ejecutar("", sql2);
            RevisarAprobacion();
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
    }

    /* Metodo que verifica si el proyecto final fue aprobado*/
    private void RevisarAprobacion(){
        string estado="";
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "select Pf_Estado from proyecto_final where ppro_codigo='"+Codigop.Value+"' ";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows){
                estado = drc1[0].ToString();
            } 
            drc1.Close();
        }

        if (estado.Equals("APROBADO")){
            LBGenerar.Enabled = true;
            LBGenerar.ForeColor = System.Drawing.Color.Black;
        }else if(estado.Equals("RECHAZADO")){
            LBSubir_pfinal.Enabled = true;
            LBSubir_pfinal.ForeColor = System.Drawing.Color.Black;
            Metodo.Value = "ACTUALIZA";
            responsable = 2;
        }
    }

    /* metodo que trae el codigo de la propuesta para el alumno en sesion*/
    private void RevisarProp()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null){
            string sql = "select p.prop_codigo, p.prop_titulo from propuesta p, estudiante e where e.usu_username = '" + Session["id"] + "' and p.prop_codigo = e.prop_codigo";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows) {
                Codigop.Value = drc1.GetInt32(0).ToString();
                Titulo.Value = drc1.GetString(1).ToString();
            }
            drc1.Close();
        }
    }

    /*Metodos que manejan la interfaz del subir-consultar- generar*/
    protected void Subir_pfinal(object sender, EventArgs e)
    {
        GenerarCer.Visible = false;
        Ingreso.Visible = true;
        Consulta.Visible = false;
        Observaciones.Visible = false;
        ConsultaCrit.Visible = false;
        Linfo.Text = "";
    }
    protected void Consulta_pfinal(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        Ingreso.Visible = false;
        GenerarCer.Visible = false;
        Observaciones.Visible = false;
        ConsultaCrit.Visible = false;
        Linfo.Text = "";
        BuscarProyectoFinal();
    }
    protected void Generar_pfinal(object sender, EventArgs e)
    {
        GenerarCer.Visible = true;
        Ingreso.Visible = false;
        Observaciones.Visible = false;
        Consulta.Visible = false;
        ConsultaCrit.Visible = false;
        Linfo.Text = "";
        BuscarProyectoFinal();
    }

    /*metodo que sube el proyecto final (documento)*/
    protected void Guardar(object sender, EventArgs e)
    {
        DateTime fecha = DateTime.Today;
        if (FUdocumento.HasFile){
            if (Metodo.Value.Equals("ACTUALIZA")) {
                string filename = Path.GetFileName(FUdocumento.PostedFile.FileName);
                string contentType = FUdocumento.PostedFile.ContentType;
                using (Stream fs = FUdocumento.PostedFile.InputStream){
                    using (BinaryReader br = new BinaryReader(fs)) {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        OracleConnection conn = con.crearConexion();
                        if (conn != null) {
                            string query = "";
                            if (responsable.Equals(1)) { query = "update proyecto_final set  pf_documento=:Data, pf_nomarchivo=:pf_nomarchivo, pf_tipo= :pf_tipo, pf_fecha=:pf_fecha, pf_aprobacion='PENDIENTE' where ppro_codigo='"+Codigop.Value+"'";}
                            else if (responsable.Equals(2)) {
                                query = "update proyecto_final set  pf_documento=:Data, pf_nomarchivo=:pf_nomarchivo, pf_tipo= :pf_tipo, pf_fecha=:pf_fecha, pf_estado='PENDIENTE', pf_jur1='ASIGNADO', pf_jur2='ASIGNADO', pf_jur3='ASIGNADO' where ppro_codigo='" + Codigop.Value + "'";
                                string revision = "update jurado set jur_revisado='REVISADO' where ppro_codigo='" + Codigop.Value + "'";
                                Ejecutar("", revision);
                            }
                            using (OracleCommand cmd = new OracleCommand(query)) {
                                cmd.Connection = conn;
                                cmd.Parameters.Add(":Data", bytes);
                                cmd.Parameters.Add(":pf_nomarchivo", filename);
                                cmd.Parameters.Add(":pf_tipo", contentType);
                                cmd.Parameters.Add(":pf_fecha", fecha);
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
            } else { 
                string filename = Path.GetFileName(FUdocumento.PostedFile.FileName);
                string contentType = FUdocumento.PostedFile.ContentType;
                using (Stream fs = FUdocumento.PostedFile.InputStream) {
                    using (BinaryReader br = new BinaryReader(fs)){
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        OracleConnection conn = con.crearConexion();
                        if (conn != null) {
                            string query = "insert into proyecto_final (ppro_codigo, pf_titulo, pf_documento, pf_nomarchivo, pf_tipo, pf_fecha) values (:ppro_codigo ,:pf_titulo, :Data, :pf_nomarchivo, :pf_tipo, :pf_fecha)";
                            using (OracleCommand cmd = new OracleCommand(query)){
                                cmd.Connection = conn;
                                cmd.Parameters.Add(":ppro_codigo", Codigop.Value);
                                cmd.Parameters.Add(":pf_titulo", Titulo.Value);
                                cmd.Parameters.Add(":Data", bytes);
                                cmd.Parameters.Add(":pf_nomarchivo", filename);
                                cmd.Parameters.Add(":pf_tipo", contentType);
                                cmd.Parameters.Add(":pf_fecha", fecha);
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
            }
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = "El proyecto final se ha cargado satisfatoriamente";
            quitar();
        } else {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe elegir un archivo";
        }
    }
    private void quitar()
    {
        Ingreso.Visible = false;
        LBSubir_pfinal.Enabled = false;
        LBSubir_pfinal.ForeColor = System.Drawing.Color.Gray;
        LBConsulta_pfinal.Enabled = true;
        LBConsulta_pfinal.ForeColor = System.Drawing.Color.Black;
        Metodo.Value = "";
        responsable = 0;
    }

    /*Metodos que realizan la funcionalidad de consultar el proyectofinal*/
    protected void BuscarProyectoFinal()
    {
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "select pf.ppro_codigo, pf.pf_titulo, pf.pf_aprobacion, pf.pf_fecha, pf.pf_jur1, pf.pf_jur2, pf.pf_jur3, pf.pf_estado from proyecto_final pf, estudiante e where pf.ppro_codigo = e.prop_codigo and e.usu_username ='" + Session["id"] + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVconsulta.DataSource = dataTable;
                }
                GVconsulta.DataBind();
            }
            conn.Close();
        }  catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select PF_DOCUMENTO, PF_NOMARCHIVO, PF_TIPO FROM PROYECTO_FINAL WHERE PPRO_CODIGO=" + id + "";

        OracleConnection conn = con.crearConexion();
        if (conn != null){
            using (OracleCommand cmd = new OracleCommand(sql, conn)){
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader()){
                    drc1.Read();
                    contentype = drc1["PF_TIPO"].ToString();
                    fileName = drc1["PF_NOMARCHIVO"].ToString();
                    bytes = (byte[])drc1["PF_DOCUMENTO"];

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
    protected void GVconsulta_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "buscar"){
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsulta.Rows[index];
            ResultadoObservaciones();
            Observaciones.Visible = true;
            ResultadoConsulta();
            ConsultaCrit.Visible = true;
        }
    }

    /*Metodos que realizan la consulta de observaciones*/
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        ResultadoObservaciones();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void ResultadoObservaciones()
    {
        Linfo.Text = "";
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "SELECT PFOBS_CODIGO,PFOBS_DESCRIPCION, PFOBS_REALIZADA FROM PF_OBSERVACION  WHERE PPRO_CODIGO ='" + Codigop.Value + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                }
                GVobservacion.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    /*Evento del boton que genera el certificado*/
    protected void GenerarPdf(object sender, EventArgs e)
    {
        string sql = "select pf_titulo from proyecto_final where ppro_codigo='" + Codigop.Value + "'";
        List<string> list = con.consulta(sql, 1, 0);
        string titulo = list[0];

        sql = "select  CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director from solicitud_dir s, estudiante e, usuario u  where s.prop_codigo = e.prop_codigo and e.usu_username = '" + Session["id"] + "' and s.usu_username = u.usu_username ";
        List<string> list2 = con.consulta(sql, 1, 0);
        string director = list2[0];

        sql = "select p.prog_nombre from programa p, estudiante e where e.prog_codigo= p.prog_codigo and e.usu_username='" + Session["id"] + "'";
        List<string> list4 = con.consulta(sql, 1, 0);
        string comite = list4[0];

        sql = "select CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as estudiante from estudiante e, proyecto_final p, usuario u where e.prop_codigo = p.ppro_codigo and u.usu_username = e.usu_username and p.ppro_codigo = '" + Codigop.Value + "'";
        List<string> list3 = con.consulta(sql, 1, 0);
        string integrante = "";
        if (list3.Count.Equals(1))   {
            integrante = list3[0];
        } else{
            for (int i = 0; i < list3.Count; i++){
                integrante += list3[i] + ", ";
            }
        }

         using (var ms = new MemoryStream()) {
             using (var doc = new Document(PageSize.A4)) {
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                // Le colocamos el título y el autor
                doc.AddTitle("Acta de reunion");
                doc.AddCreator("Comite de trabajos de grado");
                doc.Open();

                // Creamos el tipo de Font que vamos utilizar
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 2, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Server.MapPath("/Images/uceva.jpg"));

                Paragraph saltoDeLinea1 = new Paragraph(" ");
                Paragraph titulo3 = new Paragraph("Comite de trabajos de grado de "+comite);
                titulo3.Alignment = Element.ALIGN_CENTER;
                Paragraph titulo4 = new Paragraph("Certifica");
                Paragraph Parrafo2 = new Paragraph("Que el/los estudiantes " + integrante + " con el trabajo de grado " + titulo + " con su director " + director + " fué aprobado por los jurados.");
                Parrafo2.Alignment = Element.ALIGN_LEFT;
                Paragraph Parrafo3 = new Paragraph("Este certificado debe ser refrendado por el comite de " + comite + " para ser llevado al CAU y programar la sustentación");
                Parrafo3.Alignment = Element.ALIGN_LEFT;
                titulo4.Alignment = Element.ALIGN_CENTER;
                Paragraph Parrafo4 = new Paragraph("___________________________");
                Parrafo4.Alignment = Element.ALIGN_LEFT;
                Paragraph Parrafo5 = new Paragraph("Comite de "+comite);
                Parrafo5.Alignment = Element.ALIGN_LEFT;
                doc.Add(saltoDeLinea1);
                doc.Add(saltoDeLinea1);
                doc.Add(titulo3);
                doc.Add(titulo4);
                doc.Add(saltoDeLinea1);
                doc.Add(saltoDeLinea1);
                doc.Add(saltoDeLinea1);
                doc.Add(Parrafo2);
                doc.Add(saltoDeLinea1);
                doc.Add(Parrafo3);
                doc.Add(saltoDeLinea1);
                doc.Add(saltoDeLinea1);
                doc.Add(saltoDeLinea1);
                doc.Add(saltoDeLinea1);
                doc.Add(Parrafo4);
                doc.Add(saltoDeLinea1);
                doc.Add(Parrafo5);
                //logo uceva pagina1
                img.ScaleToFit(125f, 60F);
                //Imagen - Movio en el eje de las Y
                img.SetAbsolutePosition(13, 794);
                doc.Add(img);
                doc.Close();
             }
             Response.Clear();
             Response.ContentType = "application/octet-stream";
             Response.AddHeader("content-disposition", "attachment;filename= ActaReunion.pdf");
             Response.Buffer = true;
             Response.Clear();
             var bytes = ms.ToArray();
             Response.OutputStream.Write(bytes, 0, bytes.Length);
             Response.OutputStream.Flush();
         }
    }

    /*Metodos que consultan si tiene criterios*/
    protected void GVcriterios_RowDataBound(object sender, GridViewRowEventArgs e) { }
    protected void GVcriterios_PageIndexChanging(object sender, GridViewPageEventArgs e){
        GVcriterios.PageIndex = e.NewPageIndex;
        ResultadoConsulta();
    }
    private void ResultadoConsulta()
    {
        Linfo.Text = "";
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
               string sql = " SELECT e.CRIT_CODIGO ,c.CRIT_NOMBRE, c.CRIT_TIPO FROM CRITERIOS c, EVALUA_CRITERIOS e , ESTUDIANTE u WHERE e.CRIT_CODIGO = c.CRIT_CODIGO and U.Prop_Codigo = E.Ppro_Codigo and E.Ppro_Codigo = '"+ Codigop.Value+ "' ORDER BY e.CRIT_CODIGO";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVcriterios.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                }
                GVcriterios.DataBind();
            }
            conn.Close();
        } catch (Exception ex) {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

}