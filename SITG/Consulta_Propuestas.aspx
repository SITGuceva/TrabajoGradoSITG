<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Consulta_Propuestas.aspx.cs" Inherits="Consulta_Propuestas" %>

<asp:Content ID="PeticionDir" ContentPlaceHolderID="MainContent" runat="Server">
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
                                OnRowDataBound="gvTablaResultado_RowDataBound" PageSize="10">
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



                                    <asp:TemplateField HeaderText="Informacion de la propuesta">
                                        <ItemTemplate>
                                            <asp:Button ID="Ver" runat="server" Text="Ver" CommandName="Ver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PROP_CODIGO") %>'></asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

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




