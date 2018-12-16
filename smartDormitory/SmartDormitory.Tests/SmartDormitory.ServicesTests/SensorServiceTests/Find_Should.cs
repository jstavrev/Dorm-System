using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Services;
using System;

namespace SmartDormitory.Tests.SmartDormitory.ServicesTests.SensorServiceTests
{
    [TestClass]
    public class Find_Should
    {
        [TestMethod]
        public void FindSensor_When_PassedId_IsValid()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "FindSensor_When_PassedId_IsValid")
               .Options;

            var ApiID = "ApiId";
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

                arrangeContext.Sensors.Add(sensorForDB);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var sensorService = new SensorService(assertContext);
                var sensor = sensorService.Find(1);

                Assert.AreEqual(sensor.ApiId, ApiID);
            }
        }

        [TestMethod]
        public void ThrowArgumentNullExc_When_PassedId_IsInvalid()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ThrowArgumentNullExc_When_PassedId_IsInvalid")
               .Options;

            var ApiID = Guid.NewGuid().ToString();
            using (var arrangeContext = new SmartDormitoryDbContext(contextOptions))
            {
                var sensorForDB = new Sensor
                {
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

                arrangeContext.Sensors.Add(sensorForDB);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var sensorService = new SensorService(assertContext);

                Assert.ThrowsException<ArgumentNullException>(() => sensorService.Find(5));
            }
        }
    }
}
