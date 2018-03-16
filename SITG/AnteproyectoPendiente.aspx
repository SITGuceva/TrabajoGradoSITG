<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AnteproyectoPendiente.aspx.cs" Inherits="AnteproyectoPendiente" %>

<asp:Content ID="ProcesoAnteproyecto" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Proceso anteproyecto</div>
        <div class="panel-body">

            <div class="container-fluid">
            </div>

            <div id="Consulta" runat="server" visible="true" class="row">
                <asp:GridView ID="GVconsultaPP" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                    OnPageIndexChanging="GVconsultaPP_PageIndexChanging" AutoGenerateColumns="False"
                    CssClass="table table-bordered bs-table" OnRowDataBound="GVconsultaPP_RowDataBound" PageSize="6" OnRowCommand="GVconsultaPP_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    <EmptyDataTemplate>
                        ¡No tiene anteproyectos pendientes para asignar jurado!  
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="APRO_CODIGO" HeaderText="CODIGO ANTEPROYECTO" />
                        <asp:BoundField DataField="ANP_NOMBRE" HeaderText="TÍTULO" />
                        <asp:BoundField DataField="ANP_FECHA" HeaderText="FECHA DE ENTREGA" />

                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="REVISAR ANTEPROYECTO">
                            <ItemTemplate>
                                <asp:Button ID="BTrevisar" runat="server" Text="Asignar jurado" class="btn btn-default" AutoPostBack="true" CommandName="ConsultarPropuesta" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div id="consulta2" runat="server" visible="true" class="row">
                <asp:GridView ID="GVconsultaPP2" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                    OnPageIndexChanging="GVconsultaPP2_PageIndexChanging" AutoGenerateColumns="False"
                    CssClass="table table-bordered bs-table" OnRowDataBound="GVconsultaPP2_RowDataBound" PageSize="6">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    <EmptyDataTemplate>
                        ¡No tiene anteproyectos pendientes para asignar jurado!  
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="APRO_CODIGO" HeaderText="CODIGO ANTEPROYECTO" />
                        <asp:BoundField DataField="ANP_NOMBRE" HeaderText="TÍTULO" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DESCARGAR">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkDescarga" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("APRO_CODIGO") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div id="formularioP" runat="server" visible="true" class="row">
                <asp:Label ID="TCodigoP" runat="server" Text="" ForeColor="Gray" Font-Bold="True"></asp:Label>
                <asp:Label ID="CodigoP" runat="server" Text="" ForeColor="Black" Font-Bold="True"></asp:Label>
                <br>
                <asp:Label ID="TTituloP" runat="server" Text="" ForeColor="Gray" Font-Bold="True"></asp:Label>
                <asp:Label ID="TituloP" runat="server" Text="" ForeColor="Black" Font-Bold="True"></asp:Label>
                <br>
            </div>

            <div id="MostrarDDLestadoP" runat="server" visible="true" style="text-align: center;">

                <asp:Table ID="Tprofesor" runat="server" HorizontalAlign="center">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:DropDownList ID="DDLprofesores" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="BTconsultar" Enabled="true" OnClick="consultarprofesor" runat="server" Text="Consultar Profesor" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <br>
            </div>


            <div id="infoprofesor" runat="server" visible="true" class="row">
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
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile2" CommandArgument='<%# Eval("USU_USERNAME") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:HiddenField ID="Metodo" runat="server" Value="" />
            </div>


            <div id="Terminar" runat="server" visible="true" style="text-align: center;" class="row">
                <br>
                <asp:Button ID="BTterminar" OnClick="terminar" runat="server" Style="background-color: lightgray" Text="Asignar jurado" class="btn btn-default" />
                <asp:Button ID="BTcancelar" OnClick="cancelar" runat="server" Style="background-color: lightgray" Text="Cancelar revisión" class="btn btn-default" />
            </div>


            <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
            <asp:Button ID="BTregresar" OnClick="regresar" runat="server" Text="Regresar" class="btn btn-default" />
        </div>

    </div>
</asp:Content>

