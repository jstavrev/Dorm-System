using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartDormitory.Services.Services
{
    public class SensorService : ISensorService
    {
        private readonly SmartDormitoryDbContext context;

        public SensorService(SmartDormitoryDbContext context)
        {
            this.context = context;
        }

        public void DeleteSensor()
        {
            throw new NotImplementedException();
        }

        public void EditSensor()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sensor> GetAll()
        {
            return this.context.Sensors.ToList();
        }

        public void RegisterSensor()
        {
            throw new NotImplementedException();
        }
    }
}
