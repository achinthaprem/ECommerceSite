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

// SWITCH

$('.BSswitch').bootstrapSwitch('state', true);


$('#CheckBoxValue').text($("#TheCheckBox").bootstrapSwitch('state'));

$('#TheCheckBox').on('switchChange.bootstrapSwitch', function () {

    $("#CheckBoxValue").text($('#TheCheckBox').bootstrapSwitch('state'));
});

$('.probeProbe').bootstrapSwitch('state', true);

$('.probeProbe').on('switchChange.bootstrapSwitch', function (event, state) {

    alert(this);
    alert(event);
    alert(state);
});

$('#toggleSwitches ').click(function () {
    $('.BSswitch ').bootstrapSwitch('toggleDisabled');
    if ($('.BSswitch ').attr('disabled')) {
        $(this).text('Enable All Switches ');
    } else {
        $(this).text('Disable All Switches ');
    }
});