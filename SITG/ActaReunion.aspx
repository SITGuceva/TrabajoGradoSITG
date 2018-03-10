<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ActaReunion.aspx.cs" Inherits="ActaReunion" %>

<asp:Content ID="ActaReunion" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="panel panel-default">
        <div class="panel-heading">Actas - Acta de Reunion</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPactareu" runat="server"> <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBgenerar" runat="server" OnClick="LBgenerar_Click" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Generar </asp:LinkButton></li>
                            <li> <asp:LinkButton ID="LBconsultar" runat="server" OnClick="LBconsultar_Click" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Consultar </asp:LinkButton></li>                         
                        </ul>
                    </div>
                </div>

                <div id="Ingreso" runat="server" visible="true" class="row">
                    <asp:Table ID="Tgenerar" runat="server" HorizontalAlign="Center">   
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lreu" runat="server" Text="Reunion:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLreu" runat="server" class="btn btn-secondary btn-lg dropdown-toggle" AutoPostBack="true"></asp:DropDownList></asp:TableCell>                    
                            <asp:TableCell><asp:Label ID="Lugar" runat="server" Text="Lugar:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBlugar" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:Label ID="Lobj" runat="server" Text="Objetivo:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBobj" runat="server" CssClass="form-control"></asp:TextBox> </asp:TableCell>                             
                         </asp:TableRow>
                        <asp:TableRow HorizontalAlign="Center">
                           <asp:TableHeaderCell HorizontalAlign="Center" >
                                 <asp:Label ID="Ltasi" runat="server" Text="ASISTENTES" ForeColor="Black" Font-Bold="True"></asp:Label>
                            </asp:TableHeaderCell>
                       </asp:TableRow>
                    </asp:Table>
                        
                    <asp:GridView ID="GVasistente" runat="server" Visible="true" AllowPaging="True" CellPadding="4" ForeColor="#333333"  GridLines="None" OnPageIndexChanging="GVasistente_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" OnRowDataBound="GVasistente_RowDataBound" PageSize="8" >
                         <AlternatingRowStyle BackColor="White" />
                         <EditRowStyle BackColor="#2461BF" />
                         <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                         <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                         <RowStyle BackColor="White" />
                         <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                         <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White"  HorizontalAlign="Center"/>
                          <EditRowStyle BackColor="#ffffcc" />
                          <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                          <EmptyDataTemplate>¡No hay integrantes del comite!</EmptyDataTemplate>
                          <Columns >
                              <asp:BoundField DataField="miembros" HeaderText="MIEMBROS" ItemStyle-HorizontalAlign="Center" />   
                              <asp:TemplateField HeaderText="ASISTIO" ItemStyle-HorizontalAlign="Center">
                                  <ItemTemplate><asp:CheckBox ID="CBasitio" runat="server" Text=""  AutoPostBack="true" /> </ItemTemplate>
                              </asp:TemplateField>
                         </Columns>
                     </asp:GridView>

                    
                    <asp:Table ID="Tinvitados" runat="server" HorizontalAlign="Center">
                       <asp:TableRow>
                         <asp:TableHeaderCell><asp:Label ID="Ltinv" runat="server" Text="INVITADOS" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableHeaderCell>
                       </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lnombre" runat="server" Text="Nombre Completo:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBnombre" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:Label ID="Lcargo" runat="server" Text="Cargo" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBcargo" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Agregar" runat="server" Text="Agregar" OnClick="Agregar_Click" class="btn btn-default" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                    <asp:GridView  ID="GVagreinte" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"  PageSize="6">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <Columns>
                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre Completo" />                                   
                            <asp:BoundField DataField="CARGO" HeaderText="Cargo" />                                   
                        </Columns>
                     </asp:GridView>

                    <asp:Table ID="Torden" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lorden" runat="server" Text="Orden del Dia:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBorden" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:Button ID="BagregarOrden" runat="server" Text="Agregar" OnClick="BagregarOrden_Click" class="btn btn-default" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                    <asp:GridView  ID="GVorden" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"  PageSize="6">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <Columns>
                            <asp:BoundField DataField="ORDEN" HeaderText="ORDEN DEL DIA" />                                   
                        </Columns>
                     </asp:GridView>

                     <asp:Table ID="TableBotones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="Bgenerar" runat="server" Text="Generar" OnClick="Bgenerar_Click" class="btn btn-default"/></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bcancelar" runat="server" Text="Cancelar" OnClick="Bcancelar_Click" class="btn btn-default"/></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>



                    
                </div>
             
              

                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                <asp:HiddenField ID="Asistio" runat="server" Value="" />   
                <asp:HiddenField ID="Noasistio" runat="server" Value=""/>
            </div>
          </ContentTemplate></asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

