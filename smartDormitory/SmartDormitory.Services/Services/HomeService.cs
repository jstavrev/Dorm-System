using Microsoft.EntityFrameworkCore;
using SmartDormitory.Data.Data;
using SmartDormitory.Data.Services.Contracts;
using SmartDormitory.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace SmartDormitory.Services.HomeService
{
    public class HomeService : IHomeService
    {
        private readonly SmartDormitoryDbContext DataContext;
        public HomeService(SmartDormitoryDbContext dataContext)
        {
            this.DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<IEnumerable<UserSensors>> FilterSensorsAsync()
        {
            var query = DataContext.UserSensors
                .Where(t => t.IsPublic == true).ToListAsync();

            return await query;
        }

    }
}
