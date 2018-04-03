<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pagos_Estudiante.aspx.cs" Inherits="Pagos_Estudiante" %>

<asp:Content ID="Pagos_Estudiante" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Decanatura - Pagos Estudiantes</div>
        <div class="panel-body">
            <asp:UpdatePanel runat="server" ID="UPpagosest"><ContentTemplate>

                    <div class="container-fluid">

                    <div id="Consulta" runat="server" visible="true" class="row" style="overflow-x: auto">
                        <asp:GridView ID="GVconsulta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                            AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowCommand="GVconsulta_RowCommand">
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                            <EmptyDataTemplate>¡No hay pagos pendientes por revisión!</EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="PAG_ID" HeaderText="ID" />
                                <asp:BoundField DataField="PAG_NOMBRE" HeaderText="NOMBRE" />
                                <asp:BoundField DataField="ESTUDIANTE" HeaderText="ESTUDIANTE" />
                                <asp:BoundField DataField="PAG_FECHA" HeaderText="FECHA" />
                                <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DOCUMENTO">
                                    <ItemTemplate><asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PAG_ID") %>'></asp:LinkButton> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="REVISAR PAGO">
                                    <ItemTemplate><asp:Button ID="BTrevisar" runat="server" Text="REVISAR" class="btn btn-default" AutoPostBack="true" CommandName="Revisar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /> </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>         
                         <asp:HiddenField ID="Metodo" runat="server" Value="" />
                    </div>

                    <div id="MostrarDDLestadoP" runat="server" visible="false" style="text-align: center;">                      
                        <asp:table id="taprobar" runat="server" HorizontalAlign="Center">
                         <asp:TableRow>
                             <asp:TableCell><asp:DropDownList ID="DDLestadoP" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                                <asp:ListItem Value="Validar Pago" Text="Validar Pago"></asp:ListItem>
                                <asp:ListItem Value="APROBADO" Text="Aprobar"></asp:ListItem>
                                <asp:ListItem Value="RECHAZADO" Text="Rechazar"></asp:ListItem>
                                </asp:DropDownList></asp:TableCell>
                             <asp:TableCell><asp:Label ID="LDescripcion" runat="server" Text="Observacion:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                             <asp:TableCell><textarea id="TAdescripcion" cols="20" enabled="true" width="900" runat="server" cssclass="form-control"></textarea></asp:TableCell>
                         </asp:TableRow>
                        </asp:table>
                        <br/>
                        <asp:Button ID="BTenviar" runat="server" Text="Guardar" OnClick="guardar" class="btn btn-default" />
                        <asp:Button ID="Bcancelar" runat="server" Text="Cancelar" OnClick="Bcancelar_Click" class="btn btn-default" />
                    </div>

                  <asp:ImageButton ID="IBregresar" OnClick="regresar" runat="server" Visible="false" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline"></asp:ImageButton>
                  <br />
                  <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>

                    </div>
            </ContentTemplate> </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
