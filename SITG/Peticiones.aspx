<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Peticiones.aspx.cs" Inherits="PeticionDir" %>

<asp:Content ID="PeticionDir" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Reunion - Peticiones de Director</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPpeticion_dir" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">

                        <div class="row">
                            <div class="col-md-12">
                                <ul class="breadcrumb">
                                    <li><asp:LinkButton ID="LBPeticion_Dir" runat="server" OnClick="LBPeticion_Dir_Click" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Peticion Director </asp:LinkButton></li>
                                    <li><asp:LinkButton ID="LBPeticion_Est" runat="server" OnClick="LBPeticion_Est_Click" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Peticiones Estudiante </asp:LinkButton></li>
                                </ul>
                            </div>
                        </div>

                        <div id="TPeticiones" runat="server" visible="false" class="row" style="overflow-x: auto">
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
                                    <asp:BoundField DataField="DIR_ID" HeaderText="Id" />
                                    <asp:BoundField DataField="DIR_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="PROP_TITULO" HeaderText="Propuesta" />
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


                        <div id="Tinfprof" runat="server" style="overflow-x: auto" visible="false" class="row">
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
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                                    <asp:BoundField DataField="USU_TELEFONO" HeaderText="Telefono" />
                                    <asp:BoundField DataField="USU_CORREO" HeaderText="Correo" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Hoja de Vida">
                                        <ItemTemplate> <asp:LinkButton ID="lnkDownload" ClientIDMode="AutoID" runat="server" Text="Download" OnClick="DownloadFile" CommandArgument='<%# Eval("USU_USERNAME") %>' ></asp:LinkButton> </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>    
                        </div>


                     <div id="PetiEstudiante" runat="server" visible="false" class="row">
                      <asp:Table ID="TSolicitar" runat="server" HorizontalAlign="Center" >                
                          <asp:TableRow>
                             <asp:TableCell><asp:Label ID="Lsol" runat="server" Text="Tipo: " ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                 
                             <asp:TableCell><asp:DropDownList ID="DDLsol" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" style="width:700px;" AutoPostBack="true" OnSelectedIndexChanged="DDLsol_SelectedIndexChanged">
                                       <asp:ListItem Value="0" Text="Seleccione"/>
                                       <asp:ListItem Value="1" Text="Cambio Propuesta" />
                                       <asp:ListItem Value="2" Text="Abandonar Propuesta" />
                                       <asp:ListItem Value="3" Text="Ingresar Integrante" />
                            </asp:DropDownList></asp:TableCell>          
                            <asp:TableCell><asp:Button ID="Bbuscar" runat="server" Text="Consultar" OnClick="Bbuscar_Click" class="btn btn-default"/></asp:TableCell>
                          </asp:TableRow> 
                       </asp:Table>
                     </div>

                     <div id="ConsultaPeti" runat="server" visible="false" class="row" style="overflow-x: auto" >                  
                        <asp:GridView ID="GVconsulta" runat="server"  AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                           AutoGenerateColumns="False" CssClass="table table-bordered bs-table"  OnRowDataBound="GVconsulta_RowDataBound"
                            OnRowUpdating="GVconsulta_RowUpdating" OnRowEditing="GVconsulta_RowEditing" OnRowCancelingEdit="GVconsulta_RowCancelingEdit">                   
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /> 
                            <RowStyle BackColor="White" />
                            <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                            <EmptyDataTemplate> ¡No tienes peticiones con el parametro seleccionado!  </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="SOLE_ID" HeaderText="ID" />
                                <asp:BoundField DataField="SOLE_FECHA" HeaderText="FECHA" />
                                <asp:BoundField DataField="SOLE_MOTIVO" HeaderText="MOTIVO" />
                                <asp:BoundField DataField="PROP_TITULO" HeaderText="TITULO" />
                                <asp:BoundField DataField="ESTUDIANTE" HeaderText="ESTUDIANTE" />
                                <asp:TemplateField HeaderText="ESTADO">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="estado" runat="server" class="btn btn-secondary btn-sm dropdown-toggle">
                                            <asp:ListItem Value="Aceptada">ACEPTAR</asp:ListItem>
                                            <asp:ListItem Value="Rechazada">RECHAZAR</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate><asp:Label ID="Lestado" runat="server" Text='<%# Bind("SOLE_ESTADO") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modificar">
                                    <ItemTemplate><asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" class="btn btn-default"/></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" class="btn btn-default"/>
                                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" class="btn btn-default" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                          
                        <asp:ImageButton id="IBregresar" OnClick="regresar" runat="server" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline" Visible="false"></asp:ImageButton>
                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
                        <asp:HiddenField ID="Tipo" runat="server" Value=""/>
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




