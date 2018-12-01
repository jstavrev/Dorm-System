using SmartDormitory.Data.Data;
using SmartDormitory.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartDormitory.Services.Services
{
    public class UserService : IUserService
    {
        private readonly SmartDormitoryDbContext context;

        public UserService(SmartDormitoryDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = this.context.Users.ToList();

            return users;
        }

        public IEnumerable<User> GetUsersContainingText(string text, int page = 1, int pageSize = 10)
        {
            var users = this.context.Users.Where(u => u.UserName.Contains(text, StringComparison.InvariantCultureIgnoreCase))
                .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                        .ToList();

            return users;
        }

        public IEnumerable<User> GetUsersWithPaging(int page = 1, int pageSize = 10)
        {
            var users = this.context.Users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return users;
        }

        public int Total()
        {
            return this.context.Users.Count();
        }

        public int TotalContainingText(string searchText)
        {
            return this.context.Users.Where(u => u.UserName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList().Count();
        }
    }
}
