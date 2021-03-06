﻿using SmartDormitory.Data.Data;
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

        public int Total()
        {
            return this.context.Users.Count();
        }


        public async Task SaveAvatarImageAsync(Stream stream, string userId)
        {
            //to add validation

            if (stream == null)
            {
                throw new ArgumentNullException();
            }

            if (userId == null)
            {
                throw new ArgumentNullException();
            }

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
            if (sortOrder == null)
            {
                throw new ArgumentNullException();
            }

            if (filter == null)
            {
                throw new ArgumentNullException();
            }

            if (pageNumber == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (pageSize == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var query = this.context.Users
                .Where(u => u.UserName.Contains(filter) || u.Email.Contains(filter));

            if (query == null)
            {
                throw new ArgumentNullException();
            }

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
