

function myFunction() {
//fixed header, menu when scroll
if (true) {
    if ($("#main > header.header").length > 0) {
        var $header = $("#main > header.header");
        $(window).scroll(function (event) {
            var scroll = $(window).scrollTop();
            if (scroll > $header.height()) {
                $header.addClass('fixed-header');
            }
            else {
                $header.removeClass('fixed-header');
            }
        });
    }
}

// xử lý khi sản phẩm quá nhiều trong một hàng sẽ xuất hiện thanh trượt
$(document).ready(function () {
    if ($(window).width() > 767) {
        $('.ega-products-owl-carousel').each(function () {
            var count = $(this).data("count");
            var url = $(this).data("url");
            var $this = $(this);
            $(this).owlCarousel({
                items: 6,
                slideBy: 6,
                dots: false,
                nav: true,
                navText: ["<span class='glyphicon glyphicon-menu-left'></span>", "<span class='glyphicon glyphicon-menu-right'></span>"]
                ,
                onInitialized: function (ev) {

                    $this.find(".owl-next").attr("data-count", count).attr('data-url', url);
                    $this.find(".owl-prev").attr("data-count", count);
                }

            });

        });
    }
})

}