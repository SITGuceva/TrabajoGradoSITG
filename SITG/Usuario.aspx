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
                                    <li><asp:LinkButton ID="LBModificar" runat="server" OnClick="Modificar" ForeColor="Black"><span class="glyphicon glyphicon-pencil"></span>Modificar</asp:LinkButton></li>
                                    <li><asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                                    <li><asp:LinkButton ID="LBInhabilitar" runat="server" OnClick="Inhabilitar" ForeColor="Black"><span class="glyphicon glyphicon-refresh"></span>Inhabilitar</asp:LinkButton></li>
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
                                    <asp:TableCell><asp:TextBox ID="TBtelefono" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="Ldireccion" runat="server" Text="Dirección:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBdireccion" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lcorreo" runat="server" Text="Correo:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBcorreo" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
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
                        

                        <div id="Actualizar" runat="server" visible="false" class="row">
                            <asp:Table ID="Tusuario2" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lcodigo2" runat="server" Text="ID:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:DropDownList ID="DDLcodigo2" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>                              
                                    <asp:TableCell ColumnSpan="2"><asp:Button ID="Bbuscar" runat="server"  class="btn btn-secondary" Text="Buscar" OnClick="consultausuario"/></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lcontra2" runat="server" Text="Contraseña:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBcontra2" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="Lnombre2" runat="server" Text="Nombre:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBnombre2" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                    
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lapellido2" runat="server" Text="Apellido:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBapellido2" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="Ltelefono2" runat="server" Text="Telefono:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBtelefono2" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                    
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Ldireccion2" runat="server" Text="Dirección:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBdireccion2" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="Lcorreo2" runat="server" Text="Correo:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBcorreo2" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table ID="Testudiante2" Visible="false" runat="server" HorizontalAlign="Center">
                                 <asp:TableRow>
                                        <asp:TableCell><asp:Label ID="Lsemestre2" runat="server" Text="Semestre:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                        <asp:TableCell><asp:TextBox ID="TBsemestre2" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox></asp:TableCell>
                                        <asp:TableCell><asp:Label ID="Lprograma2" runat="server" Text="Programa:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                        <asp:TableCell><asp:DropDownList ID="DDLprograma2" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                                    </asp:TableRow>
                             </asp:Table>
                        </div>


                        <div id="Eliminar" runat="server" visible="false" class="row">
                            <asp:Table ID="Tusuario3" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lcodigo3" runat="server" Text="ID:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:DropDownList ID="DDLcodigo" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lestado" runat="server" Text="ESTADO:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3"><asp:DropDownList ID="DDLestado" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                                            <asp:ListItem Value="ACTIVO" Text="ACTIVO"></asp:ListItem>
                                            <asp:ListItem Value="INACTIVO" Text="INACTIVO"></asp:ListItem>
                                    </asp:DropDownList></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <div id="Resultado" runat="server" visible="false" class="row">
                            <asp:GridView ID="GVusuario" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVusuario_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVusuario_RowDataBound" PageSize="6">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate> ¡No hay usuarios! </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="USU_USERNAME" HeaderText="USUARIO" />
                                    <asp:BoundField DataField="USU_NOMBRE" HeaderText="NOMBRE" />
                                    <asp:BoundField DataField="USU_APELLIDO" HeaderText="APELLIDO" />
                                    <asp:BoundField DataField="USU_TELEFONO" HeaderText="TELEFONO" />
                                    <asp:BoundField DataField="USU_DIRECCION" HeaderText="DIRECCIÓN" />
                                    <asp:BoundField DataField="USU_CORREO" HeaderText="CORREO" />
                                    <asp:BoundField DataField="USU_FCREACION" HeaderText="FECHA CREACION" />
                                    <asp:BoundField DataField="USU_FMODIFICACION" HeaderText="FECHA ULTIMA MODIFICACION" />
                                    <asp:BoundField DataField="USU_ESTADO" HeaderText="ESTADO" />
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

