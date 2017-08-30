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

