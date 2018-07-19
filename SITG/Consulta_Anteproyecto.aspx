<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Consulta_Anteproyecto.aspx.cs" Inherits="Consulta_Anteproyecto" %>

<asp:Content ID="ConsultaAnteproyecto" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-body" style="margin-left: auto; margin-right: auto; text-align: center;"> 
            <asp:Label ID="Ltitle" runat="server"  Text="DECANO" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#333333" ToolTip="La opción pertenece al rol decano." ></asp:Label>
        </div>
    </div>
<div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Documentos - Consultar Anteproyectos</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPconsulta_anteproyecto" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <div id="Ingreso" runat="server" visible="true" class="row">
                                        <asp:Table ID="Tconsulta" runat="server" HorizontalAlign="center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lprog" runat="server" Text="Programa:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                                    <asp:DropDownList ID="DDLconsultaPrograma" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" OnSelectedIndexChanged="DDLconsultaPrograma_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <asp:Label ID="Llprof" runat="server" Text="Línea de Investigación:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                                    <asp:DropDownList ID="DDLconsultaLinea" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLconsultaLinea_SelectedIndexChanged"></asp:DropDownList>
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <asp:Label ID="Ltema" runat="server" Text="Tema:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                                    <asp:DropDownList ID="DDLconsultaTema" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLconsultaTema_SelectedIndexChanged"></asp:DropDownList>
                                                </asp:TableCell>

                                                <asp:TableCell>
                                                    <asp:Button ID="Btbuscar" OnClick="buscar" runat="server" Text="BUSCAR" CssClass="btn btn-default"></asp:Button></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>

                                    <br>
                                    <div id="TResultado" runat="server" visible="false" class="row" style="overflow-x: auto">
                                        <asp:GridView ID="GVresulant" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVresulant_RowDataBound" PageSize="8">
                                            <AlternatingRowStyle BackColor="White" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>No hay anteproyectos con los parámetros especificados.</EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="APRO_CODIGO" HeaderText="Código" />
                                                <asp:BoundField DataField="ANP_NOMBRE" HeaderText="Título" />
                                                <asp:BoundField DataField="ANP_FECHA" HeaderText="Fecha Entrega" />
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado del Anteproyecto" />
                                                <asp:BoundField DataField="REVISOR" HeaderText="Revisor" />
                                                <asp:BoundField DataField="FASIGNADO" HeaderText="Fecha Asignación" />
                                                <asp:BoundField DataField="FRPTA" HeaderText="Fecha Respuesta" />
                                                <asp:BoundField DataField="FENVIO" HeaderText="Fecha Envío" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

