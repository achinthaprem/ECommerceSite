$(document).ready(function () {

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
    window.location.href = "/Shop/Index?filterBy=" + val;
}

// Auto fading alert
$(".alert").fadeTo(3000, 500).slideUp(500, function(){
    $(".alert").slideUp(500);
});
