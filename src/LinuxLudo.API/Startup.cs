using System.Linq;
using System.Text.Json.Serialization;
using LinuxLudo.API.Database.Context;
using LinuxLudo.API.Domain.Models.Auth;
using LinuxLudo.API.Domain.Response;
using LinuxLudo.API.Domain.Services;
using LinuxLudo.API.Extentions;
using LinuxLudo.API.Middleware;
using LinuxLudo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LinuxLudo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opts => opts.UseNpgsql(Configuration.GetConnectionString("Default")));
            services.AddIdentity<User, Role>(opts =>
                {
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers()
                .ConfigureApiBehaviorOptions(opts =>
                {
                    opts.SuppressMapClientErrors = true;
                    opts.InvalidModelStateResponseFactory = ctx =>
                    {
                        var err = new ErrorResponse(ctx.ModelState.First().Value.Errors.First().ErrorMessage, 400, ctx.HttpContext.TraceIdentifier).Respond();
                        return new ObjectResult(err) {StatusCode = err.StatusCode};
                    };
                })
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.WriteIndented = true;
                    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "LinuxLudo.API", Version = "v1"});
            });

            services.AddTransient<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinuxLudo.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.ApplyCustomMiddleware();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.ApplyRouteNotFoundMiddleware();
        }
    }
}