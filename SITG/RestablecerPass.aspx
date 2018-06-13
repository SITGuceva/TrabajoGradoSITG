<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RestablecerPass.aspx.cs" Inherits="RestablecerPass" %>

<asp:Content ID="RestablecerPass" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Configuración - Restablecer Contraseña</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPrestablecerpass" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">  
                      
                         <div id="Actualizar" runat="server" visible="true" class="row">
                            <asp:Table ID="Tcambiarpass" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lcodigo" runat="server" Text="Código:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label> </asp:TableCell>                                
                                    <asp:TableCell><asp:TextBox ID="TBcodigo" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>

                             <br>
                     
                            <asp:Table ID="TBotones" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Button ID="LBacpetar" runat="server" OnClick="Aceptar" Text="Consultar" class="btn btn-success" ForeColor="White" /></asp:TableCell>
                                    <asp:TableCell><asp:Button ID="LBcancelar" runat="server" OnClick="Limpiar" Text="Cancelar" class="btn btn-danger" ForeColor="White"/></asp:TableCell>
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
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />    
                            <EmptyDataTemplate>¡No hay usuario con el codigo suministrado! </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />   
                                <asp:BoundField DataField="USU_CORREO" HeaderText="Correo" />   
                                <asp:BoundField DataField="USU_TELEFONO" HeaderText="Telefono" />   
                            </Columns>
                        </asp:GridView>
                    </div>

                        <br>

                        <center><asp:Button ID="LBrestablecer" onclick="Restablecer" runat="server" Visible="false"  Text="Restablecer" class="btn btn-success" ForeColor="White" /></center>

                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

