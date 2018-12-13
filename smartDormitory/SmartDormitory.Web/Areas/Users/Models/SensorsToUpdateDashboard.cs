using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class SensorsToUpdateDashboard
    {
        public SensorsToUpdateDashboard()
        {
            this.DashboardSensorsIds = new List<int>();
        }

        public IEnumerable<int> DashboardSensorsIds { get; set; }
    }
}
