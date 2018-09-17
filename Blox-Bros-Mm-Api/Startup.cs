using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blox_Bros_Mm_Api.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Blox_Bros_Mm_Api
{
    /// <summary>
    /// Default startup type
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Instantiates a new <see cref="Startup"/> object
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Startup configuration
        /// </summary>
        public IConfiguration Configuration { get; }
        
        /// <summary>
        /// Called by .NET Core runtime; adds services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new Info { Title = "Blox Bros Matchmaking API", Version = "v1" });
                 c.IncludeXmlComments(string.Format(@"{0}\Blox-Bros-Mm-Api.xml", AppDomain.CurrentDomain.BaseDirectory));

                 // Add X-Api-Key header to operation parameters
                 c.OperationFilter<ApiAuthorize.AddSwaggerParameter>();
             });
        }
        
        /// <summary>
        /// Called by .NET Core runtime; configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blox Bros Matchmaking API");
            });
            
            ApiAuthorize.SetGlobalApiKey(Configuration["GlobalApiKey"]);
        }
    }
}
