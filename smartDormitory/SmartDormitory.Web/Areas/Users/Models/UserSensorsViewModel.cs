using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class UserSensorsViewModel
    {
        public UserSensorsViewModel()
        {
        }

        public UserSensorsViewModel(UserSensors sensors)
        {
            this.Id = sensors.Id;
            this.UserId = sensors.UserId;
            this.SensorId = sensors.SensorId;
            this.Longitude = sensors.Longitude;
            this.Latitude = sensors.Latitude;
            this.MinValue = sensors.UserMinValue;
            this.MaxValue = sensors.UserMaxValue;
            this.UpdateInterval = sensors.UpdateInterval;
            this.Name = sensors.Name;
            this.IsPublic = sensors.IsPublic;
            this.IsRequiredNotification = sensors.IsRequiredNotification;
        }

        public string UserId { get; set; }

        public int Id { get; set; }

        public int SensorId { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public int UpdateInterval { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool IsRequiredNotification { get; set; }
    }
}
