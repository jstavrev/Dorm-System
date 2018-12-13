using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class CreateDashboardSensorSelectionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public string GraphicalRepresentationId { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }
    }
}
