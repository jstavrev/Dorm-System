using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class SensorValidationViewModel
    {
        public string Type { get; set; }

        public int UpdateInterval { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }
    }
}
