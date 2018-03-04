<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Asignar_Comite.aspx.cs" Inherits="Asignar_Comite" %>

<asp:Content ID="AsignarComite" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Sistema - Observaciones</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPasignarcomite" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBasignar" runat="server" OnClick="LIAsignar" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Asignar Comite</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBconsultar" Text="jkd" runat="server" OnClick="LIConsultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar Comite</asp:LinkButton></li>
                           
                        </ul>
                            </div>
                        </div>

                        <div id="Ingreso" runat="server" visible="false" class="row">
                      

                            <asp:Table ID="TBingresar" runat="server" HorizontalAlign="center">
                                <asp:TableRow>

                                    <asp:TableCell>
                                        <asp:Label ID="LCodigo" runat="server" Text="Codigo del usuario:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                    </asp:TableCell>

                                </asp:TableRow>

                                <asp:TableRow>

                                    <asp:TableCell>
                                        <asp:TextBox ID="TBcodigo" runat="server" TextMode="Number"  CssClass="form-control"></asp:TextBox>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Button ID="Btbuscar" OnClick="Buscar_usuario" runat="server" Text="Buscar usuario" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button>
                                        
                                    
                                    </asp:TableCell>

                                </asp:TableRow>
                            </asp:Table>

                        </div>

                    <br>
                          <div id="Consultar" runat="server" visible="false" class="row">
                        
                            <asp:Table ID="TConsultar" runat="server" HorizontalAlign="center">
                              

                                <asp:TableRow>

                                    <asp:TableCell>
                                     
                                     <asp:DropDownList ID="DDLcom2" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" style="background-color:lightgray"></asp:DropDownList>
                                 
                                          </asp:TableCell>
                                    <asp:TableCell>
                                    <asp:Button ID="BTbuscarcomite" OnClick="BuscarComite" runat="server" Text="Buscar Comite" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button>

                                  </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                             <br>
                        </div>


                        <div id="Resultado2" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="gvUsuario" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvUsuario_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvUsuario_RowDataBound" PageSize="2">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>
                                  Este usuario se encuentra inactivo o no existe en el programa
                                </EmptyDataTemplate>
                                <Columns>

                                    <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                                  
                                </Columns>
                            </asp:GridView>
                        </div>


                         <div id="Roles" runat="server" visible="false" class="row">
                      

                            <asp:Table ID="Table1" runat="server" HorizontalAlign="center">
                              

                                <asp:TableRow>

                                    <asp:TableCell>
                                     
                                     <asp:DropDownList ID="DDLcom" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" style="background-color:lightgray"></asp:DropDownList>
                                 
                                          </asp:TableCell>
                                    <asp:TableCell>
                                    <asp:Button ID="BtAgregar" OnClick="AgregarComite" runat="server" Text="Agregar a este Comite" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button>

                                  </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                             <br>
                        </div>





                        <div id="Resultado" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="gvComites" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvComites_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvComites_RowDataBound" PageSize="10">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>
                                    
                                    Este profesor aún no tiene comite

                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="COM_NOMBRE" HeaderText="Comites al que pertenece" HeaderStyle-HorizontalAlign="Center" />
                                </Columns>

                            </asp:GridView>

                            <br>
                          
                        </div>
                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
                    </div>









                    <div id="Miembros" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="gvMiembros" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvMiembros_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvMiembros_RowDataBound" PageSize="10" OnRowDeleting="gvMiembros_RowDeleting">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>
                                    Este comite no tiene miembros actualmente
                                </EmptyDataTemplate>
                                <Columns>
                                     <asp:BoundField DataField="USU_USERNAME" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="MIEMBROS" HeaderText="Miembros" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>

                                    
                                </Columns>


                            </asp:GridView>
                        </div>
                        <asp:Label ID="Label1" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
                    </div>




                    <script>
                        function pulsar(e) {
                            tecla = (document.all) ? e.keyCode : e.which;
                            if (tecla == 13) return false;
                        }
                    </script>


                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

