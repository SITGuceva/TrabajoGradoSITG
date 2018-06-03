<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Asignar_Roles.aspx.cs" Inherits="Asignar_Roles" %>

<asp:Content ID="Asignar_Roles" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Usuarios - Asignar Roles</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPasigrol" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black" ><span class="glyphicon glyphicon-plus"></span>Asignar</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                        </ul>
                    </div>
                </div>

                <div id="Ingreso" runat="server" visible="true" class="row">
                    <asp:Table ID="Tasigrol" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lcodigo" runat="server" Text="Código del usuario:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBcodigo" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bconsultar" runat="server" OnClick="BuscarUsuario" Text="Consultar" class="btn btn-default" /></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bnueva" runat="server" OnClick="Nueva" Text="Nueva Consulta" class="btn btn-default" Visible="false" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                 <div id="Buscar" runat="server" visible="false" class="row">
                    <asp:Table ID="Tasigrol2" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                              <asp:TableCell>
                                  <asp:Label ID="LRol2" runat="server" Text="Roles:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                   <asp:DropDownList ID="DDLroles2" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList>
                                   <asp:Button ID="Bconsultar2" runat="server" OnClick="ConsultarRoles" Text="Buscar" class="btn btn-default" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
                    
                <br>
                
                 <div id="ResultadoUsuario" runat="server" visible="false" class="row">                   
                     <asp:GridView ID="GVnombre" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVnombre_RowDataBound" >
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" /> 
                        <EmptyDataTemplate>¡El usuario esta inactivo o no existe!</EmptyDataTemplate>
                        <Columns> <asp:BoundField DataField="USUARIO" HeaderText="Nombre del usuario" /> </Columns>
                     </asp:GridView>
                </div>

                <br>
                
                <div id="ResultadoRoles" runat="server" visible="false" class="row">                   
                     <asp:GridView ID="GVasigrol" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVasigrol_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVasigrol_RowDataBound" PageSize="8" OnRowDeleting="GVasigrol_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" /> 
                        <EmptyDataTemplate>¡El usuario no tiene roles asignados!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="USUROL_ID" HeaderText="Id" />
                            <asp:BoundField DataField="ROL_NOMBRE" HeaderText="Roles" />
                            <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>                    
                        </Columns>
                     </asp:GridView>
                     <br>
                </div>
               

                <div id="ResultadoUsuRol" runat="server" visible="false" class="row">                   
                     <asp:GridView ID="GVrolusuario" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVasigrol_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVrolusuario_RowDataBound" PageSize="8">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" /> 
                        <EmptyDataTemplate>¡Aún no existen usuarios asignados al rol seleccionado!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="USUARIO" HeaderText="Usuario" />   
                            <asp:BoundField DataField="USU_ESTADO" HeaderText="Estado" />                                    
                        </Columns>
                     </asp:GridView>
                    <br>
                </div>
                                

                <div id="AgregarRol" runat="server" visible="false" class="row">
                    <asp:Table ID="Tasignarol3" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                          <asp:TableCell>
                              <asp:Label ID="LRol" runat="server" Text="Roles:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                              <asp:DropDownList ID="DDLroles" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"> </asp:DropDownList>
                              <asp:Button ID="BTagregarrol" runat="server" OnClick="InsertarRol" Text="Asignar" class="btn btn-success" ForeColor="White" />
                          </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>       
                </div>
                
          
                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>    
                <asp:HiddenField ID="Metodo" runat="server" Value="" />                                    
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>



