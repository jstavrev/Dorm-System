using SmartDormitory.Services.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
