<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pagos_estudiante.aspx.cs" Inherits="Pagos_estudiante" %>

<asp:Content ID="Pagos_estudiante" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Pagos Estudiantes</div>
        <div class="panel-body">

            <asp:UpdatePanel runat="server" ID="UPanteproyecto">
                <ContentTemplate>


                    <div id="Consulta" runat="server" visible="false" class="row">
                        <asp:GridView ID="GVconsulta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                            AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowCommand="GVconsulta_RowCommand">
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                            <EmptyDataTemplate>¡No hay pagos pendientes por revisión!</EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="PAG_ID" HeaderText="CODIGO" />
                                <asp:BoundField DataField="PAG_NOMBRE" HeaderText="NOMBRE" />
                                <asp:BoundField DataField="PAG_FECHA" HeaderText="FECHA" />
                                <asp:BoundField DataField="PAG_ESTADO" HeaderText="ESTADO" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DOCUMENTO">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PAG_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="REVISAR PAGO">
                                    <ItemTemplate>
                                        <asp:Button ID="BTrevisar" runat="server" Text="REVISAR" class="btn btn-default" AutoPostBack="true" CommandName="Revisar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="Metodo" runat="server" Value="" />
                    </div>

                    <div id="MostrarDDLestadoP" runat="server" visible="true" style="text-align: center;">
                        <asp:DropDownList ID="DDLestadoP" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                            <asp:ListItem Value="Validar Pago" Text="Validar Pago"></asp:ListItem>
                            <asp:ListItem Value="APROBADO" Text="Aprobar"></asp:ListItem>
                            <asp:ListItem Value="RECHAZADO" Text="Rechazar"></asp:ListItem>
                        </asp:DropDownList>
                        <br>
                        <br>
                        <asp:Label ID="LDescripcion" runat="server" Text="" ForeColor="Black" Font-Bold="True"></asp:Label>
                        <br>
                        <textarea id="TAdescripcion" row="2" enabled="true" width="900" runat="server" cssclass="form-control"></textarea>
                        <br>
                        <br>
                        <asp:Button ID="BTenviar" runat="server" Text="Guardar" OnClick="guardar" class="btn btn-default" />
                    </div>
                    </div>
                    </div>
                   </div>
                     <asp:ImageButton ID="IBregresar" OnClick="regresar" runat="server" Visible="false" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline"></asp:ImageButton>
                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>

                </ContentTemplate>

            </asp:UpdatePanel>
</asp:Content>
