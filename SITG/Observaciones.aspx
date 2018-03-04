<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Observaciones.aspx.cs" Inherits="Observaciones" %>

<asp:Content ID="ObservacionesProp" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Generar Observaciones Propuesta</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UProl" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">

                        <div id="Ingreso" runat="server" visible="false" class="row">                    
                            <asp:Table ID="Tobservaciones" runat="server" HorizontalAlign="center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="LCodigo" runat="server" Text="Codigo de la propuesta:" ForeColor="Black" Font-Bold="True"></asp:Label> </asp:TableCell>
                                    <asp:TableCell><asp:Label ID="LDescripcion" runat="server" Text="Descripcion:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:TextBox ID="TBcodigo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBdescripcion" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Button ID="BTagregar" Enabled="false" OnClick="Agregar_observacion" runat="server" Text="Agregar observacion" Style="background-color: white; font-size: 14px; color: black" CssClass="form-control"></asp:Button></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="Btbuscar" OnClick="Buscar_observacion" runat="server" Text="Buscar"  CssClass="btn btn-default"></asp:Button>
                                        <asp:Button ID="Btnuevo" OnClick="Nuevo" Visible="false" runat="server" Text="Nueva Consulta"  CssClass="btn btn-default"></asp:Button>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                    <br>

                        <div id="InfGeneral" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="GVtitulo" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table" OnRowDataBound="GVtitulo_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>  ¡No hay titulo de la propuesta! </EmptyDataTemplate>
                                <Columns><asp:BoundField DataField="PROP_TITULO" HeaderText="Titulo de la propuesta" HeaderStyle-HorizontalAlign="Center" /></Columns>
                            </asp:GridView>
                        
                            <asp:GridView ID="GVintegrantes" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"       
                                AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVintegrantes_RowDataBound" PageSize="5">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate> ¡No hay integrantes de la propuesta! </EmptyDataTemplate>                               
                                <Columns><asp:BoundField DataField="integrantes" HeaderText="Integrantes" /></Columns>
                            </asp:GridView>
                        </div>

                        <div id="Resultado" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="GVobservacion" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"  OnPageIndexChanging="GVobservacion_PageIndexChanging" AutoGenerateColumns="False"  CssClass="table table-bordered bs-table" OnRowDataBound="GVobservacion_RowDataBound" PageSize="8" 
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
                                <EmptyDataTemplate> ¡Esta propuesta aun no tiene observaciones! </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="OBS_CODIGO" HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="OBS_DESCRIPCION" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" />                              
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

