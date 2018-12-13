using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Web.Areas.Administration.Models.Sensor
{
    public class SensorTableViewModel
    {
        public SensorTableViewModel(UserSensors userSensors)
        {
            this.Id = userSensors.Id;
            this.User = userSensors.User;
            this.UserId = userSensors.UserId;
            this.SensorId = userSensors.SensorId;
            this.Longitude = userSensors.Longitude;
            this.Latitude = userSensors.Latitude;
            this.UpdateInterval = userSensors.UpdateInterval;
            this.Name = userSensors.Name;
            this.Description = userSensors.Description;
            this.IsPublic = userSensors.IsPublic;
            this.IsRequiredNotification = userSensors.IsRequiredNotification;
            this.Value = userSensors.Value;
        }

         public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int SensorId { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public int UpdateInterval { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool IsRequiredNotification { get; set; }

        public double Value { get; set; }
    }
}
