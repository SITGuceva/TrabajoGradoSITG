﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProcesoProyectoF.aspx.cs" Inherits="ProcesoProyectoF" %>

<asp:Content ID="ProcesoPF" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-body" style="margin-left: auto; margin-right: auto; text-align: center;"> 
            <asp:Label ID="Ltitle" runat="server"  Text="DIRECTOR" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#333333" ToolTip="La opción pertenece al rol director." ></asp:Label>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Documentos - Proceso Proyecto Final</div>
        <div class="panel-body">
            <asp:UpdatePanel runat="server" ID="UPprocesopf">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <div id="Consulta" runat="server" style="overflow-x: auto" visible="true" class="row">
                                        <asp:GridView ID="GVproyfinalp" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVproyfinalp_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVproyfinalp_RowDataBound" PageSize="8" OnRowCommand="GVproyfinalp_RowCommand">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No tiene proyectos finales pendientes por revisar!   </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="PPRO_CODIGO" HeaderText="Código" />
                                                <asp:BoundField DataField="PF_TITULO" HeaderText="Título" />
                                                <asp:BoundField DataField="PF_FECHA" HeaderText="Fecha de Entrega" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Revisar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="BTrevisar" runat="server" Text="REVISAR" class="btn btn-default" AutoPostBack="true" CommandName="RevisaPF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <!--RESULTADO DE UN PROYECTO FINAL PARA REVISAR -->
                                    <div id="InformacionP" runat="server" visible="false" class="row">
                                         <asp:GridView ID="GVpf" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" >
                                            <AlternatingRowStyle BackColor="White" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                          <Columns>
                                                <asp:BoundField DataField="PPRO_CODIGO" HeaderText="Código" />
                                                <asp:BoundField DataField="PF_TITULO" HeaderText="Título" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LBdescarga" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PPRO_CODIGO") %>'></asp:LinkButton></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                   </div>

                                    <div id="MostrarDDLestadoP" runat="server" visible="false" style="text-align: center;">
                                        <asp:DropDownList ID="DDLestadoP" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                                            <asp:ListItem Value="Calificar Proyecto" Text="Calificar"></asp:ListItem>
                                            <asp:ListItem Value="APROBADO" Text="Aprobar"></asp:ListItem>
                                            <asp:ListItem Value="RECHAZADO" Text="Rechazar"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button ID="BTmostrarObs" OnClick="MostrarObservaciones" runat="server" Text="Generar Observaciones" class="btn btn-default" />
                                        <br />
                                    </div>

                                    <div id="MostrarAgregarObs" runat="server" style="position: relative; left: -29.4%;" visible="false">
                                        <asp:Table ID="Tobservaciones" runat="server" HorizontalAlign="center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <textarea id="TBdescripcion" rows="2" enabled="true" width="900" runat="server" cssclass="form-control"></textarea></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="BTagregar" Enabled="true" OnClick="Agregar_observacion" runat="server" Text="AGREGAR" class="btn btn-default"></asp:Button></asp:TableCell>
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
                                            <EmptyDataTemplate>¡El proyecto final aún no tiene observaciones! </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="PFOBS_CODIGO" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="PFOBS_DESCRIPCION" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="Modificar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btn_Edit" runat="server" Text="Modificar" class="btn btn-default" CommandName="Edit" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btn_Update" runat="server" Text="Actualizar" class="btn btn-success" ForeColor="White" CommandName="Update" />
                                                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" class="btn btn-danger" ForeColor="White" CommandName="Cancel" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div id="Terminar" runat="server" visible="false" style="text-align: center;" class="row">
                                        <br />
                                        <asp:Button ID="BTterminar" OnClick="terminar" runat="server" Text="Terminar" class="btn btn-success" ForeColor="White" />
                                        <asp:Button ID="btnDummy" runat="server" Text="btnDummy" OnClick="btnDummy_Click" Style="display: none" />
                                        <asp:Button ID="BTcancelar" OnClick="cancelar" runat="server" Text="Cancelar" class="btn btn-danger" ForeColor="White" />
                                    </div>

                                    <asp:ImageButton ID="IBregresar" OnClick="regresar" runat="server" Visible="false" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline"></asp:ImageButton>
                                    <br />
                                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Green" Font-Bold="True"></asp:Label>
                                    <asp:HiddenField ID="Metodo" runat="server" Value="" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        function myconfirmbox() {
            if (confirm("¿Está seguro de aplicar los cambios al proyecto final?")) {
                //trigger the button click
                __doPostBack('<%= btnDummy.UniqueID %>', "");
                return true;
            } else {
                return false;
            }
        }
     </script>
</asp:Content>

