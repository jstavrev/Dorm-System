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
    public class UserSensorsCount_Should
    {
        [TestMethod]
        public void Return_UserSensorsColletion_WhenUserSensors_IsNotEmpty()
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
                var count = userSensorService.UserSensorsCount();

                Assert.AreEqual(1, count);
            }
        }

        [TestMethod]
        public void Return_EmptyUserSensorsColletion_WhenUserSensors_IsEmpty()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);
                var count = userSensorService.UserSensorsCount();

                Assert.AreEqual(0, count);
            }
        }
    }
}
