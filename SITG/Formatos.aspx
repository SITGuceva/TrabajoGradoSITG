<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Formatos.aspx.cs" Inherits="Formatos" %>

<asp:Content ID="FormatoCRUD" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Universidad - Formatos</div>
        <div class="panel-body">
           <asp:UpdatePanel runat="server" ID="UPformato"> <ContentTemplate>
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
                    <asp:Table ID="Tformatos" runat="server" HorizontalAlign="Center">                     
                         <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lnom" runat="server" Text="Tiulo:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBnom" runat="server"  CssClass="form-control"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                         <asp:TableRow>
                             <asp:TableCell><asp:Label ID="Ldocumento" runat="server" Text="Documento:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:FileUpload ID="FUdocumento" runat="server" class="btn btn-default image-preview-input" /></asp:TableCell>
                        </asp:TableRow>                                             
                    </asp:Table>
                   
                    <asp:Table ID="botones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button  ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-default" /></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-default" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
          
                <div id="ConsultaFormat" runat="server" visible="false" class="row">                           
                     <asp:GridView ID="GVformatos" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                         OnPageIndexChanging="GVformatos_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" 
                         OnRowDataBound="GVformatos_RowDataBound" PageSize="8" OnRowUpdating="GVformatos_RowUpdating" OnRowEditing="GVformatos_RowEditing"
                         OnRowCancelingEdit="GVformatos_RowCancelingEdit" caption="FORMATOS" captionalign="Top" OnRowDeleting="GVformatos_RowDeleting" >
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡No hay formatos!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="FOR_ID" HeaderText="Id" />   
                            <asp:BoundField DataField="FOR_TITULO" HeaderText="Titulo" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" ClientIDMode="AutoID" runat="server" Text="Download" OnClick="DownloadFile" CommandArgument='<%# Eval("FOR_ID") %>' ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate>
                                    <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" />
                                </ItemTemplate>
                                <EditItemTemplate>     
                                    <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" />
                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                        </Columns>
                     </asp:GridView>
                </div>
             
                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>                    
                </div>   
               </ContentTemplate>
               <Triggers>
                   <asp:PostBackTrigger ControlID="Bacpetar" />  
               </Triggers>
               </asp:UpdatePanel>
        </div>
    </div>
 
    <script>
        function pulsar(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 13) return false;
        }
    </script>

</asp:Content>

