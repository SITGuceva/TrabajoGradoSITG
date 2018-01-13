<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Propuesta.aspx.cs" Inherits="Propuesta" %>

<asp:Content ID="Propuesta" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">Gestionar Documentos - Propuesta</div>
        <div class="panel-body">

            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBSubir_propuesta" runat="server" OnClick="Subir_propuesta" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Subir </asp:LinkButton></li>
                            <li> <asp:LinkButton ID="LBEstado_propuesta" runat="server" OnClick="Estado_propuesta" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Estado </asp:LinkButton></li>
                            <li><asp:LinkButton ID="LBobservaciones" runat="server" OnClick="Observaciones_propuesta" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Observaciones</asp:LinkButton></li>
                        </ul>
                    </div>
                </div>

                <div id="Ingreso" runat="server" visible="false" class="row">
                    <asp:Table ID="Tsubir" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Lid" runat="server" Text="CODIGO:" ForeColor="Black" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="TBid" runat="server" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Lnombre" runat="server" Text="TITULO:" ForeColor="Black" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="3">
                                <asp:TextBox ID="TBnombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Ldocumento" runat="server" Text="DOCUMENTO:" ForeColor="Black" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:FileUpload ID="FUdocumento" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
             
                <div id="Estado" runat="server" visible="false" class="row">                  
                    <asp:GridView ID="GVestado" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                       AutoGenerateColumns="False" CssClass="table table-bordered bs-table" >                   
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /> 
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate> ¡No Ha Subido La Propuesta!  </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="PROP_CODIGO" HeaderText="CODIGO" />
                            <asp:BoundField DataField="PROP_TITULO" HeaderText="TITULO" />
                            <asp:BoundField DataField="PROP_FECHA" HeaderText="FECHA" />
                            <asp:BoundField DataField="PROP_ESTADO" HeaderText="ESTADO" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DOCUMENTO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile" CommandArgument='<%# Eval("PROP_CODIGO") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div id="Observaciones" runat="server" visible="false" class="row">
                    <asp:GridView ID="gvSysRol" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnPageIndexChanging="gvSysRol_PageIndexChanging"
                        AutoGenerateColumns="False"
                        CssClass="table table-bordered bs-table"
                        OnRowDataBound="gvSysRol_RowDataBound" PageSize="6">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>
                            ¡No hay observaciones!  
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="OBS_CODIGO" HeaderText="CODIGO" />
                            <asp:BoundField DataField="OBS_DESCRIPCION" HeaderText="DESCRIPCION" />
                            <asp:BoundField DataField="OBS_REALIZADA" HeaderText="REALIZADA POR" /> 
                        </Columns>
                     </asp:GridView> 
                </div>

                 <div id="DivBotones" runat="server" visible="false" class="row">
                    <asp:Table ID="TableBotones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button ID="Bguardar" runat="server" Text="Guardar" OnClick="Guardar" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="Bcancelar" runat="server" Text="Cancelar" OnClick="Cancelar" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
            </div>

        </div>
    </div>
</asp:Content>

