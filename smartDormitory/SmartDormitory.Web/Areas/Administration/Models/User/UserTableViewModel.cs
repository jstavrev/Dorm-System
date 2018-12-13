using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using MySensor = SmartDormitory.Models.DbModels.Sensor;

namespace SmartDormitory.Web.Areas.Administration.Models
{
    public class UserTableViewModel
    {
        public UserTableViewModel()
        {
        }

        public UserTableViewModel(List<MySensor> sensorsSelect, List<MySensor> sensor, UserIndexViewModel users)
        {
            this.SensorsSelect = sensorsSelect.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Name}" }).ToList();
            this.Sensor = sensor;
            this.Users = users;
        }
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

        public List<MySensor> Sensor { get; set; }

        public UserIndexViewModel Users { get; set; }

        public string SensorId { get; set; }

        public string SensorTypeId { get; set; }

        public string Default { get; set; }
    }
}
