using SmartDormitory.Services.ApiModels;
using SmartDormitory.Services.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartDormitory.Services.Api.ApiModels;
using SmartDormitory.Services.Api__Temporary_.Providers;
using SmartDormitory.Data.Data;
using System.Linq;
using SmartDormitory.Models.DbModels;
using System;

namespace SmartDormitory.Services.Services
{
    public class ApiService : IApiService
    {
        private ApiHelper apiHelper;
        private const string getAll = "sensor/all";
        private SmartDormitoryDbContext context;

        public ApiService(ApiHelper apiHelper, SmartDormitoryDbContext context)
        {
            this.apiHelper = apiHelper;
            this.context = context;
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

            SaveInDB(sensors);
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

            if (sensor.MeasureType == "(true/false)")
            {
                if (sD.Value == "true")
                {
                    sensor.Value = 1;
                }
                else
                {
                    sensor.Value = 0;
                }
            }
            else
            {
                sensor.Value = double.Parse(sD.Value);
            }
            sensor.TimeStamp = DateTime.Now;

            return sensor;
        }

        public void SaveInDB(IEnumerable<ApiSensor> sensors)
        {
            foreach (var sensor in sensors)
            {
                var dbSensor = this.context.Sensors.Where(s => s.ApiId == sensor.sensorId).FirstOrDefault();

                if (dbSensor == null)
                {
                    var type = this.context.SensorTypes.Where(t => t.Type == sensor.MeasureType).FirstOrDefault();

                    if (type == null)
                    {
                        this.context.SensorTypes.Add(new SensorTypes()
                        {
                            Type = sensor.MeasureType
                        });

                        this.context.SaveChanges();
                    }

                    this.context.Add(new Sensor()
                    {
                        ApiId = sensor.sensorId,
                        Name = sensor.Tag,
                        Description = sensor.Description,
                        MinPollingIntervalInSeconds = sensor.MinPollingIntervalInSeconds,
                        CurrentValue = sensor.Value,
                        LastUpdate = sensor.TimeStamp,
                        SensorTypeId = this.context.SensorTypes.Where(t => t.Type == sensor.MeasureType).First().Id
                    });
                }
                else
                {
                    dbSensor.CurrentValue = sensor.Value;
                    dbSensor.LastUpdate = DateTime.Now;
                }

                this.context.SaveChanges();
            }
        }
    }
}