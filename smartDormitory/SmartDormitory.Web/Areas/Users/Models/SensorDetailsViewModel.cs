using SmartDormitory.Models.DbModels;
using SmartDormitory.Web.Models;
using System.Linq;
using X.PagedList;

namespace SmartDormitory.Web.Areas.Users.Models
{
    public class SensorDetailsViewModel
    {
        public SensorDetailsViewModel(IPagedList<UserSensors> sensors, string sortOrder = "", string searchTerm = "")
        {
            this.Table = new TableViewModel<UserSensorsViewModel>()
            {
                Items = sensors.Select(c => new UserSensorsViewModel(c)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = sensors.PageCount,
                    PageNumber = sensors.PageNumber,
                    PageSize = sensors.PageSize,
                    HasNextPage = sensors.HasNextPage,
                    HasPreviousPage = sensors.HasPreviousPage,
                    SortOrder = sortOrder,
                    SearchTerm = searchTerm,
                    AreaRoute = "Users",
                    ControllerRoute = "Sensor",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<UserSensorsViewModel> Table { get; set; }
    }
}
