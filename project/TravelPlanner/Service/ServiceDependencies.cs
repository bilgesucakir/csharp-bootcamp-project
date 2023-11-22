using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Abstract;
using Service.Concrete;
using Service.Rules.Abstract;
using Service.Rules.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public static class ServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITripService, TripService>();
        services.AddScoped<IExcursionService, ExcursionService>();

        services.AddScoped<IUserRules, UserRules>();
        services.AddScoped<ITripRules, TripRules>();
        services.AddScoped<IExcursionRules, ExcursionRules>();

        return services;
    }
}
