
const $dropdown = $(".dropdown");
const $dropdownToggle = $(".dropdown-toggle");
const $dropdownMenu = $(".dropdown-menu");
const showClass = "show";

var menu = {
    AddHover: function (element) {
        element.hover(
            function () {
                const $this = $(this);
                $this.addClass(showClass);
                $this.find(".dropdown-toggle").attr("aria-expanded", "true");
                $this.find(".dropdown-menu").addClass(showClass);
            },
            function () {
                const $this = $(this);
                $this.removeClass(showClass);
                $this.find(".dropdown-toggle").attr("aria-expanded", "false");
                $this.find(".dropdown-menu").removeClass(showClass);
            }
        );
    },
}

$(window).on("load resize", function () {
    if (this.matchMedia("(min-width: 768px)").matches) {
        $dropdown.hover(
            function () {
                const $this = $(this);
                $this.addClass(showClass);
                $this.find($dropdownToggle).attr("aria-expanded", "true");
                $this.find($dropdownMenu).addClass(showClass);
            },
            function () {
                const $this = $(this);
                $this.removeClass(showClass);
                $this.find($dropdownToggle).attr("aria-expanded", "false");
                $this.find($dropdownMenu).removeClass(showClass);
            }
        );
    } else {
        $dropdown.off("mouseenter mouseleave");
    }
});




// some scripts

// jquery ready start
$(document).ready(function () {
    // jQuery code


    //////////////////////// Prevent closing from click inside dropdown
    $(document).on('click', '.dropdown-menu', function (e) {
        e.stopPropagation();
    });




    //////////////////////// Bootstrap tooltip
    if ($('[data-toggle="tooltip"]').length > 0) {  // check if element exists
        $('[data-toggle="tooltip"]').tooltip()
    } // end if





});
// jquery end





$(document).ready(function () {
    var owl = $('.allitem-slider');
    owl.owlCarousel({
        loop: false,
        margin: 15,
        items: 5,
        nav: true,
        autoplay: false,
        smartSpeed: 1800,
        navText: ['<span><i class="fa fa-angle-right"></i></span>', '<span><i class="fa fa-angle-left"></i></span>'],
        responsive: {
            0: {
                items: 2
            },
            430: {
                items: 2
            },
            991: {
                items: 5
            }
        }

    });
})








