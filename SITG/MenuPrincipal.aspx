<%@ Page Title="SITG" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MenuPrincipal.aspx.cs" Inherits="Menu" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<<<<<<< HEAD
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
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="/Images/rol_estudiante.jpg" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Es el docente</h4>
                                                    <p>Aquí encontraras información acerca del rol estudiante; desplazate hacia la derecha para ver mas</p>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="http://placehold.it/360x240" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Propuesta</h4>
                                                    <p>Hello world, something nice to develop</p>
                                                    <a class="btn btn-mini" href="#">» Read More</a>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="http://placehold.it/360x240" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Anteroyecto</h4>
                                                    <p>Hello world, something nice to develop</p>
                                                    <a class="btn btn-mini" href="#">» Read More</a>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="http://placehold.it/360x240" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Proyecto final</h4>
                                                    <p>Hello world, something nice to develop</p>
                                                    <a class="btn btn-mini" href="#">» Read More</a>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                                <!-- /Slide1 -->
                            </div>
                            <nav>
                                <ul class="control-box pager">
                                    <li class="left"><a data-slide="prev" href="#myCarousel" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                    <li class="right"><a data-slide="next" href="#myCarousel" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                                </ul>
                            </nav>
                            <!-- /.control-box -->
                        </div>
                        <!-- /#myCarousel -->
                    </div>
                    <!-- /.col-xs-12 -->
                </div>
                <!-- /.container -->
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
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="/Images/rol_estudiante.jpg" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Es el comite</h4>
                                                    <p>Aquí encontraras información acerca del rol estudiante; desplazate hacia la derecha para ver mas</p>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="http://placehold.it/360x240" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Propuesta</h4>
                                                    <p>Hello world, something nice to develop</p>
                                                    <a class="btn btn-mini" href="#">» Read More</a>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="http://placehold.it/360x240" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Anteroyecto</h4>
                                                    <p>Hello world, something nice to develop</p>
                                                    <a class="btn btn-mini" href="#">» Read More</a>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="http://placehold.it/360x240" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Proyecto final</h4>
                                                    <p>Hello world, something nice to develop</p>
                                                    <a class="btn btn-mini" href="#">» Read More</a>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                                <!-- /Slide1 -->

                            </div>


                            <nav>
                                <ul class="control-box pager">
                                    <li class="left"><a data-slide="prev" href="#dede" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                    <li class="right"><a data-slide="next" href="#dede" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                                </ul>
                            </nav>
                            <!-- /.control-box -->

                        </div>
                        <!-- /#myCarousel -->

                    </div>
                    <!-- /.col-xs-12 -->

                </div>
                <!-- /.container -->

            </div>

<!-----------------------------------------------------DIRECTOR--------------------------------------------------------------------------->
            <div class="tab-pane fade" id="DIR">
                <br />
                <div id="DIR2" class="container" runat="server">

                    <div class="col-xs-12">
                        <div class="carousel slide" id="car3">
                            <div class="carousel-inner">
                                <div class="item active">
                                    <ul class="thumbnails">
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="/Images/rol_estudiante.jpg" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Es el director</h4>
                                                    <p>Aquí encontraras información acerca del rol estudiante; desplazate hacia la derecha para ver mas</p>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="http://placehold.it/360x240" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Propuesta</h4>
                                                    <p>Hello world, something nice to develop</p>
                                                    <a class="btn btn-mini" href="#">» Read More</a>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="http://placehold.it/360x240" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Anteroyecto</h4>
                                                    <p>Hello world, something nice to develop</p>
                                                    <a class="btn btn-mini" href="#">» Read More</a>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="col-sm-3">
                                            <div class="casing">
                                                <div class="thumbnail">
                                                    <a href="#">
                                                        <img src="http://placehold.it/360x240" alt=""></a>
                                                </div>
                                                <div class="caption">
                                                    <h4>Proyecto final</h4>
                                                    <p>Hello world, something nice to develop</p>
                                                    <a class="btn btn-mini" href="#">» Read More</a>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                                <!-- /Slide1 -->

                            </div>


                            <nav>
                                <ul class="control-box pager">
                                    <li class="left"><a data-slide="prev" href="#dede" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                    <li class="right"><a data-slide="next" href="#dede" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                                </ul>
                            </nav>
                            <!-- /.control-box -->

                        </div>
                        <!-- /#myCarousel -->

                    </div>
                    <!-- /.col-xs-12 -->

                </div>
                <!-- /.container -->

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

                                       


                                        </li>

                                        <li class="col-sm-4">


                                         <section id="skills" style="position:relative; left:-20%; top:auto;">
					<h1> Estadisitcas de proyectos de grado </h1>
                    <progress value="15" max="30" style="background-color:black;"></progress><span>Propuestas </span>
					<progress value="9" max="30" style="background-color:black;"></progress><span>Anteproyectos</span>
					<progress value="6" max="30" style="background-color:black;"></progress><span>Proyecto finales</span>

					</section>

                                        </li>

                                        <li class="col-sm-4">

                                            <section class="statistics text-center" style="position: relative; left: -20%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>ADMINISTRADOR</h2>
                                                        <div class="row">

                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-building fa-5x"></i>
                                                                    <p>3</p>
                                                                    <span>Facultades</span>
                                                                </div>
                                                            </div>



                                                        </div>
                                                    </div>
                                                </div>
                                            </section>

                                        </li>


                                        <li class="col-sm-4"></li>

                                    </ul>
                                </div>
                                <!-- /Slide1 -->

                                <div class="item">
                                    <ul class="thumbnails">
                                        <li class="col-sm-4">

                                            <section>
                                                <div class="pieID pie" id="prueba2">
                                                </div>
                                                <ul class="pieID legend" id="prueba" style="position: relative; left: 10%;">
                                                    <li style="color: black">
                                                        <em>carro</em>
                                                        <span>600</span>
                                                    </li>
                                                    <li style="color: black">
                                                        <em>chelsea</em>
                                                        <span>123</span>
                                                    </li>


                                                </ul>
                                            </section>


                                        </li>

                                        <li class="col-sm-4">

                                            <section class="statistics text-center" style="position: relative; left: -20%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>ADMINISTRADOR</h2>
                                                        <div class="row">

                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-users fa-5x"></i>
                                                                    <p>30</p>
                                                                    <span>Usuarios</span>
                                                                </div>
                                                            </div>



                                                        </div>
                                                    </div>
                                                </div>
                                            </section>

                                        </li>

                                        <li class="col-sm-4">

                                            <section class="statistics text-center" style="position: relative; left: -20%;">
                                                <div class="data">
                                                    <div class="container">
                                                        <h2>ADMINISTRADOR</h2>
                                                        <div class="row">

                                                            <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <div class="stats">
                                                                    <i class="fa fa-building fa-5x"></i>
                                                                    <p>3</p>
                                                                    <span>Facultades</span>
                                                                </div>
                                                            </div>



                                                        </div>
                                                    </div>
                                                </div>
                                            </section>

                                        </li>


                                        <li class="col-sm-4"></li>

                                    </ul>
                                </div>
                                <!-- /Slide2 -->

                            </div>
                            <nav>
                                <ul class="control-box pager">
                                    <li class="left"><a data-slide="prev" href="#car4" class="arrowStil"><i class="glyphicon glyphicon-chevron-left"></i></a></li>
                                    <li class="right"><a data-slide="next" href="#car4" class="arrowStil"><i class="glyphicon glyphicon-chevron-right"></i></a></li>
                                </ul>
                            </nav>
                            <!-- /.control-box -->

                        </div>
                        <!-- /#myCarousel -->

                    </div>
                    <!-- /.col-xs-12 -->

                </div>
                <!-- /.container -->
            </div>











































       
            <!-------------------------------------------------------INICIO---------------------------------------------------------------------------->
            <div class="tab-pane fade in active" id="inicio">
                <div class="container2">
  <section>
    <header>
      <h1>BIENVENIDO A <strong>SITG</strong></h1>
      <h2><span class="action-click"></span><span class="action-tap">tap </span></h2>
    </header>
    <article>
      <input type="radio" name="switch" id="switch-on" class="switch-on">
      <div class="envelope">
        <label for="switch-on">
          <span class="triangle cap"></span>
          <div class="notification waiting">
            <span class="number">40</span>
          </div>
          <span class="triangle bag"></span>
          <span class="triangle tail"></span>
        </label>
      </div>
      <div class="title">

          <iframe width="420" height="200" src="https://www.youtube.com/embed/_I_D_8Z4sJE" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen style="position:relative; left:-33%;"></iframe>
        <input type="radio" name="switch" id="switch-off" class="switch-off">
          
          <br>
           <br>
          <label for="switch-off">Cerrar</label>
      </div>
      <div class="overlay"></div>
    </article>
  </section>
</div>
            </div>














































































































































        </div>


    </div>







    <asp:Label ID="label" runat="server" ForeColor="Red" Text=""></asp:Label>
=======
<asp:Content ID="MenuPrincipal" ContentPlaceHolderID="MainContent" Runat="Server"> 
>>>>>>> e9ac2557966f900d20302a08d90a458d3bda08b8
</asp:Content>

