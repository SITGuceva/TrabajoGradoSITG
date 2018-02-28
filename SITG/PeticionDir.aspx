<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="PeticionDir.aspx.cs" Inherits="PeticionDir" %>

<asp:Content ID="PeticionDir" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Peticiones de director</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPpeticion_dir" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                            </div>
                        </div>
                        <div id="Regresar" runat="server" visible="false" class="row">


                            <asp:Table ID="TBregresar" runat="server" HorizontalAlign="center">
                                <asp:TableRow>

                                    <asp:TableCell>
                                        <asp:Button ID="BTregresar" OnClick="regresar" runat="server" Text="Regresar a peticiones" Style="background-color: white; font-size: 14px; color: black; position: relative; left: 100%;" CssClass="form-control"></asp:Button>
                                    </asp:TableCell>

                                </asp:TableRow>
                            </asp:Table>

                        </div>

                        <br>


                        <div id="TablaPeticiones" runat="server" visible="false" class="row">

                            <asp:GridView ID="gvPeDir" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvPeDir_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvPeDir_RowDataBound" PageSize="10" OnRowCommand="gvPeDir_RowCommand">
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
                                    <asp:BoundField DataField="SOL_ID" HeaderText="Código de la petición" />
                                    <asp:BoundField DataField="SOL_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código de propuesta" />
                                    <asp:BoundField DataField="DIRECTOR" HeaderText="Director solicitante" />


                                    <asp:TemplateField HeaderText="Cambiar Estado">
                                        <ItemTemplate>
                                            <asp:Button ID="BtAprobar" runat="server" Text="Aprobar" CommandName="Aprobar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:Button ID="BtRechazar" runat="server" Text="Rechazar" CommandName="Rechazar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Informacion de la propuesta">
                                        <ItemTemplate>
                                            <asp:Button ID="Ver" runat="server" Text="Ver" CommandName="Ver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PROP_CODIGO") %>'></asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="Metodo" runat="server" Value="" />
                        </div>


                        <div id="TablaTitulo" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="gvSysDatosTitulo" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvSysDatosTitulo_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvSysDatosTitulo_RowDataBound" PageSize="2">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>
                                    Esta propuesta aun no tiene observaciones
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="PROP_TITULO" HeaderText="Titulo de la propuesta" HeaderStyle-HorizontalAlign="Center" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div id="TablaIntegrantes" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="gvDatosEstudiantes" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvDatosEstudiantes_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvDatosEstudiantes_RowDataBound" PageSize="2">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="integrantes" HeaderText="Integrantes" />

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




