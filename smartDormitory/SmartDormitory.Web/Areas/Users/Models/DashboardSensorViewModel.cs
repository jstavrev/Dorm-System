using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class DashboardSensorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public double Value { get; set; }

        public int UpdateInterval { get; set; }

        public string GraphicalId { get; set; }

        public double UserMinValue { get; set; }

        public double UserMaxValue { get; set; }
    }
}
