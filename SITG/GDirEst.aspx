<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GDirEst.aspx.cs" Inherits="GDirEst" %>

<asp:Content ID="GDirEst" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Solicitar director</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPsolicitardir" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                       <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBSolicitar" runat="server" OnClick="Solicitar" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Solicitar Director</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBConsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-pencil"></span>Consultar Solicitudes</asp:LinkButton></li>
                        </ul> 
                    </div>
                </div>

                <div id="Ingreso" runat="server" visible="true" class="row">
                  <asp:Table ID="TSolicitar" runat="server" HorizontalAlign="Center" >                
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lprof" runat="server" Text="Profesores: " ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                 
                            <asp:TableCell><asp:DropDownList ID="DDLlista" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" style="width:700px;"></asp:DropDownList></asp:TableCell>                                    
                            <asp:TableCell><asp:Button ID="Bconsulta" runat="server" Text="Consultar" OnClick="InfProfesor" class="btn btn-default" /></asp:TableCell>
                            
                        </asp:TableRow>                           
                    </asp:Table>  

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
                            <asp:BoundField DataField="USU_TELEFONO" HeaderText="Telefono" />
                            <asp:BoundField DataField="USU_DIRECCION" HeaderText="Direccion" />
                            <asp:BoundField DataField="USU_CORREO" HeaderText="Correo" />
                        </Columns>
                    </asp:GridView>
                    

                     <asp:Table ID="Tbotones" runat="server" Visible="false" HorizontalAlign="Center" >                
                        <asp:TableRow>
                            <asp:TableCell> <asp:Button ID="Bsolicitar" runat="server"  Text="Solicitar " OnClick="Aceptar" class="btn btn-default" /></asp:TableCell>
                             <asp:TableCell><asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Cancelar" class="btn btn-default" /></asp:TableCell>
                        </asp:TableRow>                           
                    </asp:Table>  

                </div>


                <div id="resultado" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                    <asp:GridView ID="GVsolicitud" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVsolicitud_PageIndexChanging"  AutoGenerateColumns="False"  CssClass="table table-bordered bs-table" OnRowDataBound="GVsolicitud_RowDataBound" PageSize="8">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate> ¡No tiene solicitudes de director! </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="SOL_ID" HeaderText="Id Solicitud" />
                            <asp:BoundField DataField="SOL_FECHA" HeaderText="Fecha de solicitud" />
                            <asp:BoundField DataField="SOL_ESTADO" HeaderText="Estado" />
                            <asp:BoundField DataField="director" HeaderText="Director" />
                        </Columns>
                    </asp:GridView>
                </div>

                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label> 
                </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>