var camerasPerRover = {
    "Curiosity": [
        "FHAZ",
        "RHAZ",
        "MAST",
        "CHEMCAM",
        "MAHLI",
        "MARDI",
        "NAVCAM"
    ],
    "Spirit": [
        "FHAZ",
        "RHAZ",
        "NAVCAM",
        "PANCAM",
        "MINITES"
    ],
    "Opportunity": [
        "FHAZ",
        "RHAZ",
        "NAVCAM",
        "PANCAM",
        "MINITES"
    ]
}

$("#Rover").change(function () {
    let roverText = $("#Rover option:selected")[0].text;

    let roverCameras = camerasPerRover[roverText];

    $("#Camera option").each(function (i, option) {
        if (!option.value) {
            return;
        }

        option.hidden = roverCameras.indexOf(option.text) < 0;
    });
})

$('#EarthDate').datepicker({
    dateFormat: "dd-mm-yy"
});

$('.gallery a').simpleLightbox();
