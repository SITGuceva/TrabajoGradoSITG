<%@ Page Title="SITG" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <%--  --%>

    <div class="row">
        <div class="col-md-12">
            <section id="loginForm">
                <div class="form-horizontal">

                    <br />

                    <div class="form-group">
                        <img src="Images/logo-sitg.png" class="img-responsive" style="position: relative; left: 30%;"/>
                    </div>

                    <div id="Login" runat="server" visible="true" class="row" >
                        <asp:Table ID="TableLogin" runat="server" style="position: relative; left: 30%;" >
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label" ForeColor="Black" Font-Bold="true" Text="Código"> </asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="5">
                                    <asp:TextBox runat="server" ID="UserName" TextMode="Number" CssClass="form-control" Width="170px" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" CssClass="text-danger" ErrorMessage="El codigo es obligatorio." ForeColor="#CC0000" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label" ForeColor="Black" Font-Bold="true" Text="Contraseña:"> </asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="5">
                                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" Width="171px" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="La contraseña es obligatoria." ForeColor="#CC0000" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                     <asp:Label ID="Lerror" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Center" ColumnSpan="5">
                                    <asp:Button runat="server" OnClick="LogIn" Text="INGRESAR" Font-Bold="true" CssClass="btn btn-success" ForeColor="White" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                </div>
            </section>
        </div>
    </div>

</asp:Content>
