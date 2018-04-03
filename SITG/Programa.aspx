<%@ Page Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="Programa.aspx.cs" Inherits="Programa" %>

<asp:Content ID="Programa" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="panel panel-default">
        <div class="panel-heading">Universidad - Programa</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPprog" runat="server"> <ContentTemplate>
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
                    <asp:Table ID="TProg" runat="server" HorizontalAlign="Center" >                  
                         <asp:TableRow>
                           <asp:TableCell><asp:Label ID="Lnombre" runat="server" Text="NOMBRE:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>                         
                            <asp:TableCell><asp:TextBox ID="TBnombre" runat="server" CssClass="form-control"></asp:TextBox>    </asp:TableCell>
                        </asp:TableRow>                      
                        <asp:TableRow>
                             <asp:TableCell><asp:Label ID="Lfacultad" runat="server" Text="FACULTAD:" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                         
                            <asp:TableCell ColumnSpan="3"><asp:DropDownList ID="DDLfacultad" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>                         
                        </asp:TableRow>
                          <asp:TableRow>
                             <asp:TableCell><asp:Label ID="Lsemestre" runat="server" Text="SEMESTRE:" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                         
                            <asp:TableCell> <asp:TextBox ID="TBsemestre" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>    </asp:TableCell>                         
                        </asp:TableRow>
                    </asp:Table>                  
                    
                     <asp:Table ID="Tbotones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-secondary" /></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-secondary" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div id="BuscarF" runat="server" visible="false" class="row">
                    <asp:Table ID="TProg1" runat="server" HorizontalAlign="Center" >
                        <asp:TableRow>
                           <asp:TableCell><asp:Label ID="Lfacultad1" runat="server" Text="Facultad:" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                 
                           <asp:TableCell><asp:DropDownList ID="DDLfacultad1" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>                           
                                  <asp:TableCell><asp:Button ID="Bbuscar" runat="server" OnClick="Buscar" Text="Buscar" class="btn btn-secondary" /></asp:TableCell>
                        </asp:TableRow>                                    
                    </asp:Table>                  
                </div>
      
                <div id="Resultado" runat="server" visible="false" class="row" style="overflow-x: auto">
                     <asp:GridView ID="GVprog" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVprog_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVprog_RowDataBound" 
                         PageSize="8"  OnRowUpdating="GVprog_RowUpdating" OnRowEditing="GVprog_RowEditing" OnRowCancelingEdit="GVprog_RowCancelingEdit">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="White" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡No hay programas!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="PROG_CODIGO" HeaderText="ID" />
                            <asp:BoundField DataField="PROG_NOMBRE" HeaderText="NOMBRE" />                           
                            <asp:BoundField DataField="PROG_SEMESTRE" HeaderText="SEMESTRE" />         
                            <asp:TemplateField HeaderText="Estado">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="estado" runat="server" class="btn btn-secondary btn-sm dropdown-toggle">
                                        <asp:ListItem Value="ACTIVO">ACTIVO</asp:ListItem>
                                        <asp:ListItem Value="INACTIVO">INACTIVO</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate><asp:Label ID="Lestado" runat="server" Text='<%# Bind("PROG_ESTADO") %>'></asp:Label></ItemTemplate>
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

                <asp:HiddenField ID="Verificador" runat="server" />
                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>



