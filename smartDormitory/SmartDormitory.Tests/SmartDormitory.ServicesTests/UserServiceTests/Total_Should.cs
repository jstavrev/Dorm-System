using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Services;
using System;

namespace SmartDormitory.Tests.SmartDormitory.ServicesTests.UserServiceTests
{
    [TestClass]
    public class Total_Should
    {
        private DbContextOptions<SmartDormitoryDbContext> contextOptions;

        [TestMethod]
        public void ReturnUsers_WhenValidData_IsPassed()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            using (var arrangeContext = new SmartDormitoryDbContext(contextOptions))
            {

                var usersForDB = new User
                {
                    Id = "test"
                };

                arrangeContext.Users.Add(usersForDB);
                arrangeContext.SaveChanges();
            }

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userService = new UserService(assertContext);
                var users = userService.Total();
                Assert.AreEqual(1, users);
            }
        }


        [TestMethod]
        public void ReturnUsers_WhenUsers_IsEmpty()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userService = new UserService(assertContext);
                var users = userService.Total();
                Assert.AreEqual(0, users);
            }
        }
    }
}
