using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Services;
using System;
using System.Linq;

namespace SmartDormitory.Tests.SmartDormitory.ServicesTests.UserServiceTests
{
    [TestClass]
    public class GetAllUsers_Should
    {
        private DbContextOptions<SmartDormitoryDbContext> contextOptions;

        [TestMethod]
        public void ReturnUsers_WhenUsers_IsNotEmpty()
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
                var users = userService.GetAllUsers();
                Assert.IsNotNull(users);
            }
        }

        [TestMethod]
        public void ThrowError_WhenUsers_AreNotFound()
        {
            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            // Act && Asert
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userService = new UserService(assertContext);
                var users = userService.GetAllUsers().ToList();

                Assert.AreEqual(0, users.Count);
            }
        }

    }
}
