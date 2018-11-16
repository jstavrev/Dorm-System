﻿using SmartDormitory.Data.Data;

namespace SmartDormitory.Models.Models
{
    public class UserSensors
    {
        public User UserID { get; set; }

        public Sensor SensorId { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public int UpdateInterval { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool IsRequiredNotification { get; set; }
    }
}
