﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DatoPersonal.aspx.cs" Inherits="DatoPersonal" %>

<asp:Content ID="DatoPersonal" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Configuracion - Actualizar Datos Personales</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPdatopersonal" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">  
                        <asp:Label ID="Ltitulo" runat="server" ForeColor="Black" Font-Bold="True" class="text-justify" Text="Actualizar Datos Personales"></asp:Label>                    
                       
                         <div id="Actualizar" runat="server" visible="true" class="row">
                            <asp:Table ID="Tdatopersonal" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lcodigo" runat="server" Text="Codigo:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="Lanscod" runat="server" Text="" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>                                   
                                </asp:TableRow>                               
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lnombre" runat="server" Text="Nombre:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label> </asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBnombre" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lapellido" runat="server" Text="Apellido:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBapellido" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>                                    
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Ltelefono" runat="server" Text="Telefono:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBtelefono" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Ldireccion" runat="server" Text="Dirección:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBdireccion" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>                                  
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="Lcorreo" runat="server" Text="Correo:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="TBcorreo" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox></asp:TableCell>                                   
                                </asp:TableRow>
                            </asp:Table>
                       
                            <asp:Table ID="TBotones" runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Button ID="LBacpetar" runat="server" OnClick="Aceptar" Text="Guardar Cambios" class="btn btn-default" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="LBcancelar" runat="server" OnClick="Limpiar" Text="Limpiar" class="btn btn-default" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>

                        <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

