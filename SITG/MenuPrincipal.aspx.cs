
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
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
        //Ftp();
    }

    private void Ftp()
    {
        // Get the object used to communicate with the server.  
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://192.168.1.7/"+FUdocumento.FileName);
        request.Method = WebRequestMethods.Ftp.UploadFile;

        // This example assumes the FTP site uses anonymous logon.  
        request.Credentials = new NetworkCredential("ASUS VX199H", "cole1");

        // Copy the contents of the file to the request stream.  
        StreamReader sourceStream = new StreamReader(FUdocumento.FileContent);
        byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
        sourceStream.Close();

        request.ContentLength = fileContents.Length;
        Stream requestStream = request.GetRequestStream();
        requestStream.Write(fileContents, 0, fileContents.Length);
        requestStream.Close();

        var response = (FtpWebResponse)request.GetResponse();
        Linfo.Text = response.StatusDescription;
        response.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Ftp();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://192.168.1.7/primera.jpeg");
        request.Method = WebRequestMethods.Ftp.DownloadFile;

        // This example assumes the FTP site uses anonymous logon.  
        request.Credentials = new NetworkCredential("ASUS VX199H", "cole1");

        FtpWebResponse response = (FtpWebResponse)request.GetResponse();

        Stream responseStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(responseStream);
        Console.WriteLine(reader.ReadToEnd());

        Linfo.Text="Download Complete" +response.StatusDescription;

        reader.Close();
        response.Close();
    }
}
