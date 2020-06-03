using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_swagger_authentication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

//for documenting the object model and customizing the UI to match your theme.
using System;
using System.Reflection;
using System.IO;

namespace api_swagger_authentication
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
            // Add DbContext to our project
            services.AddDbContext<Context>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add swagger service to our project
            services.AddSwaggerGen(c=> 
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo
                {
                    Title = "Promena Api",
                    Version = "v1",
                    Description = "Examaple of AspNetCore WebAPI with Swagger",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name="Rajesh Kumar",
                        Email=string.Empty,
                        Url=new Uri("https://twitter.com/rkmarapaka")
                    },
                    License = new OpenApiLicense
                    {
                        Name="Use Under LICX",
                        Url=new Uri("https://example.com/license")
                    }
                    
                }); ;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add swagger to our project
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","My Api v1");
                //c.RoutePrefix = string.Empty;
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
