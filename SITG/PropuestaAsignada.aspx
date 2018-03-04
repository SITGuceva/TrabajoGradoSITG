<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false"   AutoEventWireup="true" CodeFile="PropuestaAsignada.aspx.cs" Inherits="PropuestaAsignada" %>

<asp:Content ID="PropuestaAsignada" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="panel panel-default">
        <div class="panel-heading">Propuestas Asignadas</div>
        <div class="panel-body">

            <div class="container-fluid">

                <div id="ResultadoPropuesta" runat="server" visible="true" class="row">
                    <asp:GridView ID="GVpropuesta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVpropuesta_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                        OnRowDataBound="GVpropuesta_RowDataBound" PageSize="8" OnRowCommand="GVpropuesta_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡No hay propuestas asignadas!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código" />
                            <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                            <asp:BoundField DataField="PROP_ESTADO" HeaderText="Estado" />
                            <asp:BoundField DataField="PROP_FECHA" HeaderText="Fecha" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PROP_CODIGO") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Observaciones">
                                <ItemTemplate>
                                    <asp:Button ID="Btobservaciones" runat="server" Text="Ver" CssClass="btn btn-default" CommandName="agregar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:HiddenField ID="Metodo" runat="server" Value="" />
                </div>

            
                <div id="Ingreso" runat="server" visible="false" class="row">
                    <asp:Table ID="Tobservacion" runat="server" HorizontalAlign="center">                    
                        <asp:TableRow>
                            <asp:TableCell> <asp:TextBox ID="TBdescripcion" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:Button ID="BTagregar" OnClick="Agregar_observacion" runat="server" Text="Agregar observacion"  CssClass="btn btn-default"></asp:Button></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Btnueva" OnClick="Nueva" runat="server" Text="Seleccionar otra propuesta" CssClass="btn btn-default"></asp:Button></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <br>


                <div id="ResultadoObservacion" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                    <asp:GridView ID="GVobservacion" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"  OnPageIndexChanging="GVobservacion_PageIndexChanging"  AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                        OnRowDataBound="GVobservacion_RowDataBound" PageSize="8" OnRowDeleting="GVobservacion_RowDeleting" OnRowUpdating="GVobservacion_RowUpdating" OnRowEditing="GVobservacion_RowEditing" OnRowCancelingEdit="GVobservacion_RowCancelingEdit">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡La propuesta aun no tiene observaciones!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="OBS_CODIGO" HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="OBS_DESCRIPCION" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate>
                                    <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" />
                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                              <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
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

        </div>
  </div>
</asp:Content>




