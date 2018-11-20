using SmartDormitory.Data.Data;
using SmartDormitory.Models.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartDormitory.Data.Services.Contracts
{
   public  interface IHomeService
    {
        Task<IEnumerable<UserSensors>> FilterSensorsAsync();
    }
}
