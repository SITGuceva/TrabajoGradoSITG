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
                            <asp:Table ID="TableRol1" runat="server" HorizontalAlign="left">
                            </asp:Table>

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
                                        <asp:Button ID="Btbuscar" OnClick="Buscar_observacion" runat="server" Text="Buscar propuesta" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button>
                                        <asp:Button ID="BtCancelar" OnClick="cancelar" runat="server" Text="Buscar otra propuesta" Style="background-color: #999595; font-size: 14px; color: white" CssClass="form-control"></asp:Button>
                                    </asp:TableCell>

                                </asp:TableRow>
                            </asp:Table>

                        </div>




                        <div id="Actualizar" runat="server" visible="false" class="row">
                            <asp:Table ID="TableRol2" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Lid2" runat="server" Text="ID:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="DDLid" runat="server"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Lnombre2" runat="server" Text="NOMBRE:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:TextBox ID="TBnombre2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                        </div>

                        <div id="Eliminar" runat="server" visible="false" class="row">
                            <asp:Table ID="TableRol3" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Lid3" runat="server" Text="ID:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="DDLid2" runat="server"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Lestado" runat="server" Text="ESTADO:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:DropDownList ID="DDLestado" runat="server">
                                            <asp:ListItem Value="ACTIVO" Text="ACTIVO"></asp:ListItem>
                                            <asp:ListItem Value="INACTIVO" Text="INACTIVO"></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <br>


                        <div id="Resultado3" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="gvSysDatosTitulo" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvSysDatosTitulo_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvSysRol_RowDataBound" PageSize="2">
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
                            <asp:GridView ID="gvSysDatosPropuesta" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvSysDatosPropuesta_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvSysDatosPropuesta_RowDataBound" PageSize="2">
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
                                    <asp:BoundField DataField="OBS_FECHA" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" />

                                    <asp:TemplateField HeaderText="Estado">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="estado" runat="server">
                                                <asp:ListItem Value="ACTIVO">ACTIVO</asp:ListItem>
                                                <asp:ListItem Value="INACTIVO">INACTIVO</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("OBS_ESTADO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>

                                    <asp:TemplateField>
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

