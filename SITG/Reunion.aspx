﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reunion.aspx.cs" Inherits="Reunion" %>

<asp:Content ID="CReunion" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-body" style="margin-left: auto; margin-right: auto; text-align: center;"> 
            <asp:Label ID="Ltitle" runat="server"  Text="COMITÉ" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#333333" ToolTip="La opción pertenece al rol comité." ></asp:Label>
        </div>
    </div>
     <div class="panel panel-default">
        <div class="panel-heading"style="background-color:#1C2833 ;color:white">Gestionar Reunión - Reunión</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPreu" runat="server"> <ContentTemplate>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <ul class="breadcrumb">
                                <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>
                                <li><asp:LinkButton ID="LBConsultar" runat="server" OnClick="LBConsultar_Click" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                            </ul>

                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <div id="Ingreso" runat="server" visible="true" class="row">
                                        <asp:Table ID="Treu" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lcomi" runat="server" Text="Comité:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="Rcomite" runat="server" CssClass="form-control"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lfecha" runat="server" Text="Fecha:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="TBfecha" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:ImageButton ID="Ifecha" runat="server" Height="23px" ImageUrl="~/Images/Icalendar.png" Width="36px" OnClick="Ifecha_Click" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Center" ColumnSpan="3">
                                                    <asp:Calendar ID="Cfecha" runat="server" Style="margin-top: 0px" Visible="false" OnSelectionChanged="Cfecha_SelectionChanged"></asp:Calendar>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <asp:Table ID="Tbotones" runat="server" HorizontalAlign="Center">
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

                                    <div id="Consulta" runat="server" visible="false" class="row" style="overflow-x: auto">
                                        <asp:Table ID="Tbuscar" runat="server" HorizontalAlign="Center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Lmes" runat="server" Text="Mes: " ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList ID="DDLmes" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" Style="width: 700px;" OnSelectedIndexChanged="DDLmes_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0" Text="Seleccione" />
                                                        <asp:ListItem Value="01" Text="Enero" />
                                                        <asp:ListItem Value="02" Text="Febrero" />
                                                        <asp:ListItem Value="03" Text="Marzo" />
                                                        <asp:ListItem Value="04" Text="Abril" />
                                                        <asp:ListItem Value="05" Text="Mayo" />
                                                        <asp:ListItem Value="06" Text="Junio" />
                                                        <asp:ListItem Value="07" Text="Julio" />
                                                        <asp:ListItem Value="08" Text="Agosto" />
                                                        <asp:ListItem Value="09" Text="Septiembre" />
                                                        <asp:ListItem Value="10" Text="Octubre" />
                                                        <asp:ListItem Value="11" Text="Noviembre" />
                                                        <asp:ListItem Value="12" Text="Diciembre" />
                                                    </asp:DropDownList>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Bbuscar" runat="server" Text="BUSCAR" OnClick="Bbuscar_Click" class="btn btn-default" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                        <asp:GridView ID="GVconsulta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVconsulta_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                                            OnRowDataBound="GVconsulta_RowDataBound" PageSize="8" OnRowCommand="GVconsulta_RowCommand">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No hay reuniones estipuladas con el parámetro seleccionado.!</EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="REU_CODIGO" HeaderText="Código" />
                                                <asp:BoundField DataField="REU_FPROP" HeaderText="Fecha Propuesta" />
                                                <asp:BoundField DataField="REU_ESTADO" HeaderText="Estado" />
                                                <asp:TemplateField HeaderText="Iniciar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="Biniciar" runat="server" Text="INICIAR" CssClass="btn btn-default" CommandName="iniciar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    <asp:HiddenField ID="Metodo" Value="" runat="server" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        function myconfirmbox() {
            if (confirm("¿Está seguro de crear la reunión?")) {
                //trigger the button click
                __doPostBack('<%= btnDummy.UniqueID %>', "");
                return true;
            } else {
                return false;
            }
        }
     </script>
</asp:Content>

