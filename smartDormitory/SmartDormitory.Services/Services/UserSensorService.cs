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
    public class UserSensorService : IUserSensorService
    {
        private readonly SmartDormitoryDbContext context;

        public UserSensorService(SmartDormitoryDbContext context)
        {
            this.context = context;
        }

        public async Task<IPagedList<UserSensors>> FilterUserSensorsAsync(string userId, string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10)
        {

            //descending
            var query = this.context.UserSensors
                .Where(t => t.Name.Contains(filter) && t.UserId.Equals(userId)).OrderByDescending(x => x.Id);

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

        public async Task<IPagedList<UserSensors>> FilterAllSensorsAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            //descending
            var query = this.context.UserSensors.Include(u => u.User).OrderByDescending(x => x.Id);

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

            sensor.UserMinValue = min;
            sensor.UserMaxValue = max;
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

        public async Task<UserSensors> ChangeDescriptionAsync(int sensorId, string description)
        {
            var sensor = await this.context.UserSensors.FindAsync(sensorId);

            sensor.Description = description;
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

        public UserSensors RegisterSensor(double lng, double lat, double minValue, double maxValue, int updateInterval, string name, string description,
            bool isPublic, bool notification, string defaultPosition, string userId, string sensorId)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (sensorId == null)
            {
                throw new ArgumentNullException();
            }

            var sensor = this.context.Sensors.Find(int.Parse(sensorId));

            if (minValue < sensor.MinValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (maxValue > sensor.MaxValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (updateInterval < sensor.MinPollingIntervalInSeconds)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (name == null || description == null || userId == null)
            {
                throw new ArgumentNullException();
            }

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
                LastUpdatedOn = sensor.LastUpdate,
                Type = sensor.SensorTypeId
            };

            if (defaultPosition == null)
            {
                userSensor.UserMinValue = minValue;
                userSensor.UserMaxValue = maxValue;
                userSensor.MinValue = sensor.MinValue;
                userSensor.MaxValue = sensor.MaxValue;
            }
            else
            {
                userSensor.MinValue = 0;
                userSensor.MaxValue = 1;
                userSensor.UserMaxValue = int.Parse(defaultPosition);
                userSensor.UserMinValue = int.Parse(defaultPosition);
            }

            this.context.UserSensors.Add(userSensor);
            this.context.SaveChanges();

            return userSensor;
        }

        public IEnumerable<UserSensors> GetAllUserSensorsByUser(string Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException();
            }

            //descending
            return this.context.UserSensors.Where(uS => uS.UserId == Id).OrderByDescending(x => x.Id).ToList();
        }

        public UserSensors GetUserSensorsById(int id)
        {
            return this.context.UserSensors.Where(uS => uS.Id == id).FirstOrDefault();
        }

        public IDictionary<int, UserSensors> GetAllUserSensorsByUserDictionary(string Id)
        {
            var userSensorsList = this.context.UserSensors.Where(uS => uS.UserId == Id).ToList();
            var userSensorsDict = new Dictionary<int, UserSensors>();

            foreach (var userSensor in userSensorsList)
            {
                userSensorsDict[userSensor.Id] = userSensor;
            }

            return userSensorsDict;
        }

        public async Task<IEnumerable<UserSensors>> GetSensorsForMapAsync()
        {
            var query = this.context.UserSensors
                .Where(t => t.IsPublic == true).ToListAsync();

            return await query;
        }
    }
}
