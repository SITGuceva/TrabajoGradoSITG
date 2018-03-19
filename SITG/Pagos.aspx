<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pagos.aspx.cs" Inherits="Pagos" %>

<asp:Content ID="Pagos" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Pagos</div>
        <div class="panel-body">
            <asp:UpdatePanel runat="server" ID="UPpagos"><ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="breadcrumb">
                                    <li><asp:LinkButton ID="LBSubir_pago" runat="server" OnClick="Subir_pago" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Subir pago </asp:LinkButton></li>
                                    <li><asp:LinkButton ID="LBConsulta_pago" runat="server" OnClick="Consulta_pago" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consulta </asp:LinkButton></li>
                                </ul>
                            </div>
                        </div>

                        <div id="Ingreso" runat="server" visible="false" class="row">
                            <asp:Label ID="Lmensaje" Text="Ingresa un documento en formato pdf el cual debe contener los certificados de cada pago correspondientes a los requerimientos establecidos por la UCEVA" ForeColor="Black" runat="server"></asp:Label>
                            <br>
                            <asp:Table ID="Tsubir" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Ltitulo" runat="server" Text="Título del pago" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBtitulo" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="Ldocumento" runat="server" Text="Documento:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:FileUpload ID="FUdocumento" runat="server" class="btn btn-default image-preview-input" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br>
                            <asp:Table ID="TableBotones" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Button ID="Bguardar" runat="server" Text="Subir" OnClick="Guardar" class="btn btn-default" /> </asp:TableCell>
                                    <asp:TableCell><asp:Button ID="Blimpiar" runat="server" Text="Limpiar" OnClick="Blimpiar_Click" class="btn btn-default" /> </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <div id="Consulta" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVconsulta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" CssClass="table table-bordered bs-table">
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>¡No ha subido ningun pago!</EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="PAG_ID" HeaderText="CODIGO" />
                                    <asp:BoundField DataField="PAG_NOMBRE" HeaderText="NOMBRE" />
                                    <asp:BoundField DataField="PAG_FECHA" HeaderText="FECHA" />
                                    <asp:BoundField DataField="PAG_ESTADO" HeaderText="ESTADO" />
                                    <asp:BoundField DataField="PAG_OBSERVACION" HeaderText="OBSERVACIÓN" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DOCUMENTO">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PAG_ID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="Metodo" runat="server" Value="" />
                     </div>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Bguardar" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
