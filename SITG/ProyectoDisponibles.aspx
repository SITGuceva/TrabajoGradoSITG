<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProyectoDisponibles.aspx.cs" Inherits="ProyectoDisponibles" %>

<asp:Content ID="ProyectoDisponible" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Proyectos - Proyectos Disponibles</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPproyectos" runat="server"> <ContentTemplate>
            <div class="container-fluid">
              
                <div id="Consultaproyectos" runat="server" visible="false" class="row" style="overflow-x: auto">                           
                     <asp:GridView ID="GVproyectos" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVproyectos_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"   OnRowDataBound="GVproyectos_RowDataBound" PageSize="8" caption="PROYECTOS DISPONIBLES" captionalign="Top">
                         <AlternatingRowStyle BackColor="White" />                        
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />  
                        <EmptyDataTemplate>¡No hay proyectos disponibles en el programa!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="PROY_ID" HeaderText="Id" />   
                            <asp:BoundField DataField="LINV_NOMBRE" HeaderText="Línea Investigación" />
                            <asp:BoundField DataField="TEM_NOMBRE" HeaderText="Tema" />
                            <asp:BoundField DataField="PROY_NOMBRE" HeaderText="Título" />
                            <asp:BoundField DataField="PROY_DESCRIPCION" HeaderText="Descripción" />
                            <asp:BoundField DataField="PROY_CANTEST" HeaderText="#Est" />
                            <asp:BoundField DataField="PROY_FECHA" HeaderText="Fecha" />
                            <asp:BoundField DataField="CREADOR" HeaderText="Profesor" />
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

