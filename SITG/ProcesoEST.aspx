<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ProcesoEST.aspx.cs" Inherits="ProcesoEST" %>

<asp:Content ID="ProcesoEST" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Documentos - Seguimiento Estudiante</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UProcesoEST" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">

                        <div id="Ingreso" runat="server" visible="true" class="row">
                            <asp:Table ID="Tgepropuesta" runat="server" HorizontalAlign="center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="LCodigoE" runat="server" Text="Código del Estudiante" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:TextBox ID="TBCodigoE" type="number" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Button ID="BTbuscar" runat="server" Text="CONSULTAR" OnClick="Buscar" CssClass="btn btn-default"></asp:Button></asp:TableCell>
                                    <asp:TableCell><asp:Button ID="BTnueva" runat="server" Text="NUEVA CONSULTA" OnClick="Nueva" CssClass="btn btn-default" Visible="false"></asp:Button></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        
                        <asp:Label ID="LBpropuesta" runat="server" Text=" Propuesta: " ForeColor="black" Font-Bold="True"></asp:Label>
                        <div id="TablaResultado" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVgepropuesta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVgepropuesta_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" /> 
                                <EmptyDataTemplate>¡El estudiante aún no ha subido ninguna propuesta o el comité no tiene permiso para ver esta información.!</EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código" />
                                    <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                                    <asp:BoundField DataField="PROP_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="DIRECTOR" HeaderText="Director" />
                                    <asp:BoundField DataField="DIRESTADO" HeaderText="Condición Director" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <br>

                          <asp:Label ID="LBanteproyecto" runat="server" Text="Anteproyecto: " ForeColor="black" Font-Bold="True"></asp:Label>
                          <div id="TablaResultado2" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVanteproyecto" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVanteproyecto_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" /> 
                                <EmptyDataTemplate>¡El estudiante aún no ha subido ningún anteproyecto.!</EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="APRO_CODIGO" HeaderText="Código" />
                                    <asp:BoundField DataField="ANP_NOMBRE" HeaderText="Título" />
                                    <asp:BoundField DataField="ANP_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="APROBACION" HeaderText="Autorización Director" />
                                    <asp:BoundField DataField="REVISOR" HeaderText="Evaluador" /> 
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                                </Columns>
                            </asp:GridView>
                        </div>

                          <br>

                          <asp:Label ID="LBproyectofinal" runat="server" Text="Proyecto final: " ForeColor="black" Font-Bold="True"></asp:Label>
                          <div id="TablaResultado3" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVproyectofinal" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVproyectofinal_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" /> 
                                <EmptyDataTemplate>¡El estudiante aún no ha subido ningún proyecto final.!</EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="PPRO_CODIGO" HeaderText="Código" />
                                    <asp:BoundField DataField="PF_TITULO" HeaderText="Título" />
                                    <asp:BoundField DataField="PF_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="APROBACION" HeaderText="Autorización Director" />
                                    <asp:BoundField DataField="PF_JUR1" HeaderText="Jurado 1" />
                                    <asp:BoundField DataField="PF_JUR2" HeaderText="Jurado 2" />
                                    <asp:BoundField DataField="PF_JUR3" HeaderText="Jurado 3" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
                    </div>
                </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>





