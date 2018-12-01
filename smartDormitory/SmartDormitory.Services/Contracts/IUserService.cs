using SmartDormitory.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDormitory.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        IEnumerable<User> GetUsersWithPaging(int page = 1, int pageSize = 10);

        IEnumerable<User> GetUsersContainingText(string text, int page = 1, int pageSize = 10);

        int Total();

        int TotalContainingText(string text);
    }
}
