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
    public class ChangeIsRequiredNotificationAsync_Should
    {
        private DbContextOptions<SmartDormitoryDbContext> contextOptions;

        [TestMethod]
        public async Task ThrowException_When_SensorIsNotFound()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var sensorId = 0;
            bool isRequiredNotification = true;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userSensorService.ChangeIsRequiredNotificationAsync(sensorId, isRequiredNotification));
            }
        }

        [TestMethod]
        public async Task Change_IsRequiredNotification_When_Invoked()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var sensorId = 1;
            bool isRequiredNotification = false;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensor = new UserSensors
                {
                    Id = 1,
                    IsRequiredNotification = true
                };

                assertContext.UserSensors.Add(userSensor);
                assertContext.SaveChanges();

                var userSensorService = new UserSensorService(assertContext);
                await userSensorService.ChangeIsRequiredNotificationAsync(sensorId, isRequiredNotification);

                Assert.AreEqual(isRequiredNotification, userSensor.IsRequiredNotification);
            }
        }
    }
}
