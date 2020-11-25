using ApplicationServices;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using WalletApi.Filters;
using WalletApi.Validations;

namespace WalletApi
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
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidationFilter());
                options.Filters.Add(typeof(CustomExceptionFilter));
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<CreateWalletRequestValidator>();
            }); ;

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            string dbConnString = Configuration.GetConnectionString("CurrencyWalletConnection");

            //services.AddDbContext<CurrencyWalletContext>(options =>
            //               options.UseSqlServer(dbConnString));

            services.AddDbContextPool<CurrencyWalletContext>(options =>
            {
                options.UseSqlServer(dbConnString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(CurrencyWalletContext).GetTypeInfo().Assembly.GetName()
                            .Name);
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), errorNumbersToAdd: null);

                    });
            }
            );

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo() { Title = "Currency Wallet Service", Version = "v1" });

                // Get xml comments path
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // Set xml path
                config.IncludeXmlComments(xmlPath);
            });
            // Make routes globally lowercase.
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            services.AddApplication();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();


            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseSwagger(
                s =>
                {
                    s.RouteTemplate = "/api/currency-wallet-service/swagger/ui/{documentName}/swagger.json";

                });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api/currency-wallet-service";
                c.SwaggerEndpoint("/api/currency-wallet-service/swagger/ui/v1/swagger.json", "Currency Wallet Service");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
