using SmartDormitory.Services.ApiModels;
using SmartDormitory.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SmartDormitory.Services.Services
{
    class ApiService : IApiService
    {
        private HttpClient client;

        public ApiService(HttpClient client)
        {
            this.client = client;
        }

        public ICollection<ApiSensor> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApiSensor GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
