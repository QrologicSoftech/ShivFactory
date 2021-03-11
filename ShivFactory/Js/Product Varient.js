var productVarient = {

    jQuery(document).ready(function () {
        $("#append").click(function (e) {
            e.preventDefault();
            $(".inc").append('<div class="controls">\
                <input class="form-control" type="text" name="textbox" placeholder="textbox">\
                <input class="form-control" type="text" name="text" placeholder="text">\
                <a href="#" class="remove_this btn btn-danger">remove</a>\
                <br>\
                <br>\
            </div>');
            return false;
        });

        jQuery(document).on('click', '.remove_this', function () {
            jQuery(this).parent().remove();
            return false;
        });
        $("input[type=submit]").click(function (e) {
            e.preventDefault();
            $(this).next("[name=textbox]")
                .val(
                    $.map($(".inc :text"), function (el) {
                        return el.value
                    }).join(",\n")
                )
        })
    });

}


