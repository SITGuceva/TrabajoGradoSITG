﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OpcRol.aspx.cs" Inherits="OpcRol" %>

<asp:Content ID="OpcRol" ContentPlaceHolderID="MainContent" Runat="Server">
 <div class="panel panel-default">
        <div class="panel-heading">Sistema - Asignar Opciones al Rol</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPsysrol" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>                          
                            <li><asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>                     
                         </ul>
                    </div>
                </div>

                <div id="Busqueda" runat="server" visible="true" class="row">
                    <asp:Label ID="Ltitulo" runat="server" Text="Asignar Opcion al Rol" ForeColor="Black" Font-Bold="True"></asp:Label>
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
                    
                    <asp:GridView ID="GVasignaopc" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"  OnPageIndexChanging="GVasignaopc_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVasignaopc_RowDataBound" PageSize="8">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <Columns>
                            <asp:BoundField DataField="OPCS_ID" HeaderText="ID" />
                            <asp:BoundField DataField="OPCS_NOMBRE" HeaderText="NOMBRE" />                           
                            <asp:TemplateField HeaderText="ELEGIR">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CBeligio" runat="server" Text="" OnCheckedChanged="CBeligio_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <asp:Table ID="Tbotones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-default" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-default" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                </div>
                  
               
                <div id="Resultado" runat="server" visible="false" class="row">              
                     <asp:GridView  ID="GVopcrol" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVopcrol_PageIndexChanging"
                        AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVopcrol_RowDataBound" PageSize="8" OnRowDeleting="GVopcrol_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>
                            ¡No hay opciones del rol con el parámetro seleccionado!  
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="OPCROL_ID" HeaderText="ID" />
                            <asp:BoundField DataField="OPCS_ID" HeaderText="ID SISTEMA" />
                            <asp:BoundField DataField="OPCS_NOMBRE" HeaderText="OPCION DEL SISTEMA" />     
                            <asp:CommandField ShowDeleteButton="true" HeaderText="ELIMINAR" ShowHeader="true"></asp:CommandField>                    
                        </Columns>
                     </asp:GridView> 
                </div>


                <asp:HiddenField ID="Metodo" runat="server" Value="1" />   
                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

