using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            IsEmpty = true;
        }

        public DashboardViewModel(List<DashboardSensorViewModel> sensors)
        {
            this.Sensors = sensors;
            IsEmpty = false;
        }

        public bool IsEmpty { get; set; }

        public IList<DashboardSensorViewModel> Sensors { get; set; }

        public DateTime LastUpdate { get; set; } = DateTime.Now;
    }
}
