﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DocenteProyectos.aspx.cs" Inherits="DocenteProyectos" %>

<asp:Content ID="DocenteProyectos" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-body" style="margin-left: auto; margin-right: auto; text-align: center;"> 
            <asp:Label ID="Ltitle" runat="server"  Text="DOCENTE" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#333333" ToolTip="La opción pertenece al rol docente." ></asp:Label>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Proyectos - Proyectos</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPproyectosdoc" runat="server"> <ContentTemplate>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">

                            <ul class="breadcrumb">
                                <li> <asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>
                                <li><asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                            </ul>

                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <div id="Ingreso" runat="server" visible="true" class="row">
                                        <asp:Table ID="Tproyectos" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lprog" runat="server" Text="Programa:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList ID="DDLprograma" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" OnSelectedIndexChanged="DDLconsultaPrograma_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Llprof" runat="server" Text="Línea Investigación:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList ID="DDLlprof" runat="server" class="btn btn-secondary btn-lg dropdown-toggle" AutoPostBack="true" OnSelectedIndexChanged="DDLlprof_SelectedIndexChanged"></asp:DropDownList></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Ltema" runat="server" Text="Tema:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList ID="DDLtema" runat="server" class="btn btn-secondary btn-lg dropdown-toggle"></asp:DropDownList></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lnom" runat="server" Text="Título:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">
                                                    <asp:TextBox ID="TBnombre" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Ldescrip" runat="server" Text="Descripción:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">
                                                    <textarea id="TBdescripcion" runat="server" cssclass="form-control" row="3"></textarea></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lcantest" runat="server" Text="Cantidad Estudiantes:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="TBcant" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                        <asp:Table ID="botones" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-success" ForeColor="White" />
                                                    <asp:Button ID="btnDummy" runat="server" Text="btnDummy" OnClick="btnDummy_Click" Style="display: none" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Cancelar" class="btn btn-danger" ForeColor="White" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>

                                    <div id="Consultaproyectos" runat="server" visible="false" class="row" style="overflow-x: auto">
                                        <asp:GridView ID="GVproyectos" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVproyectos_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVproyectos_RowDataBound" PageSize="8" OnRowUpdating="GVproyectos_RowUpdating" OnRowEditing="GVproyectos_RowEditing" OnRowCancelingEdit="GVproyectos_RowCancelingEdit" Caption="PROYECTOS" CaptionAlign="Top">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No tiene proyectos creados!</EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="PROY_ID" HeaderText="Id" />
                                                <asp:BoundField DataField="LINV_NOMBRE" HeaderText="Línea Investigación" />
                                                <asp:BoundField DataField="TEM_NOMBRE" HeaderText="Tema" />
                                                <asp:BoundField DataField="PROY_NOMBRE" HeaderText="Título" />
                                                <asp:BoundField DataField="PROY_DESCRIPCION" HeaderText="Descripción" />
                                                <asp:BoundField DataField="PROY_CANTEST" HeaderText="Cantidad Estudiantes" />
                                                <asp:BoundField DataField="PROY_FECHA" HeaderText="Fecha Creación" />
                                                <asp:TemplateField HeaderText="Estado">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="estado" runat="server">
                                                            <asp:ListItem Value="DISPONIBLE">DISPONIBLE</asp:ListItem>
                                                            <asp:ListItem Value="NO DISPONIBLE">NO DISPONIBLE</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lestado" runat="server" Text='<%# Bind("ESTADO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modificar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btn_Edit" runat="server" Text="Modificar" class="btn btn-default" CommandName="Edit" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btn_Update" runat="server" Text="Actualizar" class="btn btn-success" ForeColor="White" CommandName="Update" />
                                                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" class="btn btn-danger" ForeColor="White" CommandName="Cancel" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
 
    <script>
        function pulsar(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 13) return false;
        }
    </script>
    <script type="text/javascript">
        function myconfirmbox() {
            if (confirm("¿Está seguro guardar el proyecto?")) {
                //trigger the button click
                __doPostBack('<%= btnDummy.UniqueID %>', "");
                return true;
            } else {
                return false;
            }
        }
     </script>
</asp:Content>

