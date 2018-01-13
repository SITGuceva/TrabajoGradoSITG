<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Asignar_Roles.aspx.cs" Inherits="Docente" %>

<asp:Content ID="AsignarRol" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="panel panel-default">
        <div class="panel-heading">Gestionar Usuarios - Asignar Roles</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPasigrol" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBModificar" runat="server" OnClick="Modificar" ForeColor="Black"><span class="glyphicon glyphicon-pencil"></span>Modificar</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBInhabilitar" runat="server" OnClick="Inhabilitar" ForeColor="Black"><span class="glyphicon glyphicon-refresh"></span>Inhabilitar</asp:LinkButton></li>
                        </ul>
                    </div>
                </div>

                <div id="Ingreso" runat="server" visible="true" class="row">
                    <asp:Table ID="Tasigrol" runat="server" HorizontalAlign="Center">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell><asp:Label ID="Ltitulo" runat="server" Text="ASIGNAR ROL AL USUARIO" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lcodigo" runat="server" Text="Codigo:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBcodigo" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                         <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lusuario" runat="server" Text="Usuario:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLusuario" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lrol" runat="server" Text="Rol:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLrol" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="DIVBuscar" runat="server" visible="true" class="row">
                    <asp:Table ID="Tablebuscar" runat="server" HorizontalAlign="Center" Visible="false">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lcodigo2" runat="server" Text="Codigo de Usuario:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLcodigo2" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bbuscar" runat="server" OnClick="Buscar" CssClass="btn btn-default" Text="BUSCAR" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="Tableinfo" runat="server" HorizontalAlign="Center" Visible="false">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Luser" runat="server" Text="Usuario:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label ID="Lansusu" runat="server" CssClass="form-control"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="Actualizar" runat="server" visible="false" class="row">
                    <asp:Table ID="Tasigrol2" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lrolactualizar" runat="server" Text="Rol:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLrolactu" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lroles" runat="server" Text="Roles:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLroles" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="Eliminar" runat="server" visible="false" class="row">
                    <asp:Table ID="Tasigrol3" runat="server" HorizontalAlign="Center">
                         <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lroleliminar" runat="server" Text="Rol:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLrolelim" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lestado" runat="server" Text="Estado:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell ColumnSpan="3"><asp:DropDownList ID="DDLestado" class="btn btn-secondary btn-lg dropdown-toggle"  runat="server">
                                    <asp:ListItem Value="ACTIVO" Text="ACTIVO"></asp:ListItem>
                                    <asp:ListItem Value="INACTIVO" Text="INACTIVO"></asp:ListItem>
                            </asp:DropDownList></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="Resultado" runat="server" visible="false" class="row">                   
                     <asp:GridView ID="GVasigrol" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVasigrol_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVasigrol_RowDataBound" PageSize="6">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡No hay usuarios asignados para el rol!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="USUROL_ID" HeaderText="ID" />   
                            <asp:BoundField DataField="ROL_ID" HeaderText="CODIGO ROL" /> 
                            <asp:BoundField DataField="ROL_NOMBRE" HeaderText="ROL" /> 
                            <asp:BoundField DataField="USUROL_ESTADO" HeaderText="ESTADO" />                           
                        </Columns>
                     </asp:GridView>
                </div>

                <div id="Botones" runat="server" class="row" visible="true">
                    <asp:Table ID="Tbotones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-secondary" /></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-secondary" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>    
                <asp:HiddenField ID="Metodo" runat="server" Value="" />                    
                </div>
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>



