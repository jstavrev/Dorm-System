using SmartDormitory.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace SmartDormitory.Services.Contracts
{
    public interface ISensorService
    {
        IEnumerable<Sensor> GetAll();

        IEnumerable<SensorTypes> GetAllTypes();     

        Sensor Find(int sensorId);
    }
}
