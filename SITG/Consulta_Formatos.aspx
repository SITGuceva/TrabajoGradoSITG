<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Consulta_Formatos.aspx.cs" Inherits="Consulta_Formatos" %>

<asp:Content ID="ConsultaFormato" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Documentos - Consultar Formatos</div>
        <div class="panel-body">       
            <asp:UpdatePanel runat="server" ID="UPformatos"><ContentTemplate>
            <div class="container-fluid">

               <div id="ConsultaFormat" runat="server" visible="true" class="row">                           
                     <asp:GridView ID="GVformatos" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVformatos_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" 
                         OnRowDataBound="GVformatos_RowDataBound" PageSize="8"  caption="FORMATOS" captionalign="Top" >
                        <AlternatingRowStyle BackColor="White" />                       
                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="white" />
                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" /> 
                        <EmptyDataTemplate>¡No hay formatos!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="FOR_ID" HeaderText="Id" />   
                            <asp:BoundField DataField="FOR_TITULO" HeaderText="Título" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("FOR_ID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                     </asp:GridView>
                </div>
             
                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>                    
                </div>  
                </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

