using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

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

        public async Task SaveAvatarImageAsync(Stream stream, string userId)
        {
            //to add validation
            User user = await this.context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new EntryPointNotFoundException();
            }

            using (BinaryReader br = new BinaryReader(stream))
            {
                user.AvatarImage = br.ReadBytes((int)stream.Length);
            }

            await this.context.SaveChangesAsync();
        }

        public async Task<IPagedList<User>> FilterUsersAsync(string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            //to add validation

            var query = this.context.Users
                .Where(u => u.UserName.Contains(filter) || u.Email.Contains(filter));

            switch (sortOrder)
            {
                case "username_asc":
                    query = query.OrderBy(u => u.UserName);
                    break;
                case "username_desc":
                    query = query.OrderByDescending(u => u.UserName);
                    break;
                case "email_asc":
                    query = query.OrderBy(u => u.Email);
                    break;
                case "email_desc":
                    query = query.OrderByDescending(u => u.Email);
                    break;
            }

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

    }
}
