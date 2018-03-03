<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="GEPropuesta.aspx.cs" Inherits="CambiarEstadoP" %>

<asp:Content ID="CambiarEstadoP" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Peticiones de director</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPconsulta_propuestas" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                            </div>
                        </div>

                        <div id="Ingreso" runat="server" visible="false" class="row">


                            <asp:Table ID="TBIngreso" runat="server" HorizontalAlign="center">
                                <asp:TableRow>

                                    <asp:TableCell>
                                        <asp:Label ID="LCodigoP" runat="server" Text="Codigo de la propuesta:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Label ID="LCodigoE" runat="server" Text="Codigo del estudiante" ForeColor="Black" Font-Bold="True"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>

                                    <asp:TableCell>
                                        <asp:TextBox ID="TBCodigoP" runat="server" CssClass="form-control"></asp:TextBox>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:TextBox ID="TBCodigoE" runat="server" CssClass="form-control"></asp:TextBox>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Button ID="BTbuscar" runat="server" Text="Buscar" OnClick="Buscar" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button>
                                    </asp:TableCell>



                                </asp:TableRow>
                            </asp:Table>

                        </div>

                        <br>
                        <div id="TablaResultado" runat="server" visible="false" class="row">

                            <asp:GridView ID="gvTablaResultado" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvTablaResultado_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvTablaResultado_RowDataBound" PageSize="10" OnRowCommand="gvTablaResultado_RowCommand">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate></EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código" />
                                    <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                                    <asp:BoundField DataField="PROP_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="PROP_ESTADO" HeaderText="Estado de la propuesta" />
                                    <asp:BoundField DataField="DIRECTOR" HeaderText="Director" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado director" />

                                    
                                    <asp:TemplateField HeaderText="Cambiar Estado">
                                        <ItemTemplate>
                                            <asp:Button ID="BtAprobar" runat="server" Text="Aprobar" CommandName="Aprobar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:Button ID="BtRechazar" runat="server" Text="Rechazar" CommandName="Rechazar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                 



                                </Columns>
                            </asp:GridView>

                            <asp:HiddenField ID="Metodo" runat="server" Value="" />

                        </div>











                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
                    </div>

                    <script>
                        function pulsar(e) {
                            tecla = (document.all) ? e.keyCode : e.which;
                            if (tecla == 13) return false;
                        }
                    </script>


                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>




