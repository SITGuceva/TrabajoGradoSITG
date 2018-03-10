<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Consulta_Propuestas.aspx.cs" Inherits="Consulta_Propuestas" %>

<asp:Content ID="ConsultaPropuesta" ContentPlaceHolderID="MainContent" runat="Server" >
    <div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Consultar Propuestas</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPconsulta_propuestas" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">

                        <div id="Ingreso" runat="server" visible="false" class="row">
                            <asp:Table ID="Tconsulta" runat="server" HorizontalAlign="center">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label1" runat="server" Text="programa:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                        <asp:DropDownList ID="DDLconsultaPrograma" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" OnSelectedIndexChanged="DDLconsultaPrograma_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                  

                                   <asp:TableCell>
                                        <asp:Label ID="Label2" runat="server" Text="linea de profundización:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                        <asp:DropDownList ID="DDLconsultaLinea" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLconsultaLinea_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                  
                            
                                     <asp:TableCell>
                                         <asp:Label ID="Label3" runat="server" Text="tema:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label>
                                        <asp:DropDownList ID="DDLconsultaTema" class="btn btn-secondary btn-lg dropdown-toggle" runat="server">
                                        </asp:DropDownList>
                                    </asp:TableCell>

                                    
                                     <asp:TableCell>
                                          <asp:Button ID="Btbuscar" OnClick="buscar" runat="server" Text="Buscar"  CssClass="btn btn-default"></asp:Button>
                                            </asp:TableCell>
                                  
                                  
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <br>
                        <div id="TResultado" runat="server" visible="false" class="row">
                            <asp:GridView ID="GVresulprop" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVresulprop_RowDataBound" PageSize="8">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>No se encuentra propuestas con los datos ingresados</EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="PROP_CODIGO" HeaderText="Código" />
                                    <asp:BoundField DataField="PROP_TITULO" HeaderText="Título" />
                                    <asp:BoundField DataField="PROP_FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="PROP_ESTADO" HeaderText="Estado de la propuesta" />
                                    <asp:BoundField DataField="DIRECTOR" HeaderText="Director" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado director" />
                                    
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




