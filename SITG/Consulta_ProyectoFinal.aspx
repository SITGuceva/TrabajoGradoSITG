<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Consulta_ProyectoFinal.aspx.cs" Inherits="Consulta_ProyectoFinal" %>

<asp:Content ID="ConsultaProyectoF" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Consultar Proyecto Final</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPconsulta_proyectoF" runat="server">
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
                            <asp:GridView ID="GVresulpro" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVresulpro_RowDataBound" PageSize="8">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>No hay proyectos finales con los parametros especificados.</EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="PPRO_CODIGO" HeaderText="Código" />
                                    <asp:BoundField DataField="PF_TITULO" HeaderText="Título" />
                                    <asp:BoundField DataField="PF_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="PF_ESTADO" HeaderText="Estado del anteproyecto" />                                </Columns>
                            </asp:GridView>
                        </div>

                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

