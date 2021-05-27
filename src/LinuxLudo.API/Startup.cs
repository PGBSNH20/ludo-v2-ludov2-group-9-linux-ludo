using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinuxLudo.API.Database.Context;
using LinuxLudo.API.Database.Repositories;
using LinuxLudo.API.Domain.Repositories;
using LinuxLudo.API.Domain.Response;
using LinuxLudo.API.Domain.Services;
using LinuxLudo.API.Extensions;
using LinuxLudo.API.Hubs;
using LinuxLudo.API.Services;
using MessagePack;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
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
            services.AddAutoMapper(typeof(Startup));
            services.AddSignalR(opts =>
            {
                opts.EnableDetailedErrors = true;
                opts.KeepAliveInterval = TimeSpan.FromSeconds(10);
            }).AddMessagePackProtocol(options =>
            {
                options.SerializerOptions = MessagePackSerializerOptions.Standard
                    .WithSecurity(MessagePackSecurity.UntrustedData);
            });

            services.AddControllers()
                .ConfigureApiBehaviorOptions(opts =>
                {
                    opts.SuppressMapClientErrors = true;
                    opts.InvalidModelStateResponseFactory = ctx =>
                    {
                        var err = new ErrorResponse(ctx.ModelState.First().Value.Errors.First().ErrorMessage, 400, ctx.HttpContext.TraceIdentifier).Respond();
                        return new ObjectResult(err) { StatusCode = err.StatusCode };
                    };
                })
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.WriteIndented = true;
                    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinuxLudo.API", Version = "v1" });

            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IGameService, GameService>();
            services.AddSingleton<IGameHubRepository, GameHubRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "Open",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader());
            });

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseResponseCompression();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinuxLudo.API v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("Open");
            app.ApplyCustomMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<GameHub>("/gamehub");
            });
            app.ApplyRouteNotFoundMiddleware();
        }
    }
}