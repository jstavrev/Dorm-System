using MySensor = SmartDormitory.Models.DbModels.Sensor;

namespace SmartDormitory.Web.Areas.Administration.Models.Sensors
{
    public class SensorValidationsViewModel
    {
        public SensorValidationsViewModel()
        {
        }

        public SensorValidationsViewModel(MySensor sensor)
        {
            this.Id = sensor.Id;
            this.MinValue = sensor.MinValue;
            this.MaxValue = sensor.MaxValue;
            this.UpdateInterval = sensor.MinPollingIntervalInSeconds;
            this.TypeId = sensor.SensorTypeId;
        }

        public int Id { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public int TypeId { get; set; }

        public int UpdateInterval { get; set; }

    }
}
