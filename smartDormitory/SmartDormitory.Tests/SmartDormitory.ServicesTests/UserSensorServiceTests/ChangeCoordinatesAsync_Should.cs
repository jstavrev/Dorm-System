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
   public class ChangeCoordinatesAsync_Should
    {
        private DbContextOptions<SmartDormitoryDbContext> contextOptions;

        [TestMethod]
        public async Task ThrowException_When_SensorIsNotFound()
        {
             contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var sensorId = 0;
            var longitude = 2;
            var latitude = 3;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userSensorService.ChangeCoordinatesAsync(sensorId, longitude,latitude));
            }
        }

        [TestMethod]
        public async Task Change_Latitude_When_Invoked()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;
            
            var longitude = 2;
            var latitude = 3;


            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensor = new UserSensors
                {
                    Id = 1,
                    Longitude = 2,
                    Latitude = 2
                };

                assertContext.UserSensors.Add(userSensor);
                assertContext.SaveChanges();

                var userSensorService = new UserSensorService(assertContext);
                await userSensorService.ChangeCoordinatesAsync(1, longitude, latitude);

                Assert.AreEqual(latitude, userSensor.Latitude);
            }
        }

        [TestMethod]
        public async Task Change_Longitude_When_Invoked()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var longitude = 3;
            var latitude = 2;


            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensor = new UserSensors
                {
                    Id = 1,
                    Longitude = 2,
                    Latitude = 2
                };

                assertContext.UserSensors.Add(userSensor);
                assertContext.SaveChanges();

                var userSensorService = new UserSensorService(assertContext);
                await userSensorService.ChangeCoordinatesAsync(1, longitude, latitude);

                Assert.AreEqual(longitude, userSensor.Longitude);
            }
        }
    }
}
