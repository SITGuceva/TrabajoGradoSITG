<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Evaluar_Criterio.aspx.cs" Inherits="Evaluar_Criterio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Universidad - Asignar Comite</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPasigrol" runat="server"><ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="breadcrumb">
                                    <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>
                                    <li><asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                                </ul>
                            </div>
                        </div>

                        <div id="Ingreso" runat="server" visible="true" class="row">
                            <asp:Table ID="Tevaluarcrit" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lreunion" runat="server" Text="Reunion:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell> <asp:DropDownList ID="DDLreunion" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                                     <asp:TableCell><asp:Label ID="Lpropuesta" runat="server" Text="Propuesta:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell> <asp:DropDownList ID="DDLpropuesta" class="btn btn-secondary btn-lg dropdown-toggle" runat="server"></asp:DropDownList></asp:TableCell>
                                    <asp:TableCell><asp:Button ID="Brevisar" runat="server" OnClick="Revisar" Text="Revisar" class="btn btn-default" /></asp:TableCell>
                                 </asp:TableRow>
                            </asp:Table>



                            <asp:GridView ID="GVevaluarcrit" runat="server" Visible="false" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="GVevaluarcrit_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                                OnRowDataBound="GVevaluarcrit_RowDataBound" PageSize="8" >
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>¡No hay criterios disponibles!</EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="CRIT_CODIGO" HeaderText="ID" />
                                    <asp:BoundField DataField="CRIT_NOMBRE" HeaderText="NOMBRE" />
                                    <asp:BoundField DataField="CRIT_PORCENTAJE" HeaderText="VALOR" />
                                    <asp:TemplateField HeaderText="CUMPLE">
                                        <ItemTemplate>
                                           <asp:CheckBox ID="CBcumplio" runat="server" Text="" OnCheckedChanged="CBcumplio_CheckedChanged"  AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        

                             <asp:Table ID="Tevaluarcrit2" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lrecomendacion" runat="server" Text="Recomendacion:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell> <asp:TextBox ID="TBrecomendacion" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>                                
                                </asp:TableRow>
                            </asp:Table>


                        <asp:Table ID="Tbotones" runat="server" HorizontalAlign="Center">
                            <asp:TableRow>
                                <asp:TableCell><asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-default" /></asp:TableCell>
                                <asp:TableCell><asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-default" /></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                    </div>

                    <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                    <asp:HiddenField ID="Metodo" runat="server" Value="" />
                   
            </div>
            </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

