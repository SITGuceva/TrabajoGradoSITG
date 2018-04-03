<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Consulta_Anteproyecto.aspx.cs" Inherits="Consulta_Anteproyecto" %>

<asp:Content ID="ConsultaAnteproyecto" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Consultar Anteproyectos</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPconsulta_anteproyecto" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">

                        <div id="Ingreso" runat="server" visible="true" class="row">
                            <asp:Table ID="Tconsulta" runat="server" HorizontalAlign="center">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Lprog" runat="server" Text="programa:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                        <asp:DropDownList ID="DDLconsultaPrograma" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" OnSelectedIndexChanged="DDLconsultaPrograma_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </asp:TableCell>
                                 
                                   <asp:TableCell>
                                        <asp:Label ID="Llprof" runat="server" Text="linea de Investigación:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                        <asp:DropDownList ID="DDLconsultaLinea" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLconsultaLinea_SelectedIndexChanged"></asp:DropDownList>
                                    </asp:TableCell>
                                  
                                     <asp:TableCell>
                                         <asp:Label ID="Ltema" runat="server" Text="tema:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                        <asp:DropDownList ID="DDLconsultaTema" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList>
                                    </asp:TableCell>

                                     <asp:TableCell><asp:Button ID="Btbuscar" OnClick="buscar" runat="server" Text="Buscar"  CssClass="btn btn-default"></asp:Button></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <br>
                        <div id="TResultado" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVresulant" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVresulant_RowDataBound" PageSize="8">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>No hay anteproyectos con los parametros especificados.</EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="APRO_CODIGO" HeaderText="Código" />
                                    <asp:BoundField DataField="ANP_NOMBRE" HeaderText="Título" />
                                    <asp:BoundField DataField="ANP_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="ANP_ESTADO" HeaderText="Estado del anteproyecto" />
                                    <asp:BoundField DataField="REVISOR" HeaderText="Revisor" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

