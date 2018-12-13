using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Web.Areas.Administration.Models
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            this.Id = user.Id;
            this.Username = user.UserName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsAdmin { get; set; }

    }
}
