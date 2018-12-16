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
        public async Task ThrowException_When_SensorIsNotFound()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var sensorId = 0;
            var min = 2;
            var max = 3;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userSensorService.ChangeMinMaxAsync(sensorId, min, max));
            }
        }

        [TestMethod]
        public async Task Change_Min_When_Invoked()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var min = 3;
            var max = 2;


            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensor = new UserSensors
                {
                    Id = 1,
                    UserMinValue = 2,
                    UserMaxValue = 2
                };

                assertContext.UserSensors.Add(userSensor);
                assertContext.SaveChanges();

                var userSensorService = new UserSensorService(assertContext);
                await userSensorService.ChangeMinMaxAsync(1, min, max);

                Assert.AreEqual(min, userSensor.UserMinValue);
            }
        }

        [TestMethod]
        public async Task Change_Max_When_Invoked()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var min = 2;
            var max = 3;


            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensor = new UserSensors
                {
                    Id = 1,
                    UserMinValue = 2,
                    UserMaxValue = 2
                };

                assertContext.UserSensors.Add(userSensor);
                assertContext.SaveChanges();

                var userSensorService = new UserSensorService(assertContext);
                await userSensorService.ChangeMinMaxAsync(1, min, max);

                Assert.AreEqual(max, userSensor.UserMaxValue);
            }
        }
    }
}
