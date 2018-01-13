<%@ Page Title="SITG" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--  --%>
    <div class="row">
        <div class="col-md-12">
            <section id="loginForm">
                <div class="form-horizontal">
                    
                    <font size=4 color="White" face="Arial, Bold italic"/> <%--ARREGLAR--%>
                    <br/>
                    <hr/>

                   <div class="form-group"> 
                       <img src="Images/Lprincipal.png" class="img-responsive">
                       <br />
                   </div>

                    <div id="Login" runat="server" visible="true" class="row">
                        <asp:Table ID="TableLogin" runat="server" HorizontalAlign="Left">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">Codigo: </asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox runat="server" ID="UserName" TextMode="Number" CssClass="form-control" Width="170px" />
                                </asp:TableCell><asp:TableCell>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" CssClass="text-danger" ErrorMessage="El codigo es obligatorio." ForeColor="#CC0000" />
                                </asp:TableCell></asp:TableRow><asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Contraseña: </asp:Label>
                                </asp:TableCell><asp:TableCell>
                                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" Width="171px" />
                                </asp:TableCell><asp:TableCell>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="La contraseña es obligatoria." ForeColor="#CC0000" />
                                </asp:TableCell></asp:TableRow></asp:Table></div><asp:Label ID="Lerror" runat="server" Text="" ForeColor="Red"></asp:Label><div class="form-group">
                        <span style="padding-left: 200px;"><asp:Button runat="server" OnClick="LogIn" Text="LOGIN" CssClass="btn btn-default" /></span>
                    </div>
                </font>
                </div>
            </section>  
        </div>
    </div>    
</asp:Content>
