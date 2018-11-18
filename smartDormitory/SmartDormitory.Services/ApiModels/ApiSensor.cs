using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDormitory.Services.ApiModels
{
    public class ApiSensor
    {
        public string Id { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public int MinPollingInterval { get; set; }

        public string MeasureType { get; set; }

        public double Value { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
