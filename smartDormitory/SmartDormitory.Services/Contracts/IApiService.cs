using SmartDormitory.Services.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDormitory.Services.Contracts
{
    public interface IApiService
    {
        ICollection<ApiSensor> GetAll();

        ApiSensor GetById(string id);
    }
}
