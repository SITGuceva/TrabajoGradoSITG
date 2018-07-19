<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PropuestaPendiente.aspx.cs" Inherits="PropuestaPendiente" %>

<asp:Content ID="PropuestaPendiente" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-body" style="margin-left: auto; margin-right: auto; text-align: center;"> 
            <asp:Label ID="Ltitle" runat="server"  Text="COMITÉ" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#333333" ToolTip="La opción pertenece al rol comité." ></asp:Label>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Reunión - Propuestas Pendientes</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPproppendi" runat="server"> <ContentTemplate>
                <div class="container-fluid">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-body">

                                <div id="Consulta" runat="server" visible="true" class="row" style="overflow-x: auto">
                                    <asp:GridView ID="GVconsultaPP" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                        OnPageIndexChanging="GVconsultaPP_PageIndexChanging" AutoGenerateColumns="False"
                                        CssClass="table table-bordered bs-table" OnRowDataBound="GVconsultaPP_RowDataBound" PageSize="6" OnRowCommand="GVconsultaPP_RowCommand">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="white" />
                                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#ffffcc" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        <EmptyDataTemplate>¡No hay propuestas pendientes!   </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código" />
                                            <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                                            <asp:BoundField DataField="PROP_FECHA" HeaderText="Fecha" />
                                            <asp:BoundField DataField="PESTADO" HeaderText="Estado" />
                                            <asp:BoundField DataField="DIRECTOR" HeaderText="Director" />
                                            <asp:BoundField DataField="ESTADO" HeaderText="Estado Director" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Revisar">
                                                <ItemTemplate>
                                                    <asp:Button ID="BTrevisar" runat="server" Text="REVISAR" class="btn btn-default" AutoPostBack="true" CommandName="ConsultarPropuesta" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <!-- -->

                                <div id="MostrarDDLReunion" runat="server" visible="false" class="row">
                                    <asp:DropDownList ID="DDLconsultaReunion" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList>
                                </div>

                                <div id="ConsultaContenidoP" runat="server" visible="false" class="row" style="overflow-x: auto">
                                    <asp:GridView ID="GVConsultaContenidoP" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                        OnPageIndexChanging="GVConsultaContenidoP_PageIndexChanging" AutoGenerateColumns="False"
                                        CssClass="table table-bordered bs-table" OnRowDataBound="GVConsultaContenidoP_RowDataBound" PageSize="6">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="white" />
                                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#ffffcc" />
                                        <Columns>
                                            <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código" />
                                            <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                                            <asp:BoundField DataField="LINV_NOMBRE" HeaderText="Línea Investigación" />
                                            <asp:BoundField DataField="TEM_NOMBRE" HeaderText="Tema" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PROP_CODIGO") %>'></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div id="MostrarDDLestadoP" runat="server" visible="false" style="text-align: center;">
                                    <asp:DropDownList ID="DDLestadoP" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                                        <asp:ListItem Value="Calificar Propuesta" Text="Calificar"></asp:ListItem>
                                        <asp:ListItem Value="APROBADO" Text="Aprobar"></asp:ListItem>
                                        <asp:ListItem Value="RECHAZADO" Text="Rechazar"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Button ID="BTmostrarObs" OnClick="MostrarObservaciones" runat="server" Text="Generar Observaciones" class="btn btn-default" />
                                    <br>
                                </div>

                                <div id="MostrarAgregarObs" runat="server" visible="false">
                                    <asp:Table ID="Tobservaciones" runat="server" HorizontalAlign="center">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <textarea id="TBdescripcion" row="2" enabled="true" width="900" runat="server" cssclass="form-control"></textarea>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Button ID="BTagregar" Enabled="true" OnClick="Agregar_observacion" runat="server" Text="AGREGAR" class="btn btn-default"></asp:Button>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>

                                <div id="Resultado" runat="server" style="overflow-x: auto" visible="false" class="row">
                                    <asp:GridView ID="GVobservacion" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVobservacion_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVobservacion_RowDataBound" PageSize="8"
                                        OnRowDeleting="GVobservacion_RowDeleting" OnRowUpdating="GVobservacion_RowUpdating" OnRowEditing="GVobservacion_RowEditing" OnRowCancelingEdit="GVobservacion_RowCancelingEdit">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="white" />
                                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#ffffcc" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        <EmptyDataTemplate>¡Esta propuesta aún no tiene observaciones! </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="OBS_CODIGO" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="OBS_DESCRIPCION" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="Modificar">
                                                <ItemTemplate>
                                                    <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" class="btn btn-default" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" class="btn btn-success" ForeColor="White" />
                                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" class="btn btn-danger" ForeColor="White" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </div>


                                <div id="Terminar" runat="server" visible="false" style="text-align: center;" class="row">
                                    <asp:Button ID="BTterminar" OnClick="terminar" runat="server" Text="Terminar" class="btn btn-success" ForeColor="White" />
                                    <asp:Button ID="btnDummy" runat="server" Text="btnDummy" OnClick="btnDummy_Click" Style="display: none" />
                                    <asp:Button ID="BTcancelar" OnClick="cancelar" runat="server" Text="Cancelar" class="btn btn-danger" ForeColor="White" />
                                </div>

                                <asp:ImageButton ID="IBregresar" OnClick="regresar" runat="server" Visible="false" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline"></asp:ImageButton>
                                <br/>
                                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                                <asp:HiddenField ID="Metodo" runat="server" Value="" />

                            </div>
                        </div>
                    </div>
                </div>
             </ContentTemplate></asp:UpdatePanel>
         </div>
    </div>
     <script type="text/javascript">
        function myconfirmbox() {
            if (confirm("¿Está seguro de aplicar los cambios a la propuesta?")) {
                //trigger the button click
                __doPostBack('<%= btnDummy.UniqueID %>', "");
                return true;
            } else {
                return false;
            }
        }
     </script>
</asp:Content>

