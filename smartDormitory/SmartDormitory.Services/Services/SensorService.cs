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

        public IEnumerable<SensorTypes> GetAllTypes()
        {
            return this.context.SensorTypes.ToList();
        }

        public void RegisterSensor()
        {
            throw new NotImplementedException();
        }

        public async Task<IPagedList<UserSensors>> FilterUserSensorsAsync(string userId, string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10)
        {

            var query = this.context.UserSensors
                .Where(t => t.Name.Contains(filter) && t.UserId.Equals(userId));

            switch (sortOrder)
            {
                case "name_asc":
                    query = query.OrderBy(c => c.Name);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(c => c.Name);
                    break;
            }

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<IPagedList<UserSensors>> FilterAllSensorsAsync( string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10)
        {

            var query = this.context.UserSensors.Include(u => u.User);

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<UserSensors> FindAsync(int sensorId)
        {
            var query = await this.context.UserSensors.FindAsync(sensorId);

            return query;
        }

        public async Task<UserSensors> ChangeCoordinatesAsync(int sensorId, double longitude, double latitude)
        {
            var sensor = await this.context.UserSensors.FindAsync(sensorId);

            sensor.Longitude = longitude;
            sensor.Latitude = latitude;
            await context.SaveChangesAsync();

            return sensor;
        }

        public async Task<UserSensors> ChangeMinMaxAsync(int sensorId, double min, double max)
        {
            var sensor = await this.context.UserSensors.FindAsync(sensorId);

            sensor.MinValue = min;
            sensor.MaxValue = max;
            await context.SaveChangesAsync();

            return sensor;
        }

        public async Task<UserSensors> ChangeNameAsync(int sensorId, string name)
        {
            var sensor = await this.context.UserSensors.FindAsync(sensorId);

            sensor.Name = name;
            await context.SaveChangesAsync();

            return sensor;
        }

        public async Task<UserSensors> ChangeIsPublicAsync(int sensorId, bool isPublic)
        {
            var sensor = await this.context.UserSensors.FindAsync(sensorId);

            sensor.IsPublic = isPublic;
            await context.SaveChangesAsync();

            return sensor;
        }

        public async Task<UserSensors> ChangeUpdatenIntervalAsync(int sensorId, int updateInterval)
        {
            var sensor = await this.context.UserSensors.FindAsync(sensorId);

            sensor.UpdateInterval = updateInterval;
            await context.SaveChangesAsync();

            return sensor;
        }

        public async Task<UserSensors> ChangeIsRequiredNotificationAsync(int sensorId, bool isRequiredNotification)
        {
            var sensor = await this.context.UserSensors.FindAsync(sensorId);

            sensor.IsRequiredNotification = isRequiredNotification;
            await context.SaveChangesAsync();

            return sensor;
        }

        public void RegisterSensor(double lng, double lat, double minValue, double maxValue, int updateInterval, string name, string description,
            bool isPublic, bool notification, string defaultPosition, string userId, string sensorId)
        {
            var sensor = Find(int.Parse(sensorId));

            var userSensor = new UserSensors()
            {
                Name = name,
                Description = description,
                UpdateInterval = updateInterval,
                Latitude = lat,
                Longitude = lng,
                IsPublic = isPublic,
                IsRequiredNotification = notification,
                UserId = userId,
                SensorId = int.Parse(sensorId),
                Value = sensor.CurrentValue,
                LastUpdatedOn = sensor.LastUpdate
            };

            if (defaultPosition == null)
            {
                userSensor.MinValue = minValue;
                userSensor.MaxValue = maxValue;
            }
            else
            {
                userSensor.MinValue = int.Parse(defaultPosition);
                userSensor.MaxValue = int.Parse(defaultPosition);
            }

            this.context.UserSensors.Add(userSensor);
            this.context.SaveChanges();
        }

        public Sensor Find(int sensorId)
        {
            return this.context.Sensors.Find(sensorId);
        }
    }
}
