using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FightTracker.Application.Fighters.FightersValidation;
using FightTracker.Application.Fights.FightsValidation;
using FightTracker.Application.Behaviors;

namespace FightTracker.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR( cfg =>
                {cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                 cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                 cfg.AddOpenBehavior(typeof(CachingBehavior<,>));
                });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
