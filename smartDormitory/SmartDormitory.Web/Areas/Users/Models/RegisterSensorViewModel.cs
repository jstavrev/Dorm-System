using Microsoft.AspNetCore.Mvc.Rendering;
using SmartDormitory.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class RegisterSensorViewModel
    {
        public RegisterSensorViewModel(List<Sensor> sensorsSelect, List<SensorTypes> sensorTypes, List<Sensor> sensors)
        {
            this.SensorsSelect = sensorsSelect.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Name}" }).ToList();
            this.SensorTypes = sensorTypes.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Type}" }).ToList();
            this.Sensors = sensors;
        }

        public RegisterSensorViewModel()
        {
            this.SensorTypes = new List<SelectListItem>();
            this.Sensors = new List<Sensor>();
            this.SensorsSelect = new List<SelectListItem>();
        }

        public string UserID { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public int UpdateInterval { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool IsRequiredNotification { get; set; }

        public List<SelectListItem> SensorsSelect { get; set; }

        public List<SelectListItem> SensorTypes { get; set; }

        public List<Sensor> Sensors { get; set; }

        public string SensorId { get; set; }

        public string SensorTypeId { get; set; }

        public string Default { get; set; }
    }
}
