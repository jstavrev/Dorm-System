using SmartDormitory.Data.Data;
using SmartDormitory.Services.Contracts;
using SmartDormitory.Services.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            Validator.ValidateNull(stream, "Image stream cannot be null!");
            Validator.ValidateNull(userId, "User Id cannot be null!");
            Validator.ValidateGuid(userId, "User id is not in the correct format.Unable to parse to Guid!");

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
    }
}
