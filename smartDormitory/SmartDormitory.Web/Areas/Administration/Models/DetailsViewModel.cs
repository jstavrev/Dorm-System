using SmartDormitory.Data.Data;

namespace SmartDormitory.Web.Areas.Administration.Models
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
        }

        public DetailsViewModel(User user, bool isAdmin)
        {
            this.Id = user.Id;
            this.Username = user.UserName;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.UserRole = new UserRoleViewModel(user, isAdmin);
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public UserRoleViewModel UserRole { get; set; }
    }
}
