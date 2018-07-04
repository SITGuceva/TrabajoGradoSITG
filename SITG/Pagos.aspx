<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pagos.aspx.cs" Inherits="Pagos" %>

<asp:Content ID="Pagos" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Documentos - Pagos</div>
        <div class="panel-body">
            <asp:UpdatePanel runat="server" ID="UPpagos"><ContentTemplate>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <ul class="breadcrumb">
                                <li><asp:LinkButton ID="LBSubir_pago" runat="server" OnClick="Subir_pago" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Subir </asp:LinkButton></li>
                                <li><asp:LinkButton ID="LBConsulta_pago" runat="server" OnClick="Consulta_pago" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar </asp:LinkButton></li>
                            </ul>

                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <div id="Ingreso" runat="server" visible="false" class="row">
                                        <asp:Label ID="Lmensaje" Text="Ingresa un documento en formato pdf el cual debe contener los certificados de cada pago correspondientes a los requerimientos establecidos por la UCEVA." ForeColor="Black" runat="server"></asp:Label>
                                        <br>
                                        <asp:Table ID="Tsubir" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Ltitulo" runat="server" Text="Título:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="TBtitulo" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="Ldocumento" runat="server" Text="Documento:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:FileUpload ID="FUdocumento" runat="server" class="btn btn-default image-preview-input" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <br>
                                        <asp:Table ID="TableBotones" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bguardar" runat="server" Text="Guardar" OnClick="Guardar" class="btn btn-success" ForeColor="White" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Blimpiar" runat="server" Text="Cancelar" OnClick="Blimpiar_Click" class="btn btn-danger" ForeColor="White" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>

                                    <div id="Consulta" runat="server" visible="false" class="row" style="overflow-x: auto">
                                        <asp:GridView ID="GVconsulta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                            AutoGenerateColumns="False" CssClass="table table-bordered bs-table">
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No se ha subido ningún pago.!</EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="PAG_ID" HeaderText="Id" />
                                                <asp:BoundField DataField="PAG_NOMBRE" HeaderText="Título" />
                                                <asp:BoundField DataField="PAG_FECHA" HeaderText="Fecha" />
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                                                <asp:BoundField DataField="PAG_OBSERVACION" HeaderText="Observación" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
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
                            </div>
                        </div>
                    </div>
                </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Bguardar" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
