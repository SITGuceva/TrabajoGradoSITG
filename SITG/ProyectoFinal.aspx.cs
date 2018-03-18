using System;
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
    string codprop;
    string titulo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!Page.IsPostBack)
        {
            BloqueoSubir();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        ValidarPagos();
        RevisarProp(); //Recoge el codigo de la propuesta y el titulo
        RevisarProyectoF();// Metodo que revisa si ya subió un proyecto final
        RevisarAprobacion();// Metodo que revisa si ya subió un proyecto final
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Bgenerar);

    }

    private void BloqueoSubir()
    {
        LBSubir_pfinal.Enabled = false;
        LBConsulta_pfinal.Enabled = false;
        LBGenerar.Enabled = false;
        LBSubir_pfinal.ForeColor = System.Drawing.Color.Gray;
        LBConsulta_pfinal.ForeColor = System.Drawing.Color.Gray;
        LBGenerar.ForeColor = System.Drawing.Color.Gray;
        Linfo.Text = "No ha enviado o no ha sido aprobados los pagos requeridos para subir el proyecto final para uno o mas integrantes";

    }

    private void ValidarPagos()
    {
        List<System.Web.UI.WebControls.ListItem> list = new List<System.Web.UI.WebControls.ListItem>();
        try
        {
            OracleConnection conn = crearConexion();
            if (conn != null)
            {
                string sql = "select p.pag_estado from pagos p, estudiante e, anteproyecto a where e.usu_username = p.usu_username and p.pag_estado = 'APROBADO' and a.apro_codigo = e.prop_codigo and a.ant_estado = 'APROBADO' and a.apro_codigo" +
            " in (select a.apro_codigo from estudiante e, anteproyecto a where e.prop_codigo = a.apro_codigo and e.usu_username = '" + Session["id"] + "')";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new System.Web.UI.WebControls.ListItem(dr[0].ToString()));
                }
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Response.Write("Error al cargar la lista: " + ex.Message);
        }
        int aprobo=0;

        if (list.Count.Equals(1))
        {
            if (list[0].Equals("APROBADO"))
            {
                LBSubir_pfinal.Enabled = true;
                LBSubir_pfinal.ForeColor = System.Drawing.Color.Black;
            }
        }else {
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Text.Equals("APROBADO")) {
                    aprobo++;
                }
            }
            if (aprobo.Equals(list.Count)){

                LBSubir_pfinal.Enabled = true;
                LBConsulta_pfinal.Enabled = true;
                LBGenerar.Enabled = true;
                LBSubir_pfinal.ForeColor = System.Drawing.Color.Black;
                LBConsulta_pfinal.ForeColor = System.Drawing.Color.Black;
                LBGenerar.ForeColor = System.Drawing.Color.Black;
                Linfo.Text = "";
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }
    }

    /* Metodo que verifica si ya subio un proyecto final*/
    private void RevisarProyectoF()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select * from proyecto_final where ppro_codigo='"+codprop+"'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                LBSubir_pfinal.Enabled = false;
                LBSubir_pfinal.ForeColor = System.Drawing.Color.Gray;
            }
            drc1.Close();
        }
    }

    /* Metodo que verifica si el proyecto final fue aprobado*/
    private void RevisarAprobacion()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select * from proyecto_final where ppro_codigo='"+codprop+"' and pf_estado='APROBADO'";
            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                LBGenerar.Enabled = true;
                LBGenerar.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                LBGenerar.Enabled = false;
                LBGenerar.ForeColor = System.Drawing.Color.Gray;
            }
            drc1.Close();
        }
    }

    /* metodo que trae el codigo de la propuesta para el alumno en sesion*/
    private void RevisarProp()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "select p.prop_estado, p.prop_codigo, p.prop_titulo, s.sol_estado from propuesta p, estudiante e, solicitud_dir s where e.usu_username='" + Session["id"] + "' and s.prop_codigo=e.prop_codigo and p.prop_codigo = e.prop_codigo";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                string estado_propuesta = drc1.GetString(0).ToString();
                string estado_director = drc1.GetString(3).ToString();
                codprop = drc1.GetInt32(1).ToString();
                titulo = drc1.GetString(2).ToString();
            }
            drc1.Close();
        }
    }

    /*Metodos que manejan la interfaz del subir-consultar*/
    protected void Subir_pfinal(object sender, EventArgs e)
    {
        GenerarCer.Visible = false;
        Ingreso.Visible = true;
        Consulta.Visible = false;
        Observaciones.Visible = false;
        Linfo.Text = "";
    }

    //metodo que llama a la tabla del proyecto final
    protected void Consulta_pfinal(object sender, EventArgs e)
    {
        Consulta.Visible = true;
        Ingreso.Visible = false;
        GenerarCer.Visible = false;
        Observaciones.Visible = false;
        Linfo.Text = "";
        BuscarProyectoFinal();
    }

    //metodo que llama al boto para generar el certificado
    protected void Generar_pfinal(object sender, EventArgs e)
    {
        GenerarCer.Visible = true;
        Ingreso.Visible = false;
        Observaciones.Visible = false;
        Consulta.Visible = false;
        Linfo.Text = "";
        BuscarProyectoFinal();
    }

    //metodo que sube el proyecto final (documento);
    protected void Guardar(object sender, EventArgs e)
    {
        DateTime fecha = DateTime.Today;
        if (FUdocumento.HasFile)
        {
            string filename = Path.GetFileName(FUdocumento.PostedFile.FileName);
            string contentType = FUdocumento.PostedFile.ContentType;
            using (Stream fs = FUdocumento.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    OracleConnection conn = con.crearConexion();
                    if (conn != null)
                    {
                        string query = "insert into proyecto_final (ppro_codigo, pf_titulo, pf_documento, pf_nomarchivo, pf_tipo, pf_fecha) values (:ppro_codigo ,:pf_titulo, :Data, :pf_nomarchivo, :pf_tipo, :pf_fecha)";
                        using (OracleCommand cmd = new OracleCommand(query))
                        {
                            cmd.Connection = conn;
                            cmd.Parameters.Add(":ppro_codigo", codprop);
                            cmd.Parameters.Add(":pf_titulo", titulo);
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
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = "El proyecto final se ha satisfatoriamente";
            Ingreso.Visible = false;
            LBSubir_pfinal.Enabled = false;
            LBSubir_pfinal.ForeColor = System.Drawing.Color.Gray;
        }
        else
        {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe elegir un archivo";
        }


    }

    /*Metodos que realizan la funcionalidad de consultar el proyectofinal*/
    protected void BuscarProyectoFinal()
    {
        List<System.Web.UI.WebControls.ListItem> list = new List<System.Web.UI.WebControls.ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                string sql = "select pf.ppro_codigo, pf.pf_titulo, pf.pf_aprobacion, pf.pf_fecha, pf.pf_jur1, pf.pf_jur2, pf.pf_jur3 from proyecto_final pf, estudiante e where pf.ppro_codigo = e.prop_codigo and e.usu_username ='" + Session["id"] + "'";

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

    /*Metodos que descarga el proyecto final subido*/
    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName = "", contentype = "";
        string sql = "select ANP_DOCUMENTO, ANP_NOMARCHIVO, ANP_TIPO FROM ANTEPROYECTO WHERE APRO_CODIGO=" + id + "";

        OracleConnection conn = con.crearConexion();
        if (conn != null)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandText = sql;
                using (OracleDataReader drc1 = cmd.ExecuteReader())
                {
                    drc1.Read();
                    contentype = drc1["ANP_TIPO"].ToString();
                    fileName = drc1["ANP_NOMARCHIVO"].ToString();
                    bytes = (byte[])drc1["ANP_DOCUMENTO"];

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

    /*Metodos que se encargan de consultar las observaciones que tiene el proyecto final del estudiante en la sesión*/
    protected void GVconsulta_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "buscar")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVconsulta.Rows[index];
            ResultadoObservaciones();
            Observaciones.Visible = true;
        }
    }
    protected void GVobservacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVobservacion.PageIndex = e.NewPageIndex;
        ResultadoObservaciones();
    }
    protected void GVobservacion_RowDataBound(object sender, GridViewRowEventArgs e) { }
    public void ResultadoObservaciones()
    {
        string sql = "";
        List<System.Web.UI.WebControls.ListItem> list = new List<System.Web.UI.WebControls.ListItem>();
        try
        {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null)
            {
                sql = "SELECT PFOBS_DESCRIPCION, PFOBS_REALIZADA FROM PF_OBSERVACIONES  WHERE PPRO_CODIGO ='" + codprop + "'";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVobservacion.DataSource = dataTable;
                    int cantfilas = Convert.ToInt32(dataTable.Rows.Count.ToString());
                    Linfo.Text = "Cantidad de filas encontradas: " + cantfilas;
                }
                GVobservacion.DataBind();

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }

    protected void GenerarPdf(object sender, EventArgs e)
    {
        string sql = "select pf_titulo from proyecto_final where ppro_codigo='" + codprop + "'";
        List<string> list = con.consulta(sql, 1, 0);
        string titulo = list[0];

        sql = "select  CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as director from solicitud_dir s, estudiante e, usuario u  where s.prop_codigo = e.prop_codigo and e.usu_username = '" + Session["id"] + "' and s.usu_username = u.usu_username ";
        List<string> list2 = con.consulta(sql, 1, 0);
        string director = list2[0];

        sql = "select p.prog_nombre from programa p, estudiante e where e.prog_codigo= p.prog_codigo and e.usu_username='" + Session["id"] + "'";
        List<string> list4 = con.consulta(sql, 1, 0);
        string comite = list4[0];

        sql = "select CONCAT(CONCAT(u.usu_nombre, ' '), u.usu_apellido) as estudiante from estudiante e, proyecto_final p, usuario u where e.prop_codigo = p.ppro_codigo and u.usu_username = e.usu_username and p.ppro_codigo = '" + codprop + "'";
        List<string> list3 = con.consulta(sql, 1, 0);
        string integrante = "";
        if (list3.Count.Equals(1))   {
            integrante = list3[0];
        } else{
            for (int i = 0; i < list3.Count; i++){
                integrante += list3[i] + ", ";
            }
        }


         using (var ms = new MemoryStream())
         {
             using (var doc = new Document(PageSize.A4))
             {
                //Document doc = new Document(PageSize.A4);
                // PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
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
}