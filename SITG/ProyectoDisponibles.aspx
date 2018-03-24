<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProyectoDisponibles.aspx.cs" Inherits="ProyectoDisponibles" %>

<asp:Content ID="ProyectoDisponible" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="panel panel-default">
        <div class="panel-heading">Proyectos Disponibles - Proyectos</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPproyectos" runat="server"> <ContentTemplate>
            <div class="container-fluid">
              
                <div id="Consultaproyectos" runat="server" visible="false" class="row" style="overflow-x: auto">                           
                     <asp:GridView ID="GVproyectos" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVproyectos_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"   OnRowDataBound="GVproyectos_RowDataBound" PageSize="8" caption="PROYECTOS DISPONIBLES" captionalign="Top">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡No hay proyectos disponibles en tu programa!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="PROY_ID" HeaderText="ID" />   
                            <asp:BoundField DataField="LINV_NOMBRE" HeaderText="LINEA INVESTIGACION" />
                            <asp:BoundField DataField="TEM_NOMBRE" HeaderText="TEMA" />
                            <asp:BoundField DataField="PROY_NOMBRE" HeaderText="NOMBRE" />
                            <asp:BoundField DataField="PROY_DESCRIPCION" HeaderText="DESCRIPCION" />
                            <asp:BoundField DataField="PROY_CANTEST" HeaderText="#EST" />
                            <asp:BoundField DataField="PROY_FECHA" HeaderText="FECHA" />
                            <asp:BoundField DataField="CREADOR" HeaderText="PROFESOR" />
                        </Columns>
                     </asp:GridView>
                </div>

             
                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>                    
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
</asp:Content>

