<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RestablecerPass.aspx.cs" Inherits="RestablecerPass" %>

<asp:Content ID="RestablecerPass" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color: #1C2833; color: white">Configuración - Restablecer Contraseña</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPrestablecerpass" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <div id="Actualizar" runat="server" visible="true" class="row">
                                        <asp:Table ID="Tcambiarpass" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lcodigo" runat="server" Text="Código:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="TBcodigo" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Consultar" class="btn btn-success" ForeColor="White" /></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Cancelar" class="btn btn-danger" ForeColor="White" Visible="false" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>

                                    <br>

                                    <div id="Mostrardatos" runat="server" visible="false" class="row" style="overflow-x: auto">
                                        <asp:GridView ID="GVdatos" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVdatos_RowDataBound">
                                            <AlternatingRowStyle BackColor="White" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                                                <asp:BoundField DataField="USU_CORREO" HeaderText="Correo" />
                                                <asp:BoundField DataField="USU_TELEFONO" HeaderText="Telefono" />
                                            </Columns>
                                        </asp:GridView>
                                        <br>
                                        <asp:Table ID="Treestablece" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Button ID="Brestablecer" OnClick="Restablecer" runat="server" Text="Restablecer" class="btn btn-success" ForeColor="White" OnClientClick="return confirm('¿Está seguro de restablecer la contraseña?');" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>

                                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript"> 
        function Confirmacion() {
            var seleccion = confirm("acepta el mensaje ?");
            if (seleccion)
                alert("se acepto el mensaje");
            else
                alert("NO se acepto el mensaje");
            return seleccion;
        }
    </script>
</asp:Content>

