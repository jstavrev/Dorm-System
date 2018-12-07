using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Web.Models
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {

        }

        public IndexViewModel(UserSensors usersensors)
        {
            this.SensorName = usersensors.Name;
            this.Longitude = usersensors.Longitude;
            this.Longitude = usersensors.Latitude;
        }

        public string SensorName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

    }
}
