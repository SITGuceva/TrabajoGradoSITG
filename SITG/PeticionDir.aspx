<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="PeticionDir.aspx.cs" Inherits="PeticionDir" %>

<asp:Content ID="PeticionDir" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Reunion - Peticiones de Director</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPpeticion_dir" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        
                        <div id="Regresar" runat="server" visible="false" class="row">
                            <asp:Table ID="TBregresar" runat="server" HorizontalAlign="center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Button ID="BTregresar" OnClick="regresar" runat="server" Text="Regresar a Peticiones" CssClass="btn btn-default"></asp:Button> </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <br>

                        <div id="TPeticiones" runat="server" visible="false" class="row">
                            <asp:GridView ID="GVpeticion" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"  OnPageIndexChanging="GVpeticion_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                                OnRowDataBound="GVpeticion_RowDataBound" PageSize="8" OnRowCommand="GVpeticion_RowCommand">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>¡No hay peticiones de director pendientes!</EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="SOL_ID" HeaderText="Código de la petición" />
                                    <asp:BoundField DataField="SOL_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código de propuesta" />
                                    <asp:BoundField DataField="DIRECTOR" HeaderText="Director solicitante" />
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <asp:Button ID="BtAprobar" runat="server"  Text="Aprobar" CommandName="Aprobar" CssClass="btn btn-default" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:Button ID="BtRechazar" runat="server" Text="Rechazar" CommandName="Rechazar" CssClass="btn btn-default"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:HiddenField ID="Director" Value='<%# Bind("USU_USERNAME") %>'  runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Informacion del profesor">
                                        <ItemTemplate>
                                            <asp:Button ID="Ver" runat="server" Text="Ver" CommandName="Ver" CssClass="btn btn-default" CommandArgument='<%# Bind("USU_USERNAME") %>' /> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>


                        <div id="Tinfprof" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                             <asp:GridView ID="GVinfprof" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"  AutoGenerateColumns="False"  CssClass="table table-bordered bs-table" OnRowDataBound="GVinfprof_RowDataBound" >
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate> ¡No hay informacion del profesor! </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="USU_TELEFONO" HeaderText="Telefono" />
                                    <asp:BoundField DataField="USU_DIRECCION" HeaderText="Direccion" />
                                    <asp:BoundField DataField="USU_CORREO" HeaderText="Correo" />
                                </Columns>
                            </asp:GridView>    
                        </div>

                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
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




