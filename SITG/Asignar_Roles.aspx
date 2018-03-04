<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Asignar_Roles.aspx.cs" Inherits="Asignar_Roles" %>

<asp:Content ID="Asignar_Roles" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="panel panel-default">
        <div class="panel-heading">Gestionar Usuarios - Asignar Roles</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPasigrol" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Asignar Rol</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBconsultar" runat="server" OnClick="ConsultarR2" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                           
                        </ul>
                    </div>
                </div>

                <div id="Ingreso" runat="server" visible="true" class="row">
                    <asp:Table ID="Tasigrol" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lcodigo" runat="server" Text="Código del usuario:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBcodigo" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox></asp:TableCell>
                               <asp:TableCell><asp:Button ID="Bconsultar" runat="server" OnClick="Consultar" Text="Consultar" class="btn btn-secondary" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                 <div id="ConsultarR" runat="server" visible="true" class="row">
                    <asp:Table ID="Table2" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                              <asp:TableCell>
                                  <asp:Label ID="LRol2" runat="server" Text="Rol:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                   <asp:DropDownList ID="DDLroles2" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" style="width:700px; background-color:#F1F1F1;"> 
                                   
                              </asp:DropDownList>
                                   <asp:Button ID="Bconsultar2" runat="server" OnClick="ConsultarRoles" Text="Consultar" class="btn btn-secondary" style="position:relative; left:10px;" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
                    
                <br>
                
                 <div id="Resultado3" runat="server" visible="false" class="row">                   
                     <asp:GridView ID="GVnombre" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVnombre_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVnombre_RowDataBound" PageSize="10">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡El usuario esta inactivo o no existe!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="USUARIO" HeaderText="Nombre del usuario" />   
                           
                                                
                        </Columns>
                     </asp:GridView>
                       <br>
                </div>

              
                
                <div id="Resultado" runat="server" visible="false" class="row">                   
                     <asp:GridView ID="GVasigrol" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVasigrol_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVasigrol_RowDataBound" PageSize="10" OnRowDeleting="GVasigrol_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>El usuario no tiene roles asignados</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="USUROL_ID" HeaderText="Rol id" />
                            <asp:BoundField DataField="ROL_NOMBRE" HeaderText="Roles de este usuario" />
                              <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar rol" ShowHeader="true"></asp:CommandField>                    
                        </Columns>
                     </asp:GridView>
                     <br>
                </div>
               

                <div id="Resultado2" runat="server" visible="false" class="row">                   
                     <asp:GridView ID="GVrolusuario" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVasigrol_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVrolusuario_RowDataBound" PageSize="10">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>Aún no hay usuarios asignados a este rol</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="USUARIO" HeaderText="Usuario" />   
                            <asp:BoundField DataField="USU_ESTADO" HeaderText="Estado" /> 
                                                
                        </Columns>
                     </asp:GridView>
                    <br>
                </div>
                
                 

                <div id="AgregarRol" runat="server" visible="false" class="row">
                    <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                          <asp:TableCell>
                              <asp:Label ID="LRol" runat="server" Text="Rol:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                              <asp:DropDownList ID="DDLroles" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" style="width:700px; background-color:#F1F1F1;"> 
                                   
                              </asp:DropDownList>

                                <asp:Button ID="BTagregarrol" runat="server" OnClick="InsertarRol" Text="Asignar rol" class="btn btn-secondary" style="position:relative; left:10px;" />
                          </asp:TableCell>
                        
                        </asp:TableRow>
                    </asp:Table>
                          <br>
                </div>
                    
          

     
                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>    
                <asp:HiddenField ID="Metodo" runat="server" Value="" />                    
                </div>
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>



