using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lufuno.DataAccessLogic;
using Lufuno.DataAccessLogic.Interfaces;
using Lufuno.DomainManager;
using Lufuno.DomainManager.Interfaces;
using Lufuno.Utilities;
using Lufuno.Utilities.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace TestAPI
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc().AddJsonOptions(opt =>
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddOptions();

            services.AddScoped<Lufuno.Utilities.Interfaces.ILogger, Logger>();
            services.AddScoped<IUtil, Util>();
            services.AddScoped<IFileUploadManager, FileUploadManager>();
            services.AddScoped<IRecordManager, RecordManager>();
            services.AddScoped<IFileUploadLogic, FileUploadLogic>();
            services.AddScoped<IRecordLogic, RecordLogic>();

            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

            app.UseMvc();
        }
    }
}
