<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProcesoAnteproyecto.aspx.cs" Inherits="ProcesoAnteproyecto" %>

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
                        ¡No tiene anteproyectos pendientes por revisar!  
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="APRO_CODIGO" HeaderText="CODIGO ANTEPROYECTO" />
                        <asp:BoundField DataField="ANP_NOMBRE" HeaderText="TÍTULO" />
                        <asp:BoundField DataField="ANP_FECHA" HeaderText="FECHA DE ENTREGA" />
                       
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="REVISAR ANTEPROYECTO">
                            <ItemTemplate>
                                <asp:Button ID="BTrevisar" runat="server" Text="REVISAR" class="btn btn-default" AutoPostBack="true" CommandName="ConsultarPropuesta" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:HiddenField ID="Metodo" runat="server" Value="" />
            </div>
            <!-- -->
            <div id="InformacionP" runat="server" visible="true" class="row">
                <asp:Label ID="TCodigoP" runat="server" Text="" ForeColor="Gray" Font-Bold="True"></asp:Label>
                <asp:Label ID="CodigoP" runat="server" Text="" ForeColor="Black" Font-Bold="True"></asp:Label>
                <br>
                <asp:Label ID="TTituloP" runat="server" Text="" ForeColor="Gray" Font-Bold="True"></asp:Label>
                <asp:Label ID="TituloP" runat="server" Text="" ForeColor="Black" Font-Bold="True"></asp:Label>
            </div>

            <div id="MostrarDDLestadoP" runat="server" visible="true" style="text-align: center;">
                <asp:DropDownList ID="DDLestadoP" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                    <asp:ListItem Value="Calificar Anteproyecto" Text="Calificar Anteproyecto"></asp:ListItem>
                    <asp:ListItem Value="APROBADO" Text="Aprobar"></asp:ListItem>
                    <asp:ListItem Value="RECHAZADO" Text="Rechazar"></asp:ListItem>
                </asp:DropDownList>
               
                 
                <asp:Button ID="BTmostrarObs" OnClick="MostrarObservaciones" runat="server" Text="Generar Observaciones" class="btn btn-default" />
                <br>
            </div>

            <div id="MostrarDescarga" runat="server" visible="true" style="text-align: center;">
                <br>
                <asp:LinkButton ID="LinkDescarga" runat="server" Text="Descargar documento" OnClick="DownloadFile"></asp:LinkButton>
            </div>

            <div id="MostrarAgregarObs" runat="server" style="position: relative; left: -29.4%;" visible="false">
                
                <asp:Table ID="Tobservaciones" runat="server" HorizontalAlign="center">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:TextBox ID="TBdescripcion" Enabled="true" Width="900" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="BTagregar" Enabled="true" OnClick="Agregar_observacion" runat="server" Text="Agregar observacion" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

            </div>

            <div id="Resultado" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
          
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
                    <EmptyDataTemplate>¡Esta propuesta aun no tiene observaciones! </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="AOBS_CODIGO" HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AOBS_DESCRIPCION" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Modificar">
                            <ItemTemplate>
                                <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" />
                                <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                    </Columns>
                </asp:GridView>
            </div>


            <div id="Terminar" runat="server" visible="true" style="text-align: center;" class="row">
                <br>
                <asp:Button ID="BTterminar" OnClick="terminar" runat="server" Style="background-color: lightgray" Text="Terminar revisión" class="btn btn-default" />
                <asp:Button ID="BTcancelar" OnClick="cancelar" runat="server" Style="background-color: lightgray" Text="Cancelar revisión" class="btn btn-default" />
            </div>

             <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Green" Font-Bold="True"></asp:Label>
             <asp:Label ID="Linfo2" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
             <asp:Button ID="BTregresar" OnClick="regresar" runat="server" Text="Regresar" class="btn btn-default" />
            </div>

    </div>
</asp:Content>

