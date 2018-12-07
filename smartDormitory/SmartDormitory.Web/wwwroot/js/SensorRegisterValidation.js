$(function () {
    $('#SensorId').on('change', function (event) {
        let $id = $('#SensorId').val();
        let urlForInfo = 'https://localhost:44398/Users/Sensor/SensorValidationInfo?sensorId=' + $id;
        var currentType;

        $.ajax({
            url: urlForInfo,
            method: "get",
            dataType: "json"
        }).done(function (data) {
            var result = JSON.parse(JSON.stringify(data));
            currentType = result;
            validator.destroy();

            $.ajax({
                url: "https://localhost:44398/Users/Sensor/ValidationView?type=" + currentType.type,
                type: 'Get',
                cache: false,
                success: function (response) {
                    updateValidations(currentType.minValue, currentType.maxValue, currentType.updateInterval); 

                    $("#validation").empty();
                    $('#validation').html(response);
                }
            });
        });
    });
});
