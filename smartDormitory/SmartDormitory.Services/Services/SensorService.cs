using Microsoft.EntityFrameworkCore;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SmartDormitory.Services.Services
{
    public class SensorService : ISensorService
    {
        private readonly SmartDormitoryDbContext context;

        public SensorService(SmartDormitoryDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Sensor> GetAll()
        {
            return this.context.Sensors.ToList();
        }

        public IEnumerable<SensorTypes> GetAllTypes()
        {
            return this.context.SensorTypes.ToList();
        }

        public Sensor Find(int sensorId)
        {
            return this.context.Sensors.Find(sensorId);
        }
    }
}
