using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using System.IO;
using System.Net;
using System.Text;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Menu : Page{
    Conexion con = new Conexion();
    public string usuarios; // cantidad de usuarios
    public string facultad; // cantidad de facultades
    public string programas; // cantidad de programas
    public string estudiantes; // cantidad de programas
    public string profesores; // cantidad de profesores
    public string edirector; // estado director propuesta
    public string epropuesta; // estado director propuesta
    public string obsdirectorp; // cantidad de observaciones director propuesta
    public string obscomitep; // cantidad de observaciones director propuesta
    public string anteasigeva; // asignación evaluador anteproyecto
    public string antecaleva; // calificacíon evaluador anteproyecto
    public string antecaldir; // calificacíon director anteproyecto
    public string anteobseva; // observaciones anteproyecto evaluador
    public string anteobsedir; // observaciones anteproyecto director
    public string epago; // observaciones anteproyecto director
    public string edirproyfinal; // aprobacion director proyecto final
    public string edirproyfinalobs; // director proyecto final obs
    public string ejurproyfinal; // estado final proyecto final
    public string ejurproyfinalobs; // estado final proyecto final obs
    public string cantantreproeva; // cantidad anteproyecto asignados;
    public string cantrevisar; // cantidad anteproyectos que faltan por revisar;
    public string cantproyectosdoc; // cantidad anteproyectos que faltan por revisar;
    public string proyectosasigdir; // cantidad de proyectos asignados director;
    public string proppendiente; // cantidad de propuestas pendientes director
    public string antependiente; // cantidad de anteproyectos pendientes director
    public string proyfinalpendiente; // cantidad de anteproyectos pendientes director
    public string proyfinalasignado; // cantidad de proyectos finales asignados jurado
    public string proyfinalrevjur; // cantidad de proyectos finales asignados jurado
    public string cantpropaprob; // cantidad de propuestas aprobadas
    public string cantproprecha; // cantidad de propuestas rechazadas
    public string cantproppen; // cantidad propuestas pendientes
    public string cantanteaprob; // cantidad anteproyectos aprobados
    public string cantanterecha; // cantidad anteproyectos rechazados
    public string cantantepen; // cantidad anteproyectos pendientes
    public string cantproyaprob; // cantidad proyectos finales aprobados
    public string cantproyrecha; // cantidad proyectos finales rechazado
    public string cantproypen; // cantidad proyectos finales pendientes
    public string proyfinalsinjur; // cantidad proyectos finales pendientes
    public string cantpropaprobcom;// cantidad propuestas aprobadas comite
    public string cantproprechacom;// cantidad propuestas rechazadas comite
    public string cantproppencom;// cantidad propuestas pendientes comite
    public string cantanteaprobcom;// cantidad anteproyectos aprobados comite
    public string cantanterechacom;// cantidad anteproyectos rechazados comite
    public string cantantepencom;// cantidad anteproyectos pendientes comite
    public string finalaprobcom;// cantidad anteproyectos aprobados comite
    public string finalrechacom;// cantidad anteproyectos rechazados comite
    public string finalpencom;// cantidad anteproyectos pendientes comite
    public string propuestapencom;// cantidad de propuestas pendientes por revisar comite
    public string antepenasignacion;// cantidad de anteproyectos pendientes por asignar evaluador
    public string dirpeticion;// cantidad de anteproyectos pendientes por asignar evaluador
    public string solicom;// solicitudes estudiantes comite
    public int sumapropuesta;
    public int sumaanteproyecto;
    public int sumaproyectofinal;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }


       /* EST.Visible = true;
        ADM.Visible = false;
        estudianterol.Visible = false;
        profesorrol.Visible = false;
        comiterol.Visible = false;
        directorrol.Visible = false;*/

        consultaroles();
        
    }



    //--------------------------------------ADMINISTRADOR-------------------------------------


    protected void consultarfacultades()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM facultad";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                facultad = drc1.GetInt32(0).ToString();
            }
            drc1.Close();
        }
    }

    protected void consultarusuarios()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM usuario";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                usuarios = drc1.GetInt32(0).ToString();
            }
            drc1.Close();
        }
    }

    protected void consultarestudiantes()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM estudiante";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                estudiantes = drc1.GetInt32(0).ToString();
            }
            drc1.Close();
        }
    }

    protected void consultarprofesor()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM  profesor";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                profesores = drc1.GetInt32(0).ToString();
            }
            drc1.Close();
        }
    }

    protected void consultarprogramas()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM  programa";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                programas = drc1.GetInt32(0).ToString();
            }
            drc1.Close();
        }
    }



    //--------------------------------------ESTUDIANTE-------------------------------------

    //**PROPUESTA**//

    protected void estadodirector()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT INITCAP(d.dir_Estado) AS estado FROM director d, estudiante e WHERE d.prop_codigo=e.prop_codigo AND e.usu_username='"+Session["id"]+"' order by d.dir_estado ASC";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                edirector = drc1.GetString(0).ToString();

                if (edirector.Equals("Aprobado"))
                {
                    daprobado.Visible = true;
                }
                if (edirector.Equals("Rechazado"))
                {
                    drechazado.Visible = true;
                }
                if (edirector.Equals("Pendiente"))
                {
                    dpendiente.Visible = true;
                }
            }
            else
            {
                dsinsolicitud.Visible = true;
            }
            drc1.Close();
        }
    }


    protected void obsdirectorpropuesta()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM observacion o, estudiante e WHERE o.prop_codigo=e.prop_codigo AND e.usu_username='"+Session["id"]+"' AND o.obs_realizada='DIRECTOR'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                obsdirectorp = drc1.GetInt32(0).ToString();
            }
            drc1.Close();
        }
    }

    protected void estadopropuesta()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT INITCAP(P.Prop_Estado) AS estado FROM propuesta p, estudiante e WHERE E.Prop_Codigo=P.Prop_Codigo AND E.Usu_Username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                epropuesta = drc1.GetString(0).ToString();

                if (epropuesta.Equals("Aprobado"))
                {
                    paprobado.Visible = true;
                }
                if (epropuesta.Equals("Rechazado"))
                {
                    prechazado.Visible = true;
                }
                if (epropuesta.Equals("Pendiente"))
                {
                    ppendiente.Visible = true;
                }
            }
            else
            {
                psinsubir.Visible = true;
            }
            drc1.Close();
        }
    }


    protected void obscomitepropuesta()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM observacion o, estudiante e WHERE o.prop_codigo=e.prop_codigo AND e.usu_username='"+Session["id"]+"' AND o.obs_realizada='COMITE'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                obscomitep = drc1.GetInt32(0).ToString();
            }
            drc1.Close();
        }
    }


//**ANTEPROYECTO**

        //---evaluador---
    protected void anteproyectoeva() // saber si el evaluador fue asignado
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT INITCAP(a.anp_evaluador) AS evaluador FROM anteproyecto a, estudiante e WHERE a.apro_codigo=e.prop_codigo AND e.usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                anteasigeva = drc1.GetString(0).ToString();

                    aevaluador.Visible = true;
            }
            else
            {
                sinaevaluador.Visible = true;
            }
            drc1.Close();
        }
    }
    protected void anteproyectoevacal() // saber calificación evaluador
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT INITCAP(a.anp_estado) AS estado FROM anteproyecto a, estudiante e WHERE a.apro_codigo=e.prop_codigo AND e.usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                antecaleva = drc1.GetString(0).ToString();
               
            }
            drc1.Close();
        }
    }

    protected void anteproyectoevaobs() // saber cantidad observaciones evaluador
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM ante_observacion a, estudiante e WHERE a.aprop_codigo=e.prop_codigo AND e.usu_username='"+Session["id"]+"' AND a.aobs_realizada='EVALUADOR'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                anteobseva = drc1.GetInt32(0).ToString();

            }
            drc1.Close();
        }
    }

    //---director---


    protected void anteproyectodircal() // saber calificación director
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT INITCAP(a.anp_aprobacion) AS director FROM anteproyecto a, estudiante e WHERE a.apro_codigo=e.prop_codigo AND e.usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                antecaldir = drc1.GetString(0).ToString();

                if (antecaldir.Equals("Aprobado"))
                {
                   adirectoraprobado.Visible = true;
                }
                if (antecaldir.Equals("Rechazado"))
                {
                    adirectorrechazado.Visible = true;
                }
                if (antecaldir.Equals("Pendiente"))
                {
                    adirectorpendiente.Visible = true;
                }
            }
            else
            {
                sindirectorante.Visible = true;
            }
            drc1.Close();
        }
    }

    protected void anteproyectodirobs() // saber cantidad observaciones director
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM ante_observacion a, estudiante e WHERE a.aprop_codigo=e.prop_codigo AND e.usu_username='"+Session["id"]+"' AND a.aobs_realizada='DIRECTOR'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                anteobsedir = drc1.GetInt32(0).ToString();

            }
            drc1.Close();
        }
    }



    //**PROYECTO FINAL**


    //---pagos---

    protected void pagosestudiante()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT pag_estado FROM pagos WHERE usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                epago = drc1.GetString(0).ToString();

                if (epago.Equals("APROBADO"))
                {
                    pagoaprobado.Visible = true;
                }
                if (epago.Equals("RECHAZADO"))
                {
                    pagorechazado.Visible = true;
                }
                if (epago.Equals("PENDIENTE"))
                {
                    pagopendiente.Visible = true;
                }
            }
            else
            {
                pagosinsubir.Visible = true;
            }
            drc1.Close();
        }

    }



    //---Aceptación director proyecto final---


    protected void proyfinaldirector()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT INITCAP(p.pf_aprobacion) AS director from proyecto_final p, estudiante e where p.ppro_codigo=e.prop_codigo AND e.usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                edirproyfinal = drc1.GetString(0).ToString();

                if (edirproyfinal.Equals("Aprobado"))
                {
                    aproyfinaldiraprobado.Visible = true;
                }
                if (edirproyfinal.Equals("Rechazado"))
                {
                    aproyfinaldirrechazado.Visible = true;
                }
                if (edirproyfinal.Equals("Pendiente"))
                {
                    aproyfinaldirpendiente.Visible = true;
                }
            }
            else
            {
                aproyfinalsinsubir.Visible = true;
            }
            drc1.Close();
        }

    }


    //---observaciones director proyecto final---
    protected void proyfinaldirectorobs()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM pf_observaciones p, estudiante e where p.ppro_codigo=e.prop_codigo AND  e.usu_username='"+Session["id"]+"' AND p.pfobs_realizada='DIRECTOR'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                edirproyfinalobs = drc1.GetInt32(0).ToString();

            } 
            drc1.Close();

        }

    }



    //---Aceptación jurado proyecto final---
    protected void proyfinaljurado()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT INITCAP(p.pf_estado) AS estado FROM proyecto_final p, estudiante e WHERE p.ppro_codigo=e.prop_codigo AND e.usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                ejurproyfinal = drc1.GetString(0).ToString();

                if (ejurproyfinal.Equals("Aprobado"))
                {
                    proyfinaljuraprobado.Visible = true;
                }
                if (ejurproyfinal.Equals("Rechazado"))
                {
                    proyfinaljurrechazado.Visible = true;
                }
                if (ejurproyfinal.Equals("Pendiente"))
                {
                    proyfinaljurpendiente.Visible = true;
                }
            }
            else
            {
               sinproyfinaljur.Visible = true;
            }
            drc1.Close();
        }

    }


    //---observaciones jurado proyecto final---
    protected void proyfinaljuradoobs()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM pf_observaciones p, estudiante e where p.ppro_codigo=e.prop_codigo AND  e.usu_username='"+Session["id"]+"' AND p.pfobs_realizada='JURADO'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                ejurproyfinalobs = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }




    //--------------------------------------EVALUADOR-------------------------------------

        //Cantidad de anteproyectos asignados
    protected void anteproasignado()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM evaluador WHERE usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantantreproeva = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }


        //Cantidad de anteproyectos que faltan por revisar
    protected void anteprorevisareva()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(A.Apro_Codigo) AS ante_pendientes FROM anteproyecto a, estudiante e, evaluador r WHERE r.Usu_Username = '"+Session["id"]+"' and E.Prop_Codigo = A.Apro_Codigo and A.Anp_Estado = 'PENDIENTE' and A.Anp_Aprobacion='APROBADO'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantrevisar = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }



    //--------------------------------------DOCENTE-------------------------------------

    //Cantidad de proyectos subidos
    protected void proyectosdocente()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM proyectos WHERE usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantproyectosdoc = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }



    //--------------------------------------DIRECTOR-------------------------------------


    //cantidad de proyectos asignados
    protected void proyectodir()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM  director WHERE usu_username='"+Session["id"]+"' AND dir_estado='APROBADO'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                proyectosasigdir = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }

    //cantidad de propuestas sin revisar
    protected void propuestaspen()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM  propuesta p, director s WHERE P.Prop_Estado='RECHAZADO' AND  P.Prop_Codigo=s.prop_codigo AND s.usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                proppendiente = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }

    //cantidad de anteproyectos sin revisar
    protected void anteproyectopen()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM  anteproyecto a, director s WHERE a.anp_aprobacion='PENDIENTE' AND  a.apro_codigo=s.prop_codigo AND s.usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                antependiente = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }

    //cantidad de anteproyectos sin revisar
    protected void proyectofinalpen()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM proyecto_final p,director s where p.pf_aprobacion='PENDIENTE' AND s.prop_codigo=p.ppro_codigo AND s.usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                proyfinalpendiente = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }


    //--------------------------------------JURADO-------------------------------------


    //cantidad de proyectos finales asignados
    protected void proyfinaleasig()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM jurado WHERE usu_username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                proyfinalasignado = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }


    //cantidad de proyectos finales sin revisar
    protected void proyfinalrevision()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(P.Ppro_Codigo) FROM proyecto_final p, estudiante e, jurado j WHERE J.Usu_Username = '"+Session["id"]+"' and E.Prop_Codigo = P.Ppro_Codigo and P.Pf_Estado = 'PENDIENTE' and P.Pf_Aprobacion = 'APROBADO' and J.Ppro_Codigo = P.Ppro_Codigo and J.Jur_Revisado = 'PENDIENTE'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                proyfinalrevjur = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }


    //--------------------------------------DECANO-------------------------------------


    //Cantidad de propuestas


    protected void propuestasaprob()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(P.Prop_Estado) AS propuestas_aprobadas FROM propuesta p, estudiante e WHERE e.prop_codigo=p.prop_codigo AND P.Prop_Estado='APROBADO'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantpropaprob = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }


    protected void propuestasrecha()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(P.Prop_Estado) AS propuestas_aprobadas FROM propuesta p, estudiante e WHERE e.prop_codigo=p.prop_codigo AND P.Prop_Estado='RECHAZADO'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantproprecha = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }


    protected void propuestaspendiente()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(P.Prop_Estado) AS propuestas_aprobadas FROM propuesta p, estudiante e WHERE e.prop_codigo=p.prop_codigo AND P.Prop_Estado='PENDIENTE'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantproppen = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }



    //Cantidad de anteproyectos


    protected void anteproyectosaprob()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(A.Anp_Estado) AS anteproyectos_aprobados FROM anteproyecto a, estudiante e WHERE e.prop_codigo=a.apro_codigo AND A.Anp_Estado='APROBADO'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantanteaprob = drc1.GetInt32(0).ToString();
                
            };
            drc1.Close();

        }

    }


    protected void anteproyectosrecha()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(A.Anp_Estado) AS anteproyectos_aprobados FROM anteproyecto a, estudiante e WHERE e.prop_codigo=a.apro_codigo AND A.Anp_Estado='RECHAZADO'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantanterecha = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }

    protected void anteproyectospen()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(A.Anp_Estado) AS anteproyectos_aprobados FROM anteproyecto a, estudiante e WHERE e.prop_codigo=a.apro_codigo AND A.Anp_Estado='PENDIENTE'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantantepen = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }


    //Cantidad de proyectos finales


    protected void proyfinalaprob()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(P.Pf_Estado) AS proyectosf_aprobados FROM proyecto_final p, estudiante e WHERE e.prop_codigo=p.ppro_codigo AND P.Pf_Estado='APROBADO'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantproyaprob = drc1.GetInt32(0).ToString();

            };
            drc1.Close();

        }

    }


    protected void proyfinalrecha()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(P.Pf_Estado) AS proyectosf_aprobados FROM proyecto_final p, estudiante e WHERE e.prop_codigo=p.ppro_codigo AND P.Pf_Estado='RECHAZADO'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantproyrecha = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }

    protected void proyfinalpen()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(P.Pf_Estado) AS proyectosf_aprobados FROM proyecto_final p, estudiante e WHERE e.prop_codigo=p.ppro_codigo AND P.Pf_Estado='PENDIENTE'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantproypen = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }



    //Cantidad de proyectos finales sin jurado

    protected void proyfinaljur()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(*) FROM proyecto_final WHERE (pf_jur1='PENDIENTE' OR pf_jur2='PENDIENTE' OR pf_jur3='PENDIENTE') AND pf_aprobacion='APROBADO'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                proyfinalsinjur = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }


    }





    //--------------------------------------COMITE-------------------------------------


    //Cantidad de propuestas


    protected void propuestasaprobcom()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(p.PROP_CODIGO) FROM propuesta p, estudiante e, profesor d WHERE p.PROP_CODIGO = e.PROP_CODIGO AND p.PROP_ESTADO = 'APROBADO' AND D.Com_Codigo= E.Prog_Codigo AND D.Usu_Username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantpropaprobcom = drc1.GetInt32(0).ToString();
                int Valor = Convert.ToInt32(cantpropaprobcom);
                sumapropuesta = sumapropuesta + Valor;
            }
            drc1.Close();

        }

    }


    protected void propuestasrechacom()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(p.PROP_CODIGO) FROM propuesta p, estudiante e, profesor d WHERE p.PROP_CODIGO = e.PROP_CODIGO AND p.PROP_ESTADO = 'RECHAZADO' AND D.Com_Codigo= E.Prog_Codigo AND D.Usu_Username='" + Session["id"] + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantproprechacom = drc1.GetInt32(0).ToString();
                int Valor = Convert.ToInt32(cantproprechacom);
                sumapropuesta = sumapropuesta + Valor;
            }
            drc1.Close();

        }
    }

    protected void propuestaspencom()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(p.PROP_CODIGO) FROM propuesta p, estudiante e, profesor d WHERE p.PROP_CODIGO = e.PROP_CODIGO AND p.PROP_ESTADO = 'PENDIENTE' AND D.Com_Codigo= E.Prog_Codigo AND D.Usu_Username='" + Session["id"] + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantproppencom = drc1.GetInt32(0).ToString();
                int Valor = Convert.ToInt32(cantproppencom);
                sumapropuesta = sumapropuesta + Valor;
            }
            drc1.Close();

        }
    }



    //Cantidad de anteproyectos


    protected void anteproyectosaprobcom()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(A.Apro_Codigo) FROM  anteproyecto a, estudiante e, profesor d WHERE a.Apro_Codigo = e.PROP_CODIGO and a.Anp_Estado = 'APROBADO'  AND d.Com_Codigo= e.Prog_Codigo AND d.Usu_Username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantanteaprobcom = drc1.GetInt32(0).ToString();
                int Valor = Convert.ToInt32(cantanteaprobcom);
                sumaanteproyecto = sumaanteproyecto + Valor;
            }
            drc1.Close();

        }

    }


    protected void anteproyectorechacom()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(A.Apro_Codigo) FROM  anteproyecto a, estudiante e, profesor d WHERE a.Apro_Codigo = e.PROP_CODIGO and a.Anp_Estado = 'RECHAZADO'  AND d.Com_Codigo= e.Prog_Codigo AND d.Usu_Username='" + Session["id"] + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantanterechacom = drc1.GetInt32(0).ToString();
                int Valor = Convert.ToInt32(cantanterechacom);
                sumaanteproyecto = sumaanteproyecto + Valor;
            }
            drc1.Close();

        }
    }

    protected void anteproyectopencom()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(A.Apro_Codigo) FROM  anteproyecto a, estudiante e, profesor d WHERE a.Apro_Codigo = e.PROP_CODIGO and a.Anp_Estado = 'PENDIENTE'  AND d.Com_Codigo= e.Prog_Codigo AND d.Usu_Username='" + Session["id"] + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                cantantepencom = drc1.GetInt32(0).ToString();
                int Valor = Convert.ToInt32(cantantepencom);
                sumaanteproyecto = sumaanteproyecto + Valor;
            }
            drc1.Close();

        }
    }



    //Cantidad de proyectos finales


    protected void proyfinalaprobcom()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(P.Ppro_Codigo) FROM  proyecto_final p, estudiante e, profesor d WHERE P.Ppro_Codigo= e.PROP_CODIGO AND P.Pf_Estado ='APROBADO' AND d.Com_Codigo= e.Prog_Codigo AND d.Usu_Username='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                finalaprobcom = drc1.GetInt32(0).ToString();
                int Valor = Convert.ToInt32(finalaprobcom);
                sumaproyectofinal = sumaproyectofinal + Valor;

            }
            drc1.Close();

        }

    }
    
    protected void proyfinalrechacom()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(P.Ppro_Codigo) FROM  proyecto_final p, estudiante e, profesor d WHERE P.Ppro_Codigo= e.PROP_CODIGO AND P.Pf_Estado ='RECHAZADO' AND d.Com_Codigo= e.Prog_Codigo AND d.Usu_Username='" + Session["id"] + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
               finalrechacom = drc1.GetInt32(0).ToString();
                int Valor = Convert.ToInt32(finalrechacom);
                sumaproyectofinal = sumaproyectofinal + Valor;
            }
            drc1.Close();

        }
    }

    protected void proyfinalpencom()
    {
        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT COUNT(P.Ppro_Codigo) FROM  proyecto_final p, estudiante e, profesor d WHERE P.Ppro_Codigo= e.PROP_CODIGO AND P.Pf_Estado ='PENDIENTE' AND d.Com_Codigo= e.Prog_Codigo AND d.Usu_Username='" + Session["id"] + "'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                finalpencom = drc1.GetInt32(0).ToString();
                int Valor = Convert.ToInt32(finalpencom);
                sumaproyectofinal = sumaproyectofinal + Valor;
            }
            drc1.Close();

        }
    }


    //Cantidad de propuestas pendientes

    protected void proppendientecom()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT DISTINCT COUNT(p.PROP_CODIGO) FROM estudiante e, PROPUESTA p, PROFESOR d, director s WHERE e.prop_codigo = s.prop_codigo AND s.prop_codigo=p.prop_codigo AND p.PROP_CODIGO = e.PROP_CODIGO AND p.PROP_ESTADO = 'PENDIENTE' AND d.COM_CODIGO=e.PROG_CODIGO AND d.USU_USERNAME = '"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
               propuestapencom = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }


    //Cantidad de anteproyectos pendientes por asignar evaluador

    protected void antependientecom()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT DISTINCT COUNT(a.apro_codigo) FROM  estudiante e, anteproyecto a, profesor d WHERE a.apro_codigo = e.prop_codigo AND a.anp_evaluador = 'SIN ASIGNAR' AND d.COM_CODIGO = e.Prog_Codigo AND a.Anp_Aprobacion = 'APROBADO' AND  d.usu_username = '"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                antepenasignacion = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }



    //Cantidad de peticiones director pendientes

    protected void dirpendientecom()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT DISTINCT COUNT(s.dir_id) FROM director s, estudiante e, propuesta p, profesor d WHERE p.PROP_CODIGO = s.PROP_CODIGO AND s.PROP_CODIGO = e.PROP_CODIGO AND s.DIR_ESTADO = 'PENDIENTE' AND e.PROG_CODIGO = D.Com_Codigo AND D.Usu_Username ='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                dirpeticion = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }


    //Cantidad de peticiones director pendientes

    protected void solicitudespencom()
    {

        OracleConnection conn = con.crearConexion();
        OracleCommand cmd = null;
        if (conn != null)
        {
            string sql = "SELECT DISTINCT COUNT(s.SOLE_ID) FROM solicitud_est s, propuesta p, estudiante e, profesor d WHERE s.PROP_CODIGO = p.PROP_CODIGO AND s.SOLE_TIPO = 'Cambio Propuesta'  AND s.SOLE_ESTADO='Pendiente' AND e.PROG_CODIGO = D.Com_Codigo AND D.Usu_Username ='"+Session["id"]+"'";

            cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader drc1 = cmd.ExecuteReader();
            if (drc1.HasRows)
            {
                solicom = drc1.GetInt32(0).ToString();
            }
            drc1.Close();

        }

    }






    protected void consultaroles() {
        
        string rol = Session["rol"].ToString().Trim();
        String[] ciclo = rol.Split(' ');

        string nombre = "";
       for(int i=0; i<ciclo.Length; i++) {

            if (ciclo[i].Equals("EST"))
            {
                nombre = "Estudiante";
                daprobado.Visible = false;
                drechazado.Visible = false;
                dpendiente.Visible = false;
                dsinsolicitud.Visible = false;
                paprobado.Visible = false;
                prechazado.Visible = false;
                ppendiente.Visible = false;
                psinsubir.Visible = false;
                sinaevaluador.Visible = false;
                aevaluador.Visible = false;
                pagosinsubir.Visible = false;
                pagopendiente.Visible = false;
                pagorechazado.Visible = false;
                pagoaprobado.Visible = false;
                sindirectorante.Visible = false;
                adirectorpendiente.Visible = false;
                adirectoraprobado.Visible = false;
                adirectorrechazado.Visible = false;
                aproyfinaldirpendiente.Visible = false;
                aproyfinaldirrechazado.Visible = false;
                aproyfinaldiraprobado.Visible = false;
                aproyfinalsinsubir.Visible = false;
                sinproyfinaljur.Visible = false;
                proyfinaljuraprobado.Visible = false;
                proyfinaljurpendiente.Visible = false;
                proyfinaljurrechazado.Visible = false;
                pagosestudiante();
                estadodirector();
                estadopropuesta();
                obsdirectorpropuesta();
                obscomitepropuesta();
                anteproyectoeva();
                anteproyectoevacal();
                anteproyectoevaobs();
                anteproyectodircal();
                anteproyectodirobs();
                proyfinaldirector();
                proyfinaldirectorobs();
                proyfinaljurado();
                proyfinaljuradoobs();
                
            }
            if (ciclo[i].Equals("DOC"))
            {
                nombre = "Docente";
                proyectosdocente();
            }
            if (ciclo[i].Equals("COM"))
            {
                nombre = "Comité";
                propuestasaprobcom();
                propuestasrechacom();
                propuestaspencom();
                anteproyectosaprobcom();
                anteproyectorechacom();
                anteproyectopencom();
                proyfinalaprobcom();
                proyfinalrechacom();
                proyfinalpencom();
                proppendientecom();
                antependientecom();
                dirpendientecom();
                solicitudespencom();
            }
            if (ciclo[i].Equals("DIR"))
            {
                nombre = "Director";
                proyectodir();
                propuestaspen();
                anteproyectopen();
                proyectofinalpen();
            }
            if (ciclo[i].Equals("ADM"))
            {
                nombre = "Administrador";
                consultarfacultades();
                consultarusuarios();
                consultarprogramas();
                consultarestudiantes();
                consultarprofesor();
               
            }
            if (ciclo[i].Equals("JUR"))
            {
                nombre = "Jurado";
                proyfinaleasig();
                proyfinalrevision();
            }
            if (ciclo[i].Equals("EVA"))
            {
                nombre = "Evaluador";
                anteproasignado();
                anteprorevisareva();
            }
            if (ciclo[i].Equals("DEC"))
            {
                nombre = "Decana";
                propuestasaprob();
                propuestasrecha();
                propuestaspendiente();
                anteproyectosaprob();
                anteproyectosrecha();
                anteproyectospen();
                proyfinalaprob();
                proyfinalrecha();
                proyfinalpen();
                proyfinaljur();
            }
            Notificaciones.Controls.Add(new LiteralControl("<li style='background-color:black'><a href =\"#"+ciclo[i]+ "\" data-toggle=\"tab\" style=\"color:gray;\" > " +nombre+"</a></li>"));
            
         
        }
    }




    }
