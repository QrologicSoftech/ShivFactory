$(document).ready(function(){
  $(".top-toggle").click(function(){
    $(".sidenav").toggleClass("mainbar");
  });
  $(".closebtn").click(function(){
    $(".sidenav").removeClass("mainbar");
  });
  $(".mobile-btn").click(function(){
    $("#container").toggleClass("mobile-res");
  });
  $("._1ELwxS").click(function(){
    $("#container").removeClass("mobile-res");
  });
});

window.onscroll = function() {myFunction()};

var header = document.getElementById("myHeader");
var sticky = header.offsetTop;

function myFunction() {
  if (window.pageYOffset > sticky) {
    header.classList.add("sticky");
  } else {
    header.classList.remove("sticky");
  }
}

$(document).ready(function(){
  $(".click-open").click(function(){
    $("._2hriZF ").toggleClass("_2rbIyg");
  });
  $("._2doB4z").click(function(){
    $("._2hriZF").removeClass("_2rbIyg");
  });
});

$('.IiD88i input').keyup(function(){
    if($(this).val()){
    $(this).parent().find('._36T8XR').addClass("_3umUoc");
  }else{
    $(this).parent().find('._36T8XR').removeClass("_3umUoc");
  }
  if($(this).val()){
    $(this).parent().find('._1fqY3P').addClass("_3umUoc");
  }else{
    $(this).parent().find('._1fqY3P').removeClass("_3umUoc");
  }
  if($(this).val()){
    $(this).parent().find('.enter-number').addClass("_1Z69nn");
  }else{
    $(this).parent().find('.enter-number').removeClass("_1Z69nn");
  }

  if($(this).val()){
    $(this).parent().find('._1_wZ0r').addClass("show-number");
  }else{
    $(this).parent().find('._1_wZ0r').removeClass("show-number");
  }

  if($(this).val()){
    $(this).parent().find('._2_ryRS').addClass("_3_6wHc");
  }else{
    $(this).parent().find('._2_ryRS').removeClass("_3_6wHc");
  }

});

  $('._3xj6pt input').keyup(function(){
    
  if($(this).val()){
    $(this).parent().find('._2_ryRS').addClass("_3_6wHc");
  }else{
    $(this).parent().find('._2_ryRS').removeClass("_3_6wHc");
  }
   if($(this).val()){
    $(this).parent().find('._1cbrzd').addClass("_72ioSN");
  }else{
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

  jQuery(".demo").hover(function(){
  jQuery('.full-img-2 .images-1-0').addClass("hover-1");
});
  jQuery(".demo-6").hover(function(){
  jQuery('.full-img-2 .images-1-5').addClass("hover-1");
});
   jQuery(".demo-7").hover(function(){
  jQuery('.full-img-2 .images-1-6').addClass("hover-1");
});
    jQuery(".demo-8").hover(function(){
  jQuery('.full-img-2 .images-1-7').addClass("hover-1");
});
     jQuery(".demo-9").hover(function(){
  jQuery('.full-img-2 .images-1-8').addClass("hover-1");
});

  jQuery(".demo-6").hover(function(){
  jQuery('.full-img-2 .images-1-0').removeClass("hover-1");
});
  jQuery(".demo-7").hover(function(){
  jQuery('.full-img-2 .images-1-5').removeClass("hover-1");
});
  jQuery(".demo-8").hover(function(){
  jQuery('.full-img-2 .images-1-6').removeClass("hover-1");
});
  jQuery(".demo-9").hover(function(){
  jQuery('.full-img-2 .images-1-7').removeClass("hover-1");
});
  /*jQuery(".demo-5").hover(function(){
  jQuery('.full-img-2 .images-1-4').removeClass("hover-1");
});*/

  jQuery(".demo-6").hover(function(){
  jQuery('.full-img-2 .images-1-0').removeClass("hover-1");
});
  jQuery(".demo-7").hover(function(){
  jQuery('.full-img-2 .images-1-0').removeClass("hover-1");
});
  jQuery(".demo-8").hover(function(){
  jQuery('.full-img-2 .images-1-0').removeClass("hover-1");
});
  jQuery(".demo-9").hover(function(){
  jQuery('.full-img-2 .images-1-0').removeClass("hover-1");
});

  jQuery(".demo").hover(function(){
  jQuery('.full-img-2 .images-1-5').removeClass("hover-1");
});
  jQuery(".demo-7").hover(function(){
  jQuery('.full-img-2 .images-1-5').removeClass("hover-1");
});
  jQuery(".demo-8").hover(function(){
  jQuery('.full-img-2 .images-1-5').removeClass("hover-1");
});
  jQuery(".demo-9").hover(function(){
  jQuery('.full-img-2 .images-1-5').removeClass("hover-1");
});

  jQuery(".demo").hover(function(){
  jQuery('.full-img-2 .images-1-6').removeClass("hover-1");
});
  jQuery(".demo-8").hover(function(){
  jQuery('.full-img-2 .images-1-6').removeClass("hover-1");
});
  jQuery(".demo-6").hover(function(){
  jQuery('.full-img-2 .images-1-6').removeClass("hover-1");
});
  jQuery(".demo-9").hover(function(){
  jQuery('.full-img-2 .images-1-6').removeClass("hover-1");
});

  jQuery(".demo").hover(function(){
  jQuery('.full-img-2 .images-1-7').removeClass("hover-1");
});
  jQuery(".demo-7").hover(function(){
  jQuery('.full-img-2 .images-1-7').removeClass("hover-1");
});
  jQuery(".demo-6").hover(function(){
  jQuery('.full-img-2 .images-1-7').removeClass("hover-1");
});
  jQuery(".demo-9").hover(function(){
  jQuery('.full-img-2 .images-1-7').removeClass("hover-1");
});

  jQuery(".demo").hover(function(){
  jQuery('.full-img-2 .images-1-8').removeClass("hover-1");
});
  jQuery(".demo-7").hover(function(){
  jQuery('.full-img-2 .images-1-8').removeClass("hover-1");
});
  jQuery(".demo-6").hover(function(){
  jQuery('.full-img-2 .images-1-8').removeClass("hover-1");
});
  jQuery(".demo-8").hover(function(){
  jQuery('.full-img-2 .images-1-8').removeClass("hover-1");
});

  jQuery(".demo").hover(function(){
  jQuery('.color-text-1').addClass("color-hover");
});
  jQuery(".demo-6").hover(function(){
  jQuery('.color-text-6').addClass("color-hover");
});
   jQuery(".demo-7").hover(function(){
  jQuery('.color-text-7').addClass("color-hover");
});
    jQuery(".demo-8").hover(function(){
  jQuery('.color-text-8').addClass("color-hover");
});
     jQuery(".demo-9").hover(function(){
  jQuery('.color-text-9').addClass("color-hover");
});

    
  jQuery(".demo-6").hover(function(){
  jQuery('.color-text-1').removeClass("color-hover");
});
  jQuery(".demo-7").hover(function(){
  jQuery('.color-text-1').removeClass("color-hover");
});
  jQuery(".demo-8").hover(function(){
  jQuery('.color-text-1').removeClass("color-hover");
});
  jQuery(".demo-9").hover(function(){
  jQuery('.color-text-1').removeClass("color-hover");
});

  jQuery(".demo").hover(function(){
  jQuery('.color-text-6').removeClass("color-hover");
});
  jQuery(".demo-7").hover(function(){
  jQuery('.color-text-6').removeClass("color-hover");
});
  jQuery(".demo-8").hover(function(){
  jQuery('.color-text-6').removeClass("color-hover");
});
  jQuery(".demo-9").hover(function(){
  jQuery('.color-text-6').removeClass("color-hover");
});

  jQuery(".demo").hover(function(){
 jQuery('.color-text-7').removeClass("color-hover");
});
  jQuery(".demo-6").hover(function(){
  jQuery('.color-text-7').removeClass("color-hover");
});
  jQuery(".demo-8").hover(function(){
  jQuery('.color-text-7').removeClass("color-hover");
});
  jQuery(".demo-9").hover(function(){
  jQuery('.color-text-7').removeClass("color-hover");
});
  jQuery(".demo").hover(function(){
  jQuery('.color-text-8').removeClass("color-hover");
});
  jQuery(".demo-7").hover(function(){
  jQuery('.color-text-8').removeClass("color-hover");
});
  jQuery(".demo-6").hover(function(){
  jQuery('.color-text-8').removeClass("color-hover");
});
  jQuery(".demo-9").hover(function(){
  jQuery('.color-text-8').removeClass("color-hover");
});
  jQuery(".demo").hover(function(){
  jQuery('.color-text-9').removeClass("color-hover");
});
  jQuery(".demo-6").hover(function(){
  jQuery('.color-text-9').removeClass("color-hover");
});
  jQuery(".demo-7").hover(function(){
  jQuery('.color-text-9').removeClass("color-hover");
});
  jQuery(".demo-8").hover(function(){
  jQuery('.color-text-9').removeClass("color-hover");
});

  jQuery(".demo").hover(function(){
  jQuery('.full-img-2 .images-1-0').addClass("hover-1");
});
  jQuery(".demo-1").hover(function(){
  jQuery('.full-img-2 .images-1-1').addClass("hover-1");
});
   jQuery(".demo-2").hover(function(){
  jQuery('.full-img-2 .images-1-2').addClass("hover-1");
});
    jQuery(".demo-3").hover(function(){
  jQuery('.full-img-2 .images-1-3').addClass("hover-1");
});
     jQuery(".demo-4").hover(function(){
  jQuery('.full-img-2 .images-1-4').addClass("hover-1");
});

  jQuery(".demo-1").hover(function(){
  jQuery('.full-img-2 .images-1-0').removeClass("hover-1");
});
  jQuery(".demo-2").hover(function(){
  jQuery('.full-img-2 .images-1-1').removeClass("hover-1");
});
  jQuery(".demo-3").hover(function(){
  jQuery('.full-img-2 .images-1-2').removeClass("hover-1");
});
  jQuery(".demo-4").hover(function(){
  jQuery('.full-img-2 .images-1-3').removeClass("hover-1");
});
  /*jQuery(".demo-5").hover(function(){
  jQuery('.full-img-2 .images-1-4').removeClass("hover-1");
});*/

  jQuery(".demo-1").hover(function(){
  jQuery('.full-img-2 .images-1-0').removeClass("hover-1");
});
  jQuery(".demo-2").hover(function(){
  jQuery('.full-img-2 .images-1-0').removeClass("hover-1");
});
  jQuery(".demo-3").hover(function(){
  jQuery('.full-img-2 .images-1-0').removeClass("hover-1");
});
  jQuery(".demo-4").hover(function(){
  jQuery('.full-img-2 .images-1-0').removeClass("hover-1");
});

  jQuery(".demo").hover(function(){
  jQuery('.full-img-2 .images-1-1').removeClass("hover-1");
});
  jQuery(".demo-2").hover(function(){
  jQuery('.full-img-2 .images-1-1').removeClass("hover-1");
});
  jQuery(".demo-3").hover(function(){
  jQuery('.full-img-2 .images-1-1').removeClass("hover-1");
});
  jQuery(".demo-4").hover(function(){
  jQuery('.full-img-2 .images-1-1').removeClass("hover-1");
});

  jQuery(".demo").hover(function(){
  jQuery('.full-img-2 .images-1-2').removeClass("hover-1");
});
  jQuery(".demo-3").hover(function(){
  jQuery('.full-img-2 .images-1-2').removeClass("hover-1");
});
  jQuery(".demo-1").hover(function(){
  jQuery('.full-img-2 .images-1-2').removeClass("hover-1");
});
  jQuery(".demo-4").hover(function(){
  jQuery('.full-img-2 .images-1-2').removeClass("hover-1");
});

  jQuery(".demo").hover(function(){
  jQuery('.full-img-2 .images-1-3').removeClass("hover-1");
});
  jQuery(".demo-2").hover(function(){
  jQuery('.full-img-2 .images-1-3').removeClass("hover-1");
});
  jQuery(".demo-1").hover(function(){
  jQuery('.full-img-2 .images-1-3').removeClass("hover-1");
});
  jQuery(".demo-4").hover(function(){
  jQuery('.full-img-2 .images-1-3').removeClass("hover-1");
});

  jQuery(".demo").hover(function(){
  jQuery('.full-img-2 .images-1-4').removeClass("hover-1");
});
  jQuery(".demo-2").hover(function(){
  jQuery('.full-img-2 .images-1-4').removeClass("hover-1");
});
  jQuery(".demo-1").hover(function(){
  jQuery('.full-img-2 .images-1-4').removeClass("hover-1");
});
  jQuery(".demo-3").hover(function(){
  jQuery('.full-img-2 .images-1-4').removeClass("hover-1");
});

  jQuery(".demo").hover(function(){
  jQuery('.color-text-1').addClass("color-hover");
});
  jQuery(".demo-1").hover(function(){
  jQuery('.color-text-2').addClass("color-hover");
});
   jQuery(".demo-2").hover(function(){
  jQuery('.color-text-3').addClass("color-hover");
});
    jQuery(".demo-3").hover(function(){
  jQuery('.color-text-4').addClass("color-hover");
});
     jQuery(".demo-4").hover(function(){
  jQuery('.color-text-5').addClass("color-hover");
});

    
  jQuery(".demo-1").hover(function(){
  jQuery('.color-text-1').removeClass("color-hover");
});
  jQuery(".demo-2").hover(function(){
  jQuery('.color-text-1').removeClass("color-hover");
});
  jQuery(".demo-3").hover(function(){
  jQuery('.color-text-1').removeClass("color-hover");
});
  jQuery(".demo-4").hover(function(){
  jQuery('.color-text-1').removeClass("color-hover");
});

  jQuery(".demo").hover(function(){
  jQuery('.color-text-2').removeClass("color-hover");
});
  jQuery(".demo-2").hover(function(){
  jQuery('.color-text-2').removeClass("color-hover");
});
  jQuery(".demo-3").hover(function(){
  jQuery('.color-text-2').removeClass("color-hover");
});
  jQuery(".demo-4").hover(function(){
  jQuery('.color-text-2').removeClass("color-hover");
});

  jQuery(".demo").hover(function(){
 jQuery('.color-text-3').removeClass("color-hover");
});
  jQuery(".demo-1").hover(function(){
  jQuery('.color-text-3').removeClass("color-hover");
});
  jQuery(".demo-3").hover(function(){
  jQuery('.color-text-3').removeClass("color-hover");
});
  jQuery(".demo-4").hover(function(){
  jQuery('.color-text-3').removeClass("color-hover");
});

  jQuery(".demo").hover(function(){
  jQuery('.color-text-4').removeClass("color-hover");
});
  jQuery(".demo-2").hover(function(){
  jQuery('.color-text-4').removeClass("color-hover");
});
  jQuery(".demo-1").hover(function(){
  jQuery('.color-text-4').removeClass("color-hover");
});
  jQuery(".demo-4").hover(function(){
  jQuery('.color-text-4').removeClass("color-hover");
});

  jQuery(".demo").hover(function(){
  jQuery('.color-text-5').removeClass("color-hover");
});
  jQuery(".demo-1").hover(function(){
  jQuery('.color-text-5').removeClass("color-hover");
});
  jQuery(".demo-2").hover(function(){
  jQuery('.color-text-5').removeClass("color-hover");
});
  jQuery(".demo-3").hover(function(){
  jQuery('.color-text-5').removeClass("color-hover");
});


  $(document).ready(function(){
  $("._32l7f0").click(function(){
    $(".rvsx1l-1").addClass("information");
  });
  $(".closebtn").click(function(){
    $(".sidenav").removeClass("mainbar");
  });
  $(".mobile-btn").click(function(){
    $("#container").removeClass("mobile-res");
  });
  $("._1ELwxS").click(function(){
    $("#container").removeClass("mobile-res");
  });
});