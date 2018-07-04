<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OpcSistema.aspx.cs" Inherits="OpcSistema" %>

<asp:Content ID="OpcSistema" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Sistema - Opciones del Sistema</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPsysopc" runat="server"> <ContentTemplate>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <ul class="breadcrumb">
                                <li>
                                    <asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                            </ul>

                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <div id="Ingreso" runat="server" visible="true" class="row">
                                        <asp:Table ID="TOpcSys" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lnombre" runat="server" Text="Nombre:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="TBnombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lcategoria" runat="server" Text="Categorías:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">
                                                    <asp:DropDownList ID="DDLcategoria" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lurl" runat="server" Text="Url:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">
                                                    <asp:TextBox ID="TBurl" runat="server" CssClass="form-control"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                        <br />
                                        
                                        <asp:Table ID="Tbotones" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-success" ForeColor="White" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Cancelar" class="btn btn-danger" ForeColor="White" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>


                                    <div id="Busqueda" runat="server" visible="false" class="row">
                                        <asp:Table ID="TOpcSys1" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lcat" runat="server" Text="Categorías:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">
                                                    <asp:DropDownList ID="DDLcat" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" OnSelectedIndexChanged="DDLcat_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bbuscar" runat="server" OnClick="Buscar" Text="Buscar" class="btn btn-default" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>


                                    <div id="Resultado" runat="server" visible="false" class="row">
                                        <asp:GridView ID="GVSysRol" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVSysRol_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVSysRol_RowDataBound"
                                            PageSize="8" OnRowUpdating="GVSysRol_RowUpdating" OnRowEditing="GVSysRol_RowEditing" OnRowCancelingEdit="GVSysRol_RowCancelingEdit">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No existen opciones del sistema con la categoría seleccionada!  </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="OPCS_ID" HeaderText="Id" />
                                                <asp:BoundField DataField="OPCS_NOMBRE" HeaderText="Nombre" />
                                                <asp:BoundField DataField="OPCS_URL" HeaderText="Url" />
                                                <asp:TemplateField HeaderText="Estado">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="estado" runat="server" class="btn btn-secondary btn-sm dropdown-toggle">
                                                            <asp:ListItem Value="ACTIVO">ACTIVO</asp:ListItem>
                                                            <asp:ListItem Value="INACTIVO">INACTIVO</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lestado" runat="server" Text='<%# Bind("OPCS_ESTADO") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modificar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" class="btn btn-default" /></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" class="btn btn-success" ForeColor="White" />
                                                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" class="btn btn-danger" ForeColor="White" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>

                                </div>
                            </div>
                        </div>
                    </div> 
                </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>



