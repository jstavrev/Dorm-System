using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Services;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.SmartDormitory.ServicesTests.UserServiceTests
{
    [TestClass]
    public class SaveAvatarImageAsync_Should
    {
        private DbContextOptions<SmartDormitoryDbContext> contextOptions;

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedNullStream()
        {

            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            Stream stream = null;
            string userId = "test";

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userService = new UserService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userService.SaveAvatarImageAsync(stream,userId));
            }
        }

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedNullUserId()
        {

            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            string userId = null;
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes("test"));
            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userService = new UserService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userService.SaveAvatarImageAsync(stream, userId));
            }
        }

        [TestMethod]
        public async Task ThrowArgumentNullException_UserNotFound()
        {

            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            string userId = null;
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes("test"));

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userService = new UserService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userService.SaveAvatarImageAsync(stream, userId));
            }
        }
    }
}
