<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GDirEst.aspx.cs" Inherits="GDirEst" %>

<asp:Content ID="GDirEst" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Solicitar director</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UProl" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                       <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBSolicitar" runat="server" OnClick="Solicitar" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Solicitar Director</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBConsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-pencil"></span>Consultar Solicitudes</asp:LinkButton></li>
                        </ul> 
                    </div>
                </div>

                  
                <div id="Solicitar2" runat="server" visible="false" class="row">

                  <asp:Table ID="TSolicitar2" runat="server" HorizontalAlign="Center" >                
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Llista" runat="server" Text="Lista de profesores" ForeColor="Black" Font-Bold="True" ></asp:Label></asp:TableCell>                 
                            <asp:TableCell><asp:DropDownList ID="DDLlista" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" style="width:700px;"></asp:DropDownList></asp:TableCell>                           
                             <asp:TableCell>
                                <asp:Button ID="BSolicitar" runat="server" Text="Solicitar director" OnClick="Aceptar" class="btn btn-secondary" />
                            </asp:TableCell>
                            
                        </asp:TableRow>       
                      

 
     
                    </asp:Table>  
                </div>
             
            
                       <div id="resultado" runat="server" style="width: 100%; height: 100%;" visible="false" class="row">
                            <asp:GridView ID="gvSysDatosConsulta" runat="server" AllowPaging="True" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanging="gvSysDatosConsulta_PageIndexChanging"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered bs-table"
                                OnRowDataBound="gvSysDatosConsulta_RowDataBound" PageSize="2">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="gray" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="white" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>
                                </EmptyDataTemplate>
                                <Columns>
                                     <asp:BoundField DataField="SOL_ID" HeaderText="Id Solicitud" />
                                     <asp:BoundField DataField="SOL_FECHA" HeaderText="Fecha de solicitud" />
                                     <asp:BoundField DataField="SOL_ESTADO" HeaderText="Estado" />
                                     <asp:BoundField DataField="director" HeaderText="Director" />
                 
                                  





                                </Columns>
                            </asp:GridView>
                        </div>

                           
                <div id="consulta2" runat="server" visible="false" class="row">

                 
                </div>



                 <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label> 
                </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>