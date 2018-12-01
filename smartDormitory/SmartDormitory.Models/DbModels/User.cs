﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Data.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<SensorDataHistory> DataHistory { get; set; }

        public ICollection<UserSensors> UserSensor { get; set; }

    }
}