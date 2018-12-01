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
        public RegisterSensorViewModel()
        {
            this.Sensors = new List<SelectListItem>();
        }

        public RegisterSensorViewModel(List<Sensor> sensors)
        {
            this.Sensors = sensors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Name}" }).ToList();
        }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public int UpdateInterval { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool IsRequiredNotification { get; set; }

        public IEnumerable<SelectListItem> Sensors { get; set; }

        public string SensorId { get; set; }
    }
}
