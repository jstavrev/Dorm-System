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

        Task<UserSensors> ChangeCoordinatesAsync(int sensorId, double longitude, double latitude);

        Task<UserSensors> ChangeMinMaxAsync(int sensorId, double min, double max);

        Task<UserSensors> ChangeIsPublic(int sensorId, bool isPublic);

        Task<UserSensors> ChangeIsRequiredNotification(int sensorId, bool isRequiredNotification);

        void RegisterSensor(double lng, double lat, double minValue, double maxValue, int updateInterval, string name, string description,
            bool isPublic, bool notification, string defaultPosition, string userId, string sensorId);

        Sensor Find(int sensorId);
    }
}
