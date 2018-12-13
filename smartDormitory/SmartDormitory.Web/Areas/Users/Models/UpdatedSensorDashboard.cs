using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class UpdatedSensorDashboard
    {
        public UpdatedSensorDashboard()
        {

        }

        public double Value { get; set; }

        public DateTime LastUpdate { get; set; }

        public int Id { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public double UserMinValue { get; set; }

        public double UserMaxValue { get; set; }
    }
}
