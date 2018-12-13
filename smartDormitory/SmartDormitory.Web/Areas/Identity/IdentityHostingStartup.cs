using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;

[assembly: HostingStartup(typeof(SmartDormitory.Web.Areas.Identity.IdentityHostingStartup))]
namespace SmartDormitory.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<SmartDormitoryDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SmartDormitoryDbContextConnection")));

                services.AddIdentity<User, IdentityRole>()
                     .AddEntityFrameworkStores<SmartDormitoryDbContext>()
                        .AddDefaultTokenProviders();
            });
        }
    }
}