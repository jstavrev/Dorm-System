using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SmartDormitory.Services.Api__Temporary_.Providers
{
    public class ApiHelper
    {
        public HttpClient client;

        public ApiHelper(HttpClient client)
        {
            this.client = client;
            Setup();
        }

        private void Setup()
        {
            this.client.BaseAddress = new Uri("http://telerikacademy.icb.bg/api/");

            this.client.DefaultRequestHeaders.Add("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
            this.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
