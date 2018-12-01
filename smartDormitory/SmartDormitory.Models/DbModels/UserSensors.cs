using SmartDormitory.Data.Data;

namespace SmartDormitory.Models.DbModels
{
    public class UserSensors
    {
        public User User { get; set; }

        public string UserId { get; set; }

        public Sensor Sensor { get; set; }

        public int SensorId { get; set; }

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
