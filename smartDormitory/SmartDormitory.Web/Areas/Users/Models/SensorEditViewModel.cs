using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class SensorEditViewModel
    {
        public SensorEditViewModel()
        {

        }

        public SensorEditViewModel(UserSensors sensors)
        {
            this.Id = sensors.Id;
            this.Longitude = sensors.Longitude.ToString();
            this.Latitude = sensors.Latitude.ToString();
            this.MinValue = sensors.MinValue;
            this.MaxValue = sensors.MaxValue;
            this.UpdateInterval = sensors.UpdateInterval;
            this.Name = sensors.Name;
            this.IsPublic = sensors.IsPublic;
            this.IsRequiredNotification = sensors.IsRequiredNotification;
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public int SensorId { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public int UpdateInterval { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool IsRequiredNotification { get; set; }
    }
}
