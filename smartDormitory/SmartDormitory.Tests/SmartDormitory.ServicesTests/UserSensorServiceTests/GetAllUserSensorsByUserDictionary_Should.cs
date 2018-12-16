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
    public class GetAllUserSensorsByUserDictionary_Should
    {
        [TestMethod]
        public void ReturnUserSensorsDictionary_When_ValidId_IsPassed()
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
                var userSensorsDic = userSensorService.GetAllUserSensorsByUserDictionary(UserId);

                Assert.AreEqual(1, userSensorsDic.Count);
            }
        }

        [TestMethod]
        public void ReturnEmptyUserSensorDictionary_When_InvalidId_IsPassed()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var UserId = Guid.NewGuid().ToString();

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);
                var userSensorsDic = userSensorService.GetAllUserSensorsByUserDictionary(UserId);

                Assert.AreEqual(0, userSensorsDic.Count);
            }
        }

        [TestMethod]
        public void ThrowArgumentNullExc_When_NullId_IsPassed()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            string UserId = null;

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                Assert.ThrowsException<ArgumentNullException>(() => userSensorService.GetAllUserSensorsByUserDictionary(UserId));
            }
        }
    }
}
