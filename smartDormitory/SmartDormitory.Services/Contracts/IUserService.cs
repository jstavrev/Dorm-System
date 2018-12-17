using SmartDormitory.Models.DbModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

namespace SmartDormitory.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        int Total();

        Task SaveAvatarImageAsync(Stream stream, string userId);

        Task<IPagedList<User>> FilterUsersAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10);
    }
}
