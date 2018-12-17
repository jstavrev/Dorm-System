using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SmartDormitory.Models.DbModels
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] AvatarImage { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public ICollection<UserSensors> UserSensor { get; set; }

    }
}
