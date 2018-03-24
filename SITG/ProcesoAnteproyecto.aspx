<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProcesoAnteproyecto.aspx.cs" Inherits="ProcesoAnteproyecto" %>

<asp:Content ID="ProcesoAnteproyecto" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Proceso anteproyecto</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPprocesoantepro" runat="server"> <ContentTemplate>
            <div class="container-fluid">

            <div id="Consulta" runat="server" visible="true" class="row" style="overflow-x: auto">
                <asp:GridView ID="GVantependiente" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVantependiente_PageIndexChanging" AutoGenerateColumns="False"  CssClass="table table-bordered bs-table" OnRowDataBound="GVantependiente_RowDataBound" PageSize="8" OnRowCommand="GVantependiente_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    <EmptyDataTemplate> ¡No tiene anteproyectos por revisar!  </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="APRO_CODIGO" HeaderText="CODIGO ANTEPROYECTO" />
                        <asp:BoundField DataField="ANP_NOMBRE" HeaderText="TÍTULO" />
                        <asp:BoundField DataField="ANP_FECHA" HeaderText="FECHA DE ENTREGA" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="REVISAR ANTEPROYECTO">
                            <ItemTemplate><asp:Button ID="BTrevisar" runat="server" Text="REVISAR" class="btn btn-default" AutoPostBack="true" CommandName="RevisarAnte" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /> </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <!-- -->
            <div id="InfoAnteproy" runat="server" visible="false" class="row">
                <asp:Label ID="Lcodigo" runat="server" Text="Codigo:" ForeColor="Gray" Font-Bold="True"></asp:Label>
                <asp:Label ID="CodigoP" runat="server" Text="" ForeColor="Black" Font-Bold="True"></asp:Label>
                <br/>
                <asp:Label ID="Ltitulo" runat="server" Text="Titulo:" ForeColor="Gray" Font-Bold="True"></asp:Label>
                <asp:Label ID="TituloP" runat="server" Text="" ForeColor="Black" Font-Bold="True"></asp:Label>
                <br />
                <asp:Label ID="Ldocumento" runat="server" Text="Documento:" ForeColor="Gray" Font-Bold="True"></asp:Label>
                <asp:LinkButton ID="LBdescarga" runat="server" Text="Descargar" OnClick="DownloadFile"></asp:LinkButton>
            </div>

            <div id="MostrarCalifica" runat="server" visible="false" style="text-align: center;">
                <asp:DropDownList ID="DDLestadoP" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                    <asp:ListItem Value="Calificar Anteproyecto" Text="Calificar Anteproyecto"></asp:ListItem>
                    <asp:ListItem Value="APROBADO" Text="Aprobar"></asp:ListItem>
                    <asp:ListItem Value="RECHAZADO" Text="Rechazar"></asp:ListItem>
                </asp:DropDownList>             
                <asp:Button ID="BTmostrarObs" OnClick="MostrarObservaciones" runat="server" Text="Generar Observaciones" class="btn btn-default" />
                <br>
            </div>

            <div id="MostrarAgregarObs" runat="server" style="position: relative; left: -29.4%;" visible="false"> 
                <asp:Table ID="Tobservaciones" runat="server" HorizontalAlign="center">
                    <asp:TableRow>
                        <asp:TableCell> <TextArea  id="TBdescripcion" row="2" Enabled="true" Width="900" runat="server" CssClass="form-control"></TextArea></asp:TableCell>
                        <asp:TableCell><asp:Button ID="BTagregar" Enabled="true" OnClick="Agregar_observacion" runat="server" Text="Agregar observacion" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

            </div>

            <div id="Resultado" runat="server" style="overflow-x: auto" visible="false" class="row">
                <asp:GridView ID="GVobservacion" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVobservacion_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVobservacion_RowDataBound" PageSize="8"
                    OnRowDeleting="GVobservacion_RowDeleting" OnRowUpdating="GVobservacion_RowUpdating" OnRowEditing="GVobservacion_RowEditing" OnRowCancelingEdit="GVobservacion_RowCancelingEdit">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="white" />
                    <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    <EmptyDataTemplate>¡El anteproyecto aun no tiene observaciones! </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="AOBS_CODIGO" HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AOBS_DESCRIPCION" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Modificar">
                            <ItemTemplate><asp:Button ID="btn_Edit" runat="server" Text="Modificar" class="btn btn-default" CommandName="Edit" /></ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btn_Update" runat="server" Text="Actualizar" class="btn btn-default" CommandName="Update" />
                                <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" class="btn btn-default" CommandName="Cancel" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                    </Columns>
                </asp:GridView>
            </div>


            <div id="Terminar" runat="server" visible="false" style="text-align: center;" class="row">
                <br>
                <asp:Button ID="BTterminar" OnClick="terminar" runat="server"  Text="Terminar" class="btn btn-default" />
                <asp:Button ID="BTcancelar" OnClick="cancelar" runat="server"  Text="Cancelar" class="btn btn-default" />
            </div>

             <asp:ImageButton id="IBregresar" OnClick="regresar" runat="server" Visible="false" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline"></asp:ImageButton>
             <br />
             <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Green" Font-Bold="True"></asp:Label>
             <asp:HiddenField ID="Codigo" runat="server" Value="" />
             <asp:HiddenField ID="Titulo" runat="server" Value="" />

            </div>
            </ContentTemplate></asp:UpdatePanel>
            </div>
    </div>
</asp:Content>

