using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Web.Areas.Administration.Models
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel(User user, bool isAdmin)
        {
            this.Id = user.Id;
            this.IsAdmin = isAdmin;
        }

        public string Id { get; set; }

        public bool IsAdmin { get; set; }
    }
}
