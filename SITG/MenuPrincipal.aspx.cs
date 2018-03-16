
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MenuPrincipal : Page
{
    Conexion con = new Conexion();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null )
        {
            Response.Redirect("Default.aspx");
        }
        Menu();
    }
    List<ListItem> list = new List<ListItem>();
    List<ListItem> list2 = new List<ListItem>();
    private void Menu()
    {
        string rol = Session["rol"].ToString().Trim();
        String[] ciclo = rol.Split(' ');
        foreach (var cadena in ciclo){

           
            try
            {
                OracleConnection conn = con.crearConexion();
                if (conn != null)
                {
                    string sql = "SELECT UNIQUE C.CATS_ID ,c.CATS_NOMBRE FROM OPCION_ROL r, OPCION_SISTEMA s, CATEGORIA_SISTEMA  c WHERE r.ROL_ID = '"+cadena+"' and s.OPCS_ID = r.OPCS_ID and s.CATS_ID = c.CATS_ID and c.CATS_ESTADO = 'ACTIVO'";
                  
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read()){



                        /*    String cat = dr.GetString(0).Replace(" ", "");
                            string sql1 = "SELECT s.OPCS_NOMBRE, s.OPCS_URL FROM  OPCION_ROL r,OPCION_SISTEMA s, CATEGORIA_SISTEMA  c WHERE  r.ROL_ID = '"+cadena+"' AND s.OPCS_ID = r.OPCS_ID  AND s.CATS_ID = c.CATS_ID AND C.Cats_Id = '" + dr.GetString(0) + "' and s.OPCS_ESTADO='ACTIVO'";
                          
                           OracleCommand cmd1 = new OracleCommand(sql1, conn);
                            cmd1.CommandType = CommandType.Text;
                            OracleDataReader drc2 = cmd1.ExecuteReader();
                            if (drc2.HasRows)
                            {
                                list2.Add(new ListItem(drc2[1].ToString(), drc2[0].ToString()));
                                Linfo.Text += "es" + list2.Count;
                            }
                        
                        Linfo.Text += "la" + list.Count;*/
                        //Linfo.Text += list.Count+ "es";
                        if (list.Count.Equals(0))
                        {
                            list.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                        }
                        else { 
                        for (int i = 0; i < list.Count; i++){

                               if (dr[0].ToString().Equals(list[i].Value))
                               
                            {
                             //  Linfo.Text += "la cat existe";
                            } else {
                                list.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                            }
                             //Linfo.Text += list[i].Value + list[i].Text;
                        }
                        }
                    }
                conn.Close();
                }
            } catch (Exception ex){
                Response.Write("Error al cargar la lista: " + ex.Message);
            }
        }
        recorrer();
   }

    private void recorrer()
    {
        Linfo.Text += list.Count;
        for(int i =0; i < list.Count; i++)
        {
           Linfo.Text += list[i].Value + list[i].Text;
        }
       // Linfo.Text += list.Count+"espera"+ list2.Count;
      //  for (int i = 0; i < list2.Count; i++)
        //{
          //  Linfo.Text += list2[i].Value + list2[i].Text;
        //}
    }
}
