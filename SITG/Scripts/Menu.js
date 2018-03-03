$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("active");
});

// Add open class if active
$('.sidebar-nav').find('li.dropdown.active').addClass('open');

// Open submenu if active
$('.sidebar-nav').find('li.dropdown.open ul').css("display", "block");

// Change active menu
$(".sidebar-nav > li").click(function () {
    $(".sidebar-nav > li").removeClass("active");
    $(this).addClass("active");
});

// Add open animation
$('.dropdown').on('show.bs.dropdown', function (e) {
    $(this).find('.dropdown-menu').first().stop(true, true).slideDown();
});

// Add close animation
$('.dropdown').on('hide.bs.dropdown', function (e) {
    $(this).find('.dropdown-menu').first().stop(true, true).slideUp();
});

