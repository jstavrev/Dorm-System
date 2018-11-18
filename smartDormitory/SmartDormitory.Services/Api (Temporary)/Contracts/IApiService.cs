using SmartDormitory.Services.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDormitory.Services.Contracts
{
    public interface IApiService
    {
        Task<IEnumerable<ApiSensor>> GetAll();

        Task<ApiSensor> GetById(ApiSensor sensor, string id);
    }
}
