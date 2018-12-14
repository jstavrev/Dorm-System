$(function () {
    $('#SensorTypeId').on('change', function (event) {

        let $id = $('#SensorTypeId').val()
        let urlForSensors = 'https://localhost:44398/Users/Sensor/sensorselect?typeId=' + $id;

        $.ajax({
            url: urlForSensors,
            method: "get"
        }).done(function (data) {
            var results = jQuery.parseJSON(JSON.stringify(data));
            $('#SensorId option:gt(0)').remove();
            $.each(results, function (i, l) {
                $("#SensorId").append(new Option(l.name, l.id));
            });
        });
    });
});