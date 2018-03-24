<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Agregar_LineaProf.aspx.cs" Inherits="Agregar_LineaProf" %>

<asp:Content ID="AsigLProf" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Universidad - Agregar Linea Profundización</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPasiglprof" runat="server"> <ContentTemplate>
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
                    <asp:Table ID="Tasiglprof" runat="server" HorizontalAlign="Center">                     
                         <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lprog" runat="server" Text="Programa:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLprog" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                        </asp:TableRow>
                         <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lnomlinea" runat="server" Text="Linea Investigación:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBnomlinea" runat="server"  CssClass="form-control"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>                      
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lnomtema" runat="server" Text="Temas:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBnomtema" runat="server"  CssClass="form-control"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bagregar" runat="server" OnClick="AgregarFila" CssClass="btn btn-default" Text="AGREGAR" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:GridView  ID="GVagretema" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"  PageSize="6">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <Columns>
                            <asp:BoundField DataField="TEMAS" HeaderText="TEMAS" />                                   
                        </Columns>
                     </asp:GridView>
                    <asp:Table ID="botones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-default" /></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-default" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

              <div id="DIVBuscar" runat="server" visible="false" class="row">
                    <asp:Table ID="Tablebuscar" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lprog2" runat="server" Text="Programa:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLprog2" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"  AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="Bbuscar" runat="server" OnClick="Buscar" CssClass="btn btn-default" Visible="true" Text="BUSCAR" />
                                <asp:Button ID="Bnueva" runat="server" OnClick="Nueva" CssClass="btn btn-default" Visible="false" Text="NUEVA CONSULTA" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>                   
                </div>

                <div id="SolLinProf" runat="server" visible="false" class="row">                          
                     <asp:GridView ID="GVlineaprof" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                         OnPageIndexChanging="GVlineaprof_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" 
                         OnRowDataBound="GVlineaprof_RowDataBound" PageSize="6" OnRowUpdating="GVlineaprof_RowUpdating" OnRowEditing="GVlineaprof_RowEditing"
                         OnRowCancelingEdit="GVlineaprof_RowCancelingEdit" OnRowCommand="GVlineaprof_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡No hay lineas de investigación agregadas para el programa!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="LPROF_CODIGO" HeaderText="ID" />   
                            <asp:BoundField DataField="LPROF_NOMBRE" HeaderText="INVESTIGACIÓN" />
                            <asp:TemplateField HeaderText="Estado">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="estado" runat="server">
                                        <asp:ListItem Value="ACTIVO">ACTIVO</asp:ListItem>
                                        <asp:ListItem Value="INACTIVO">INACTIVO</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Lestado" runat="server" Text='<%# Bind("LPROF_ESTADO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate>
                                    <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" />
                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Temas">
                                <ItemTemplate>
                                    <asp:Button ID="Bbuscartema" runat="server" Text="Ver" CommandName="buscar"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                     </asp:GridView>
                </div>

                <div id="SolTema" runat="server" visible="false" class="row">      
                    
                    
                    
                    <asp:Table ID="TAgregartema" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lprof" runat="server" Text="Linea Investigación:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label ID="Lreslp" runat="server"  ForeColor="Black" CssClass="form-control"></asp:Label></asp:TableCell>               
                        </asp:TableRow>
                        <asp:TableRow>
                             <asp:TableCell><asp:Label ID="Lagregt" runat="server" Text="Agregar Tema:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBagregt" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:Button ID="BagregarT" runat="server" OnClick="AgregarT" CssClass="btn btn-default" Text="AGREGAR" /></asp:TableCell>
                            <asp:TableCell><asp:Button ID="BregresarLP" runat="server" OnClick="RegresarLP" CssClass="btn btn-default" Text="REGRESAR" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>    

                    <asp:GridView ID="GVtema" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" 
                        OnPageIndexChanging="GVtema_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" 
                        OnRowDataBound="GVtema_RowDataBound" PageSize="6"  OnRowUpdating="GVtema_RowUpdating" OnRowEditing="GVtema_RowEditing"
                         OnRowCancelingEdit="GVtema_RowCancelingEdit">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡No hay temas agregados para la linea investigación!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="TEM_CODIGO" HeaderText="ID" />   
                            <asp:BoundField DataField="TEM_NOMBRE" HeaderText="TEMA" /> 
                            <asp:TemplateField HeaderText="Estado">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="estadotem" runat="server">
                                        <asp:ListItem Value="ACTIVO">ACTIVO</asp:ListItem>
                                        <asp:ListItem Value="INACTIVO">INACTIVO</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Lestado" runat="server" Text='<%# Bind("TEM_ESTADO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate>
                                    <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" />
                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                     </asp:GridView>
                    <asp:HiddenField ID="Metodo" runat="server" Value="" />                    
                </div>

                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>                    
                </div>
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>

    
    <script>
        function pulsar(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 13) return false;
        }
    </script>

</asp:Content>

