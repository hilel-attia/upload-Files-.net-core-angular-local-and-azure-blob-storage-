using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stargate.API.Data;
using AutoMapper;
using Stargate.API.Data.Repository;
using Stargate.API.Services;
using Microsoft.Extensions.Options;
using Microsoft.Azure.KeyVault.Models;
using System;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using Azure.Storage.Blobs;
using System.Reflection;
using System.Linq;

namespace Stargate.API
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
            services.AddMvc();


           
            services.AddCors();
            // Database


           



            services.Configure<FilestoreDatabaseSettings>(
        Configuration.GetSection(nameof(FilestoreDatabaseSettings)));

            services.AddSingleton<IFilestoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<FilestoreDatabaseSettings>>().Value);
            /* if (Environment.IsDevelopment())
             {
                 var connectionString = Configuration.GetConnectionString("StargateContext");

                 services.AddDbContext<StargateContext>(options =>
                     options.UseSqlite(connectionString));
             }
             else
             {
                 var connectionString = Configuration.GetConnectionString("StargateContext");
                 services.AddDbContext<StargateContext>(options => 
                     options.UseSqlServer(connectionString));
             }
             */

            services.AddAutoMapper();

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                
            }
           /* using (var scope = app.ApplicationServices.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<StargateSeeder>();
                seeder.Seed();
            }*/

            app.UseCors(
                options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Azure Blob Storage API");
            });

        }

        /// <summary>
        /// Setup dependencies 
        /// </summary>
        /// <param name="services"></param>
        private void RegisterServices(IServiceCollection services)
        {
         //   services.AddTransient<StargateSeeder>();
            services.AddScoped<IStargateRepository, StargateRepository>();
           //ervices.AddScoped<IFileUploader, AzureBlobFileUploader>();
            services.AddSingleton<IUriShortener, BijectiveUriService>();
          //ervices.AddSingleton<IConfiguration>(Configuration);
            var storageSection = Configuration.GetSection("Azure:Storage");
        //  services.AddScoped(x => new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorage")));
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped(x => new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorage")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Azure Blob Storage API", Description = "Welcome to the API documentation that includes all information you need to smoothing connect with our application services", Version = "1.0" });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });



        }
}
}
