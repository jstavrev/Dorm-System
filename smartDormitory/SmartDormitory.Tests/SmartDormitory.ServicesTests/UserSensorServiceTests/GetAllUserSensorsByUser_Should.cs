using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartDormitory.Tests.SmartDormitory.ServicesTests.UserSensorServiceTests
{
    [TestClass]
    public class GetAllUserSensorsByUser_Should
    {
        [TestMethod]
        public void Return_AllUserSensors_When_UserId_IsValid()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "Return_AllUserSensors_When_UserId_IsValid")
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
                var userSensors = userSensorService.GetAllUserSensorsByUser(UserId).ToList();

                Assert.AreEqual(1, userSensors.Count);
            }
        }

        [TestMethod]
        public void Return_EmptyCollection_When_UserSensorsDB_IsEmpty()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "Return_AllUserSensors_When_UserId_IsValid")
               .Options;

            var UserId = Guid.NewGuid().ToString();            

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);
                var userSensors = userSensorService.GetAllUserSensorsByUser(UserId).ToList();

                Assert.AreEqual(0, userSensors.Count);
            }
        }

        [TestMethod]
        public void ThrowException_When_UserId_IsNull()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "Return_AllUserSensors_When_UserId_IsValid")
               .Options;

            string UserId = null;

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                Assert.ThrowsException<ArgumentNullException>(() => userSensorService.GetAllUserSensorsByUser(UserId));
            }
        }
    }
}
