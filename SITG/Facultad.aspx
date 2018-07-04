<%@ Page Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="Facultad.aspx.cs" Inherits="Facultad" %>

<asp:Content ID="Facultad" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Universidad - Facultad</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPfac" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                       <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>                       
                            <li ><asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>                         
                       </ul>
                   
                <div class="panel panel-default">
                  <div class="panel-body">
                    
                 </br>
                 <div id="Ingreso" runat="server" visible="true" class="row">
                    <asp:Table ID="TFac" runat="server" HorizontalAlign="Center" >                     
                         <asp:TableRow>
                           <asp:TableCell><asp:Label ID="Lnombre" runat="server" Text="Nombre Facultad:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>                         
                           <asp:TableCell><asp:TextBox ID="TBnombre" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>                                             
                    </asp:Table>  
                      <asp:Table ID="TBotonoes" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="Bguardar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-success" ForeColor="White"/></asp:TableCell>
                            
                            <asp:TableCell><asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-danger" ForeColor="White" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="Resultado" runat="server" visible="false" class="row">
                     <asp:GridView ID="GVfac" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVfac_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVfac_RowDataBound" 
                         PageSize="8"  OnRowUpdating="GVfac_RowUpdating" OnRowEditing="GVfac_RowEditing" OnRowCancelingEdit="GVfac_RowCancelingEdit">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />                               
                        <EmptyDataTemplate>¡No Existen Facultades!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="FAC_CODIGO" HeaderText="Id" />
                            <asp:BoundField DataField="FAC_NOMBRE" HeaderText="Nombre" />
                            <asp:TemplateField HeaderText="Estado">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="estado" runat="server" class="btn btn-secondary btn-sm dropdown-toggle">
                                        <asp:ListItem Value="ACTIVO">ACTIVO</asp:ListItem>
                                        <asp:ListItem Value="INACTIVO">INACTIVO</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate><asp:Label ID="Lestado" runat="server" Text='<%# Bind("FAC_ESTADO") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate><asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" class="btn btn-default"/></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" class="btn btn-success"/>
                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" class="btn btn-danger" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                     </asp:GridView>
                </div>

                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>            
                       </div>
                    </div>
                     </div>
             </div>
             </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>


 
