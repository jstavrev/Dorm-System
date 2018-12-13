using System;
using System.Collections.Generic;

namespace SmartDormitory.Models.DbModels
{
    public class Sensor
    {
        public int Id { get; set; }

        public string ApiId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public int MinPollingIntervalInSeconds { get; set; }

        public SensorTypes SensorType { get; set; }

        public int SensorTypeId { get; set; }

        public double CurrentValue { get; set; }

        public DateTime LastUpdate { get; set; }

        public ICollection<SensorDataHistory> DataHistory { get; set; }

        public ICollection<UserSensors> UserSensors { get; set; }
    }
}
