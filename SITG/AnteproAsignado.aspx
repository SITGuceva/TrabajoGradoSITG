﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AnteproAsignado.aspx.cs" Inherits="AnteproAsignado" %>

<asp:Content ID="AnteproyectoAsignado" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Documentos - Anteproyectos Asignados</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPanteasignado" runat="server"> <ContentTemplate>
            <div class="container-fluid">
           
            <div id="Consulta" runat="server" visible="true" class="row" style="overflow-x: auto">
                <asp:GridView ID="GVconsultaAA" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                    OnPageIndexChanging="GVconsultaAA_PageIndexChanging" AutoGenerateColumns="False"
                    CssClass="table table-bordered bs-table" OnRowDataBound="GVconsultaAA_RowDataBound" PageSize="6" OnRowCommand="GVconsultaAA_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="white" />
                    <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" /> 
                    <EmptyDataTemplate>¡No existen anteproyectos asignados!</EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="APRO_CODIGO" HeaderText="Código" />
                        <asp:BoundField DataField="ANP_NOMBRE" HeaderText="Título" />
                        <asp:BoundField DataField="ANP_FECHA" HeaderText="Fecha" />
                        <asp:BoundField DataField="ANP_ESTADO" HeaderText="Estado" />
                        <asp:BoundField DataField="ANP_APROBACION" HeaderText="Director" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                             <ItemTemplate><asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("APRO_CODIGO") %>'></asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Anteproyecto">
                            <ItemTemplate> <asp:Button ID="BTrevisar" runat="server" Text="REVISAR" class="btn btn-default" AutoPostBack="true" CommandName="ConsultarAnteproyecto" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /> </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- -->
            
            <div id="MostrarDDLestadoP" runat="server" visible="false" style="text-align: center;">
                <asp:DropDownList ID="DDLestadoA" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                    <asp:ListItem Value="0" Text="Calificar Anteproyecto"></asp:ListItem>
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
                            <TextArea id="TBdescripcion" row="2" Enabled="true" Width="900" runat="server" CssClass="form-control"></TextArea></asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="BTagregar" Enabled="true" OnClick="Agregar_observacion" runat="server" Text="Agregar observación" class="btn btn-default"></asp:Button></asp:TableCell>
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
                    <EmptyDataTemplate>¡El anteproyecto seleccionado aún no tiene observaciones! </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="AOBS_CODIGO" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AOBS_DESCRIPCION" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Modificar">
                            <ItemTemplate>
                                <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" class="btn btn-default"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" class="btn btn-success" ForeColor="White"/>
                                <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" class="btn btn-danger" ForeColor="White" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                    </Columns>
                </asp:GridView>
            </div>


            <div id="Terminar" runat="server" visible="false" style="text-align: center;" class="row">
                <asp:Button ID="BTterminar" OnClick="terminar" runat="server"  Text="Terminar" class="btn btn-success" ForeColor="White"/>
                <asp:Button ID="BTcancelar" OnClick="cancelar" runat="server"  Text="Cancelar" class="btn btn-danger" ForeColor="White"/>
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

