using OnlineMovieStore.Models.Abstract;
using SmartDormitory.Data.Data;
using System;

namespace SmartDormitory.Models.Models
{
    public class SensorDataHistory : Entity
    {
        public DateTime AddedOn { get; set; }

        public Sensor Sensor { get; set; }

        public int SensorId { get; set; }

        public string Value { get; set; }
    }
}
