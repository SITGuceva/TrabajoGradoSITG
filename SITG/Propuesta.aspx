<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Propuesta.aspx.cs" Inherits="Propuesta" %>

<asp:Content ID="Propuesta" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Documentos - Propuesta</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPpropuesta" runat="server"> <ContentTemplate>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <ul class="breadcrumb">
                                <li><asp:LinkButton ID="LBSubir_propuesta" runat="server" OnClick="Subir_propuesta" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Subir </asp:LinkButton></li>
                                <li> <asp:LinkButton ID="LBConsulta_propuesta" runat="server" OnClick="Consulta_propuesta" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar </asp:LinkButton></li>
                                <li><asp:LinkButton ID="LBSolicitar" runat="server" OnClick="LBSolicitar_Click" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Solicitar Director </asp:LinkButton></li>
                                <li> <asp:LinkButton ID="LBConsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-pencil"></span>Consultar Director</asp:LinkButton></li>
                            </ul>

                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <div id="Ingreso" runat="server" visible="false" class="row">
                                        <asp:Table ID="Tsubir" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Llprof" runat="server" Text="Línea Investigación:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList ID="DDLlprof" runat="server" class="btn btn-secondary btn-lg dropdown-toggle" AutoPostBack="true" OnSelectedIndexChanged="DDLlprof_SelectedIndexChanged"></asp:DropDownList></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="Ltema" runat="server" Text="Tema:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList ID="DDLtema" runat="server" class="btn btn-secondary btn-lg dropdown-toggle"></asp:DropDownList></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lnombre" runat="server" Text="Título:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <textarea id="TAnombre" runat="server" cssclass="form-control"></textarea>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="Ldocumento" runat="server" Text="Documento:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:FileUpload ID="FUdocumento" runat="server" class="btn btn-default image-preview-input" /></asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableHeaderCell>
                                                    <asp:Label ID="Ltin" runat="server" Text="INTEGRANTES" ForeColor="Black" Font-Bold="True"></asp:Label>
                                                </asp:TableHeaderCell>
                                            </asp:TableRow>
                                            <asp:TableRow runat="server" ID="integra">
                                                <asp:TableCell>
                                                    <asp:Label ID="Lintegrante" runat="server" Text="Código del Estudiante:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="TBcodint" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bintegrante" runat="server" Text="Buscar" OnClick="Bintegrante_Click" class="btn btn-default" />
                                                    <asp:Button ID="Bnueva" runat="server" Text="Nueva Consulta" Visible="false" OnClick="Bintegrante_Click" class="btn btn-default" />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow ID="RespInte" runat="server" Visible="false">
                                                <asp:TableCell>
                                                    <asp:Label ID="Nombre" runat="server" Text="Nombre del Estudiante:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="Rnombre" runat="server" Text="x" ForeColor="Black" CssClass="form-control"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Agregar" runat="server" Text="AGREGAR" OnClick="AgregarIntegrante" class="btn btn-default" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                        <asp:GridView ID="GVagreinte" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" PageSize="6">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <Columns>
                                                <asp:BoundField DataField="CODIGO" HeaderText="Código" />
                                                <asp:BoundField DataField="INTEGRANTES" HeaderText="Nombre" />
                                            </Columns>
                                        </asp:GridView>

                                        <asp:Table ID="TBotones" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bguardar" runat="server" Text="Guardar" OnClick="Guardar" class="btn btn-success" ForeColor="White" /></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bcancelar" runat="server" Text="Cancelar" OnClick="Cancelar" class="btn btn-danger" ForeColor="White" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>

                                    <div id="Consulta" runat="server" visible="false" class="row" style="overflow-x: auto">
                                        <asp:GridView ID="GVconsulta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                            AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowCommand="GVconsulta_RowCommand">
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No se ha subido la propuesta!  </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="PROP_CODIGO" HeaderText="Id" />
                                                <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                                                <asp:BoundField DataField="LINV_NOMBRE" HeaderText="Línea Investigación" />
                                                <asp:BoundField DataField="TEM_NOMBRE" HeaderText="Tema" />
                                                <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("PROP_CODIGO") %>'></asp:LinkButton></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Observaciones">
                                                    <ItemTemplate>
                                                        <asp:Button ID="Bobservaciones" runat="server" Text="VER" CommandName="buscar" class="btn btn-default" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div id="Observaciones" runat="server" visible="false" class="row">
                                        <asp:GridView ID="GVobservacion" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                            OnPageIndexChanging="GVobservacion_PageIndexChanging" AutoGenerateColumns="False"
                                            CssClass="table table-bordered bs-table" OnRowDataBound="GVobservacion_RowDataBound" PageSize="6">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No hay observaciones!</EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="OBS_CODIGO" HeaderText="Código" />
                                                <asp:BoundField DataField="OBS_DESCRIPCION" HeaderText="Descripción" />
                                                <asp:BoundField DataField="OBS_REALIZADA" HeaderText="Realizada Por" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>


                                    <div id="Solicitar" runat="server" visible="false" class="row">
                                        <asp:Table ID="Ttipodir" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Linterno" runat="server" Text="Interno" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:RadioButton ID="RBinterno" runat="server" Text="" AutoPostBack="true" GroupName="TipoDirector" OnCheckedChanged="RBtipodirector_CheckedChanged" Checked="True" /></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lexterno" runat="server" Text="Externo" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:RadioButton ID="RBexterno" runat="server" Text="" AutoPostBack="true" GroupName="TipoDirector" OnCheckedChanged="RBtipodirector_CheckedChanged" /></asp:TableCell>
                                            </asp:TableRow>

                                        </asp:Table>
                                        <asp:Table ID="TSolicitar" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lprof" runat="server" Text="Profesores: " ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList ID="DDLlista" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" Style="width: 700px;"></asp:DropDownList></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bconsulta" runat="server" Text="Consultar" OnClick="InfProfesor" class="btn btn-default" />
                                                    <asp:Button ID="Blimpiar" runat="server" OnClick="Limpiar" Text="Nueva Consulta" Visible="false" class="btn btn-default" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                        <asp:GridView ID="GVinfprof" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVinfprof_RowDataBound">
                                            <AlternatingRowStyle BackColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No hay información del profesor! </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                                                <asp:BoundField DataField="USU_CORREO" HeaderText="Correo Electrónico" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Hoja de Vida">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LBhv" runat="server" Text="Descargar" OnClick="DescargarHV" CommandArgument='<%# Eval("USU_USERNAME") %>'></asp:LinkButton></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:Table ID="Tcarta" runat="server" Visible="false" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Label1" runat="server" Text="Carta Director:" ForeColor="Black" Font-Bold="True"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:FileUpload ID="FUdirector" runat="server" class="btn btn-default image-preview-input" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <asp:Table ID="Tbotones2" runat="server" Visible="false" HorizontalAlign="Center">
                                            <asp:TableRow HorizontalAlign="Center">
                                                <asp:TableCell>
                                                    <asp:Button ID="Bsolicitar" runat="server" Text="Solicitar" OnClick="SolicitarDir" class="btn btn-success" ForeColor="White" /></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bcancel" runat="server" OnClick="Limpiar" Text="Cancelar" class="btn btn-danger" ForeColor="White" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>

                                    <div id="ConsultaSolicitud" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                                        <asp:GridView ID="GVsolicitud" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVsolicitud_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVsolicitud_RowDataBound" PageSize="8">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No tiene solicitudes de director! </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="DIR_ID" HeaderText="Id Solicitud" />
                                                <asp:BoundField DataField="DIR_FECHA" HeaderText="Fecha" />
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                                                <asp:BoundField DataField="director" HeaderText="Director" />
                                                <asp:BoundField DataField="DIR_OBSERVACION" HeaderText="Obsevación" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Carta">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LBcarta" ClientIDMode="AutoID" runat="server" Text="Descargar" OnClick="DescargarCarta" CommandArgument='<%# Eval("DIR_ID") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    <asp:HiddenField ID="Metodo" runat="server" Value="" />
                                    <asp:HiddenField ID="Verificador" runat="server" Value="" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
          </ContentTemplate>
               <Triggers>
                   <asp:PostBackTrigger ControlID="Bguardar" />                  
                   <asp:PostBackTrigger ControlID="Bsolicitar" />  
             </Triggers>
           </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

