<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reunion.aspx.cs" Inherits="Calendario" %>

<asp:Content ID="CReunion" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="panel panel-default">
        <div class="panel-heading">Reunion - Gestionar Reunion</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPreu" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBCrear" runat="server" OnClick="Crear" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Crear</asp:LinkButton></li>
               
                        </ul>
                    </div>
                </div>

                <div id="Botones" runat="server" class="row" visible="true">
                    <asp:Table ID="Tbotones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="Bacpetar" runat="server" OnClick="Aceptar" Text="CREAR" class="btn btn-secondary" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True" ></asp:Label>
            </div>
           </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

