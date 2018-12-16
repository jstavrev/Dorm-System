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
        public void ThrowArgumentOutOfRangeEx_When_MinValue_IsBigger_Than_MaxValue()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeEx_When_MinValue_IsBigger_Than_MaxValue")
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
    }
}
