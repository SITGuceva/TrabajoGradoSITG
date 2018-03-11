<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ProcesoEST.aspx.cs" Inherits="ProcesoEST" %>

<asp:Content ID="ProcesoEST" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Consultar proceso del estudiante</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UProcesoEST" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">

                        <div id="Ingreso" runat="server" visible="false" class="row">
                            <asp:Table ID="Tgepropuesta" runat="server" HorizontalAlign="center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="LCodigoE" runat="server" Text="Codigo del estudiante" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:TextBox ID="TBCodigoE" type="number" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Button ID="BTbuscar" runat="server" Text="Buscar" OnClick="Buscar" CssClass="btn btn-default"></asp:Button></asp:TableCell>
                                    <asp:TableCell><asp:Button ID="BTnueva" runat="server" Text="Nueva Consulta" OnClick="Nueva" CssClass="btn btn-default" Visible="false"></asp:Button></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <br>
                         <asp:Label ID="LBpropuesta" runat="server" Text=" Propuesta: " ForeColor="black" Font-Bold="True"></asp:Label>
                        <div id="TablaResultado" runat="server" visible="false" class="row">
                            <asp:GridView ID="GVgepropuesta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVgepropuesta_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>
                                    El estudiante aún no ha subido ninguna propuesta o el comite no tiene permiso para ver esta información  
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código" />
                                    <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                                    <asp:BoundField DataField="PROP_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="PROP_ESTADO" HeaderText="Estado propuesta" />
                                    <asp:BoundField DataField="DIRECTOR" HeaderText="Director" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado director" />

                                  
                                </Columns>
                            </asp:GridView>

                            <asp:HiddenField ID="Metodo" runat="server" Value="" />

                        </div>

                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>





