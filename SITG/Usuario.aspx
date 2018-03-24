<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Usuario.aspx.cs" Inherits="Estudiante" %>

<asp:Content ID="Usuario" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Gestionar Usuarios - Usuarios</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPsysrol" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="breadcrumb">
                                    <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>
                                    <li><asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>                                 
                                </ul>
                            </div>
                        |</div>
                      
                        <div id="Ingreso" runat="server" visible="true" class="row">
                            <asp:Table ID="Tusuario" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lcodigo" runat="server" Text="Codigo:"  ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBcodigo" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                    <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="TBcodigo" CssClass="text-danger" ErrorMessage="El codigo es obligatorio." ForeColor="#CC0000" />--%>
                                    <asp:TableCell><asp:Label ID="Lcontra" runat="server" Text="Contraseña:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBcontra" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lnombre" runat="server" Text="Nombre:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBnombre" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>      
                                    <asp:TableCell><asp:Label ID="Lapellido" runat="server" Text="Apellido:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBapellido" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Ltelefono" runat="server" Text="Telefono:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBtelefono" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="Ldireccion" runat="server" Text="Dirección:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBdireccion" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lcorreo" runat="server" Text="Correo:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBcorreo" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="Lrol" runat="server" Text="Rol:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:DropDownList ID="DDLrol" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" OnSelectedIndexChanged="DDLrol_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="NULL" Text="NINGUNO"></asp:ListItem>
                                            <asp:ListItem Value="EST" Text="ESTUDIANTE"></asp:ListItem>
                                            <asp:ListItem Value="DOC" Text="DOCENTE"></asp:ListItem>
                                        </asp:DropDownList></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                             <asp:Table ID="Testudiante" Visible="false" runat="server" HorizontalAlign="Center">
                                 <asp:TableRow>
                                        <asp:TableCell><asp:Label ID="Lsemestre" runat="server" Text="Semestre:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                        <asp:TableCell><asp:TextBox ID="TBsemestre" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox></asp:TableCell>
                                        <asp:TableCell><asp:Label ID="Lprograma" runat="server" Text="Programa:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                        <asp:TableCell><asp:DropDownList ID="DDLprograma" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                                    </asp:TableRow>
                             </asp:Table>
                        </div>


                           <div id="ConsultarUsuario" runat="server" visible="false" class="row">
                            <asp:Table ID="Tconsulta" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Label1" runat="server" Text="Rol del usuario:"  ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                    <asp:DropDownList ID="DDLconsulta" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" OnSelectedIndexChanged="DDLconsulta_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="TODOS" Text="TODOS"></asp:ListItem>
                                            <asp:ListItem Value="EST" Text="ESTUDIANTE"></asp:ListItem>
                                            <asp:ListItem Value="DOC" Text="DOCENTE"></asp:ListItem>
                                        </asp:DropDownList>
                                        </asp:TableCell>
                                        
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <div id="Resultado" runat="server" style="overflow-x: auto" visible="false" class="row">
                            <asp:GridView ID="GVusuarios" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVusuarios_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVusuarios_RowDataBound" PageSize="10"
                                OnRowUpdating="GVusuarios_RowUpdating" OnRowEditing="GVusuarios_RowEditing" OnRowCancelingEdit="GVusuarios_RowCancelingEdit" CellSpacing="10" CellPadding="10">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>¡No hay usuarios con los parametros especificados.! </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="USU_USERNAME" HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="USU_NOMBRE" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="USU_APELLIDO" HeaderText="Apellido" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="USU_TELEFONO" HeaderText="Telefono" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="USU_DIRECCION" HeaderText="Dirección" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Estado">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="estado" runat="server" class="btn btn-secondary btn-sm dropdown-toggle">
                                                <asp:ListItem Value="ACTIVO">ACTIVO</asp:ListItem>
                                                <asp:ListItem Value="INACTIVO">INACTIVO</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate><asp:Label ID="Lestado" runat="server" Text='<%# Bind("USU_ESTADO") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modificar">
                                        <ItemTemplate><asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" /></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" />
                                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div id="Botones" runat="server" class="row" visible="true">
                            <asp:Table ID="Tbotones" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Button ID="LBacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-secondary"/></asp:TableCell>
                                    <asp:TableCell><asp:Button ID="LBcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-secondary"/></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

