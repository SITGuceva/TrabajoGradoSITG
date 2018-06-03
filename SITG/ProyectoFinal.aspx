<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProyectoFinal.aspx.cs" Inherits="ProyectoFinal" %>

<asp:Content ID="ProyectoFinal" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Documentos - Proyecto final</div>
        <div class="panel-body">
            <asp:UpdatePanel runat="server" ID="UPproyectofinal"> <ContentTemplate>
                    <div class="container-fluid">
                  
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="breadcrumb">
                                    <li><asp:LinkButton ID="LBSubir_pfinal" runat="server" OnClick="Subir_pfinal" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Subir </asp:LinkButton></li>
                                    <li><asp:LinkButton ID="LBConsulta_pfinal" runat="server" OnClick="Consulta_pfinal" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consulta </asp:LinkButton></li>
                                    <li><asp:LinkButton ID="LBGenerar" runat="server" OnClick="Generar_pfinal" ForeColor="Black"><span class="glyphicon glyphicon-download"></span>Generar certificado </asp:LinkButton></li>
                                </ul>
                            </div>
                        </div>

                         <div id="GenerarCer" runat="server" visible="false" class="row">
                            <asp:Table ID="TBgenerar" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lcertificado" runat="server" Text="Certificado en cuál válida que el proyecto final ha sido aprobado." ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Button ID="Bgenerar" runat="server" Text="Generar" OnClick="GenerarPdf" class="btn btn-success" ForeColor="White" /> </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <div id="Ingreso" runat="server" visible="false" class="row">
                            <asp:Table ID="Tsubir" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell> <asp:Label ID="Ldocumento" runat="server" Text="Documento:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:FileUpload ID="FUdocumento" runat="server" class="btn btn-default image-preview-input" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table ID="TableBotones" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell> <asp:Button ID="Bguardar" runat="server" Text="Guardar" OnClick="Guardar" class="btn btn-success" ForeColor="White"/> </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <div id="Consulta" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVconsulta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowCommand="GVconsulta_RowCommand">
                                <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />                      
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />  
                                <EmptyDataTemplate>¡No ha subido el proyecto final</EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="PPRO_CODIGO" HeaderText="Código" />
                                    <asp:BoundField DataField="PF_TITULO" HeaderText="Título" />
                                    <asp:BoundField DataField="PF_APROBACION" HeaderText="Autorización Director" />
                                    <asp:BoundField DataField="PF_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="PF_JUR1" HeaderText="Jurado 1" />
                                    <asp:BoundField DataField="PF_JUR2" HeaderText="Jurado 2" />
                                    <asp:BoundField DataField="PF_JUR3" HeaderText="Jurado 3" />
                                    <asp:BoundField DataField="PF_ESTADO" HeaderText="ESTADO" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PPRO_CODIGO") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OBSERVACIONES">
                                        <ItemTemplate>
                                            <asp:Button ID="Bobservaciones" runat="server" Text="Ver" CommandName="buscar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div id="Observaciones" runat="server" visible="false" class="row" style="overflow-x: auto">
                            <asp:GridView ID="GVobservacion" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="GVobservacion_PageIndexChanging" AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table" OnRowDataBound="GVobservacion_RowDataBound" PageSize="6">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />  
                                <EmptyDataTemplate> ¡No hay observaciones!   </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="PFOBS_CODIGO" HeaderText="Id" />
                                    <asp:BoundField DataField="PFOBS_DESCRIPCION" HeaderText="Descripción" />
                                    <asp:BoundField DataField="PFOBS_REALIZADA" HeaderText="Realizada Por" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div id="ConsultaCrit" runat="server" visible="false" class="row"  style="overflow-x: auto">                           
                         <asp:GridView ID="GVcriterios" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                             OnPageIndexChanging="GVcriterios_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" 
                             OnRowDataBound="GVcriterios_RowDataBound" PageSize="8" caption="CRITERIOS EVALUADOS NO CUMPLIDOS" captionalign="Top">
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="white" />
                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />  
                            <EmptyDataTemplate>¡No hay una evaluación del proyecto final!</EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="CRIT_CODIGO" HeaderText="Id" />   
                                <asp:BoundField DataField="CRIT_TIPO" HeaderText="Tipo" />
                                <asp:BoundField DataField="CRIT_NOMBRE" HeaderText="Descripción" />
                            </Columns>
                         </asp:GridView>
                    </div>

                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="Codigop" runat="server" Value="" />
                        <asp:HiddenField ID="Titulo" runat="server" Value="" />
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
