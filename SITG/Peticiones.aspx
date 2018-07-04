<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Peticiones.aspx.cs" Inherits="PeticionDir" %>

<asp:Content ID="PeticionDir" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Reunión - Peticiones Estudiantes</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPpeticion_dir" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="breadcrumb">
                                    <li> <asp:LinkButton ID="LBPeticion_Dir" runat="server" OnClick="LBPeticion_Dir_Click" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Peticiones Director </asp:LinkButton></li>
                                    <li> <asp:LinkButton ID="LBPeticion_Est" runat="server" OnClick="LBPeticion_Est_Click" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Peticiones Estudiantes </asp:LinkButton></li>
                                </ul>
                                <div class="panel panel-default">
                                    <div class="panel-body">

                                        <div id="TPeticiones" runat="server" visible="false" class="row" style="overflow-x: auto">
                                            <asp:GridView ID="GVpeticion" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVpeticion_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                                                OnRowDataBound="GVpeticion_RowDataBound" PageSize="8" OnRowCommand="GVpeticion_RowCommand">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="white" />
                                                <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                <EmptyDataTemplate>¡No hay peticiones de director pendientes.!</EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="DIR_ID" HeaderText="Id" />
                                                    <asp:BoundField DataField="DIR_FECHA" HeaderText="Fecha" />
                                                    <asp:BoundField DataField="PROP_TITULO" HeaderText="Propuesta" />
                                                    <asp:BoundField DataField="DIRECTOR" HeaderText="Director Solicitante" />
                                                    <asp:BoundField DataField="DIR_OBSERVACION" HeaderText="Observación" />
                                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                                                    <asp:BoundField DataField="EXTERNO" HeaderText="Externo" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Carta">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LBcarta" ClientIDMode="AutoID" runat="server" Text="Descargar" OnClick="DescargarCarta" CommandArgument='<%# Eval("DIR_ID") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Información">
                                                        <ItemTemplate>
                                                            <asp:Button ID="Ver" runat="server" Text="VER" CommandName="Ver" CssClass="btn btn-default" CommandArgument='<%# Bind("USU_USERNAME") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Revisar">
                                                        <ItemTemplate>
                                                            <asp:Button ID="Calificar" runat="server" Text="CALIFICAR" CommandName="Calificar" CssClass="btn btn-default" CommandArgument='<%# Bind("DIR_ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                        <div id="Tinfprof" runat="server" style="overflow-x: auto" visible="false" class="row">
                                            <asp:GridView ID="GVinfprof" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVinfprof_RowDataBound">
                                                <AlternatingRowStyle BackColor="White" />
                                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="white" />
                                                <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                <EmptyDataTemplate>¡No hay información del profesor.! </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="USU_TELEFONO" HeaderText="Teléfono" />
                                                    <asp:BoundField DataField="USU_CORREO" HeaderText="Correo Electrónico" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Hoja de Vida">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" ClientIDMode="AutoID" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("USU_USERNAME") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div id="CalificarPdir" runat="server" visible="false" class="row">
                                            <asp:Table ID="Tcalificar" runat="server" HorizontalAlign="Center">
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:Label ID="Lestad" runat="server" Text="Estado: " ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:DropDownList ID="DDLestado" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" Style="width: 700px;">
                                                            <asp:ListItem Value="0" Text="Seleccione" />
                                                            <asp:ListItem Value="APROBADO">Aprobar</asp:ListItem>
                                                            <asp:ListItem Value="RECHAZADO">Rechazar</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:Label ID="Lobs" runat="server" Text="Observaciones: " ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                    <asp:TableCell>
                                                        <textarea id="TAobs" runat="server" cssclass="form-control" rows="2"></textarea></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:Button ID="Bcalificar" runat="server" Text="Guardar" OnClick="Bcalificar_Click" class="btn btn-success" ForeColor="White" />
                                                        <asp:Button ID="btnDummy" runat="server" Text="btnDummy" OnClick="btnDummy_Click" Style="display: none" />
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Button ID="Bcancel" runat="server" Text="Cancelar" OnClick="regresar" class="btn btn-danger" ForeColor="White" /></asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </div>


                                        <div id="PetiEstudiante" runat="server" visible="false" class="row">
                                            <asp:Table ID="TSolicitar" runat="server" HorizontalAlign="Center">
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:Label ID="Lsol" runat="server" Text="Tipo: " ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:DropDownList ID="DDLsol" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" Style="width: 700px;" AutoPostBack="true" OnSelectedIndexChanged="DDLsol_SelectedIndexChanged">
                                                            <asp:ListItem Value="0" Text="Seleccione" />
                                                            <asp:ListItem Value="1" Text="Cambio Propuesta" />
                                                            <asp:ListItem Value="2" Text="Abandonar Propuesta" />
                                                            <asp:ListItem Value="3" Text="Ingresar Integrante" />
                                                        </asp:DropDownList>
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Button ID="Bbuscar" runat="server" Text="Consultar" OnClick="Bbuscar_Click" class="btn btn-default" /></asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </div>

                                        <div id="ConsultaPeti" runat="server" visible="false" class="row" style="overflow-x: auto">
                                            <asp:GridView ID="GVconsulta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                                AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVconsulta_RowDataBound"
                                                OnRowUpdating="GVconsulta_RowUpdating" OnRowEditing="GVconsulta_RowEditing" OnRowCancelingEdit="GVconsulta_RowCancelingEdit">
                                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="white" />
                                                <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                <EmptyDataTemplate>¡No tiene peticiones con el parámetro seleccionado.!  </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="SOLE_ID" HeaderText="Id" />
                                                    <asp:BoundField DataField="SOLE_FECHA" HeaderText="Fecha" />
                                                    <asp:BoundField DataField="SOLE_MOTIVO" HeaderText="Motivo" />
                                                    <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                                                    <asp:BoundField DataField="ESTUDIANTE" HeaderText="Estudiante" />
                                                    <asp:TemplateField HeaderText="ESTADO">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="estado" runat="server" class="btn btn-secondary btn-sm dropdown-toggle">
                                                                <asp:ListItem Value="Aceptada">ACEPTAR</asp:ListItem>
                                                                <asp:ListItem Value="Rechazada">RECHAZAR</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lestado" runat="server" Text='<%# Bind("SOLE_ESTADO") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Modificar">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" class="btn btn-default" /></ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" class="btn btn-success" ForeColor="White" />
                                                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" class="btn btn-danger" ForeColor="White" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                        <asp:ImageButton ID="IBregresar" OnClick="regresar" runat="server" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline" Visible="false"></asp:ImageButton>
                                        <br />
                                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="red" Font-Bold="True"></asp:Label>
                                        <asp:HiddenField ID="Tipo" runat="server" Value="" />
                                    </div>
                                </div>
                            </div>
                        </div>
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
    <script type="text/javascript">
        function myconfirmbox() {
            if (confirm("¿Está seguro de aplicar los cambios a la petición del director?")) {
                //trigger the button click
                __doPostBack('<%= btnDummy.UniqueID %>', "");
                return true;
            } else {
                return false;
            }
        }
     </script>
</asp:Content>




