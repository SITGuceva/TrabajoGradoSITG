<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Calendario.aspx.cs" Inherits="Calendario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="panel panel-default">
        <div class="panel-heading">Universidad - Programa</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPprog" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>
                           
                        </ul>
                    </div>
                </div>

                 <div id="Ingreso" runat="server" visible="true" class="row">
                    <asp:Table ID="TableProg" runat="server" HorizontalAlign="Center" >                 
                         <asp:TableRow>
                           <asp:TableCell><asp:Label ID="Lnombre" runat="server" Text="Titulo:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>                         
                            <asp:TableCell><asp:TextBox ID="TBnombre" runat="server" CssClass="form-control"></asp:TextBox>    </asp:TableCell>
                        </asp:TableRow>                      
                        
                          <asp:TableRow>
                             <asp:TableCell><asp:Label ID="Ldescripcion" runat="server" Text="Descripcion:" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                         
                            <asp:TableCell> <asp:TextBox ID="TBdescrip" runat="server" CssClass="form-control" ></asp:TextBox>    </asp:TableCell>                         
                        </asp:TableRow>

                        <asp:TableRow>
                             <asp:TableCell><asp:Label ID="Lfecha" runat="server" Text="Fecha:" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                         
                              <asp:TableCell><asp:Calendar ID="Cfecha" runat="server"></asp:Calendar></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>                  
                </div>

                <div id="Botones" runat="server" class="row" visible="true">
                    <asp:Table ID="Tbotones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-secondary" /></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-secondary" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

