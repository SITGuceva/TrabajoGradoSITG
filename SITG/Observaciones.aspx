<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Observaciones.aspx.cs" Inherits="Observaciones" %>

<asp:Content ID="Roles" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Sistema - Observaciones</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UProl" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                            </div>
                        </div>

                        <div id="Ingreso" runat="server" visible="false" class="row">
                      

                            <asp:Table ID="TBingresar" runat="server" HorizontalAlign="center">
                                <asp:TableRow>

                                    <asp:TableCell>
                                        <asp:Label ID="LCodigo" runat="server" Text="Codigo de la propuesta:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Label ID="LDescripcion" runat="server" Text="Descripcion:" ForeColor="Black" Font-Bold="True"></asp:Label>
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
                                        <asp:Button ID="Btbuscar" OnClick="Buscar_observacion" runat="server" Text="Buscar propuesta" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button>
                                        <asp:Button ID="BtCancelar" OnClick="cancelar" runat="server" Text="Buscar otra propuesta" Style="background-color: #999595; font-size: 14px; color: white" CssClass="form-control"></asp:Button>
                                    </asp:TableCell>

                                </asp:TableRow>
                            </asp:Table>

                        </div>

                    <br>

                        <div id="Resultado3" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="gvTitulo" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvTitulo_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvTitulo_RowDataBound" PageSize="10">
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

                        <div id="Resultado2" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="gvDatosPropuesta" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvDatosPropuesta_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvDatosPropuesta_RowDataBound" PageSize="2">
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

                        <div id="Resultado" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="gvObservaciones" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvObservaciones_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvObservaciones_RowDataBound" PageSize="10" OnRowDeleting="gvObservaciones_RowDeleting" OnRowUpdating="gvObservaciones_RowUpdating" OnRowEditing="gvObservaciones_RowEditing" OnRowCancelingEdit="gvObservaciones_RowCancelingEdit">
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

