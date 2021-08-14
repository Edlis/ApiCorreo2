using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Modelos.Conn;
using Procesos;
using Procesos.Errores;

namespace ApiCorreo2
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
            var ConfSMTP = Configuration.GetSection("SMTP").Get<SMTPConfiguracionModelo>();
            services.AddSingleton(ConfSMTP);
            services.AddMassTransit(x =>
            {
                x.AddConsumer<Consumidor>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.PrefetchCount = 1;                    
                    cfg.ConfigureEndpoints(context);
                    cfg.UseMessageRetry(r =>
                    {
                        r.Ignore<ErrorControlado>();
                        r.Immediate(5);
                    }                    
                    ) ; 
                });
            });
            services.AddMassTransitHostedService();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
