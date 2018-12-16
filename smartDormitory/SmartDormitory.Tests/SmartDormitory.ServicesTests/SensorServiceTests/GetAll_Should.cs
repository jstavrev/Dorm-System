using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Services;
using System;
using System.Linq;

namespace SmartDormitory.Tests.SmartDormitory.ServicesTests.SensorServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void ReturnsAllSensors_When_Invoked()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ReturnAllSensors_When_Invoked")
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
                var allSensors = sensorService.GetAll().ToList();

                Assert.AreEqual(1, allSensors.Count);
            }
        }
    }
}
