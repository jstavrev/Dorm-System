using System;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class UpdateDashboardViewModel
    {
        public int Id { get; set; }

        public DateTime LastUpdate { get; set; }

        public double Value { get; set; }
    }
}
