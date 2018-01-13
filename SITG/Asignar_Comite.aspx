<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Asignar_Comite.aspx.cs" Inherits="Asignar_Comite" %>

<asp:Content ID="AsignarComite" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="panel panel-default">
        <div class="panel-heading">Universidad - Asignar Comite</div>
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
                    <asp:Table ID="Tasigcom" runat="server" HorizontalAlign="Center">
                         <asp:TableRow>
                             <asp:TableCell><asp:Label ID="Lcomite" runat="server" Text="Comite:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                             <asp:TableCell><asp:DropDownList ID="DDLcom" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lusuario" runat="server" Text="Profesores:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell>
                                <asp:CheckBoxList ID="CBLusuario" runat="server" AutoPostBack="false" BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" ForeColor="Black" TextAlign="Right"></asp:CheckBoxList>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="Buscar" runat="server" visible="false" class="row">
                    <asp:Table ID="Tablebuscar" runat="server" HorizontalAlign="Center" >
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lcomite2" runat="server" Text="Comite:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLcom2" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bbuscar" runat="server" OnClick="BuscarProfe" CssClass="btn btn-default" Text="BUSCAR" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>  

                <div id="Actualizar" runat="server" visible="false" class="row">
                    <asp:Table ID="Tasigcom2" runat="server" HorizontalAlign="Center">                     
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lusuario2" runat="server" Text="Usuarios:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell>
                                <asp:CheckBoxList ID="CBLusuario2" runat="server" AutoPostBack="false" BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" ForeColor="Black" TextAlign="Right"></asp:CheckBoxList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lcomite3" runat="server" Text="Comite:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLcom3" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="Eliminar" runat="server" visible="false" class="row">
                    <asp:Table ID="Tasigcom3" runat="server" HorizontalAlign="Center">
                         <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lusuario3" runat="server" Text="Usuarios:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell>
                                <asp:CheckBoxList ID="CBLusuario3" runat="server" AutoPostBack="false" BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" ForeColor="Black" TextAlign="Right"></asp:CheckBoxList>
                            </asp:TableCell>
                        </asp:TableRow>  
                    </asp:Table>
                </div>

                <div id="Resultado" runat="server" visible="false" class="row">                   
                     <asp:GridView ID="GVasigcom" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVasigcom_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVasigcom_RowDataBound" PageSize="6">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡No hay usuarios asignados para el comite!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="USU_USERNAME" HeaderText="ID" />   
                            <asp:BoundField DataField="USU_NOMBRE" HeaderText="NOMBRE" /> 
                            <asp:BoundField DataField="USU_APELLIDO" HeaderText="APELLIDO" />                         
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

