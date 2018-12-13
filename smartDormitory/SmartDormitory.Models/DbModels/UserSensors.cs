using SmartDormitory.Data.Data;
using System;

namespace SmartDormitory.Models.DbModels
{
    public class UserSensors
    {
        public int Id { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }

        public Sensor Sensor { get; set; }

        public int SensorId { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public double UserMinValue { get; set; }

        public double UserMaxValue { get; set; }

        public int UpdateInterval { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool IsRequiredNotification { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public double Value { get; set; }
    }
}
