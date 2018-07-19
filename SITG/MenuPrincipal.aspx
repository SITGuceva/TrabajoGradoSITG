<%@ Page Title="SITG" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MenuPrincipal.aspx.cs" Inherits="Menu" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="MenuPrincipal" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="col-lg-12">
        <ul class="nav nav-tabs">
            <asp:PlaceHolder ID="Notificaciones" runat="server"></asp:PlaceHolder>
        </ul>

        <div class="tab-content">
            <div class="tab-pane fade" id="DOC">
                <br />
                <div id="DOC2" class="container" runat="server">
                    <div class="col-xs-12">
                        <div class="carousel slide" id="car1">
                            <div class="carousel-inner">
                                <div class="item active">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>DOCENTE</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-upload fa-5x"></i>
                                                                    <p><%= cantproyectosdoc %></p>
                                                                    <span>Proyectos subidos</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <nav><ul class="control-box pager">
                                <li class="left"><a data-slide="prev" href="#myCarousel" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                <li class="right"><a data-slide="next" href="#myCarousel" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                            </ul></nav><!-- /.control-box -->
                        </div><!-- /#myCarousel -->
                    </div> <!-- /.col-xs-12 -->
                </div><!-- /.container -->
            </div>
            <!-----------------------------------------------------COMITE--------------------------------------------------------------------------->
                <div class="tab-pane fade" id="COM">
                <br />
                <div id="COM2" class="container" runat="server">
                    <div class="col-xs-12">
                        <div class="carousel slide" id="car2">
                            <div class="carousel-inner">
                                <div class="item active">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                                <section id="skills" style="position: relative; top: auto;">
                                                <h2 style="color:black;">PROPUESTAS </h2>
                                                <progress value="<%= cantpropaprobcom %>" max="<%= sumapropuesta %>" style="background-color: black;"></progress><span>Aprobados <%=  cantpropaprobcom %></span>
                                                <progress value="<%= cantproprechacom %>" max="<%= sumapropuesta %>" style="background-color: black;"></progress><span>Rechazados <%= cantproprechacom %></span>
                                                <progress value="<%= cantproppencom %>" max="<%= sumapropuesta %>" style="background-color: black;"></progress><span>Pendiente <%=  cantproppencom %></span>
                                                 <h3 style="color:black;">Total: <%= sumapropuesta %></h3>
                                                </section>
                                        </li>
                                        <li class="col-sm-4">
                                               <section id="skills" style="position: relative; top: auto;">
                                                <h2 style="color:black;">ANTEPROYECTOS </h2>
                                                <progress value="<%= cantanteaprobcom %>" max="<%= sumaanteproyecto %>" style="background-color: black;"></progress><span>Aprobados <%=  cantanteaprobcom %></span>
                                                <progress value="<%= cantanterechacom %>" max="<%= sumaanteproyecto %>" style="background-color: black;"></progress><span>Rechazados <%= cantanterechacom %></span>
                                                <progress value="<%= cantantepencom %>" max="<%= sumaanteproyecto %>" style="background-color: black;"></progress><span>Pendiente <%=  cantantepencom %></span>
                                                <h3 style="color:black;">Total: <%= sumaanteproyecto %></h3>
                                              </section>
                                        </li>
                                        <li class="col-sm-4">
                                             <section id="skills" style="position: relative; top: auto;">
                                                <h2 style="color:black;">PROYECTO FINAL</h2>
                                                <progress value="<%= finalaprobcom %>" max="<%= sumaproyectofinal %>" style="background-color: black;"></progress><span>Aprobados <%=  finalaprobcom %></span>
                                                <progress value="<%= finalrechacom %>" max="<%= sumaproyectofinal %>" style="background-color: black;"></progress><span>Rechazados <%= finalrechacom %></span>
                                                <progress value="<%= finalpencom %>" max="<%= sumaproyectofinal %>" style="background-color: black;"></progress><span>Pendiente <%=  finalpencom %></span>
                                                <h3 style="color:black;">Total: <%= sumaproyectofinal %></h3>
                                             </section>
                                        </li>
                                    </ul>
                                </div><!-- /Slide1 -->
                                <div class="item">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>PROPUESTAS PENDIENTES</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-file-word-o fa-5x"></i>
                                                                    <p><%= propuestapencom %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                             <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>ANTEPROYECTOS PENDIENTES EVALUADOR</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-legal fa-5x"></i>
                                                                    <p><%= antepenasignacion %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                             <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>SOLICITUD DIRECTOR PENDIENTE</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-user fa-5x"></i>
                                                                    <p><%= dirpeticion %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                    </ul>
                                </div><!-- /Slide2 -->
                                  <div class="item">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                             <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>SOLICITUDES ESTUDIANTES</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-male fa-5x"></i>
                                                                    <p><%= solicom %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                          <li class="col-sm-4">
                                                  <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>REUNIONES DEL MES</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-calendar fa-5x"></i>
                                                                    <p><%= cantreunionescom %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                          </li>
                                    </ul>
                                </div>                            
                            </div>
                            <nav><ul class="control-box pager">
                                <li class="left"><a data-slide="prev" href="#car2" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                <li class="right"><a data-slide="next" href="#car2" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                            </ul></nav><!-- /.control-box -->
                        </div> <!-- /#myCarousel -->
                    </div> <!-- /.col-xs-12 -->
                </div> <!-- /.container -->
            </div>

            <!-----------------------------------------------------DIRECTOR--------------------------------------------------------------------------->
             <div class="tab-pane fade" id="DIR">
                <br />
                <div id="DIR2" class="container" runat="server">
                    <div class="col-xs-12">
                        <div class="carousel slide" id="car12">
                            <div class="carousel-inner">
                                <div class="item active">
                                    <ul class="thumbnails">
                                         <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>CANTIDAD DE PROYECTOS ASIGNADOS</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-file-word-o fa-5x"></i>
                                                                    <p><%= proyectosasigdir %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                             <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>PROPUESTAS PENDIENTES POR REVISION</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-file-word-o fa-5x"></i>
                                                                    <p><%= proppendiente %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                             <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>CANTIDAD DE ANTEPROYECTOS SIN REVISION</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-file-word-o fa-5x"></i>
                                                                    <p><%= antependiente %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>   
                                    </ul>
                                </div>
                                <div class="item">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                             <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>CANTIDAD DE PROYECTOS FINALES SIN REVISION</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-file-word-o fa-5x"></i>
                                                                    <p><%= proyfinalpendiente %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                    </ul>
                                </div>                                                  
                            </div>
                            <nav><ul class="control-box pager">
                                <li class="left"><a data-slide="prev" href="#car12" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                <li class="right"><a data-slide="next" href="#car12" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                            </ul></nav><!-- /.control-box -->
                        </div> <!-- /#myCarousel -->
                    </div> <!-- /.col-xs-12 -->
                </div> <!-- /.container -->
            </div>

            <!-----------------------------------------------------ESTUDIANTE--------------------------------------------------------------------------->
            <div class="tab-pane fade" id="EST">
                <br />
                <div id="EST2" class="container" runat="server">
                   <div class="col-xs-12">
                        <div class="carousel slide" id="car4">
                            <div class="carousel-inner">
                                <div class="item active">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: 1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>DIRECTOR</h2>
                                                        <div class="row">
                                                            <div id="dsinsolicitud" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-minus-circle fa-5x"></i>
                                                                        <p></p>
                                                                        <span>Sin solicitud</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="dpendiente" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-clock-o fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= edirector %></p>
                                                                        <span>Observaciones comite:0</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="daprobado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-check fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= edirector %></p>
                                                                        <span>Observaciones comite:0</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="drechazado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-times fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= edirector %></p>
                                                                        <span>Observaciones comite:1</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>PROPUESTA</h2>
                                                        <div class="row">
                                                            <div id="psinsubir" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-minus-circle fa-5x"></i>
                                                                        <p></p>
                                                                        <span>Sin subir propuesta</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="ppendiente" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-clock-o fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= epropuesta %></p>
                                                                        <span>Observaciones comite:<%= obscomitep %></span>
                                                                        <span>Observaciones director:<%= obsdirectorp %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="paprobado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-check fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= epropuesta %></p>
                                                                        <span>Observaciones comite:<%= obscomitep %></span>
                                                                        <span>Observaciones director:<%= obsdirectorp %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="prechazado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-times fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= epropuesta %></p>
                                                                        <span>Observaciones comite:<%= obscomitep %></span>
                                                                        <span>Observaciones director:<%= obsdirectorp %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>ANTEPROYECTO (EVALUADOR)</h2>
                                                        <div class="row">
                                                            <div id="aevaluador" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-legal fa-5x"></i>
                                                                        <p style="font-size: 28px;"><%= anteasigeva %></p>
                                                                        <span>Calificación:<%= antecaleva %></span>
                                                                        <span>Observaciones:<%= anteobseva %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="sinaevaluador" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-legal fa-5x"></i>
                                                                        <p></p>
                                                                        <span>Sin subir anteproyecto</span>
                                                                        <span></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                    </ul>
                                </div><!-- /Slide1 -->
                                <div class="item">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>ANTEPROYECTO (DIRECTOR)</h2>
                                                        <div class="row">
                                                            <div id="adirectorpendiente" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-clock-o fa-5x"></i>
                                                                        <p><%= antecaldir%></p>
                                                                        <span>Observaciones:<%= anteobsedir %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="adirectoraprobado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-check fa-5x"></i>
                                                                        <p><%= antecaldir%></p>
                                                                        <span>Observaciones:<%= anteobsedir %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="adirectorrechazado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-times fa-5x"></i>
                                                                        <p><%= antecaldir%></p>
                                                                        <span>Observaciones:<%= anteobsedir %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="sindirectorante" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-minus-circle fa-5x"></i>
                                                                        <p></p>
                                                                        <span>Sin subir anteproyecto</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                           <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>PAGOS</h2>
                                                        <div class="row">
                                                            <div id="pagosinsubir" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-minus-circle fa-5x"></i>
                                                                        <p></p>
                                                                        <span>Sin subir pago</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="pagopendiente" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-clock-o fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= epago %></p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="pagoaprobado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-check fa-5x"></i>
                                                                        <p style="font-size: 30px;"><%= epago %></p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="pagorechazado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-times fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= epago %></p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                           <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>PROYECTO FINAL (DIRECTOR)</h2>
                                                        <div class="row">
                                                            <div id="aproyfinaldirpendiente" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-clock-o fa-5x"></i>
                                                                        <p><%= edirproyfinal %></p>
                                                                        <span>Observaciones:<%= edirproyfinalobs %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="aproyfinalsinsubir" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-minus-circle fa-5x"></i>
                                                                        <span>Sin subir proyecto final</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="aproyfinaldiraprobado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-check fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= edirproyfinal %></p>
                                                                        <span>Observaciones:<%= edirproyfinalobs %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="aproyfinaldirrechazado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-times fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= edirproyfinal %></p>
                                                                        <span>Observaciones:<%= edirproyfinalobs %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                    </ul>
                                </div> <!-- /Slide2 -->
                                <div class="item">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>PROYECTO FINAL (JURADOS)</h2>
                                                        <div class="row">
                                                            <div id="proyfinaljurpendiente" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-clock-o fa-5x"></i>
                                                                        <p><%= ejurproyfinal %></p>
                                                                        <span>Observaciones:<%= ejurproyfinalobs %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="sinproyfinaljur" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-minus-circle fa-5x"></i>
                                                                        <span>Sin subir proyecto final</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="proyfinaljuraprobado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-check fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= ejurproyfinal %></p>
                                                                        <span>Observaciones:<%= ejurproyfinalobs %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="proyfinaljurrechazado" runat="server">
                                                                <div class="col-md-3 col-sm-6 col-xs-12">
                                                                    <div class="stats">
                                                                        <i class="fa fa-times fa-5x"></i>
                                                                        <p style="font-size: 37px;"><%= ejurproyfinal %></p>
                                                                        <span>Observaciones:<%= ejurproyfinalobs %></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <nav><ul class="control-box pager">
                                    <li class="left"><a data-slide="prev" href="#car4" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                    <li class="right"><a data-slide="next" href="#car4" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                            </ul></nav> <!-- /.control-box -->
                        </div><!-- /#myCarousel -->
                    </div><!-- /.col-xs-12 -->
                </div> <!-- /.container -->
            </div>
            <!-----------------------------------------------------ADMINISTRADOR--------------------------------------------------------------------------->
            <div class="tab-pane fade" id="ADM">
                <br />
                <div id="ADM2" class="container" runat="server">
                    <div class="col-xs-12">
                        <div class="carousel slide" id="car5">
                            <div class="carousel-inner">
                                <div class="item active">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: 1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>USUARIOS</h2>
                                                        <div class="row">
                                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-user fa-5x"></i> 
                                                                    <p><%= usuarios %></p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>FACULTADES</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-building fa-5x"></i>
                                                                    <p><%= facultad %></p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -2%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>PROGRAMAS</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-book fa-5x"></i>
                                                                    <p><%= programas %></p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                    </ul>
                                </div><!-- /Slide1 -->
                                <div class="item">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: 1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>ESTUDIANTES</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-graduation-cap fa-5x"></i>
                                                                    <p><%= estudiantes %></p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                       <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>PROFESORES</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-male fa-5x"></i>
                                                                    <p><%= profesores %></p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                    </ul>
                                </div><!-- /Slide2 -->
                            </div>
                            <nav><ul class="control-box pager">
                                 <li class="left"><a data-slide="prev" href="#car5" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                  <li class="right"><a data-slide="next" href="#car5" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                            </ul></nav><!-- /.control-box -->
                        </div><!-- /#myCarousel -->
                    </div> <!-- /.col-xs-12 -->
                </div><!-- /.container -->
            </div>
            <!-----------------------------------------------------DECANO--------------------------------------------------------------------------->
            <div class="tab-pane fade" id="DEC">
                <br />
                <div id="DEC2" class="container" runat="server">
                    <div class="col-xs-12">
                        <div class="carousel slide" id="car8">
                            <div class="carousel-inner">
                                <div class="item active">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                            <section>
                                                <h3 style="position: relative; left: 8%; color:black;">PROPUESTAS</h3>
                                                <div class="pieID pie" id="propuesta">
                                                </div>
                                                <ul class="pieID legend" id="propuesta2" style="position: relative; left: 2%; width: 200px;">
                                                    <li style="color: black; width: auto;">
                                                        <em>Aprobados</em>
                                                        <span><%= cantpropaprob %></span>
                                                    </li>
                                                    <li style="color: black; width: auto;">
                                                        <em>Rechazados</em>
                                                        <span><%= cantproprecha %></span>
                                                    </li>
                                                    <li style="color: black; width: auto;">
                                                        <em>Pendientes</em>
                                                        <span><%= cantproppen %></span>
                                                    </li>
                                                </ul>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                            <section>
                                                <h3 style="color:black;">ANTEPROYECTOS</h3>
                                                <div class="pieID pie" id="anteproyecto" style="position: relative; left: 4%;">
                                                </div>
                                                <ul class="pieID legend" id="anteproyecto2" style="position: relative; left: 4%; width: 200px;">
                                                    <li style="color: black; width: auto;">
                                                        <em>Aprobados</em>
                                                        <span><%= cantanteaprob %></span>
                                                    </li>
                                                    <li style="color: black; width: auto;">
                                                        <em>Rechazados</em>
                                                        <span><%= cantanterecha %></span>
                                                    </li>
                                                    <li style="color: black; width: auto;">
                                                        <em>Pendientes</em>
                                                        <span><%= cantantepen %></span>
                                                    </li>
                                                </ul>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                            <section>
                                                <h3 style="color:black;">PROYECTOS FINALES</h3>
                                                <div class="pieID pie" id="proyectofinal" style="position: relative; left: 4%;">
                                                </div>
                                                <ul class="pieID legend" id="proyectofinal2" style="position: relative; left: 4%; width: 200px;">
                                                    <li style="color: black; width: auto;">
                                                        <em>Aprobados</em>
                                                        <span><%= cantproyaprob %></span>
                                                    </li>
                                                    <li style="color: black; width: auto;">
                                                        <em>Rechazados</em>
                                                        <span><%= cantproyrecha %></span>
                                                    </li>
                                                    <li style="color: black; width: auto;">
                                                        <em>Pendientes</em>
                                                        <span><%= cantproypen %></span>
                                                    </li>
                                                </ul>
                                            </section>
                                        </li>
                                    </ul>
                                </div> <!-- /Slide1 -->
                                <div class="item">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>PROYECTO FINAL SIN JURADO</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-users fa-5x"></i>
                                                                    <p><%= proyfinalsinjur %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li> 
                                    </ul>
                                </div><!-- /Slide2 -->
                            </div>
                            <nav><ul class="control-box pager">
                                <li class="left"><a data-slide="prev" href="#car8" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                <li class="right"><a data-slide="next" href="#car8" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                            </ul></nav><!-- /.control-box -->
                        </div> <!-- /#myCarousel -->
                    </div><!-- /.col-xs-12 -->
                </div> <!-- /.container -->
            </div>
            <!-----------------------------------------------------EVALUADOR--------------------------------------------------------------------------->
            <div class="tab-pane fade" id="EVA">
                <br />
                <div id="EVA2" class="container" runat="server">
                    <div class="col-xs-12">
                        <div class="carousel slide" id="car6">
                            <div class="carousel-inner">
                                <div class="item active">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>ANTEPROYECTOS ASIGNADOS</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-legal fa-5x"></i>
                                                                    <p><%= cantantreproeva %></p>
                                                                    <span></span>
                                                                </div>
                                                           </div>
                                                       </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>ANTEPROYECTOS POR REVISAR</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-check-circle fa-5x"></i>
                                                                    <p><%= cantrevisar %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                    </ul>
                                </div><!-- /Slide1 -->
                            </div>
                            <nav><ul class="control-box pager">
                                <li class="left"><a data-slide="prev" href="#car6" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                <li class="right"><a data-slide="next" href="#car6" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                            </ul></nav><!-- /.control-box -->
                        </div> <!-- /#myCarousel -->
                    </div> <!-- /.col-xs-12 -->
                </div><!-- /.container -->
            </div>
            <!-----------------------------------------------------JURADO--------------------------------------------------------------------------->
            <div class="tab-pane fade" id="JUR">
                <br />
                <div id="JUR2" class="container" runat="server">
                    <div class="col-xs-12">
                        <div class="carousel slide" id="car7">
                            <div class="carousel-inner">
                                <div class="item active">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>PROYECTOS FINALES ASIGNADOS</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-file-word-o fa-5x"></i>
                                                                    <p><%= proyfinalasignado %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                        <li class="col-sm-4">
                                            <section class="statistics text-center" style="position: relative; left: -1%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>PROYECTO FINALES SIN REVISAR</h2>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-file-word-o fa-5x"></i>
                                                                    <p><%= proyfinalrevjur %></p>
                                                                    <span></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <nav> <ul class="control-box pager">
                                 <li class="left"><a data-slide="prev" href="#car7" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                 <li class="right"><a data-slide="next" href="#car7" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                            </ul></nav><!-- /.control-box -->
                        </div><!-- /#myCarousel -->
                    </div> <!-- /.col-xs-12 -->
                </div> <!-- /.container -->
            </div>

           

        </div>
    </div>
</asp:Content>

