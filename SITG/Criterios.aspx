<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Criterios.aspx.cs" Inherits="Criterios" %>

<asp:Content ID="CriteriosCRUD" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Universidad - Criterios</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPcriterios" runat="server"> <ContentTemplate>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">

                            <ul class="breadcrumb">
                                <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>
                                <li><asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                            </ul>

                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <div id="Ingreso" runat="server" visible="true" class="row">
                                        <asp:Table ID="Tcriterios" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Ltipo" runat="server" Text="Tipo:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="TBtipo" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lnom" runat="server" Text="Nombre:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="TBnom" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                        <asp:Table ID="botones" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-success" ForeColor="White" /></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Cancelar" class="btn btn-danger" ForeColor="White" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>

                                    <div id="ConsultaCrit" runat="server" visible="false" class="row" style="overflow-x: auto">
                                        <asp:GridView ID="GVcriterios" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                            OnPageIndexChanging="GVcriterios_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                                            OnRowDataBound="GVcriterios_RowDataBound" PageSize="6" OnRowUpdating="GVcriterios_RowUpdating" OnRowEditing="GVcriterios_RowEditing"
                                            OnRowCancelingEdit="GVcriterios_RowCancelingEdit">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No existen criterios!</EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="CRIT_CODIGO" HeaderText="Id" />
                                                <asp:BoundField DataField="CRIT_NOMBRE" HeaderText="Nombre" />
                                                <asp:BoundField DataField="CRIT_TIPO" HeaderText="Tipo" />
                                                <asp:TemplateField HeaderText="Estado">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="estado" runat="server">
                                                            <asp:ListItem Value="ACTIVO">ACTIVO</asp:ListItem>
                                                            <asp:ListItem Value="INACTIVO">INACTIVO</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lestado" runat="server" Text='<%# Bind("CRIT_ESTADO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modificar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" class="btn btn-default" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" class="btn btn-success" ForeColor="White" />
                                                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" class="btn btn-danger" ForeColor="White" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                        </div>
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

