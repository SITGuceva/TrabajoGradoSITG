<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AnteproyectoPendiente.aspx.cs" Inherits="AnteproyectoPendiente" %>

<asp:Content ID="AnteproPendiente" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Anteproyectos Pendientes</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPantepropendi" runat="server"> <ContentTemplate>
            <div class="container-fluid">
            

            <div id="Consulta" runat="server" visible="true" class="row" style="overflow-x: auto">
                <asp:GridView ID="GVconsultaAP" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"  OnPageIndexChanging="GVconsultaAP_PageIndexChanging" AutoGenerateColumns="False"
                    CssClass="table table-bordered bs-table" OnRowDataBound="GVconsultaAP_RowDataBound" PageSize="6" OnRowCommand="GVconsultaAP_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    <EmptyDataTemplate>¡No tiene anteproyectos pendientes para asignar jurado! </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="APRO_CODIGO" HeaderText="CODIGO" />
                        <asp:BoundField DataField="ANP_NOMBRE" HeaderText="TÍTULO" />
                        <asp:BoundField DataField="ANP_FECHA" HeaderText="FECHA DE ENTREGA" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="EVALUADOR">
                            <ItemTemplate> <asp:Button ID="BTrevisar" runat="server" Text="Asignar" class="btn btn-default" AutoPostBack="true" CommandName="Asignar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div id="MostrarDDLReunion" runat="server" visible="false" class="row">
                <asp:DropDownList ID="DDLconsultaReunion" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList>
            </div>

            <div id="InfoAnteproyecto" runat="server" visible="false" class="row" style="overflow-x: auto">
                <asp:GridView ID="GVanteproy" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"  OnPageIndexChanging="GVanteproy_PageIndexChanging" AutoGenerateColumns="False"  CssClass="table table-bordered bs-table" OnRowDataBound="GVanteproy_RowDataBound" PageSize="6">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    <EmptyDataTemplate>¡No tiene anteproyectos pendientes para asignar jurado!   </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="APRO_CODIGO" HeaderText="CODIGO ANTEPROYECTO" />
                        <asp:BoundField DataField="ANP_NOMBRE" HeaderText="TÍTULO" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DOCUMENTO">
                            <ItemTemplate><asp:LinkButton ID="LinkDescarga" runat="server" Text="Descargar" OnClick="DescargaAnteProyecto" CommandArgument='<%# Eval("APRO_CODIGO") %>'></asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div id="Mostrarprof" runat="server" visible="false" style="text-align: center;">
                <asp:Table ID="Tprofesor" runat="server" HorizontalAlign="center">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:DropDownList ID="DDLprofesores" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"> </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell><asp:Button ID="BTconsultar" Enabled="true" OnClick="consultarprofesor" runat="server" Text="Consultar" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br>
            </div>


            <div id="infoprofesor" runat="server" visible="false" class="row" style="overflow-x: auto">
                <asp:GridView ID="GVinfprof" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVinfprof_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="gray" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="white" />
                    <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    <EmptyDataTemplate>¡No hay informacion del profesor! </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="USU_USERNAME" HeaderText="Código" />
                        <asp:BoundField DataField="USU_TELEFONO" HeaderText="Telefono" />
                        <asp:BoundField DataField="USU_DIRECCION" HeaderText="Direccion" />
                        <asp:BoundField DataField="USU_CORREO" HeaderText="Correo" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="HOJA DE VIDA">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DescargaHV" CommandArgument='<%# Eval("USU_USERNAME") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>


            <div id="Terminar" runat="server" visible="false" style="text-align: center;" class="row">
                <br>
                <asp:Button ID="BTterminar" OnClick="terminar" runat="server" Text="Asignar" class="btn btn-default" />
                <asp:Button ID="BTcancelar" OnClick="cancelar" runat="server" Text="Cancelar" class="btn btn-default" />
            </div>


            
            <asp:ImageButton id="IBregresar" OnClick="regresar" runat="server" Visible="false" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline"></asp:ImageButton>
           <br />
            <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
            <asp:HiddenField ID="Metodo" runat="server" Value="" />
            <asp:HiddenField ID="Verificador" runat="server" Value="" />
            </div>
      </ContentTemplate></asp:UpdatePanel>
      </div>
    </div>
</asp:Content>

