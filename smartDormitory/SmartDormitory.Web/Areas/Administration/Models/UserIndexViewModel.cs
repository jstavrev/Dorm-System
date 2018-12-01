using SmartDormitory.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitory.Web.Areas.Administration.Models
{
    public class UserIndexViewModel
    {
        public IEnumerable<User> users;

        public UserIndexViewModel(IEnumerable<User> users)
        {
            this.users = users;
        }

        public UserIndexViewModel()
        {

        }

        public int TotalPages { get; set; }

        public int Page { get; set; } = 1;

        public int PreviousPage => this.Page ==
            1 ? 1 : this.Page - 1;

        public int NextPage => this.Page ==
            this.TotalPages ? this.TotalPages : this.Page + 1;

        public string SearchText { get; set; } = string.Empty;
    }
}
