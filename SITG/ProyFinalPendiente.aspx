﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProyFinalPendiente.aspx.cs" Inherits="ProyFinalPendiente" %>

<asp:Content ID="ProyFinalPendiente" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-body" style="margin-left: auto; margin-right: auto; text-align: center;"> 
            <asp:Label ID="Ltitle" runat="server"  Text="DECANO" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#333333" ToolTip="La opción pertenece al rol decano." ></asp:Label>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Decanatura - Proyectos Finales Pendientes</div>
        <div class="panel-body">
            <asp:UpdatePanel runat="server" ID="UPasignarjur"> <ContentTemplate>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <ul class="breadcrumb">
                                <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="asignarproy" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Asignar</asp:LinkButton></li>
                                <li> <asp:LinkButton ID="LBconsultar" runat="server" OnClick="consultarproy" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                            </ul>

                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <div id="Consulta" runat="server" visible="true" class="row">
                                        <asp:GridView ID="GVconsulta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                            AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowCommand="GVconsulta_RowCommand">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No hay proyectos finales pendientes por asignar jurados!</EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="PPRO_CODIGO" HeaderText="Código" />
                                                <asp:BoundField DataField="PF_TITULO" HeaderText="Título" />
                                                <asp:BoundField DataField="PF_FECHA" HeaderText="Fecha" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DescargaPF" CommandArgument='<%# Eval("PPRO_CODIGO") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Jurados">
                                                    <ItemTemplate>
                                                        <asp:Button ID="BTrevisar" runat="server" Text="ASIGNAR" class="btn btn-default" AutoPostBack="true" CommandName="Asignar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                       
                                    </div>

                                    <div id="Jurado1" runat="server" style="overflow-x: auto" visible="false" class="row">
                                        <asp:Label ID="Ljurado1" runat="server" Text="---JURADO 1: EVALUADOR ANTEPROYECTO---" ForeColor="Black"></asp:Label>
                                        <asp:GridView ID="GVjurado1" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVjurado1_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVjurado1_RowDataBound" OnRowDeleting="GVjurado1_RowDeleting" PageSize="8">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡El proyecto final no tiene jurado del anteproyecto! </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="JUR_ID" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="USU_USERNAME" HeaderText="Cédula" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="USU_CORREO" HeaderText="Correo Electrónico" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="JUR_FECHA" HeaderText="Fecha Asignación" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="JUR_FRPTA" HeaderText="Fecha Respuesta" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Hoja de Vida">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DescargaHV" CommandArgument='<%# Eval("USU_USERNAME") %>'></asp:LinkButton></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PPRO_CODIGO" HeaderText="Proyecto Final" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div id="Jurado2" runat="server" style="overflow-x: auto" visible="false" class="row">
                                        <asp:Label ID="Ljurado2" runat="server" Text="---JURADO 2---" ForeColor="Black"></asp:Label>
                                        <asp:GridView ID="GVjurado2" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVjurado2_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVjurado2_RowDataBound" OnRowDeleting="GVjurado2_RowDeleting" PageSize="8">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No se ha asignado el jurado 2! </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="JUR_ID" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="USU_USERNAME" HeaderText="Cédula" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="USU_CORREO" HeaderText="Correo Electrónico" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="JUR_FECHA" HeaderText="Fecha Asignación" HeaderStyle-HorizontalAlign="Center" />
                                               <asp:BoundField DataField="JUR_FRPTA" HeaderText="Fecha Respuesta" HeaderStyle-HorizontalAlign="Center" />
                                               <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Hoja de Vida">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DescargaHV" CommandArgument='<%# Eval("USU_USERNAME") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PPRO_CODIGO" HeaderText="Proyecto Final" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div id="Jurado3" runat="server" style="overflow-x: auto" visible="false" class="row">
                                        <asp:Label ID="Ljurado3" runat="server" Text="---JURADO 3---" ForeColor="Black"></asp:Label>
                                        <asp:GridView ID="GVjurado3" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVjurado3_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVjurado3_RowDataBound" OnRowDeleting="GVjurado3_RowDeleting" PageSize="8">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No se ha asignado el jurado 3! </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="JUR_ID" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="USU_USERNAME" HeaderText="Cédula" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="USU_CORREO" HeaderText="Correo Electrónico" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="JUR_FECHA" HeaderText="Fecha Asignación" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="JUR_FRPTA" HeaderText="Fecha Respuesta" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Hoja de Vida">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DescargaHV" CommandArgument='<%# Eval("USU_USERNAME") %>'></asp:LinkButton></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PPRO_CODIGO" HeaderText="Proyecto Final" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>


                                    <div id="Mostrarprof" runat="server" visible="false" style="text-align: center;">
                                        <asp:Table ID="Tprofesor" runat="server" HorizontalAlign="center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:DropDownList ID="DDLprofesores" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="BTconsultar" Enabled="true" OnClick="consultarprofesor" runat="server" Text="Consultar" class="btn btn-default"></asp:Button></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <br>
                                    </div>

                                    <div id="Mostrarprog" runat="server" visible="false" style="text-align: center;">
                                        <asp:Table ID="Tprograma" runat="server" HorizontalAlign="center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:DropDownList ID="DDLprogramas" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLprogramas_SelectedIndexChanged"></asp:DropDownList></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="BTconsultarp" Enabled="true" OnClick="consultarprograma" runat="server" Text="Consultar" class="btn btn-default"></asp:Button></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <br>
                                    </div>

                                    <div id="infoprofesor" runat="server" visible="false" class="row" style="overflow-x: auto">
                                        <asp:GridView ID="GVinfprof" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVinfprof_RowDataBound">
                                            <AlternatingRowStyle BackColor="White" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No hay información del profesor! </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="USU_USERNAME" HeaderText="Código" />
                                                <asp:BoundField DataField="USU_TELEFONO" HeaderText="Teléfono" />
                                                <asp:BoundField DataField="USU_DIRECCION" HeaderText="Dirección" />
                                                <asp:BoundField DataField="USU_CORREO" HeaderText="Correo Electrónico" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Hoja de Vida">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DescargaHV" CommandArgument='<%# Eval("USU_USERNAME") %>'></asp:LinkButton></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Table ID="TBbotonesProf" runat="server" HorizontalAlign="center">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Button ID="BTasignar" Enabled="true" OnClick="asignarjurado" runat="server" Text="Asignar" class="btn btn-success" ForeColor="White"></asp:Button></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="BTcancelar" Enabled="true" OnClick="cancelarasignar" runat="server" Text="Cancelar" class="btn btn-danger" ForeColor="White"></asp:Button></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>



                                    <div id="trabajosdegrado" runat="server" visible="false" class="row" style="overflow-x: auto">
                                        <asp:GridView ID="GVtg" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVtg_RowDataBound" OnRowCommand="GVtg_RowCommand">
                                            <AlternatingRowStyle BackColor="White" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No hay trabajos de grado para esta consulta! </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="PPRO_CODIGO" HeaderText="Código" />
                                                <asp:BoundField DataField="PF_TITULO" HeaderText="Título" />
                                                <asp:BoundField DataField="PF_FECHA" HeaderText="Fecha" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DescargaPF" CommandArgument='<%# Eval("PPRO_CODIGO") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Jurados">
                                                    <ItemTemplate>
                                                        <asp:Button ID="BTjurados" runat="server" Text="VER JURADOS" class="btn btn-default" AutoPostBack="true" CommandName="Ver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="Metodo2" runat="server" Value="" />
                                        
                                    </div>

                                    <div id="Mostrarjurados" runat="server" visible="false" class="row" style="overflow-x: auto">
                                        <asp:GridView ID="GVmjurados" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVmjurados_RowDataBound">
                                            <AlternatingRowStyle BackColor="White" />
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="white" />
                                            <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <EmptyDataTemplate>¡No hay trabajos de grado para esta consulta! </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="JURADOS" HeaderText="Jurados" />
                                                 <asp:BoundField DataField="JUR_FECHA" HeaderText="Fecha Asignación" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="JUR_FRPTA" HeaderText="Fecha Respuesta" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="JUR_FENVIO" HeaderText="Fecha Envío" HeaderStyle-HorizontalAlign="Center" />
                                                
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <asp:ImageButton ID="IBregresar" OnClick="regresar" runat="server" Visible="false" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline"></asp:ImageButton>
                                    <asp:ImageButton ID="IBregresarCon" OnClick="regresarCon" runat="server" Visible="false" ImageUrl="/Images/flecha.png" ToolTip="Regresar" ImageAlign="Baseline"></asp:ImageButton>
                                    <br />
                                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>

                                     <asp:HiddenField ID="Metodo" runat="server" Value="" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
