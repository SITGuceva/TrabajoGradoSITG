﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Roles.aspx.cs" Inherits="Roles" %>

<asp:Content ID="Roles" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Sistema - Roles</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UProl" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>                        
                            <li><asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>                           
                        </ul>
                    </div>
                </div>

                 <div id="Ingreso" runat="server" visible="true" class="row">
                    <asp:Table ID="TRol" runat="server" HorizontalAlign="Center" >                
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lid" runat="server" Text="ID:" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                 
                            <asp:TableCell><asp:TextBox ID="TBid" runat="server" CssClass="form-control"></asp:TextBox>    </asp:TableCell>                           
                        </asp:TableRow>                        
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lnombre" runat="server" Text="NOMBRE:" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                         
                            <asp:TableCell ColumnSpan="3"><asp:TextBox ID="TBnombre" runat="server" CssClass="form-control"></asp:TextBox>    </asp:TableCell>                         
                        </asp:TableRow>                                                                                             
                    </asp:Table>   
                    <asp:Table ID="Tbotones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="Baceptar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-default" /></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-default" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="Resultado" runat="server" visible="false" class="row">
                     <asp:GridView ID="GVrol" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVrol_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVrol_RowDataBound"
                         PageSize="8"  OnRowUpdating="GVrol_RowUpdating" OnRowEditing="GVrol_RowEditing" OnRowCancelingEdit="GVrol_RowCancelingEdit">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡No hay roles!  </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="ROL_ID" HeaderText="Id" />
                            <asp:BoundField DataField="ROL_NOMBRE" HeaderText="Nombre" />                          
                            <asp:TemplateField HeaderText="Estado">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="estado" runat="server" class="btn btn-secondary btn-sm dropdown-toggle">
                                        <asp:ListItem Value="ACTIVO">ACTIVO</asp:ListItem>
                                        <asp:ListItem Value="INACTIVO">INACTIVO</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate><asp:Label ID="Lestado" runat="server" Text='<%# Bind("ROL_ESTADO") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate><asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" class="btn btn-default"/></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" class="btn btn-default"/>
                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" class="btn btn-default" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                     </asp:GridView>
                </div>
              
               <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label> 
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

