﻿$(document).ready(function () {

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {

                var img = document.getElementById("image_preview");
                img.src = e.target.result;

            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("input[type=file]").change(function () {
        readURL(this);
    });

});

function test() {
    console.log("Hello World!");
}

// Show more
$('#more').click(function () {
    var text = $(this).prev('.text');
    text.toggleClass('summary');
    if (text.hasClass('summary')) {
        $(this).text('more');
    } else {
        $(this).text('less');
    }
});

// FilterBy
function FilterBy(val) {

    if (val != 0) {
        window.location.href = "/Shop/Index?filterBy=" + val;
    } else {
        window.location.href = "/Shop/Index";
    }
}

function TopSellingFilterBy(val) {

    if (val != 0) {
        //window.location.href = "/Home/Index?TopSellingFilterBy=" + val;
        insertParam('TopSellingFilterBy', val);
    } else {
        //window.location.href = "/Home/Index";
        insertParam('TopSellingFilterBy', '0');
    }
}

function SearchProduct() {
    var g = $('#product_list').val();
    var id = $('#products').find('option').filter(function () { return $.trim($(this).text()) === g; }).attr('data-value');
    //alert(id);

    insertParam('SelectedProduct', id);
}

function insertParam(key, value) {
    key = encodeURI(key); value = encodeURI(value);

    var kvp = document.location.search.substr(1).split('&');

    var i = kvp.length; var x; while (i--) {
        x = kvp[i].split('=');

        if (x[0] == key) {
            x[1] = value;
            kvp[i] = x.join('=');
            break;
        }
    }

    if (i < 0) { kvp[kvp.length] = [key, value].join('='); }

    //this will reload the page, it's likely better to store this until finished
    document.location.search = kvp.join('&');
}

// Auto fading alert
$(".alert").fadeTo(3000, 500).slideUp(500, function(){
    $(".alert").slideUp(500);
});

// Contact form message length script
$(document).ready(function () {
    $('#characterLeft').text('250 characters left');
    $('#Message').keyup(function () {
        var max = 250;
        var len = $(this).val().length;
        if (len >= max) {
            $('#characterLeft').text('You have reached the limit');
            $('#characterLeft').addClass('red');
            $('#btnSubmit').addClass('disabled');
        }
        else {
            var ch = max - len;
            $('#characterLeft').text(ch + ' characters left');
            //$('#btnSubmit').removeClass('disabled');
            $('#characterLeft').removeClass('red');
        }
    });
});


/**
 * Clearable text inputs
 */
$(".clearable").each(function () {

    var $inp = $(this).find("input:text"),
        $cle = $(this).find(".clearable__clear");

    $inp.on("input", function () {
        $cle.toggle(!!this.value);
    });

    $cle.on("touchstart click", function (e) {
        e.preventDefault();
        $inp.val("").trigger("input");
    });

});