using SmartDormitory.Services.ApiModels;
using SmartDormitory.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using SmartDormitory.Services.Api.ApiModels;
using SmartDormitory.Services.Api__Temporary_.Providers;

namespace SmartDormitory.Services.Services
{
    public class ApiService : IApiService
    {
        private ApiHelper apiHelper;
        private const string getAll = "sensor/all";

        public ApiService(ApiHelper apiHelper)
        {
            this.apiHelper = apiHelper;
        }

        public async Task<IEnumerable<ApiSensor>> GetAll()
        {
            List<ApiSensor> sensors = new List<ApiSensor>();
            HttpResponseMessage response = await apiHelper.client.GetAsync(getAll);

            if (response.IsSuccessStatusCode)
            {
                sensors = JsonConvert.DeserializeObject<List<ApiSensor>>(await response.Content.ReadAsStringAsync());
            }

            for (int i = 0; i < sensors.Count; i++)
            {
                await this.GetById(sensors[i], sensors[i].sensorId);
            }

            return sensors;
        }

        public async Task<ApiSensor> GetById(ApiSensor sensor, string id)
        {
            HttpResponseMessage response = await apiHelper.client.GetAsync($"sensor/{sensor.sensorId}");
            SensorData sD = new SensorData();

            if (response.IsSuccessStatusCode)
            {
                sD = JsonConvert.DeserializeObject<SensorData>(await response.Content.ReadAsStringAsync());
            }

            sensor.Value = sD.Value;
            sensor.TimeStamp = sD.TimeStamp;

            return sensor;
        }
    }
}
