<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DocumentosCom.aspx.cs" Inherits="DocumentosCom" %>

<asp:Content ID="Documentoscom" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Documentos</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPdocumentoscom" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="breadcrumb">
                                    <li><asp:LinkButton ID="LBpropuesta" runat="server" OnClick="LBpropuesta_Click" ForeColor="Black"><span class="glyphicons glyphicons-notes-2"></span>Propuesta </asp:LinkButton></li>
                                    <li> <asp:LinkButton ID="LBanteproyecto" runat="server" OnClick="LBanteproyecto_Click" ForeColor="Black"><span class="glyphicons glyphicons-article"></span>Anteproyecto </asp:LinkButton></li>                         
                                    <li> <asp:LinkButton ID="LBproyecto" runat="server" OnClick="LBproyecto_Click" ForeColor="Black"><span class="glyphicons glyphicons-book"></span>Proyecto </asp:LinkButton></li>                         
                                </ul>
                            </div>
                        </div>

                        <asp:Label ID="Lprop" runat="server" Text="PROPUESTA" ForeColor="Black" Font-Bold="True" class="text-justify" Visible="false"></asp:Label>
                        <asp:Label ID="Lant" runat="server" Text="ANTEPROYECTO" ForeColor="Black" Font-Bold="True" class="text-justify"  Visible="false"></asp:Label>
                        <asp:Label ID="Lpf" runat="server" Text="PROYECTO FINAL" ForeColor="Black" Font-Bold="True" class="text-justify"  Visible="false"></asp:Label>

                        <div id="Ingreso" runat="server" visible="false" class="row">
                            <asp:Table ID="Tconsulta" runat="server" HorizontalAlign="center">
                                <asp:TableRow>                                 
                                   <asp:TableCell>
                                        <asp:Label ID="Llprof" runat="server" Text="linea de Investigación:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                        <asp:DropDownList ID="DDLconsultaLinea" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" ></asp:DropDownList>
                                    </asp:TableCell>
                                     <asp:TableCell>
                                         <asp:Label ID="Lestado" runat="server" Text="Estado:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                        <asp:DropDownList ID="DDLestado" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                                            <asp:ListItem Value="0" Text="Seleccione"/>
                                            <asp:ListItem Value="1" Text="Aprobado" />
                                            <asp:ListItem Value="2" Text="Rechazado" />
                                        </asp:DropDownList> </asp:TableCell>
                                     <asp:TableCell><asp:Button ID="Btbuscar" OnClick="Btbuscar_Click" runat="server" Text="Buscar"  CssClass="btn btn-default"></asp:Button></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <br>
                        <div id="ResultadoPropuesta" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVpropuesta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVpropuesta_RowDataBound" PageSize="8">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>No hay propuestas con los parametros especificados.</EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código" />
                                    <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="DIRECTOR" HeaderText="Director" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado director" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div id="ResultadoAnteproyecto" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVanteproyecto" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVanteproyecto_RowDataBound" PageSize="8">
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
                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="DIRECTOR" HeaderText="Director" />
                                    <asp:BoundField DataField="ANT_APROBACION" HeaderText="Aprobacion director" />
                                    <asp:BoundField DataField="REVISOR" HeaderText="Revisor" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div id="ResultadoProyectoF" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVproyectoF" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVproyectoF_RowDataBound" PageSize="8">
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
                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="DIRECTOR" HeaderText="Director" />
                                    <asp:BoundField DataField="PF_APROBACION" HeaderText="Aprobacion director" />
                                </Columns>
                            </asp:GridView>
                        </div>


                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="tipo" Value="" runat="server" />
                     </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>