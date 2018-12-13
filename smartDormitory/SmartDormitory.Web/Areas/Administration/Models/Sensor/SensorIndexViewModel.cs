using SmartDormitory.Models.DbModels;
using SmartDormitory.Web.Models;
using System.Linq;
using X.PagedList;

namespace SmartDormitory.Web.Areas.Administration.Models.Sensor
{
    public class SensorIndexViewModel
    {
        public SensorIndexViewModel(IPagedList<UserSensors> userSensors, string sortOrder = "", string searchTerm = "")
        {
            this.Table = new TableViewModel<SensorTableViewModel>()
            {
                Items = userSensors.Select(u => new SensorTableViewModel(u)),
                Pagination = new PaginationViewModel()
                {
                    PageCount = userSensors.PageCount,
                    PageNumber = userSensors.PageNumber,
                    PageSize = userSensors.PageSize,
                    HasNextPage = userSensors.HasNextPage,
                    HasPreviousPage = userSensors.HasPreviousPage,
                    SearchTerm = searchTerm,
                    SortOrder = sortOrder,
                    AreaRoute = "Administration",
                    ControllerRoute = "Sensor",
                    ActionRoute = "Filter"
                }
            };
        }

        public TableViewModel<SensorTableViewModel> Table { get; set; }
    }
}
