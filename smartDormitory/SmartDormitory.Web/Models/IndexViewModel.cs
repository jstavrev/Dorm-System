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
            this.UserMinValue = usersensors.UserMinValue;
            this.UserMaxValue = usersensors.UserMaxValue;
            this.Value = usersensors.Value;
        }

        public string SensorName { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Owner { get; set; }

        public double UserMinValue { get; set; }

        public double UserMaxValue { get; set; }

        public double Value { get; set; }
    }
}
