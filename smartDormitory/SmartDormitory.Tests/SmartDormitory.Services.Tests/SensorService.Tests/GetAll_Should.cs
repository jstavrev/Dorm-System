using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitory.Data.Data;

namespace SmartDormitory.Tests.SmartDormitory.Services.Tests.SensorService.Tests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void ReturnAllSensors_When_Invoked()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<SmartDormitoryDbContext>()
               .UseInMemoryDatabase(databaseName: "ReturnAllSensors_When_Invoked")
               .Options;

            //Act


            //Assert
        }
    }
}
