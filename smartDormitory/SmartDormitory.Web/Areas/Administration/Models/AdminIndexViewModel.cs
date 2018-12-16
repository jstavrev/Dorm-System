using System.Collections.Generic;
using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Web.Areas.Administration.Models
{
    public class AdminIndexViewModel
    {
        public IEnumerable<Sensor> sensors;

        public AdminIndexViewModel(IEnumerable<Sensor> sensors)
        {
            this.sensors = sensors;
        }
    }
}
