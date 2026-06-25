using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightTracker.Application;
using FightTracker.Infrastructure;

namespace FightTracker.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
            .AddInfrastructureDI(configuration);
            return services;
        }
    }
}
