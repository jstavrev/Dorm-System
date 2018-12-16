using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDormitory.Tests.SmartDormitory.ServicesTests.UserSensorServiceTests
{
    [TestClass]
    public class RegisterSensor_Should
    {
        [TestMethod]
        public void SuccesfullyRegisterSensor_WhenValidData_IsPassed()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeEx_When_UserMinValue_IsSmaller_Than_SensorMinValue")
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

                arrangeContext.Sensors.Add(sensorForDB);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);
                var userSensor = userSensorService.RegisterSensor(0, 0, 10, 99, 61,
                    "name", "description", false, false,
                        "0", "userId", "1");

                Assert.IsNotNull(userSensor);
            }
        }

        [TestMethod]
        public void ThrowArgumentOutOfRangeEx_When_UserMinValue_IsBigger_Than_UserMaxValue()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeEx_When_UserMinValue_IsBigger_Than_UserMaxValue")
               .Options;

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                Assert.ThrowsException<ArgumentOutOfRangeException>(() => userSensorService.RegisterSensor(0, 0, 10, 9, 40, 
                    "name", "description", false, false,
                        "0", "userId", "1"));
            }
        }

        [TestMethod]
        public void ThrowArgumentNullExc_When_SensorId_IsNull()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ThrowArgumentNullExc_When_SensorId_IsNull")
               .Options;

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                Assert.ThrowsException<ArgumentNullException>(() => userSensorService.RegisterSensor(0, 0, 10, 99, 40,
                    "name", "description", false, false,
                        "0", "userId", null));
            }
        }

        [TestMethod]
        public void ThrowArgumentOutOfRangeEx_When_UserMinValue_IsSmaller_Than_SensorMinValue()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeEx_When_UserMinValue_IsSmaller_Than_SensorMinValue")
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

                arrangeContext.Sensors.Add(sensorForDB);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                Assert.ThrowsException<ArgumentOutOfRangeException>(() => userSensorService.RegisterSensor(0, 0, 0, 9, 40,
                    "name", "description", false, false,
                        "0", "userId", "1"));
            }
        }

        [TestMethod]
        public void ThrowArgumentOutOfRangeEx_When_UserMaxValue_IsBigger_Than_SensorMaxValue()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeEx_When_UserMaxValue_IsBigger_Than_SensorMaxValue")
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

                arrangeContext.Sensors.Add(sensorForDB);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                Assert.ThrowsException<ArgumentOutOfRangeException>(() => userSensorService.RegisterSensor(0, 0, 10, 101, 40,
                    "name", "description", false, false,
                        "0", "userId", "1"));
            }
        }

        [TestMethod]
        public void ThrowArgumentOutOfRangeEx_When_UserInterval_IsSmaller_Than_SensorPollingInterval()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeEx_When_UserInterval_IsSmaller_Than_SensorPollingInterval")
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

                arrangeContext.Sensors.Add(sensorForDB);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                Assert.ThrowsException<ArgumentOutOfRangeException>(() => userSensorService.RegisterSensor(0, 0, 5, 9, 59,
                    "name", "description", false, false,
                        "0", "userId", "1"));
            }
        }

        [TestMethod]
        public void ThrowArgumentNullExc_When_SensorName_IsNull()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ThrowArgumentNullExc_When_SensorName_IsNull")
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

                arrangeContext.Sensors.Add(sensorForDB);
                arrangeContext.SaveChanges();
            }
                // Act && Asert
                using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                Assert.ThrowsException<ArgumentNullException>(() => userSensorService.RegisterSensor(0, 0, 10, 99, 61,
                    null, "description", false, false,
                        "0", "userId", "1"));
            }
        }

        [TestMethod]
        public void ThrowArgumentNullExc_When_SensorDescription_IsNull()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ThrowArgumentNullExc_When_SensorDescription_IsNull")
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
                var userSensorService = new UserSensorService(assertContext);

                Assert.ThrowsException<ArgumentNullException>(() => userSensorService.RegisterSensor(0, 0, 10, 11, 61,
                    "name", null, false, false,
                        "0", "userId", "1"));
            }
        }

        [TestMethod]
        public void ThrowArgumentNullExc_When_UserId_IsNull()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ThrowArgumentNullExc_When_UserId_IsNull")
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

                arrangeContext.Sensors.Add(sensorForDB);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                Assert.ThrowsException<ArgumentNullException>(() => userSensorService.RegisterSensor(0, 0, 10, 99, 61,
                    "name", "description", false, false,
                        "0", null, "1"));
            }
        }
    }
}
