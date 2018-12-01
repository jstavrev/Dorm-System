using OnlineMovieStore.Models.Abstract;
using System.Collections.Generic;

namespace SmartDormitory.Models.DbModels

{
    public class SensorTypes : Entity
    {
        public string Type { get; set; }

        public ICollection<Sensor> Sensors { get; set; }
    }
}
