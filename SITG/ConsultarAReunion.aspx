<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConsultarAReunion.aspx.cs" Inherits="ConsultarAReunion" %>

<asp:Content ID="ConsultarAReunion" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-body" style="margin-left: auto; margin-right: auto; text-align: center;"> 
            <asp:Label ID="Ltitle" runat="server"  Text="DECANO" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#333333" ToolTip="La opción pertenece al rol decano." ></asp:Label>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading" style="background-color:#1C2833 ;color:white">Gestionar Documentos - Consultar Actas Reunión</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPactasreu" runat="server" > <ContentTemplate>
                <div class="container-fluid">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-body">

                                <div id="ConsultarActa" runat="server" visible="true" class="row">
                                    <asp:Table runat="server" ID="Tconsulta" HorizontalAlign="Center">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Lprograma" runat="server" Text="Comité:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="DDLprog" class="btn btn-secondary btn-lg dropdown-toggle" runat="server" OnSelectedIndexChanged="DDLprog_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Lfdesde" runat="server" Text="Desde:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="TBdesde" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:ImageButton ID="Idesde" runat="server" Height="23px" ImageUrl="~/Images/Icalendar.png" Width="36px" OnClick="IBdesde_Click" /></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="Lfhasta" runat="server" Text="Hasta:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="TBhasta" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:ImageButton ID="IBhasta" runat="server" Height="23px" ImageUrl="~/Images/Icalendar.png" Width="36px" OnClick="IBhasta_Click" /></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Button ID="BbuscarAct" runat="server" OnClick="BbuscarAct_Click" Text="BUSCAR" class="btn btn-default" /></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="Center" ColumnSpan="3">
                                                <asp:Calendar ID="Cdesde" runat="server" Style="margin-top: 0px" Visible="false" OnSelectionChanged="Cdesde_SelectionChanged"></asp:Calendar>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Center" ColumnSpan="3">
                                                <asp:Calendar ID="Chasta" runat="server" Style="margin-top: 0px" Visible="false" OnSelectionChanged="Chasta_SelectionChanged"></asp:Calendar>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                
                                    <asp:GridView ID="GVactas" runat="server" Visible="false" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GVactas_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"
                                        OnRowDataBound="GVactas_RowDataBound" PageSize="8" Caption="ACTAS" CaptionAlign="Top">
                                        <AlternatingRowStyle BackColor="White" />
                                        <FooterStyle BackColor="white" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#1C2833" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="white" />
                                        <SelectedRowStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C2833" Font-Bold="True" ForeColor="White" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        <EmptyDataTemplate>¡No se encuentran actas en el rango consultado!</EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="REU_CODIGO" HeaderText="Reunión" />
                                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acta">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" ClientIDMode="AutoID" runat="server" Text="Descargar" OnClick="DownloadFile" CommandArgument='<%# Eval("REU_CODIGO") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
          </ContentTemplate> </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
