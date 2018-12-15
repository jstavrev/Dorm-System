using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Web.Areas.Administration.Models.Sensor
{
    public class RegisterSensorViewModel
    {
        public RegisterSensorViewModel()
        {
        }

        public RegisterSensorViewModel(UserSensors userSensors, string newUserId, string newSensorId)
        {
            this.Id = userSensors.Id;
            this.UserId = userSensors.UserId;
            this.SensorId = userSensors.SensorId;
            this.MinValue = userSensors.MinValue;
            this.MaxValue = userSensors.MaxValue;
            this.UpdateInterval = userSensors.UpdateInterval;
            this.Name = userSensors.Name;
            this.IsPublic = userSensors.IsPublic;
            this.IsRequiredNotification = userSensors.IsRequiredNotification;
            this.Latitude = userSensors.Latitude;
            this.Longitude = userSensors.Longitude;
            this.NewSensorId = newSensorId;
            this.NewUserId = newUserId;
            this.Description = userSensors.Description;
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public int SensorId { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public int UpdateInterval { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool IsRequiredNotification { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string NewUserId { get; set; }

        public string NewSensorId { get; set; }

        public string Default { get; set; }

    }
}
