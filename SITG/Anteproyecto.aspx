<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Anteproyecto.aspx.cs" Inherits="Anteproyecto" %>

<asp:Content ID="Propuesta" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Anteproyecto</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPanteproyecto" runat="server"> <ContentTemplate>
                <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="breadcrumb">
                                    <li><asp:LinkButton ID="LBSubir" runat="server" OnClick="Subir_anteproyecto" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Subir </asp:LinkButton></li>
                                    <li><asp:LinkButton ID="LBConsulta" runat="server" OnClick="Consulta_anteproyecto" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consulta </asp:LinkButton></li>
                                </ul>
                            </div>
                        </div>

                        <div id="Ingreso" runat="server" visible="false" class="row">
                            <asp:Table ID="Tsubir" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Ldocumento" runat="server" Text="Documento:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:FileUpload ID="FUdocumento" runat="server" class="btn btn-default image-preview-input" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow >
                                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="2"><asp:Button ID="Bguardar" runat="server" Text="Guardar" OnClick="Guardar" class="btn btn-default" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>                          
                        </div>

                        <div id="Consulta" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVconsulta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowCommand="GVconsulta_RowCommand">
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>¡No se ha subido el anteproyecto!  </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="APRO_CODIGO" HeaderText="CODIGO" />
                                    <asp:BoundField DataField="ANP_NOMBRE" HeaderText="TITULO" />                                   
                                    <asp:BoundField DataField="ANP_FECHA" HeaderText="FECHA" />
                                    <asp:BoundField DataField="ANT_APROBACION" HeaderText="APROBACION DIRECTOR" />
                                    <asp:BoundField DataField="ANT_EVALUADOR" HeaderText="EVALUADOR" />                             
                                    <asp:BoundField DataField="ANT_ESTADO" HeaderText="ESTADO" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DOCUMENTO">
                                        <ItemTemplate><asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("APRO_CODIGO") %>'></asp:LinkButton> </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OBSERVACIONES">
                                        <ItemTemplate><asp:Button ID="Bobservaciones" runat="server" Text="Ver" CommandName="buscar" class="btn btn-default" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div id="Observaciones" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVobservacion" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"  OnPageIndexChanging="GVobservacion_PageIndexChanging" AutoGenerateColumns="False"   CssClass="table table-bordered bs-table" OnRowDataBound="GVobservacion_RowDataBound" PageSize="6">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>¡No hay observaciones!   </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="AOBS_CODIGO" HeaderText="CODIGO" />
                                    <asp:BoundField DataField="AOBS_DESCRIPCION" HeaderText="DESCRIPCION" />
                                    <asp:BoundField DataField="AOBS_REALIZADA" HeaderText="REALIZADA POR" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="Metodo" runat="server" Value="" />
                        <asp:HiddenField ID="Responsable" runat="server" Value="" />
                     </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Bguardar" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
   </div>
</asp:Content>
