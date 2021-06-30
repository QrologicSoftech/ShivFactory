$(document).ready(function () {
    $(".top-toggle").click(function () {
        $(".sidenav").toggleClass("mainbar");
    });
    $(".closebtn").click(function () {
        $(".sidenav").removeClass("mainbar");
    });
    $(".mobile-btn").click(function () {
        $("#container").toggleClass("mobile-res");
    });
    $("._1ELwxS").click(function () {
        $("#container").removeClass("mobile-res");
    });
});

window.onscroll = function () { myFunction() };

var header = document.getElementById("myHeader");
var sticky = header.offsetTop;

function myFunction() {
    if (window.pageYOffset > sticky) {
        header.classList.add("sticky");
    } else {
        header.classList.remove("sticky");
    }
}

$(document).ready(function () {
    $(".click-open").click(function () {
        $("._2hriZF ").toggleClass("_2rbIyg");
    });
    $("._2doB4z").click(function () {
        $("._2hriZF").removeClass("_2rbIyg");
    });
});
$(document).ready(function () {
    $(".sing-up").click(function () {
        $("._2hriZF ").toggleClass("_2rbIyg");
    });
    $("._2doB4z").click(function () {
        $("._2hriZF").removeClass("_2rbIyg");
    });
});

$('.IiD88i input').keyup(function () {
    if ($(this).val()) {
        $(this).parent().find('._36T8XR').addClass("_3umUoc");
    } else {
        $(this).parent().find('._36T8XR').removeClass("_3umUoc");
    }
    if ($(this).val()) {
        $(this).parent().find('._1fqY3P').addClass("_3umUoc");
    } else {
        $(this).parent().find('._1fqY3P').removeClass("_3umUoc");
    }
    if ($(this).val()) {
        $(this).parent().find('.enter-number').addClass("_1Z69nn");
    } else {
        $(this).parent().find('.enter-number').removeClass("_1Z69nn");
    }

    if ($(this).val()) {
        $(this).parent().find('._1_wZ0r').addClass("show-number");
    } else {
        $(this).parent().find('._1_wZ0r').removeClass("show-number");
    }

    if ($(this).val()) {
        $(this).parent().find('._2_ryRS').addClass("_3_6wHc");
    } else {
        $(this).parent().find('._2_ryRS').removeClass("_3_6wHc");
    }

});

$('._3xj6pt input').keyup(function () {

    if ($(this).val()) {
        $(this).parent().find('._2_ryRS').addClass("_3_6wHc");
    } else {
        $(this).parent().find('._2_ryRS').removeClass("_3_6wHc");
    }
    if ($(this).val()) {
        $(this).parent().find('._1cbrzd').addClass("_72ioSN");
    } else {
        $(this).parent().find('._1cbrzd').removeClass("_72ioSN");
    }

});

$(document).ready(function () {
    ma5menu({
        menu: '.site-menu',
        activeClass: 'active',
        footer: '#ma5menu-tools',
        position: 'left',
        closeOnBodyClick: true
    });
});


$(window).load(function () {
    $('.flexslider').flexslider({
        animation: "slide",
        controlNav: "thumbnails"
    });
});

$(document).ready(function () {
    $("._32l7f0").click(function () {
        $(".rvsx1l-1").addClass("information");
    });
    $(".closebtn").click(function () {
        $(".sidenav").removeClass("mainbar");
    });
    $(".mobile-btn").click(function () {
        $("#container").removeClass("mobile-res");
    });
    $("._1ELwxS").click(function () {
        $("#container").removeClass("mobile-res");
    });
});