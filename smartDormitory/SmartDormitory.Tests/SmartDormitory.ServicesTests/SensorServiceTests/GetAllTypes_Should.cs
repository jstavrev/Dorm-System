using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartDormitory.Tests.SmartDormitory.ServicesTests.SensorServiceTests
{
    [TestClass]
    public class GetAllTypes_Should
    {
        [TestMethod]
        public void ReturnAllSensorTypes_When_Invoked()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ReturnAllSensorTypes_When_Invoked")
               .Options;

            using (var arrangeContext = new SmartDormitoryDbContext(contextOptions))
            {
                var sensorType = new SensorTypes
                {
                    IsDeleted = false,
                    Type = "testType",
                    CreatedOn = DateTime.Now,
                };

                arrangeContext.SensorTypes.Add(sensorType);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var sensorService = new SensorService(assertContext);
                var allSensorTypes = sensorService.GetAllTypes().ToList();

                Assert.AreEqual(1, allSensorTypes.Count);
            }
        }

        [TestMethod]
        public void ReturnEmptyCollection_When_SensorTypesDB_IsEmpty()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ReturnEmptyCollection_When_SensorTypesDB_IsEmpty")
               .Options;

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var sensorService = new SensorService(assertContext);
                var allSensorTypes = sensorService.GetAllTypes().ToList();

                Assert.AreEqual(0, allSensorTypes.Count);
            }
        }
    }
}
