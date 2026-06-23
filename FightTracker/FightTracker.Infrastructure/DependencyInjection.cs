using FightTracker.Core.Interfaces;
using FightTracker.Infrastructure.Data;
using FightTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFighterRepository, FighterRepository>();
            services.AddScoped<IFightRepository, FightRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AppConnectionString")));
            return services; 
        }
    }
}
