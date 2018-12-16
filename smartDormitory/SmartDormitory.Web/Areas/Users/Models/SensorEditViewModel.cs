using SmartDormitory.Models.DbModels;
using SmartDormitory.Web.Areas.Administration.Models.Sensors;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class SensorEditViewModel
    {
        public SensorEditViewModel()
        {

        }

        public SensorEditViewModel(UserSensors userSensors, SensorValidationsViewModel sensorValidations)
        {
            this.Id = userSensors.Id;
            this.UserId = userSensors.UserId;
            this.SensorId = userSensors.SensorId;
            this.MinValue = userSensors.UserMinValue;
            this.MaxValue = userSensors.UserMaxValue;
            this.UserMaxValue = userSensors.UserMaxValue.ToString();
            this.UpdateInterval = userSensors.UpdateInterval;
            this.Name = userSensors.Name;
            this.Description = userSensors.Description;
            this.IsPublic = userSensors.IsPublic;
            this.IsRequiredNotification = userSensors.IsRequiredNotification;
            this.Latitude = userSensors.Latitude;
            this.Longitude = userSensors.Longitude;
            this.SensorValidations = sensorValidations;
            this.TypeId = userSensors.Type;
            if (userSensors.Type == 4)
            {
                this.UserMinValue = userSensors.UserMinValue.ToString();
            }
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public int SensorId { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public int TypeId { get; set; }

        public int UpdateInterval { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string UserMinValue { get; set; }

        public string UserMaxValue { get; set; }

        public bool IsPublic { get; set; }

        public bool IsRequiredNotification { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public SensorValidationsViewModel SensorValidations { get; set; }

        public string Default { get; set; }

    }
}
