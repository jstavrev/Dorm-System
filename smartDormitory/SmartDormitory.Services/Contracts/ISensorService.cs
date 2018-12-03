using SmartDormitory.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDormitory.Services.Contracts
{
    public interface ISensorService
    {
        IEnumerable<Sensor> GetAll();

        void RegisterSensor();

        void EditSensor();

        void DeleteSensor();

        IEnumerable<SensorTypes> GetAllTypes();
    }
}
