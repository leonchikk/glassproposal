using GlassProposalsApp.API.Infrastructure.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassProposalsApp.API.Infrastructure.Extensions
{
    public static class MvcServiceExtensions
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelFilter));
            });

            return services;
        }
    }
}
