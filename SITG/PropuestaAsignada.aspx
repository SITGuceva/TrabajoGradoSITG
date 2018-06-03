<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="PropuestaAsignada.aspx.cs" Inherits="PropuestaAsignada" %>

<asp:Content ID="PropuestaAsignada" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Documentos - Propuestas Asignadas</div>
        <div class="panel-body">
           <asp:UpdatePanel ID="UPpropasig" runat="server"> <ContentTemplate>
            <div class="container-fluid">

                <div id="ResultadoPropuesta" runat="server" visible="true" class="row" style="overflow-x: auto">
                    <asp:GridView ID="GVpropuesta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVpropuesta_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                        OnRowDataBound="GVpropuesta_RowDataBound" PageSize="8" OnRowCommand="GVpropuesta_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />    
                        <EmptyDataTemplate>¡No hay propuestas asignadas!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código" />
                            <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                            <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" />

                            <asp:TemplateField HeaderText="Propuesta">
                                <ItemTemplate>
                                    <asp:Button ID="Btpropuesta" runat="server" Text="Ver" CssClass="btn btn-default" CommandName="ver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
              
                <div id="ConsultaContenidoP" runat="server" visible="false" class="row" style="overflow-x: auto">
                    <br>
                    <asp:GridView ID="GVConsultaContenidoP" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnPageIndexChanging="GVConsultaContenidoP_PageIndexChanging" AutoGenerateColumns="False"
                        CssClass="table table-bordered bs-table" OnRowDataBound="GVConsultaContenidoP_RowDataBound" PageSize="6">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />    
                        <EmptyDataTemplate>¡No se encuentra la información de la propuesta.!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código" />
                            <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                            <asp:BoundField DataField="LINV_NOMBRE" HeaderText="Línea Investigación" />
                            <asp:BoundField DataField="TEM_NOMBRE" HeaderText="Tema" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                                <ItemTemplate><asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PROP_CODIGO") %>'></asp:LinkButton></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br>
                </div>

                <div id="Integrantes" runat="server" visible="false" class="row" style="overflow-x: auto">
                    <asp:GridView ID="GVintegrantes" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                        AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVintegrantes_RowDataBound" PageSize="5">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />    
                        <EmptyDataTemplate>¡No hay integrantes de la propuesta! </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="integrantes" HeaderText="Integrantes" />
                        </Columns>
                    </asp:GridView>
                </div>

                 <div id="ObservacionComite" runat="server" style="overflow-x: auto" visible="false" class="row">
                    <asp:Label ID="LBobservacionesCom" runat="server" Text="--Observaciones del comite--" ForeColor="Gray" Font-Bold="True"></asp:Label>
                    <asp:GridView ID="GVObservacionComite" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVObservacionComite_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                        OnRowDataBound="GVObservacionComite_RowDataBound" PageSize="8">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />    
                        <EmptyDataTemplate>¡La propuesta aún no tiene observaciones del comité!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="OBS_CODIGO" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="OBS_DESCRIPCION" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </div>

                <div id="Ingreso" runat="server" visible="false" class="row">
                    <asp:Table ID="Tobservacion" runat="server" HorizontalAlign="center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <textarea id="TBdescripcion" runat="server" CssClass="form-control" rows="2"></textarea>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="BTagregar" OnClick="Agregar_observacion" runat="server" Text="AGREGAR" CssClass="btn btn-default"></asp:Button>
                            </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="ResultadoObservacion" runat="server" visible="false" class="row" style="overflow-x: auto">
                    <asp:Label ID="LBMisObservaciones" runat="server" Text="--Mis observaciones--" ForeColor="Gray" Font-Bold="True"></asp:Label>
                    <asp:GridView ID="GVobservacion" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVobservacion_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                        OnRowDataBound="GVobservacion_RowDataBound" PageSize="8" OnRowDeleting="GVobservacion_RowDeleting" OnRowUpdating="GVobservacion_RowUpdating" OnRowEditing="GVobservacion_RowEditing" OnRowCancelingEdit="GVobservacion_RowCancelingEdit">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />    
                        <EmptyDataTemplate>¡Aún no se ha realizado ninguna observación a la propuesta.!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="OBS_CODIGO" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="OBS_DESCRIPCION" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate>
                                    <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" CssClass="btn btn-default"  />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" CssClass="btn btn-success" ForeColor="White" />
                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="btn btn-danger" ForeColor="White" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </div>
        
                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                <br/><br/>
                <asp:ImageButton id="BTregresar" OnClick="Nueva" runat="server" Visible="false" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline"></asp:ImageButton>   
                <asp:HiddenField ID="Metodo" runat="server" Value="" />
                <asp:HiddenField ID="Estado" runat="server" />
            </div>
            </ContentTemplate></asp:UpdatePanel>
            <script>
                function pulsar(e) {
                    tecla = (document.all) ? e.keyCode : e.which;
                    if (tecla == 13) return false;
                }
            </script>

        </div>
    </div>
</asp:Content>




