using AutoMapper;
using GlassProposalsApp.API.Infrastructure.Extensions;
using GlassProposalsApp.API.Infrastructure.Middlewares;
using GlassProposalsApp.Auth.Interfaces;
using GlassProposalsApp.Auth.Services;
using GlassProposalsApp.Dashboard.Interfaces;
using GlassProposalsApp.Dashboard.Services;
using GlassProposalsApp.Data;
using GlassProposalsApp.Data.Interfaces;
using GlassProposalsApp.Data.Repositories;
using GlassProposalsApp.Domain.Interfaces;
using GlassProposalsApp.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GlassProposalsApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GlassProposalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("dev")));

            services.ConfigureAutoMapper();
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc();
            services.ConfigureMvc();
            
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IDecisionService, DecisionService>();
            services.AddScoped<IProposalService, ProposalService>();
            services.AddScoped<IProposalRepository, ProposalRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStagesService, StagesService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<UnitOfWork>();

            services.AddSwaggerDocumentation();
            services.ConfigureAuthentication(Configuration.GetSection("Authentication"));
            services.ConfigureAuthorization();

            services.AddCors(o => o.AddPolicy("AppPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowCredentials()
                       .AllowAnyHeader();
            }));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerDocumentation();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors("AppPolicy");
            app.UseMvc();
            app.UseAuthentication();
        }
    }
}
