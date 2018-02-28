<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false"   AutoEventWireup="true" CodeFile="PropuestaAsignada.aspx.cs" Inherits="PropuestaAsignada" %>

<asp:Content ID="PropuestaAsignada" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="panel panel-default">
        <div class="panel-heading">Propuestas Asignadas</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPpropuestaAsignada" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                         <div class="row">
                    <div class="col-md-12">
                       
                    </div>
                </div>
                       
                   <div id="Resultado2" runat="server" visible="false" class="row">       
                  
                     <asp:GridView ID="gvSysAsignados" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                         OnPageIndexChanging="gvSysAsignados_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" 
                         OnRowDataBound="gvSysAsignados_RowDataBound" PageSize="6"  OnRowCommand="gvSysAsignados_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡bhskjs!</EmptyDataTemplate>
                        <Columns>
                             <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código" />
                                    <asp:BoundField DataField="PROP_TITULO" HeaderText="Título de la propuesta" />
                                    <asp:BoundField DataField="PROP_ESTADO" HeaderText="Estado de la propuesta" />
                                    <asp:BoundField DataField="PROP_FECHA" HeaderText="Fecha" />

                           
                            <asp:TemplateField HeaderText="Observaciones">
                                <ItemTemplate>
                                    <asp:Button ID="Btobservaciones" runat="server" Text="Agregar observaciones" CommandName="agregar"   CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                 
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DOCUMENTO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar"  OnClick="DownloadFile" CommandArgument='<%# Eval("PROP_CODIGO") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                     </asp:GridView>
                        <asp:HiddenField ID="Metodo" runat="server" Value="" />
                </div>


                           
                         <div id="Ingreso" runat="server" visible="false" class="row">
                            <asp:Table ID="Tableobservacion" runat="server" HorizontalAlign="center">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label1" runat="server" Text="Codigo de la propuesta:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Label ID="Label2" runat="server" Text="Descripcion:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>

                                    <asp:TableCell>
                                        <asp:TextBox ID="TBcodigo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:TextBox ID="TBdescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Button ID="BTagregar" OnClick="Agregar_observacion" runat="server" Text="Agregar observacion" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Button ID="BtCancelar" OnClick="cancelar" runat="server" Text="Seleccionar otra propuesta" Style="background-color: #999595; font-size: 14px; color: white" CssClass="form-control"></asp:Button>
                                    </asp:TableCell>

                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <br>


                        <div id="Resultado" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="gvSysRol" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvSysRol_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvSysRol_RowDataBound" PageSize="10" OnRowDeleting="gvSysRol_RowDeleting" OnRowUpdating="gvSysRol_RowUpdating" OnRowEditing="gvSysRol_RowEditing" OnRowCancelingEdit="gvSysRol_RowCancelingEdit">
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


                                    <asp:BoundField DataField="OBS_CODIGO" HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="OBS_DESCRIPCION" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" />
                               

                                   

                                    <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>

                                    <asp:TemplateField HeaderText="Modificar">
                                        <ItemTemplate>
                                            <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" />
                                        </ItemTemplate>


                                        <EditItemTemplate>
                                            <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" />
                                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" />
                                        </EditItemTemplate>
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




