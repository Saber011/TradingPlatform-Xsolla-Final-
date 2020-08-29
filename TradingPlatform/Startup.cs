using EasyCaching.Core.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Timeout;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TradingPlatform.App.ApiClient;
using TradingPlatform.App.Interfaces;
using TradingPlatform.App.Repositories;
using TradingPlatform.App.Services;
using TradingPlatform.AppStart;
using TradingPlatform.Infrastructure;
using TradingPlatform.Repositories;
using TradingPlatform.Services;

namespace TradingPlatform
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
            services.AddControllers();
            services.AddSwagger(new SwaggerOptions
            {
                HostName = "Backand",
                BasePath = AppContext.BaseDirectory,
                FileNames = new[]
                {
                    "RestService",
                },
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                       .AddJwtBearer(options =>
                       {
                           options.RequireHttpsMetadata = false;
                           options.TokenValidationParameters = new TokenValidationParameters
                           {
                               ValidateIssuer = true,

                               ValidIssuer = AuthOptions.Issuer,

                               ValidateAudience = true,

                               ValidAudience = AuthOptions.Audience,

                               ValidateLifetime = true,

                               IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

                               ValidateIssuerSigningKey = true,
                           };
                       });

            services.AddEasyCaching(options =>
            {
                options.UseRedis(redisConfig =>
                {
                    redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint("localhost", 6379));
                    redisConfig.DBConfig.AllowAdmin = true;
                }, "redis1");
            });

            services.TryAddScoped<IExecuteService, ExecuteService>();
            services.TryAddScoped<IOrderServices, OrderServices>();
            services.TryAddScoped<IOrderRepository, OrderRepository>();
            services.TryAddScoped<IProductService, ProductService>();
            services.TryAddScoped<IProductRepository, ProductRepository>();
            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<IUserService, UserService>();
            services.TryAddScoped<IEmailService, EmailService>();

            IAsyncPolicy<HttpResponseMessage> retry = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .Or<TimeoutRejectedException>()
                .RetryAsync(3);

            var timeoutPolicy = Policy.TimeoutAsync(timeout: TimeSpan.FromSeconds(3), onTimeoutAsync: (ctx, t, task, e) =>
            {
                Console.Write(e.Message);
                return Task.CompletedTask;
            });

            var policy = retry.WrapAsync(timeoutPolicy);

            services.AddHttpClient<ApiClientTradingPlatform>((client) =>
                {
                    client.BaseAddress = new Uri("http://localhost:61720");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseProjectSwagger();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
