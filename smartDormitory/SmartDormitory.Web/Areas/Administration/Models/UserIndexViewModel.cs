using SmartDormitory.Data.Data;
using SmartDormitory.Web.Models;
using X.PagedList;

namespace SmartDormitory.Web.Areas.Administration.Models
{
    public class UserIndexViewModel
    {

        public UserIndexViewModel(IPagedList<User> users, string sortOrder = "", string searchTerm = "")
        {
            this.Table = new TableViewModel<UserTableViewModel>()
            {
                Items = users.Select(u => new UserTableViewModel(u)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = users.PageCount,
                    PageNumber = users.PageNumber,
                    PageSize = users.PageSize,
                    HasNextPage = users.HasNextPage,
                    HasPreviousPage = users.HasPreviousPage,
                    SearchTerm = searchTerm,
                    SortOrder = sortOrder,
                    AreaRoute = "Administration",
                    ControllerRoute = "User",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<UserTableViewModel> Table { get; set; }
    }
}
