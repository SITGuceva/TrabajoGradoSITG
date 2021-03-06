﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Formatos.aspx.cs" Inherits="Formatos" %>

<asp:Content ID="FormatoCRUD" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-body" style="margin-left: auto; margin-right: auto; text-align: center;"> 
            <asp:Label ID="Ltitle" runat="server"  Text="DECANO" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#333333" ToolTip="La opción pertenece al rol decano." ></asp:Label>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Universidad - Formatos</div>
        <div class="panel-body">
           <asp:UpdatePanel runat="server" ID="UPformato"> <ContentTemplate>
               <div class="container-fluid">
                   <div class="row">
                       <div class="col-md-12">
                           <ul class="breadcrumb">
                               <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Subir</asp:LinkButton></li>
                               <li> <asp:LinkButton ID="LBconsultar" runat="server" OnClick="Consultar" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar</asp:LinkButton></li>
                           </ul>

                           <div class="panel panel-default">
                               <div class="panel-body">

                                   <div id="Ingreso" runat="server" visible="true" class="row">
                                       <asp:Table ID="Tformatos" runat="server" HorizontalAlign="Center">
                                           <asp:TableRow>
                                               <asp:TableCell>
                                                   <asp:Label ID="Lnom" runat="server" Text="Título:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                               <asp:TableCell>
                                                   <asp:TextBox ID="TBnom" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                           </asp:TableRow>
                                           <asp:TableRow>
                                               <asp:TableCell>
                                                   <asp:Label ID="Ldocumento" runat="server" Text="Documento:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                                               <asp:TableCell>
                                                   <asp:FileUpload ID="FUdocumento" runat="server" class="btn btn-default image-preview-input" /></asp:TableCell>
                                           </asp:TableRow>
                                       </asp:Table>

                                       <asp:Table ID="botones" runat="server" HorizontalAlign="Center">
                                           <asp:TableRow>
                                               <asp:TableCell>
                                                   <asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="Guardar" class="btn btn-success" ForeColor="White" /></asp:TableCell>
                                               <asp:TableCell>
                                                   <asp:Button ID="Bcancelar" runat="server" OnClick="Limpiar" Text="Cancelar" class="btn btn-danger" ForeColor="White" /></asp:TableCell>
                                           </asp:TableRow>
                                       </asp:Table>
                                   </div>

                                   <div id="ConsultaFormat" runat="server" visible="false" class="row" style="overflow-x: auto">
                                       <asp:GridView ID="GVformatos" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                                           OnPageIndexChanging="GVformatos_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                                           OnRowDataBound="GVformatos_RowDataBound" PageSize="8" OnRowUpdating="GVformatos_RowUpdating" OnRowEditing="GVformatos_RowEditing"
                                           OnRowCancelingEdit="GVformatos_RowCancelingEdit" OnRowDeleting="GVformatos_RowDeleting">
                                           <AlternatingRowStyle BackColor="White" />
                                           <EditRowStyle BackColor="#2461BF" />
                                           <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                           <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                           <RowStyle BackColor="white" />
                                           <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                           <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                           <EditRowStyle BackColor="#ffffcc" />
                                           <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                           <EmptyDataTemplate>¡No existen formatos!</EmptyDataTemplate>
                                           <Columns>
                                               <asp:BoundField DataField="FOR_ID" HeaderText="Id" />
                                               <asp:BoundField DataField="FOR_TITULO" HeaderText="Título" />
                                               <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Documento">
                                                   <ItemTemplate>
                                                       <asp:LinkButton ID="lnkDownload" ClientIDMode="AutoID" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("FOR_ID") %>'></asp:LinkButton>
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Modificar">
                                                   <ItemTemplate>
                                                       <asp:Button ID="btn_Edit" runat="server" Text="Modificar" CommandName="Edit" class="btn btn-default" />
                                                   </ItemTemplate>
                                                   <EditItemTemplate>
                                                       <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update" class="btn btn-success" ForeColor="White" />
                                                       <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel" class="btn btn-danger" ForeColor="White" />
                                                   </EditItemTemplate>
                                               </asp:TemplateField>
                                               <asp:CommandField ShowDeleteButton="true" HeaderText="Eliminar" ShowHeader="true"></asp:CommandField>
                                           </Columns>
                                       </asp:GridView>
                                   </div>

                                   <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                               </div>
                           </div>
                       </div>
                   </div>
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

