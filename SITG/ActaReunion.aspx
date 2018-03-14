<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ActaReunion.aspx.cs" Inherits="ActaReunion" %>

<asp:Content ID="ActaReunion" ContentPlaceHolderID="MainContent" Runat="Server">
    
     <div class="panel panel-default">
        <div class="panel-heading">Actas - Acta de Reunion</div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UPactas" runat="server" > <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><asp:LinkButton ID="LBgenerar" runat="server" OnClick="LBgenerar_Click" ForeColor="Black"><span class="glyphicon glyphicon-plus"></span>Generar </asp:LinkButton></li>
                            <li> <asp:LinkButton ID="LBsubir" runat="server" OnClick="LBsubir_Click" ForeColor="Black"><span class="glyphicon glyphicon-search"></span>Cargar </asp:LinkButton></li>                         
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
                      
                    <br>
                    <br>

                     <asp:Table ID="Tordendia" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                         <asp:TableHeaderCell><asp:Label ID="ltituloOrden" runat="server" Text="---ORDEN DEL DÍA---" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableHeaderCell>
                       </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="LrevisarP" runat="server" Text="Revisión de Propuestas de trabajos de grado" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:CheckBox ID="CBpropuesta" runat="server" Text=""  AutoPostBack="true" /></asp:TableCell>
                        </asp:TableRow> 
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="LasginarA" runat="server" Text="Asignar jurados para anteproyectos de grado" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:CheckBox ID="CBanteproyecto" runat="server" Text=""  AutoPostBack="true" /></asp:TableCell>
                         </asp:TableRow>
                          <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lcaso" runat="server" Text="Analizar casos particulares" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:CheckBox ID="CBcaso" runat="server" Text=""  AutoPostBack="true" /></asp:TableCell>
                         </asp:TableRow>
                        
                    </asp:Table>

                     <br>

                    <asp:Table ID="Torden" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                         <asp:TableHeaderCell><asp:Label ID="Label1" runat="server" Text="CASO PARTICULAR" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableHeaderCell>
                       </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lorden" runat="server" Text="Título del caso:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBorden" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:Label ID="Ldescripcion" runat="server" Text="Descripción:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><textarea ID="TAdes" runat="server" CssClass="form-control"></textarea></asp:TableCell>  
                            <asp:TableCell><asp:Button ID="BagregarOrden" runat="server" Text="Agregar" OnClick="BagregarOrden_Click" class="btn btn-default" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <br>
                    <asp:GridView  ID="GVorden" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="table table-bordered bs-table"  PageSize="6">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <Columns>
                            <asp:BoundField DataField="ORDEN" HeaderText="TÍTULO CASO ESPECIAL" />    
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCIÓN" />    
                        </Columns>
                     </asp:GridView>

                     <asp:Table ID="TableBotones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="Bgenerar" runat="server" Text="Generar" OnClick="Bgenerar_Click" class="btn btn-default"/></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Bcancelar" runat="server" Text="Cancelar" OnClick="Bcancelar_Click" class="btn btn-default"/></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>                   
                </div>

                <div id="SubirActa" runat="server" visible="false" class="row">
                    <asp:Table ID="Tformatos" runat="server" HorizontalAlign="Center">                     
                         <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lreunion" runat="server" Text="Reunion:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="DDLreunion2" runat="server" class="btn btn-secondary btn-lg dropdown-toggle" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                        </asp:TableRow>
                         <asp:TableRow>
                             <asp:TableCell><asp:Label ID="Ldocumento" runat="server" Text="Acta:" ForeColor="Black" Font-Bold="True"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:FileUpload ID="FUdocumento" runat="server" class="btn btn-default image-preview-input" />
                                            
                            </asp:TableCell>
                        </asp:TableRow>                                             
                    </asp:Table>
                    <asp:Table ID="botones" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell><asp:Button ID="Bsubir" runat="server" OnClick="Bsubir_Click" Text="Guardar" class="btn btn-default" /></asp:TableCell>
                            <asp:TableCell><asp:Button ID="Blimpiar" runat="server" OnClick="Blimpiar_Click" Text="Limpiar" class="btn btn-default" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
               
                <div id="ConsultarActa" runat="server" visible="false" class="row">
                    <asp:Table runat="server" ID="Trango" HorizontalAlign="Center"> 
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="Lfdesde" runat="server" Text="Desde:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="TBdesde" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:ImageButton ID="Idesde" runat="server" Height="23px" ImageUrl="~/Images/Icalendar.png" Width="36px" OnClick="IBdesde_Click"/></asp:TableCell>
                            <asp:TableCell><asp:Label ID="Lfhasta" runat="server" Text="Hasta:" ForeColor="Black" Font-Bold="True" class="text-justify"></asp:Label></asp:TableCell> 
                            <asp:TableCell><asp:TextBox ID="TBhasta" runat="server" CssClass="form-control"></asp:TextBox></asp:TableCell>
                            <asp:TableCell><asp:ImageButton ID="IBhasta" runat="server" Height="23px" ImageUrl="~/Images/Icalendar.png" Width="36px" OnClick="IBhasta_Click"/></asp:TableCell>
                            <asp:TableCell><asp:Button ID="BbuscarAct" runat="server" OnClick="BbuscarAct_Click" Text="Buscar" class="btn btn-default" /></asp:TableCell>
                         </asp:TableRow>       
                         <asp:TableRow>
                              <asp:TableCell HorizontalAlign="Center" ColumnSpan="3"><asp:Calendar ID="Cdesde" runat="server" style="margin-top: 0px" Visible="false" OnSelectionChanged="Cdesde_SelectionChanged"></asp:Calendar></asp:TableCell>
                              <asp:TableCell HorizontalAlign="Center" ColumnSpan="3" ><asp:Calendar ID="Chasta" runat="server" style="margin-top: 0px" Visible="false" OnSelectionChanged="Chasta_SelectionChanged"></asp:Calendar></asp:TableCell>
                         </asp:TableRow>
                    </asp:Table>    
                    <asp:GridView ID="GVactas" runat="server" Visible="false" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None"  OnPageIndexChanging="GVactas_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-bordered bs-table" 
                         OnRowDataBound="GVactas_RowDataBound" PageSize="8" caption="ACTAS" captionalign="Top"  >
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        <EmptyDataTemplate>¡No hay actas en el rango consultado!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="REU_CODIGO" HeaderText="Reunion" /> 
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" /> 
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acta">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" ClientIDMode="AutoID" runat="server" Text="Download" OnClick="DownloadFile" CommandArgument='<%# Eval("REU_CODIGO") %>' ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>  
                        </Columns>
                     </asp:GridView>
                </div>
              

                <asp:Label ID="Linfo" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                <asp:HiddenField ID="Asistio" runat="server" Value="" />   
                <asp:HiddenField ID="Noasistio" runat="server" Value=""/>
            </div>
          </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Bsubir" />
                 </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

