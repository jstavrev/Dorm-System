using SmartDormitory.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class CreateDashboardViewModel
    {
        public CreateDashboardViewModel()
        {
            this.SensorSelection = new List<CreateDashboardSensorSelectionViewModel>();
        }

        public CreateDashboardViewModel(List<CreateDashboardSensorSelectionViewModel> sensors)
        {
            this.SensorSelection = sensors;
        }

        public IList<CreateDashboardSensorSelectionViewModel> SensorSelection { get; set; }

        public string SearchText { get; set; } = string.Empty;
    }
}
