<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="panel panel-default">
        <div class="panel-heading">Roles del Sistema</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPsysrol" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBnuevo" runat="server" OnClick="Nuevo" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Nuevo</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBeditar" runat="server" OnClick="Editar" ForeColor="Black"><span class="glyphicon glyphicon-pencil"></span>Editar</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBactualizar" runat="server" ForeColor="Black"><span class="glyphicon glyphicon-refresh"></span>Actualizar</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBacpetar" runat="server"  Enabled="false" OnClick="Nuevo" ForeColor="DarkGray"><span class="glyphicon glyphicon-ok"></span>Guardar</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBcancelar" runat="server" Enabled="false" OnClick="Cancelar" ForeColor="DarkGray"><span class="glyphicon glyphicon-remove"></span>Cancelar</asp:LinkButton></li>
                        </ul>
                    </div>
                </div>
               
             <div id="Consulta" runat="server" visible="false" class="row">
                  <asp:Table ID="TableConsulta" runat="server" HorizontalAlign="Center" Width="656px" >
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:DropDownList ID="DDLtipoconsulta" style="min-width:100%;" CssClass="form-control" runat="server" >
                                <asp:ListItem>Código</asp:ListItem>                          
                             </asp:DropDownList>
                        </asp:TableCell>
                       <asp:TableCell ColumnSpan="2">
                                   <asp:TextBox ID="TBdescripconsulta"  style="min-width:100%;" CssClass="form-control" runat="server"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="Bbuscar" runat="server" Text="Buscar" CssClass="btn btn-default" OnClick="Buscar"></asp:Button>
                        </asp:TableCell>
                    </asp:TableRow>                    
                  </asp:Table>  
                 <hr/>   
             </div>
             
             <div id="Ingreso" runat="server" visible="false" class="row">
                    <asp:Table ID="TableOpcRol" runat="server" HorizontalAlign="Center" >
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Lcodigo" runat="server" Text="Código:" class="text-justify"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="TBcodigo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Ldescripcion" runat="server" Text="Descripción:" class="text-justify"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="3">
                                <asp:TextBox ID="TBdescripcion" runat="server" CssClass="form-control" style="min-width:100%;"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Lcreado" runat="server" Text="Creado:" class="text-justify"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="TBcreado" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Lmodificado" runat="server" Text="Modificado:" class="text-justify"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                  <asp:TextBox ID="TBmodificado" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="Resultado" runat="server" visible="false" class="row">
                     <asp:GridView ID="gvSysRol" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                    OnPageIndexChanging="gvSysRol_PageIndexChanging"
                    AutoGenerateColumns="False"
                    CssClass="table table-bordered bs-table"
                    OnRowDataBound="gvSysRol_RowDataBound" PageSize="3">
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
                        ¡No hay Roles con el parámetro seleccionado!  
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="CODIGO" HeaderText="CODIGO" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" />
                        <asp:BoundField DataField="CREATION_DATE" HeaderText="CREADO" />
                        <asp:BoundField DataField="LAST_TIMESTAP" HeaderText="MODIFICADO" />            
                    </Columns>
                </asp:GridView>
                </div>
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

