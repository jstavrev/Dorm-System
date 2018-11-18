using SmartDormitory.Services.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitory.Web.Areas.API.Models
{
    public class AllSensorsViewModel
    {
        private IEnumerable<ApiSensor> sensors;

        public AllSensorsViewModel(IEnumerable<ApiSensor> sensors)
        {
            this.sensors = sensors;
        }


    }
}
