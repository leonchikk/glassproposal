using AutoMapper;
using GlassProposalsApp.Dashboard.AutoMapperProfiles;
using GlassProposalsApp.Application.AutoMapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassProposalsApp.API.Infrastructure.Extensions
{
    public static class AutoMapperServiceExtensions
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProposalProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new DashBoardProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
