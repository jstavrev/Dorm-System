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
    public class ChangeIsPublicAsync_Should
    {
        private DbContextOptions<SmartDormitoryDbContext> contextOptions;

        [TestMethod]
        public async Task ThrowException_When_SensorIsNotFound()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var sensorId = 0;
            bool isPublic = true;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userSensorService.ChangeIsPublicAsync(sensorId, isPublic));
            }
        }

        [TestMethod]
        public async Task Change_IsPublic_When_Invoked()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var sensorId = 1;
            bool isPublic = false;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensor = new UserSensors
                {
                    Id = 1,
                    IsPublic = true
                };

                assertContext.UserSensors.Add(userSensor);
                assertContext.SaveChanges();

                var userSensorService = new UserSensorService(assertContext);
                await userSensorService.ChangeIsPublicAsync(sensorId, isPublic);

                Assert.AreEqual(isPublic, userSensor.IsPublic);
            }
        }
    }
}
