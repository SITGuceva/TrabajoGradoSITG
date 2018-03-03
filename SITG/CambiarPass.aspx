<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CambiarPass.aspx.cs" Inherits="CambiarPass" %>

<asp:Content ID="CambiarPass" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Configuracion - Cambiar Contraseña</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPcambiarpass" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">  
                        <asp:Label ID="Ltitulo" runat="server" ForeColor="Black" Font-Bold="True" class="text-justify" Text="Cambiar Contraseña"></asp:Label>                    
                       
                         <div id="Actualizar" runat="server" visible="true" class="row">
                            <asp:Table ID="Tcambiarpass" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lpassactual" runat="server" Text="Contraseña Actual:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label> </asp:TableCell>                                
                                    <asp:TableCell><asp:TextBox ID="TBpassactual" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lpassnueva" runat="server" Text="Contraseña Nueva:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBpassnueva" runat="server"  CssClass="form-control" TextMode="Password"></asp:TextBox> </asp:TableCell>                                    
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lpassnueva2" runat="server" Text="Repetir Contraseña Nueva:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBpassnueva2" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                     
                            <asp:Table ID="TBotones" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Button ID="LBacpetar" runat="server" OnClick="Aceptar" Text="Guardar Cambios" class="btn btn-default"/></asp:TableCell>
                                    <asp:TableCell><asp:Button ID="LBcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-default" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

