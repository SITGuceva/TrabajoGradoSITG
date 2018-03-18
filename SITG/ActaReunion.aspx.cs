using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;
using System.Web.UI.WebControls;

public partial class ActaReunion : System.Web.UI.Page
{
    Conexion con = new Conexion();
    DataTable table;
    System.Data.DataRow row;
    DataRow rows;
    DataTable torden;

   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null){
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack){
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            DDLreu.Items.Clear();
            string sql = "SELECT r.reu_codigo FROM reunion r, profesor p WHERE r.reu_estado='ACTIVO' AND r.COM_CODIGO = p.COM_CODIGO and p.USU_USERNAME = '"+ Session["id"] + "'";
            DDLreu.Items.AddRange(con.cargarDDLid(sql));
            DDLreu.Items.Insert(0, "Seleccione");
            CargarAsistente();

            table = new DataTable();
            table.Columns.Add("NOMBRE", typeof(System.String));
            table.Columns.Add("CARGO", typeof(System.String));
            Session.Add("Tabla", table);

            torden = new DataTable("TOrden");
            torden.Columns.Add("ORDEN", typeof(System.String));
            torden.Columns.Add("DESCRIPCION", typeof(System.String));
            Session.Add("Orden", torden);
        }
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GVactas);
        scriptManager.RegisterPostBackControl(this.Bgenerar);
    }

    /*Metodos que manejan el fronted en el acta*/
    protected void LBgenerar_Click(object sender, EventArgs e)
    {
        Linfo.Text = "";
        SubirActa.Visible = false;
        Ingreso.Visible = true;
        Asistio.Value = "";
        Noasistio.Value = "";
        ConsultarActa.Visible = false;
        TBhasta.Text = "";
        TBdesde.Text = "";
    }
    protected void LBconsultar_Click(object sender, EventArgs e)
    {
        Ingreso.Visible = false;
        SubirActa.Visible = false;
        ConsultarActa.Visible = true;
        Asistio.Value = "";
        Noasistio.Value = "";
        Linfo.Text = "";
        TBhasta.Text = "";
        TBdesde.Text = "";
        GVactas.Visible = false;
    }
    protected void LBsubir_Click(object sender, EventArgs e)
    {
        DDLreunion2.Items.Clear();
        string sql = "SELECT r.reu_codigo FROM reunion r, profesor p WHERE r.reu_estado='ACTIVO' AND r.COM_CODIGO = p.COM_CODIGO and p.USU_USERNAME = '" + Session["id"] + "'";
        DDLreunion2.Items.AddRange(con.cargarDDLid(sql));
        DDLreunion2.Items.Insert(0, "Seleccione");
        Ingreso.Visible = false;
        Linfo.Text = "";
        Asistio.Value = "";
        Noasistio.Value = "";
        SubirActa.Visible = true;
        ConsultarActa.Visible = false;
        TBhasta.Text = "";
        TBdesde.Text = "";
    }

    /*Metodos que consultan los miembros del comite*/
    private void CargarAsistente()
    {
        string sql = "";
       
        try {
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null) {
                sql = "Select CONCAT(CONCAT(usu_nombre, ' '), usu_apellido) as miembros from profesor p, usuario u where p.usu_username = u.usu_username and p.com_codigo in (select COM_CODIGO from profesor where USU_USERNAME = '"+ Session["id"] + "')";

                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                using (OracleDataReader reader = cmd.ExecuteReader()){
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    GVasistente.DataSource = dataTable;
                }
                GVasistente.DataBind();
            }
            conn.Close();
        }catch (Exception ex){
            Linfo.Text = "Error al cargar la lista: " + ex.Message;
        }
    }
    protected void GVasistente_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        GVasistente.PageIndex = e.NewPageIndex;
        CargarAsistente();
    }
    protected void GVasistente_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e){}

    /*Metodos que agregan el orden del dia*/
    protected void Agregar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBcargo.Text)==true){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Ingrese el nombre y cargo del invitado!!";
        } else { 

            table = (System.Data.DataTable)(Session["Tabla"]);
            row = table.NewRow();
            row["NOMBRE"] = TBnombre.Text;
            row["CARGO"] = TBcargo.Text;

            table.Rows.Add(row);
            GVagreinte.DataSource = table;
            GVagreinte.DataBind();
            
        }
        TBnombre.Text = "";
        TBcargo.Text = "";
    }
    protected void BagregarOrden_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TBorden.Text) == true){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Ingrese una actividad";
        }else{
            torden = (System.Data.DataTable)(Session["Orden"]);
            rows = torden.NewRow();
            rows["ORDEN"] = TBorden.Text;
            rows["DESCRIPCION"] = TAdes.Value;
            torden.Rows.Add(rows);

            GVorden.DataSource = torden;
            GVorden.DataBind();
            GVorden.Visible = true;
        }
        TBorden.Text = "";
        TAdes.Value = "";
    }

    /*Evento del boton limpiar*/
    protected void Bcancelar_Click(object sender, EventArgs e)
    {
         torden = (System.Data.DataTable)(Session["Orden"]);
         torden.Clear();
         table = (System.Data.DataTable)(Session["Tabla"]);
         table.Clear();
         TBorden.Text = "";
         TBcargo.Text = "";
         TBnombre.Text = "";
         TBobj.Text = "";
         TBlugar.Text = "";
         Linfo.Text = "";
         TAdes.Value = "";
         GVagreinte.DataBind();
         GVorden.DataBind();
         foreach (GridViewRow row in GVasistente.Rows)
         {
             System.Web.UI.WebControls.CheckBox check = row.FindControl("CBasitio") as System.Web.UI.WebControls.CheckBox;
             check.Checked = false;
         }
         DDLreu.SelectedIndex = 0;
         Asistio.Value = "";
         Noasistio.Value = "";
    }
  
    /*Metodos que realizan el proceso de generar el acta*/
    protected void Bgenerar_Click(object sender, EventArgs e)
    {
        if (DDLreu.SelectedIndex != 0){
            getfile();
        }else{
            Linfo.Text = "Eliga una reunion";
        }
        
    }
    public void getfile()
    {

        string sql2 = "select c.COM_NOMBRE from comite c, profesor f where f.COM_CODIGO = c.COM_CODIGO and f.USU_USERNAME='" + Session["id"] + "'";
        List<string> list = con.consulta(sql2, 1, 0);

        string sql = "select TO_CHAR(REU_FREAL,'HH24:MI')  from reunion where REU_CODIGO = '" + DDLreu.Items[DDLreu.SelectedIndex].Value.ToString() + "'";
        List<string> list2 = con.consulta(sql, 1, 0);

        sql = "select to_char(REU_FREAL,'fm dd \"de\" month \"de\" yyyy','nls_date_language=spanish') from REUNION where Reu_Codigo= '" + DDLreu.Items[DDLreu.SelectedIndex].Value.ToString() + "'";
        List<string> list3 = con.consulta(sql, 1, 0);

        using (var ms = new MemoryStream()){
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
                doc.Add(saltoDeLinea1);

                //logo uceva pagina1
                img.ScaleToFit(125f, 60F);
                //Imagen - Movio en el eje de las Y
                img.SetAbsolutePosition(0, 800);
                doc.Add(img);

                //Tabla 1
                PdfPTable table16 = new PdfPTable(1);
                PdfPCell cell = new PdfPCell(new Phrase("Secretaría General - Archivo"));
                cell.Colspan = 3000;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table16.AddCell(cell);
                table16.WidthPercentage = 100f;
                doc.Add(table16);

                //Tabla 2
                PdfPTable table2 = new PdfPTable(3);
                PdfPCell cell2 = new PdfPCell(new Phrase("Acta de reunión"));
                PdfPCell cell3 = new PdfPCell(new Phrase("Versión: 00"));
                PdfPCell cell4 = new PdfPCell(new Phrase("Código: 1105-43-2-008"));
                cell3.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table2.AddCell(cell3);
                cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table2.AddCell(cell2);
                cell4.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table2.AddCell(cell4);
                table2.WidthPercentage = 100f;
                doc.Add(table2);
                doc.Add(saltoDeLinea1);

                //Titulos del 0 al 6
                Paragraph titulo1 = new Paragraph("ACTA #" + DDLreu.Items[DDLreu.SelectedIndex].Value.ToString());
                titulo1.Alignment = 1;
                doc.Add(titulo1);
                doc.Add(saltoDeLinea1);
                Paragraph titulo0 = new Paragraph("Comite: Trabajos de Grado ");
                titulo0.Alignment = 0;
                doc.Add(titulo0);
                Paragraph titulo2 = new Paragraph("Área / Facultad: Programa de "+ list[0]);
                titulo2.Alignment = 0;
                doc.Add(titulo2);
                Paragraph titulo3 = new Paragraph("Lugar: "+TBlugar.Text);
                titulo3.Alignment = 0;
                doc.Add(titulo3);
                Paragraph titulo4 = new Paragraph("Hora de inicio: " +list2[0]+ "                         " + "Hora fin: "+DateTime.Now.ToString("HH:mm"));
                titulo4.Alignment = 0;
                doc.Add(titulo4);
                Paragraph titulo5 = new Paragraph("Fecha: "+list3[0]);
                titulo5.Alignment = 0;
                doc.Add(titulo5);
                Paragraph titulo6 = new Paragraph("Objetivo de la reunión: "+TBobj.Text);
                titulo6.Alignment = 0;
                doc.Add(titulo6);
                doc.Add(saltoDeLinea1);

                //Tabla 3
                PdfPTable table3 = new PdfPTable(1);
                PdfPCell cell5 = new PdfPCell(new Phrase("Asistentes"));
                cell5.Colspan = 3000;
                cell5.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table3.AddCell(cell5);
                table3.WidthPercentage = 100f;
                doc.Add(table3);

                //Tabla 4
                PdfPTable table4 = new PdfPTable(1);
                PdfPCell cell6 = new PdfPCell(new Phrase("Nombre y cargo"));
                cell6.Colspan = 3000;
                cell6.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table4.AddCell(cell6);
                table4.WidthPercentage = 100f;
                doc.Add(table4);

                //Tabla Asistentes
                PdfPTable table5 = new PdfPTable(2);
                PdfPTable table11 = new PdfPTable(2);
                PdfPTable table14 = new PdfPTable(3);
                PdfPCell cell29 = new PdfPCell(new Phrase("Nombre"));
                PdfPCell cell30 = new PdfPCell(new Phrase("Cargo"));
                PdfPCell cell31 = new PdfPCell(new Phrase("Firma"));
                cell29.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table14.AddCell(cell29);
                cell30.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table14.AddCell(cell30);
                cell31.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table14.AddCell(cell31);

                foreach (GridViewRow row in GVasistente.Rows){
                    System.Web.UI.WebControls.CheckBox check = row.FindControl("CBasitio") as System.Web.UI.WebControls.CheckBox;
                    if (check.Checked){
                        ///ESTO CREA EL ASISTENTE
                        PdfPCell cell7 = new PdfPCell(new Phrase(row.Cells[0].Text));
                        PdfPCell cell8 = new PdfPCell(new Phrase("Docente Tiempo Completo"));
                        cell7.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        table5.AddCell(cell7);
                        cell8.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        table5.AddCell(cell8);

                        //ESTE CREA LA FIRMA DE LOS ASISTENTES
                        PdfPCell cell32 = new PdfPCell(new Phrase(row.Cells[0].Text));
                        cell32.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        table14.AddCell(cell32);
                        PdfPCell cell33 = new PdfPCell(new Phrase("Docente Tiempo Completo"));
                        cell33.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        table14.AddCell(cell33);
                        PdfPCell cell34 = new PdfPCell(new Phrase(""));
                        cell34.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        table14.AddCell(cell34);

                    } else{
                        
                        PdfPCell cell15 = new PdfPCell(new Phrase(row.Cells[0].Text));
                        PdfPCell cell16 = new PdfPCell(new Phrase("Docente Tiempo Completo"));
                        cell15.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        table11.AddCell(cell15);
                        cell16.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        table11.AddCell(cell16);
                     }
                    
                }
                table5.WidthPercentage = 100f;
                doc.Add(table5);

                //Tabla 6
                PdfPTable table6 = new PdfPTable(1);
                PdfPCell cell9 = new PdfPCell(new Phrase("Invitados"));
                cell9.Colspan = 3000;
                cell9.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table6.AddCell(cell9);
                table6.WidthPercentage = 100f;
                doc.Add(table6);


                //Tabla 7
                PdfPTable table7 = new PdfPTable(1);
                PdfPCell cell10 = new PdfPCell(new Phrase("Nombre y cargo"));
                cell10.Colspan = 3000;
                cell10.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table7.AddCell(cell10);
                table7.WidthPercentage = 100f;
                doc.Add(table7);


                //Tabla 8 Invitados
                PdfPTable table8 = new PdfPTable(2);
                table = (System.Data.DataTable)(Session["Tabla"]);
                DataRow[] currentRows = table.Select(null, null, DataViewRowState.CurrentRows);
                foreach (DataRow row in currentRows)
                {
                    PdfPCell cell11 = new PdfPCell(new Phrase(row["NOMBRE"].ToString()));
                    PdfPCell cell12 = new PdfPCell(new Phrase(row["CARGO"].ToString()));
                    cell11.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table8.AddCell(cell11);
                    cell12.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table8.AddCell(cell12);
                }
                table8.WidthPercentage = 100f;
                doc.Add(table8);

                //Tabla 9
                PdfPTable table9 = new PdfPTable(1);
                PdfPCell cell13 = new PdfPCell(new Phrase("Ausentes"));
                cell13.Colspan = 3000;
                cell13.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table9.AddCell(cell13);
                table9.WidthPercentage = 100f;
                doc.Add(table9);

                //Tabla 10
                PdfPTable table10 = new PdfPTable(1);
                PdfPCell cell14 = new PdfPCell(new Phrase("Nombre y cargo"));
                cell14.Colspan = 3000;
                cell14.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table10.AddCell(cell14);
                table10.WidthPercentage = 100f;
                doc.Add(table10);

                //Tabla 11 Ausentes
                table11.WidthPercentage = 100f;
                doc.Add(table11);
                doc.Add(saltoDeLinea1);

                Paragraph texto1 = new Paragraph("Actuó como secretario de la Reunión_____________________sin voz y sin voto, presidido por_____________________, seguido se puso en consideración el orden del día propuesto y aprobado así: ");
                doc.Add(texto1);
                doc.Add(saltoDeLinea1);

                //Tabla 12 Orden del dia
                PdfPTable table12 = new PdfPTable(1);
                PdfPCell cell17 = new PdfPCell(new Phrase("Orden del día"));
                cell17.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table12.AddCell(cell17);

            if (CBpropuesta.Checked) { 
                PdfPCell cell18 = new PdfPCell(new Phrase("Revisión de Propuestas de trabajos de grado"));
                cell18.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                table12.AddCell(cell18);
            }
            if (CBanteproyecto.Checked) {

                PdfPCell cell18 = new PdfPCell(new Phrase("Asignar los jurados para la lectura de anteproyectos de grado"));
                cell18.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                table12.AddCell(cell18);
            }
            if (CBcaso.Checked) {

                PdfPCell cell18 = new PdfPCell(new Phrase("Analizar casos particulares"));
                cell18.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                table12.AddCell(cell18);

            }

            table12.WidthPercentage = 100f;
                doc.Add(table12);

                /*doc.NewPage();
                img.ScaleToFit(125f, 60F);
                img.SetAbsolutePosition(0, 800);
                doc.Add(img);
                doc.Add(saltoDeLinea1);*/
                doc.Add(saltoDeLinea1);
                Paragraph titulo7 = new Paragraph("Desarrollo del orden del día");
                titulo7.Alignment = 1;
                doc.Add(titulo7);


            if (CBpropuesta.Checked)
            {
                doc.Add(saltoDeLinea1);
                Paragraph texto3 = new Paragraph("Revision propuesta de trabajo de grado");
                doc.Add(texto3);
                doc.Add(saltoDeLinea1);
                //Tabla 13 Orden del dia
                PdfPTable table13 = new PdfPTable(5);
                PdfPCell cell19 = new PdfPCell(new Phrase("Código"));
                PdfPCell cell20 = new PdfPCell(new Phrase("Estudiantes"));
                PdfPCell cell21 = new PdfPCell(new Phrase("Nombre Propuesta"));
                PdfPCell cell22 = new PdfPCell(new Phrase("Pertinente"));
                PdfPCell cell23 = new PdfPCell(new Phrase("Observaciones"));
                cell19.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table13.AddCell(cell19);
                cell20.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table13.AddCell(cell20);
                cell21.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table13.AddCell(cell21);
                cell22.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table13.AddCell(cell22);
                cell23.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table13.AddCell(cell23);


                OracleConnection conn = con.crearConexion();
                OracleCommand cmd = null;
                if (conn != null)
                {
                    string prop = "select  p.PROP_TITULO, p.PROP_ESTADO,p.PROP_CODIGO from propuesta p, revision_propuesta r where r.PROP_CODIGO = p.PROP_CODIGO and r.REU_CODIGO='" + DDLreu.Items[DDLreu.SelectedIndex].Value.ToString() + "'";
                    cmd = new OracleCommand(prop, conn);
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader drc1 = cmd.ExecuteReader();
                    if (drc1.HasRows)
                    {
                        while (drc1.Read())
                        {
                            PdfPCell cell24 = new PdfPCell(new Phrase(drc1[2].ToString()));
                            cell24.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            table13.AddCell(cell24);

                            prop = "select  CONCAT(CONCAT(u.usu_apellido, ' '), u.usu_nombre) from estudiante e, propuesta p, usuario u " +
                                   "where u.USU_USERNAME = e.USU_USERNAME and p.PROP_CODIGO = e.PROP_CODIGO and p.PROP_CODIGO = '" + drc1[2] + "' ";
                            List<string> lintegra = con.consulta(prop, 1, 0);
                            for (int intre = 0; intre < lintegra.Count; intre++)
                            {
                                PdfPCell cell25 = new PdfPCell(new Phrase(lintegra[intre].ToString()));
                                cell25.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                                table13.AddCell(cell25);
                            }

                            PdfPCell cell26 = new PdfPCell(new Phrase(drc1[0].ToString()));
                            cell26.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            table13.AddCell(cell26);

                            PdfPCell cell27 = new PdfPCell(new Phrase(drc1[1].ToString()));
                            cell27.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            table13.AddCell(cell27);

                            string observaciones = "";
                            if (drc1[1].Equals("APROBADO")) { observaciones = "Ninguna"; }
                            else if (drc1[1].Equals("RECHAZADO"))
                            {
                                prop = "select OBS_DESCRIPCION from observacion where PROP_CODIGO='" + drc1[2] + "'";
                                List<string> lobs = con.consulta(prop, 1, 0);
                                for (int ob = 0; ob < lobs.Count; ob++)
                                {
                                    observaciones += lobs[ob] + " ";
                                }
                            }

                            PdfPCell cell28 = new PdfPCell(new Phrase(observaciones));
                            cell28.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            table13.AddCell(cell28);
                        }
                    }
                    drc1.Close();
                }
                table13.WidthPercentage = 100f;
                doc.Add(table13);

            }



            if (CBanteproyecto.Checked)
            {
                doc.Add(saltoDeLinea1);
                Paragraph texto50 = new Paragraph("Asignar los jurados para la lectura de anteproyectos de grado");
                doc.Add(texto50);
                doc.Add(saltoDeLinea1);
                //Tabla 13 Orden del dia
                PdfPTable table50 = new PdfPTable(5);
                PdfPCell cell50 = new PdfPCell(new Phrase("Código"));
                PdfPCell cell51 = new PdfPCell(new Phrase("Estudiantes"));
                PdfPCell cell52 = new PdfPCell(new Phrase("Nombre Anteproyecto"));
                PdfPCell cell53 = new PdfPCell(new Phrase("Jurado"));
                PdfPCell cell54 = new PdfPCell(new Phrase("Revisor"));
                cell50.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table50.AddCell(cell50);
                cell51.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table50.AddCell(cell51);
                cell52.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table50.AddCell(cell52);
                cell53.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table50.AddCell(cell53);
                cell54.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table50.AddCell(cell54);


                table50.WidthPercentage = 100f;
                doc.Add(table50);

            }


            if (CBcaso.Checked)
            {
                doc.Add(saltoDeLinea1);
                Paragraph texto60 = new Paragraph("Casos particulares");
                doc.Add(texto60);
                doc.Add(saltoDeLinea1);
                //Tabla 13 Orden del dia
                PdfPTable table60 = new PdfPTable(2);
                PdfPCell cell60 = new PdfPCell(new Phrase("Título del caso"));
                PdfPCell cell61 = new PdfPCell(new Phrase("Descripción"));
                cell60.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table60.AddCell(cell60);
                cell61.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table60.AddCell(cell61);

                table60.WidthPercentage = 100f;
                doc.Add(table60);

            }


            doc.Add(saltoDeLinea1);
                Paragraph texto4 = new Paragraph("Siendo las "+ DateTime.Now.ToString("HH:mm") + " horas el presidente dio por terminada  la reunión, se procedió a firmar la presente Acta la cual fue aprobada en sesión verificada el "+list3[0]+":");
                doc.Add(texto4);
                doc.Add(saltoDeLinea1);

                Paragraph titulo8 = new Paragraph("Firma de los asistentes");
                titulo8.Alignment = 1;
                doc.Add(titulo8);
                doc.Add(saltoDeLinea1);

                //Tabla 14 Orden del dia
                table14.WidthPercentage = 100f;
                doc.Add(table14);

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

    /*Metodos que se utilizan para cargar el acta*/
    protected void Bsubir_Click(object sender, EventArgs e)
    {
        if (DDLreunion2.SelectedIndex.Equals(0)){
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Se requiere que escoja una reunion";
        }else {
            if (FUdocumento.HasFile){
                string filename = Path.GetFileName(FUdocumento.PostedFile.FileName);
                string contentType = FUdocumento.PostedFile.ContentType;

                using (Stream fs = FUdocumento.PostedFile.InputStream){
                    using (BinaryReader br = new BinaryReader(fs)){
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        OracleConnection conn = con.crearConexion();
                        if (conn != null) {
                            string query = "update REUNION set reu_acta=:Data, reu_nomarchivo=:Nomarchivo, reu_tipo=:Tipo, reu_estado='Finalizada' where reu_codigo='"+ DDLreunion2.Items[DDLreunion2.SelectedIndex].Value.ToString() + "'";
                            using (OracleCommand cmd = new OracleCommand(query)){
                                cmd.Connection = conn;
                                cmd.Parameters.Add(":Data", bytes);
                                cmd.Parameters.Add(":Nomarchivo", filename);
                                cmd.Parameters.Add(":Tipo", contentType);
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                //Response.Redirect(Request.Url.AbsoluteUri);
                Linfo.ForeColor = System.Drawing.Color.Green;
                Linfo.Text = "Acta guardada satisfatoriamente";
                DDLreunion2.Items.Clear();
                string sql = "SELECT r.reu_codigo FROM reunion r, profesor p WHERE r.reu_estado='ACTIVO' AND r.COM_CODIGO = p.COM_CODIGO and p.USU_USERNAME = '" + Session["id"] + "'";
                DDLreunion2.Items.AddRange(con.cargarDDLid(sql));
                DDLreunion2.Items.Insert(0, "Seleccione");
            } else {
                Linfo.Text = "Escoja un documento para subir";
            }
        }
    }
    protected void Blimpiar_Click(object sender, EventArgs e)
    {
        DDLreunion2.SelectedIndex = 0;
        Linfo.Text = "";
    }

    /*Metodos que se utilizan para la consulta de las actas por un rango de fecha*/
    protected void IBdesde_Click(object sender, ImageClickEventArgs e)
    {
        if (Cdesde.Visible == false){
            Cdesde.Visible = true;
        }else{
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
        if(string.IsNullOrEmpty(TBdesde.Text) == true) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Debe elegir una fecha de inicio";
        }else {
            CargarActas();
        }
    }
    private void CargarActas()
    {
        string fhasta;
        if (string.IsNullOrEmpty(TBhasta.Text) == true){
            fhasta = DateTime.Now.ToString("dd/MM/yyyy");
        }else{
            fhasta = TBhasta.Text;
        }
        try{
            OracleConnection conn = con.crearConexion();
            OracleCommand cmd = null;
            if (conn != null){
                string sql = "SELECT r.REU_CODIGO, TO_CHAR(r.REU_FREAL,'DD/MM/YY') AS FECHA from reunion r , profesor p" +
                    " where r.REU_FREAL BETWEEN TO_DATE('" + TBdesde.Text + "', 'DD/MM/YYYY')  and TO_DATE('" + fhasta + "', 'DD/MM/YYYY')  AND r.REU_ESTADO='Finalizada'" +
                    "and p.COM_CODIGO= r.COM_CODIGO and p.USU_USERNAME='"+ Session["id"] + "' ORDER BY r.REU_CODIGO";
                cmd = new OracleCommand(sql, conn);
                 cmd.CommandType = CommandType.Text;
                 using (OracleDataReader reader = cmd.ExecuteReader()) {
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
        } catch (Exception ex) {
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
    protected void GVactas_RowDataBound(object sender, GridViewRowEventArgs e){}

}