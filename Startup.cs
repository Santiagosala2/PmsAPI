using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Resources.Repo;
using Users.Repo;

namespace PmsAPI
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
             services.AddCors(options =>
            {
                options.AddPolicy("allowClient",
                                builder =>
                                {
                                    builder.WithOrigins("http://localhost:3000")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                                });
            }); 

            services.AddDbContext<DataContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("ResourcesConnection")));
            
            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IResourcesRepo,ResourcesRepo>();
            services.AddScoped<IUsersRepo, UsersRepo>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PmsAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PmsAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("allowClient");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
