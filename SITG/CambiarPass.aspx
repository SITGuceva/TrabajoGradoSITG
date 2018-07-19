<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CambiarPass.aspx.cs" Inherits="CambiarPass" %>

<asp:Content ID="CambiarPass" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Configuración - Cambiar Contraseña</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPcambiarpass" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <div id="Actualizar" runat="server" visible="true" class="row">
                                        <asp:Table ID="Tcambiarpass" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lpassactual" runat="server" Text="Contraseña Actual:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="TBpassactual" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lpassnueva" runat="server" Text="Contraseña Nueva:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="TBpassnueva" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lpassnueva2" runat="server" Text="Repetir Contraseña Nueva:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="TBpassnueva2" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                        <br />
                                        
                                        <asp:Table ID="TBotones" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Button ID="LBacpetar" runat="server" OnClick="Aceptar" Text="Guardar Cambios" class="btn btn-success" ForeColor="White" />
                                                    <asp:Button ID="btnDummy" runat="server" Text="btnDummy" OnClick="btnDummy_Click" Style="display: none" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="LBcancelar" runat="server" OnClick="Limpiar" Text="Cancelar" class="btn btn-danger" ForeColor="White" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>

                                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    <asp:HiddenField ID="HFcontra" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script type="text/javascript">
        function myconfirmbox() {
            if (confirm("¿Está seguro de cambiar la contraseña?")) {
                //trigger the button click
                __doPostBack('<%= btnDummy.UniqueID %>', "");
                return true;
            } else {
                return false;
            }
        }
     </script>
</asp:Content>

