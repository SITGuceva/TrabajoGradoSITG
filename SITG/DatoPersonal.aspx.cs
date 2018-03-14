using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatoPersonal : Conexion
{
    Conexion con = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }else{
            if (!IsPostBack){
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                Buscar();
            }
            validarol();
        } 
    }

    private void validarol()
    {
        string rol = Session["rol"].ToString().Trim();
        String[] ciclo = rol.Split(' ');
        foreach (var cadena in ciclo)
        {
            if (cadena.Equals("DOC")){
                HVdoc.Visible = true;
                break;
            }else { HVdoc.Visible = false; }
        }
    }

    private void Buscar()
    {
        int id = Int32.Parse(Session["id"].ToString());
        string sql = "SELECT USU_NOMBRE,USU_APELLIDO, USU_DIRECCION,USU_CORREO, USU_TELEFONO FROM USUARIO WHERE USU_USERNAME='" + id + "'";
        List<string> list = con.consulta(sql, 5, 1);
        TBnombre.Text = list[0];
        TBapellido.Text = list[1];
        TBdireccion.Text = list[2];
        TBcorreo.Text = list[3];
        TBtelefono.Text = list[4];
        Lanscod.Text = Session["id"].ToString();
    }


    protected void Aceptar(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TBnombre.Text) == true || string.IsNullOrEmpty(TBapellido.Text) == true || string.IsNullOrEmpty(TBtelefono.Text) == true || string.IsNullOrEmpty(TBdireccion.Text) == true || string.IsNullOrEmpty(TBcorreo.Text) == true) {
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = "Los campos son obligatorios";
        } else{
            if (HVdoc.Visible) {
                if (FUdocumento.HasFile){
                    string filename = Path.GetFileName(FUdocumento.PostedFile.FileName);
                    string contentType = FUdocumento.PostedFile.ContentType;
                    using (Stream fs = FUdocumento.PostedFile.InputStream){
                        using (BinaryReader br = new BinaryReader(fs)) {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            OracleConnection conn = con.crearConexion();
                            if (conn != null) {
                                string query = "update PROFESOR set prof_documento= :Data, prof_nomarchivo= :Prof_nomarchivo,prof_tipo= :Prof_tipo where usu_username='"+ Session["id"] + "'";
                                using (OracleCommand cmd = new OracleCommand(query)){
                                    cmd.Connection = conn;
                                    cmd.Parameters.Add(":Data", bytes);
                                    cmd.Parameters.Add(":Prof_nomarchivo", filename);
                                    cmd.Parameters.Add(":Prof_tipo", contentType);
                                    cmd.ExecuteNonQuery();
                                    conn.Close();
                                }
                            }
                        }
                    }
                    string sql = "UPDATE USUARIO SET USU_NOMBRE='" + TBnombre.Text + "', USU_APELLIDO='" + TBapellido.Text + "', USU_TELEFONO='" + TBtelefono.Text + "', USU_DIRECCION='" + TBdireccion.Text + "', USU_CORREO='" + TBcorreo.Text + "'WHERE USU_USERNAME='" + Lanscod.Text + "'";
                    Ejecutar("Datos Modificados Satisfactoriamente", sql);
                }
                else {
                    Linfo.ForeColor = System.Drawing.Color.Red;
                    Linfo.Text = "Debe elegir un archivo";
                }
            } else {
                string sql = "UPDATE USUARIO SET USU_NOMBRE='" + TBnombre.Text + "', USU_APELLIDO='" + TBapellido.Text + "', USU_TELEFONO='" + TBtelefono.Text + "', USU_DIRECCION='" + TBdireccion.Text + "', USU_CORREO='" + TBcorreo.Text + "'WHERE USU_USERNAME='" + Lanscod.Text + "'";
                Ejecutar("Datos Modificados Satisfactoriamente", sql);
            }   
        }
    }
    private void Ejecutar(string texto, string sql)
    {
        string info = con.IngresarBD(sql);
        if (info.Equals("Funciono")) {
            Linfo.ForeColor = System.Drawing.Color.Green;
            Linfo.Text = texto;
        } else{
            Linfo.ForeColor = System.Drawing.Color.Red;
            Linfo.Text = info;
        }
    }
    protected void Limpiar(object sender, EventArgs e)
    {
        TBnombre.Text = "";
        TBapellido.Text = "";
        TBtelefono.Text = "";
        TBdireccion.Text = "";
        TBcorreo.Text = "";
        Linfo.Text = "";
    }

}