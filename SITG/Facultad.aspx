<%@ Page Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="Facultad.aspx.cs" Inherits="Facultad" %>

<asp:Content ID="Facultad" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="panel panel-default">
        <div class="panel-heading">Universidad - Facultad</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPfac" runat="server"> <ContentTemplate>
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
                    <asp:Table ID="TFac" runat="server" HorizontalAlign="Center" >
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lid" runat="server" Text="ID:" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                 
                            <asp:TableCell><asp:TextBox ID="TBid" runat="server" CssClass="form-control"></asp:TextBox>    </asp:TableCell>                           
                        </asp:TableRow>                   
                         <asp:TableRow>
                           <asp:TableCell><asp:Label ID="Lnombre" runat="server" Text="NOMBRE:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>                         
                           <asp:TableCell><asp:TextBox ID="TBnombre" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>                                             
                    </asp:Table>                
                </div>

                 <div id="Actualizar" runat="server" visible="false" class="row">
                    <asp:Table ID="TFac2" runat="server" HorizontalAlign="Center" >
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lid2" runat="server" Text="ID:" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                 
                            <asp:TableCell><asp:DropDownList ID="DDLid" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>                           
                        </asp:TableRow>                   
                         <asp:TableRow>
                           <asp:TableCell><asp:Label ID="Lnombre2" runat="server" Text="NOMBRE:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>                         
                           <asp:TableCell><asp:TextBox ID="TBnombre2" runat="server" CssClass="form-control"></asp:TextBox>    </asp:TableCell>
                        </asp:TableRow>                                             
                    </asp:Table>                
                </div>

                <div id="Eliminar" runat="server" visible="false" class="row">
                    <asp:Table ID="TFac3" runat="server" HorizontalAlign="Center" >                
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lid3" runat="server" Text="ID:" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                 
                            <asp:TableCell><asp:DropDownList ID="DDLid2" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>                           
                        </asp:TableRow>                        
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lestado" runat="server" Text="ESTADO:" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                         
                            <asp:TableCell ColumnSpan="3"><asp:DropDownList ID="DDLestado" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                                    <asp:ListItem Value="ACTIVO" Text="ACTIVO"></asp:ListItem> 
                                    <asp:ListItem Value="INACTIVO" Text="INACTIVO"></asp:ListItem> 
                            </asp:DropDownList></asp:TableCell>                         
                        </asp:TableRow>                                                                                             
                    </asp:Table>         
                </div>

                <div id="Resultado" runat="server" visible="false" class="row">
                     <asp:GridView ID="GVfac" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVfac_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVfac_RowDataBound" PageSize="6">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡No hay facultad!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="FAC_CODIGO" HeaderText="ID" />
                            <asp:BoundField DataField="FAC_NOMBRE" HeaderText="NOMBRE" />
                            <asp:BoundField DataField="FAC_ESTADO" HeaderText="ESTADO" />                            
                        </Columns>
                     </asp:GridView>
                </div>

                <div id="Botones" runat="server" class="row" visible="true">
                    <asp:Table ID="TBotonoes" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="Bguardar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-secondary" /></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-secondary" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
             
                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>                     
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>