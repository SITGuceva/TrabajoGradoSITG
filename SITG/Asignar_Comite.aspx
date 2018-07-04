<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Asignar_Comite.aspx.cs" Inherits="Asignar_Comite" %>

<asp:Content ID="AsignarComite" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Universidad - Comité</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPasignarcomite" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="breadcrumb">
                                    <li>
                                        <asp:LinkButton ID="LBasignar" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Asignar Comité</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LBconsultar" runat="server" OnClick="Buscar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar Comité</asp:LinkButton></li>
                                </ul>

                                <div class="panel panel-default">
                                    <div class="panel-body">

                                        <div id="Ingreso" runat="server" visible="true" class="row">
                                            <asp:Table ID="Tasignarcom" runat="server" HorizontalAlign="center">
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:Label ID="LCodigo" runat="server" Text="Código del profesor:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:TextBox ID="TBcodigo" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Button ID="Btbuscar" OnClick="Buscar_usuario" runat="server" Text="Buscar" CssClass="btn btn-default"></asp:Button></asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </div>

                                        <br>
                                        <div id="Consultar" runat="server" visible="false" class="row">
                                            <asp:Table ID="Tasignarcom2" runat="server" HorizontalAlign="center">
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:DropDownList ID="DDLcom2" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Button ID="BTbuscarcomite" OnClick="BuscarComite" runat="server" Text="Consultar" CssClass="btn btn-default"></asp:Button></asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                            <br>
                                        </div>

                                        <div id="ResultadoUsuario" runat="server" style="overflow-x: auto" visible="false" class="row">
                                            <asp:GridView ID="GVusuario" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVusuario_PageIndexChanging" AutoGenerateColumns="False"
                                                CssClass="table table-bordered bs-table" OnRowDataBound="GVusuario_RowDataBound" PageSize="8">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="white" />
                                                <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                <EmptyDataTemplate>¡El profesor esta inactivo o no existe! </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div id="ResultadoComite" runat="server" style="overflow-x: auto" visible="false" class="row">
                                            <asp:GridView ID="GVcomite" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                                AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVcomite_RowDataBound" PageSize="8">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="white" />
                                                <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                <EmptyDataTemplate>¡El profesor aún no tiene asignado un comité!</EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="COM_NOMBRE" HeaderText="Comite" HeaderStyle-HorizontalAlign="Center" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div id="Miembros" runat="server" style="overflow-x: auto" visible="false" class="row">
                                            <asp:GridView ID="GVmiembros" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVmiembros_PageIndexChanging"
                                                AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVmiembros_RowDataBound" PageSize="8" OnRowDeleting="GVmiembros_RowDeleting">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="white" />
                                                <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                <EmptyDataTemplate>¡El comité no tiene miembros actualmente!</EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="USU_USERNAME" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="MIEMBROS" HeaderText="Miembros" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>

                                        <div id="Roles" runat="server" visible="false" class="row">
                                            <asp:Table ID="Tasignarcom3" runat="server" HorizontalAlign="center">
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:DropDownList ID="DDLcom" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Button ID="BtAgregar" OnClick="AgregarComite" runat="server" Text="Asignar" CssClass="btn btn-success" ForeColor="White" OnClientClick="return confirm('¿Está seguro de asignar el comité al profesor?');"></asp:Button></asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
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
     <script type="text/javascript">
        function Confirmacion() {
             var seleccion = confirm("acepta el mensaje ?");
             if (seleccion)
                 alert("se acepto el mensaje");
             else
                 alert("NO se acepto el mensaje");
             return seleccion;
         }
    </script>
</asp:Content>

