using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Contracts;
using SmartDormitory.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SmartDormitory.Services.Services
{
    public class SensorService : ISensorService
    {
        private readonly SmartDormitoryDbContext context;

        public SensorService(SmartDormitoryDbContext context)
        {
            this.context = context;
        }

        public void DeleteSensor()
        {
            throw new NotImplementedException();
        }

        public void EditSensor()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sensor> GetAll()
        {
            return this.context.Sensors.ToList();
        }

        public IEnumerable<SensorTypes> GetAllTypes()
        {
            return this.context.SensorTypes.ToList();
        }

        public void RegisterSensor()
        {
            throw new NotImplementedException();
        }

        public async Task<IPagedList<UserSensors>> FilterUserSensorsAsync(string userId, string sortOrder = "", string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            Validator.ValidateNull(sortOrder, "SortOrder cannot be null!");
            Validator.ValidateNull(filter, "Filter cannot be null!");

            Validator.ValidateMinRange(pageNumber, 1, "Page number cannot be less then 1!");
            Validator.ValidateMinRange(pageSize, 0, "Page size cannot be less then 0!");

            var query = this.context.UserSensors
                .Where(t => t.Name.Contains(filter) && t.UserId.Equals(userId));

            switch (sortOrder)
            {
                case "name_asc":
                    query = query.OrderBy(c => c.Name);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(c => c.Name);
                    break;
            }

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
