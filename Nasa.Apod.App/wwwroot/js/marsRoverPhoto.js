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

$('#marsRoverInfo').popover({
    placement: "right",
    trigger: "hover",
    title: "Mars Rover Photos",
    content: "This API is designed to collect image data gathered by NASA's Curiosity, Opportunity, and Spirit rovers on Mars and make it more easily available to other developers, educators, and citizen scientists. This API is maintained by Chris Cerami."
});
