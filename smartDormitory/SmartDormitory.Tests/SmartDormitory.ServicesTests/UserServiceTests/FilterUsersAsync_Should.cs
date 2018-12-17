using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitory.Data.Data;
using SmartDormitory.Services.Services;
using System;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.SmartDormitory.ServicesTests.UserServiceTests
{
    [TestClass]
    public class FilterUsersAsync_Should
    {
        private DbContextOptions<SmartDormitoryDbContext> contextOptions;

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedNullSortOrder()
        {

            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            string sortOrder = null;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userSensorService.FilterUsersAsync(sortOrder, validFilter, validPageNumber, validPageSize));
            }
        }

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedNullFilter()
        {

            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            string sortOrder = string.Empty;
            string validFilter = null;
            int validPageNumber = 1;
            int validPageSize = 10;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await userSensorService.FilterUsersAsync( sortOrder, validFilter, validPageNumber, validPageSize));
            }
        }

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedInvalidPageNumber()
        {

            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            string sortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 0;
            int validPageSize = 10;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await userSensorService.FilterUsersAsync(sortOrder, validFilter, validPageNumber, validPageSize));
            }
        }

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedInvalidPageSize()
        {

            contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            string userId = "test";
            string sortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 0;

            using (var assertContext = new SmartDormitoryDbContext(contextOptions))
            {
                var userSensorService = new UserSensorService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await userSensorService.FilterUserSensorsAsync(userId, sortOrder, validFilter, validPageNumber, validPageSize));
            }
        }
    }
}
