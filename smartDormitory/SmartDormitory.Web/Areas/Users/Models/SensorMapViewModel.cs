using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class SensorMapViewModel
    {
        public SensorMapViewModel()
        {

        }

        public SensorMapViewModel(UserSensors sensors)
        {
            this.Id = sensors.Id;
            this.Longitude = sensors.Longitude;
            this.Latitude = sensors.Latitude;
        }

        public int Id { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

    }
}
