using OnlineMovieStore.Models.Abstract;
using System.Collections.Generic;

namespace SmartDormitory.Models.Models
{
    public class Sensor : Entity
    {
        public string ApiID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MinPollingIntervalInSeconds { get; set; }

        public SensorTypes SensorType { get; set; }

        public ICollection<SensorDataHistory> DataHistory { get; set; }

        public ICollection<UserSensors> UserSensors { get; set; }
    }
}
