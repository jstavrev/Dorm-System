using SmartDormitory.Services.ApiModels;
using System.Collections.Generic;

namespace SmartDormitory.Web.Areas.Administration.Models
{
    public class AdminIndexViewModel
    {
        public IEnumerable<ApiSensor> sensors;

        public AdminIndexViewModel(IEnumerable<ApiSensor> sensors)
        {
            this.sensors = sensors;
        }
    }
}
