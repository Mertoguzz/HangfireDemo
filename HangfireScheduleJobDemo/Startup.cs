using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HangfireScheduleJobDemo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeSerializer()
            .UseMemoryStorage());

            services.AddHangfireServer();
            services.AddSingleton<IEmailService,EmailService>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IBackgroundJobClient backgroundJobClient ,IRecurringJobManager recurringJobManager,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });


            app.UseHangfireDashboard();
            backgroundJobClient.Enqueue(()=>Console.WriteLine("Enqueued"));
            recurringJobManager.AddOrUpdate("Run every 1 minute",
                () =>serviceProvider.GetService<EmailService>().SentMail(),
                Cron.Minutely) ;
        }
    }
}
