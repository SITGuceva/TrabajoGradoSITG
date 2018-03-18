<%@ Page Title="SITG" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MenuPrincipal.aspx.cs" Inherits="MenuPrincipal" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="MenuPrincipal" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="Linfo" runat="server" Text="Label"></asp:Label>
    <asp:FileUpload ID="FUdocumento" runat="server" class="btn btn-default image-preview-input" />
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
</asp:Content>

