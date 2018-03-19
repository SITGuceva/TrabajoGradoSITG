<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProyFinalAsignado.aspx.cs" Inherits="ProyFinalAsignado" %>

<asp:Content ID="ProyFinalAsignado" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Proyecto Final Asignado</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPpfasignado" runat="server"> <ContentTemplate>
            <div class="container-fluid">
           
            <div id="Consulta" runat="server" visible="true" class="row" style="overflow-x: auto">
                <asp:GridView ID="GVpfasignado" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"  OnPageIndexChanging="GVpfasignado_PageIndexChanging" AutoGenerateColumns="False"  CssClass="table table-bordered bs-table" OnRowDataBound="GVpfasignado_RowDataBound" PageSize="6" OnRowCommand="GVpfasignado_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    <EmptyDataTemplate>¡No hay proyectos finales  asignados!   </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="PPRO_CODIGO" HeaderText="CODIGO" />
                        <asp:BoundField DataField="PF_TITULO" HeaderText="TITULO" />
                        <asp:BoundField DataField="PF_FECHA" HeaderText="FECHA" />
                        <asp:BoundField DataField="PF_ESTADO" HeaderText="ESTADO" />
                        <asp:BoundField DataField="PF_APROBACION" HeaderText="DIRECTOR" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DOCUMENTO">
                             <ItemTemplate><asp:LinkButton ID="LBdescarga" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PPRO_CODIGO") %>'></asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="PROYECTO">
                            <ItemTemplate> <asp:Button ID="BTrevisar" runat="server" Text="REVISAR" class="btn btn-default" AutoPostBack="true" CommandName="ProyectoF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /> </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- -->
            <div id="criterios" runat="server" visible="false" class="row"  >
                <asp:Label ID="Ltitulo" runat="server" Text="CRITERIOS DE EVALUACION" Font-Bold="True" ForeColor="Black"></asp:Label>
                <asp:GridView ID="GVcriterios" runat="server" Visible="true" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVcriterios_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVcriterios_RowDataBound" PageSize="40">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    <EmptyDataTemplate>¡No hay criterios disponibles!</EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="CRIT_CODIGO" HeaderText="ID" />
                        <asp:BoundField DataField="CRIT_TIPO" HeaderText="TIPO" ItemStyle-Font-Bold="true"/>
                        <asp:BoundField DataField="CRIT_NOMBRE" HeaderText="NOMBRE" />
                        <asp:TemplateField HeaderText="CUMPLE">
                            <ItemTemplate><asp:CheckBox ID="CBcumplio" runat="server" Text="" AutoPostBack="true" /> </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


                <asp:Table ID="Tevaluarcrit2" runat="server" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell><asp:Label ID="Lrecomendacion" runat="server" Text="OBSERVACIONES" ForeColor="Black" Font-Bold="True" class="text-justify" ToolTip="Realice los comentarios que considere prudentes y que ayuden a evaluar el trabajo."></asp:Label></asp:TableCell>
                        <asp:TableCell><textarea id="TBbs" runat="server" CssClass="form-control" rows="5" Value="" ></textarea></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label ID="Lcalifca" runat="server" Text="CALIFICACION" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:DropDownList ID="DDLestado" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                            <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                            <asp:ListItem Value="APROBADO" Text="Aprobar"></asp:ListItem>
                            <asp:ListItem Value="RECHAZADO" Text="Rechazar"></asp:ListItem>
                        </asp:DropDownList></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>

            <div id="Terminar" runat="server" visible="false" style="text-align: center;" class="row">
                <asp:Button ID="BTterminar" OnClick="terminar" runat="server"  Text="Terminar" class="btn btn-default" />
                <asp:Button ID="BTcancelar" OnClick="cancelar" runat="server"  Text="Cancelar" class="btn btn-default" />
            </div>

             <asp:ImageButton id="IBregresar" OnClick="regresar" runat="server" Visible="false" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline"></asp:ImageButton>
             </br>

             <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
            <asp:HiddenField ID="Metodo" runat="server" Value="" />
          </div>
             </ContentTemplate></asp:UpdatePanel>
         </div>
    </div>
</asp:Content>

