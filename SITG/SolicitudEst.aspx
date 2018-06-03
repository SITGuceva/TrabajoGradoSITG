<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SolicitudEst.aspx.cs" Inherits="GDirEst" %>

<asp:Content ID="SolicitudEst" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Documentos - Solicitudes</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPSolicitudes" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                       <ul class="breadcrumb">
                            <li> <asp:LinkButton ID="LBSolicitar" runat="server" OnClick="LBSolicitar_Click" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Solicitar</asp:LinkButton></li>                         
                            <li><asp:LinkButton ID="LBConsultar" runat="server" OnClick="LBConsultar_Click" ForeColor="Black"><span class="glyphicon glyphicon-pencil"></span>Consultar</asp:LinkButton></li>
                        </ul> 
                    </div>
                </div>
                
                <div id="TipoSolicitud" runat="server" visible="true" class="row">
                  <asp:Table ID="TSolicitar" runat="server" HorizontalAlign="Center" >                
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lsol" runat="server" Text="Tipo: " ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                 
                            <asp:TableCell><asp:DropDownList ID="DDLsol" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" style="width:700px;" AutoPostBack="true" OnSelectedIndexChanged="DDLsol_SelectedIndexChanged">
                                   <asp:ListItem Value="0" Text="Seleccione"/>
                                   <asp:ListItem Value="1" Text="Cambio Propuesta" />
                                   <asp:ListItem Value="2" Text="Abandonar Propuesta" />
                                   <asp:ListItem Value="3" Text="Ingresar Integrante" />
                            </asp:DropDownList></asp:TableCell>                                    
                        </asp:TableRow> 
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Ldescrip" runat="server" Text="Motivo: " ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                 
                            <asp:TableCell><textarea ID="TAdescrip" runat="server" CssClass="form-control"></textarea></asp:TableCell>                                    
                        </asp:TableRow>
                    </asp:Table>                   
                </div>

                 <div id="Integrante" runat="server" visible="false" class="row">
                     <asp:Table ID="Tinteg" runat="server" HorizontalAlign="Center">
                           <asp:TableRow>
                             <asp:TableHeaderCell><asp:Label ID="Ltin" runat="server" Text="INTEGRANTE" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableHeaderCell>
                           </asp:TableRow>
                            <asp:TableRow>                        
                                <asp:TableCell><asp:Label ID="Lintegrante" runat="server" Text="Código del Estudiante:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                <asp:TableCell><asp:TextBox ID="TBcodint" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="Bintegrante" runat="server" Text="Buscar" OnClick="Bintegrante_Click" class="btn btn-default"/>
                                    <asp:Button ID="Bnueva" runat="server" Text="Nueva Consulta" Visible="false"   OnClick="Bintegrante_Click" class="btn btn-default"/>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow ID="RespInte" runat="server" Visible="false">
                                <asp:TableCell> <asp:Label ID="Nombre" runat="server" Text="Nombre del Estudiante:" ForeColor="Black" Font-Bold="True"></asp:Label> </asp:TableCell>
                                <asp:TableCell><asp:Label ID="Rnombre" runat="server" Text="x" ForeColor="Black" CssClass="form-control"></asp:Label> </asp:TableCell>
                                <asp:TableCell><asp:Button ID="AgregarInt" runat="server" Text="Agregar" OnClick="AgregarInt_Click" class="btn btn-default" /></asp:TableCell>
                            </asp:TableRow>
                    </asp:Table>

                    <asp:GridView  ID="GVagreinte" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"  PageSize="6">
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
                 </div>

                <div>
                    <asp:Table ID="Tbotones" runat="server" Visible="true" HorizontalAlign="Center" >                
                        <asp:TableRow>
                           <asp:TableCell> <asp:Button ID="Bsolicitar" runat="server"  Text="Solicitar " OnClick="Bsolicitar_Click" class="btn btn-success" ForeColor="White" /></asp:TableCell>
                           <asp:TableCell><asp:Button ID="Blimpiar" runat="server" OnClick="Blimpiar_Click" Text="Cancelar" class="btn btn-danger" ForeColor="White" /></asp:TableCell>
                        </asp:TableRow>                           
                   </asp:Table>  
                </div>

                <div id="ConsultaSol" runat="server" visible="false" class="row">                  
                    <asp:GridView ID="GVconsulta" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                       AutoGenerateColumns="False" CssClass="table table-bordered bs-table"  OnRowDataBound="GVconsulta_RowDataBound">                   
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate> ¡No tienes solicitudes!  </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="SOLE_ID" HeaderText="Id" />
                            <asp:BoundField DataField="SOLE_FECHA" HeaderText="Fecha" />
                            <asp:BoundField DataField="SOLE_TIPO" HeaderText="Tipo" />
                            <asp:BoundField DataField="SOLE_MOTIVO" HeaderText="Motivo" />
                            <asp:BoundField DataField="SOLE_ESTADO" HeaderText="Estado" /> 
                        </Columns>
                    </asp:GridView>
                </div>

                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label> 
                <asp:HiddenField ID="Validar" runat="server"  Value=""/>
                </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>