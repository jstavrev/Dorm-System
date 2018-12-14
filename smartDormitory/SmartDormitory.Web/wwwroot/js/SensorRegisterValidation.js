$(function () {
    $('#SensorId').on('change', function (event) {
        let $id = $('#SensorId').val();
        let urlForInfo = '/Users/Sensor/SensorValidationInfo?sensorId=' + $id;
        var currentType;

        $.get({
            url: urlForInfo,
            method: "get",
            dataType: "json"
        }).done(function (data) {
            var result = JSON.parse(JSON.stringify(data));
            currentType = result;
            validator.destroy();
            console.log(1);
            $.get({
                url: "/Users/Sensor/ValidationView?type=" + currentType.type,
                type: 'Get',
                cache: false,
                success: function (response) {
                    updateValidations(currentType.minValue, currentType.maxValue, currentType.updateInterval); 
                    console.log(2);
                    $("#validation").empty();
                    $('#validation').html(response);
                }
            });
        });
    });
});
