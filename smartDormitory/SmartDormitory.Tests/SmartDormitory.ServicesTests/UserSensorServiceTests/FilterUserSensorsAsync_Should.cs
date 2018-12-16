using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitory.Data.Data;
using SmartDormitory.Services.Services;
using System;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.SmartDormitory.ServicesTests.UserSensorServiceTests
{
    [TestClass]
    public class FilterUserSensorsAsync_Should
    {
        [TestMethod]
        public async Task ThrowArgumentNullException_WhenPassedNullSortOrder()
        {
            // Arrange
            Mock<SmartDormitoryDbContext> ContextMock = new Mock<SmartDormitoryDbContext>();

            string userId = null;
            string invalidSortOrder = string.Empty;
            string validFilter = string.Empty;
            int validPageNumber = 1;
            int validPageSize = 10;

            UserSensorService SUT = new UserSensorService(
                ContextMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => SUT.FilterUserSensorsAsync(userId, invalidSortOrder, validFilter, validPageNumber, validPageSize));
        }
    }
}
