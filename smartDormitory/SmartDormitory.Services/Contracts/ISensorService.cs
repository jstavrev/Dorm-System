using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDormitory.Services.Contracts
{
    public interface ISensorService
    {
        void RegisterSensor();

        void EditSensor();

        void DeleteSensor();
    }
}
