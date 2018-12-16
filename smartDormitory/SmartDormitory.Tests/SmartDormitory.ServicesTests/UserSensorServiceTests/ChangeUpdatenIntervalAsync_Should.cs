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
    public class ChangeUpdatenIntervalAsync_Should
    {

        private DbContextOptions<SmartDormitoryDbContext> contextOptions;

        [TestMethod]
        public async Task ThrowException_When_SensorIsNotFound()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var sensorId = 0;
            int updateInterval = 2;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userSensorService.ChangeUpdatenIntervalAsync(sensorId, updateInterval));
            }
        }

        [TestMethod]
        public async Task Change_UpdateInterval_When_Invoked()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var sensorId = 1;
            int updateInterval = 2;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensor = new UserSensors
                {
                    Id = 1,
                    UpdateInterval = 33
                };

                assertContext.UserSensors.Add(userSensor);
                assertContext.SaveChanges();

                var userSensorService = new UserSensorService(assertContext);
                await userSensorService.ChangeUpdatenIntervalAsync(sensorId, updateInterval);

                Assert.AreEqual(updateInterval, userSensor.UpdateInterval);
            }
        }
    }
}
