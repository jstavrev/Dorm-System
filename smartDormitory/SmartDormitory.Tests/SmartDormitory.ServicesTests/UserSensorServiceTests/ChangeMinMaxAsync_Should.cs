using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Services;
using System;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.SmartDormitory.ServicesTests.UserSensorServiceTests
{
    [TestClass]
    public class ChangeMinMaxAsync_Should
    {
        private DbContextOptions<SmartDormitoryDbContext> contextOptions;

        [TestMethod]
        public async Task ThrowException_When_UserSensorIsNotFound()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var sensorId = 0;
            var min = 0;
            var max = 0;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userSensorService.ChangeMinMaxAsync(sensorId, min, max));
            }
        }

        [TestMethod]
        public async Task ThrowException_When_MinIsGreaterThanMax()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var sensorId = 0;
            var min = 3;
            var max = 2;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await userSensorService.ChangeMinMaxAsync(sensorId, min, max));
            }
        }

        [TestMethod]
        public async Task ThrowException_When_MinIsLessThanSensorMin()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var ApiID = Guid.NewGuid().ToString();
            using (var arrangeContext = new SmartDormitoryDbContext(contextOptions))
            {
                var sensorForDB = new Sensor
                {
                    Id = 1,
                    ApiId = ApiID,
                    CurrentValue = 10,
                    Description = "Description",
                    Name = "Name",
                    MinValue = 1,
                    MaxValue = 100,
                    MinPollingIntervalInSeconds = 60,
                    SensorTypeId = 1,
                    LastUpdate = DateTime.Now,
                };

                var userSensorForDB = new UserSensors
                {
                    Id = 1,
                    UserMinValue = 1,
                    UserMaxValue = 100,
                    SensorId = 1
                };

                arrangeContext.UserSensors.Add(userSensorForDB);
                arrangeContext.Sensors.Add(sensorForDB);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await userSensorService.ChangeMinMaxAsync(1, 0, 1));
            }
        }

        [TestMethod]
        public async Task Change_Min_When_Invoked()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var min = 1;
            var max = 1;

            var ApiID = Guid.NewGuid().ToString();
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var sensorForDB = new Sensor
                {
                    Id = 1,
                    ApiId = ApiID,
                    CurrentValue = 10,
                    Description = "Description",
                    Name = "Name",
                    MinValue = 1,
                    MaxValue = 100,
                    MinPollingIntervalInSeconds = 60,
                    SensorTypeId = 1,
                    LastUpdate = DateTime.Now,
                };

                var userSensorForDB = new UserSensors
                {
                    Id = 1,
                    UserMinValue = 1,
                    UserMaxValue = 100,
                    SensorId = 1
                };

                assertContext.Sensors.Add(sensorForDB);
                assertContext.UserSensors.Add(userSensorForDB);
                var userSensorService = new UserSensorService(assertContext);
                await userSensorService.ChangeMinMaxAsync(1, min, max);
            }
        }


        [TestMethod]
        public async Task Change_Max_When_Invoked()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var min = 2;
            var max = 2;

            var ApiID = Guid.NewGuid().ToString();
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var sensorForDB = new Sensor
                {
                    Id = 1,
                    ApiId = ApiID,
                    CurrentValue = 10,
                    Description = "Description",
                    Name = "Name",
                    MinValue = 1,
                    MaxValue = 100,
                    MinPollingIntervalInSeconds = 60,
                    SensorTypeId = 1,
                    LastUpdate = DateTime.Now,
                };

                var userSensorForDB = new UserSensors
                {
                    Id = 1,
                    UserMinValue = 1,
                    UserMaxValue = 100,
                    SensorId = 1
                };

                assertContext.Sensors.Add(sensorForDB);
                assertContext.UserSensors.Add(userSensorForDB);
                var userSensorService = new UserSensorService(assertContext);
                await userSensorService.ChangeMinMaxAsync(1, min, max);

                Assert.AreEqual(max, userSensorForDB.UserMaxValue);
            }
        }

        [TestMethod]
        public async Task ThrowException_When_SensorIsNotFound()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var ApiID = Guid.NewGuid().ToString();
            using (var arrangeContext = new SmartDormitoryDbContext(contextOptions))
            {
                var sensorForDB = new Sensor
                {
                    Id = 11,
                    ApiId = ApiID,
                    CurrentValue = 10,
                    Description = "Description",
                    Name = "Name",
                    MinValue = 1,
                    MaxValue = 100,
                    MinPollingIntervalInSeconds = 60,
                    SensorTypeId = 1,
                    LastUpdate = DateTime.Now,
                };

                var userSensorForDB = new UserSensors
                {
                    Id = 1,
                    UserMinValue = 1,
                    UserMaxValue = 100
                };

                arrangeContext.UserSensors.Add(userSensorForDB);
                arrangeContext.Sensors.Add(sensorForDB);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userSensorService.ChangeMinMaxAsync(1, 0, 1));
            }
        }
    }
}
