using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassProposalsApp.API.Infrastructure.Extensions
{
    public static class AuthorizationServiceExtensions
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policy => policy.RequireClaim("SecurityLevel", "2"));
            });

            return services;
        }
    }
}
