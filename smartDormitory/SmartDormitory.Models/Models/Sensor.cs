using OnlineMovieStore.Models.Abstract;
using System;
using System.Collections.Generic;

namespace SmartDormitory.Models.Models
{
    public class Sensor : Entity
    {
        public string ApiId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MinPollingIntervalInSeconds { get; set; }

        public SensorTypes SensorType { get; set; }

        public int SensorTypeId { get; set; }

        public double CurrentValue { get; set; }

        public DateTime LastUpdate { get; set; }

        public ICollection<SensorDataHistory> DataHistory { get; set; }

        public ICollection<UserSensors> UserSensors { get; set; }
    }
}
