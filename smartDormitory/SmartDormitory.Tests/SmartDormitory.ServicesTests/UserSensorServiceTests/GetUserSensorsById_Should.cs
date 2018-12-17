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
    public class GetUserSensorsById_Should
    {
        [TestMethod]
        public void ReturnUserSensor_When_PassedId_IsValid()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var UserId = Guid.NewGuid().ToString();
            using (var arrangeContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensor = new UserSensors
                {
                    Id = 1,
                    UserId = UserId,
                    Value = 10,
                    Description = "Description",
                    Name = "Name",
                    MinValue = 1,
                    MaxValue = 100,
                    UpdateInterval = 60,
                    Type = 1,
                    LastUpdatedOn = DateTime.Now,
                    IsPublic = false,
                    IsRequiredNotification = false,
                    Latitude = 0,
                    Longitude = 0,
                    UserMaxValue = 99,
                    UserMinValue = 2,
                    SensorId = 1
                };

                arrangeContext.UserSensors.Add(userSensor);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);
                var userSensor = userSensorService.GetUserSensorsById(1);

                Assert.AreEqual(userSensor.UserId, UserId);
            }
        }

        [TestMethod]
        public void ReturnNull_When_PassedId_IsInvalid()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);
                var userSensor = userSensorService.GetUserSensorsById(1);

                Assert.IsNull(userSensor);
            }
        }
    }
}
