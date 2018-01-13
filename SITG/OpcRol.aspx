<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OpcRol.aspx.cs" Inherits="OpcRol" %>

<asp:Content ID="OpcRol" ContentPlaceHolderID="MainContent" Runat="Server">
 <div class="panel panel-default">
        <div class="panel-heading">Sistema - Opciones del Rol</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPsysrol" runat="server"> <ContentTemplate>
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

                <div id="DIVBuscar" runat="server" visible="true" class="row">
                    <asp:Table ID="Tablebuscar" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Lrolbuscar" runat="server" Text="Roles:" ForeColor="Black" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="3">
                                <asp:DropDownList ID="DDLrolbuscar" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="Bbuscar" runat="server" Text="Buscar" CssClass="btn btn-default" OnClick="Buscar"></asp:Button>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                 <div id="Ingreso" runat="server" visible="false" class="row">
                    <asp:Table ID="TableOpcRol" runat="server" HorizontalAlign="Center" >                
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Lid" runat="server" Text="ID:" ForeColor="Black" Font-Bold="True" ></asp:Label>
                            </asp:TableCell>                 
                            <asp:TableCell>
                                <asp:TextBox ID="TBid" runat="server" CssClass="form-control"></asp:TextBox>    
                            </asp:TableCell>                           
                        </asp:TableRow>                                                                   
                         <asp:TableRow>
                           <asp:TableCell>
                               <asp:Label ID="Lopcs" runat="server" Text="Opciones del Sistema:" ForeColor="Black" Font-Bold="True"></asp:Label>
                            </asp:TableCell>                         
                            <asp:TableCell>
                                <asp:DropDownList ID="DDLopcs" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>                        
                    </asp:Table>                  
                </div>
                  
                <div id="Actualizar" runat="server" visible="false" class="row">
                    <asp:Table ID="TableOpcRol2" runat="server" HorizontalAlign="Center" >                
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Lid2" runat="server" Text="ID:" ForeColor="Black" Font-Bold="True" ></asp:Label>
                            </asp:TableCell>                 
                            <asp:TableCell>
                                <asp:Label ID="Lansid" runat="server" Text="" ForeColor="Black" Font-Bold="True" ></asp:Label> 
                            </asp:TableCell>                         
                        </asp:TableRow>                                                               
                         <asp:TableRow>
                           <asp:TableCell>
                               <asp:Label ID="Lopcs2" runat="server" Text="Opciones del Sistema:" ForeColor="Black" Font-Bold="True"></asp:Label>
                            </asp:TableCell> 
                             <asp:TableCell>
                                <asp:DropDownList ID="DDLansopcs" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList>
                            </asp:TableCell>                           
                            <asp:TableCell>
                                <asp:DropDownList ID="DDLopactu" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>                        
                    </asp:Table>                  
                </div>

                
                <div id="Eliminar" runat="server" visible="false" class="row">
                    <asp:Table ID="TableUsuSys5" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Lopcrol" runat="server" Text="Opciones:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="DDLopcrol" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Lestado" runat="server" Text="Estado:" ForeColor="Black" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="3">
                                <asp:DropDownList ID="DDLestado" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                                    <asp:ListItem Value="ACTIVO" Text="ACTIVO"></asp:ListItem>
                                    <asp:ListItem Value="INACTIVO" Text="INACTIVO"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="Resultado" runat="server" visible="false" class="row">              
                     <asp:GridView  ID="gvSysRol" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnPageIndexChanging="gvSysRol_PageIndexChanging"
                        AutoGenerateColumns="False"
                        CssClass="table table-bordered bs-table"
                        OnRowDataBound="gvSysRol_RowDataBound" PageSize="6">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>
                            ¡No hay opciones del rol con el parámetro seleccionado!  
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="OPCROL_ID" HeaderText="ID" />
                            <asp:BoundField DataField="OPCS_NOMBRE" HeaderText="OPCION DEL SISTEMA" />     
                           <asp:BoundField DataField="OPCROL_ESTADO" HeaderText="ESTADO" />    
                        </Columns>
                     </asp:GridView> 
                </div>

                <div id="Botones" runat="server" class="row" visible="false">
                    <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button ID="LBacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-secondary" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="LBcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-secondary" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
                <asp:HiddenField ID="Metodo" runat="server" Value="1" />   
                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

