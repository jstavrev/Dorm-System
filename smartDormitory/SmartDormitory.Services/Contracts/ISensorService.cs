using SmartDormitory.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace SmartDormitory.Services.Contracts
{
    public interface ISensorService
    {
        IEnumerable<Sensor> GetAll();

        void RegisterSensor();

        void EditSensor();

        void DeleteSensor();

        IEnumerable<SensorTypes> GetAllTypes();

        Task<IPagedList<UserSensors>> FilterUserSensorsAsync(string userId, string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10);

        Task<UserSensors> FindAsync(int sensorId);

        Task<UserSensors> ChangeCoordinatesAsync(int sensorId, string longitude, string latitude);

        Task<UserSensors> ChangeMinMaxAsync(int sensorId, int min, int max);

        Task<UserSensors> ChangeIsPublic(int sensorId, bool isPublic);
    }
}
