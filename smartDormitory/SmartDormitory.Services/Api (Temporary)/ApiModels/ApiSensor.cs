using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDormitory.Services.ApiModels
{
    public class ApiSensor
    {
        public string sensorId { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public int MinPollingIntervalInSeconds { get; set; }

        public string MeasureType { get; set; }

        public string Value { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
