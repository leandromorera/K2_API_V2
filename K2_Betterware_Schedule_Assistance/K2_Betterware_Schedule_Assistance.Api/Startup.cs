using K2_Betterware_Assistance.Core.Interfaces;
using K2_Betterware_Assistance.Scheduler.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;


namespace K2_Betterware_Schedule_Assistance.Api
{
    public class Startup
    {
        private IScheduler _quartzScheduler;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _quartzScheduler = ConfigureQuartz();
            beginTriger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Declaracion de Dependencias DOSG.
            services.AddSingleton(provider => _quartzScheduler);
        }

        private void OnShutdown()
        {
            if (!_quartzScheduler.IsShutdown) _quartzScheduler.Shutdown();
        }

        private void beginTriger()
        {

            Console.Out.WriteLineAsync("/////////////////////////////////////////////////////////////.");
            Console.Out.WriteLineAsync("Incio de Configuración de Trigger para ejecutar job Scheduler Assistance Betterware.");

            IJobDetail job = JobBuilder.Create<AssistanceJob>()
                .WithIdentity("AssistanceJob", "quartAssistance")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                            .WithIdentity("testtrigger", "quartAssistance")
                                            .StartNow()
                                            .WithCronSchedule("0/35 * * * * ?")
                                            .Build();

            _quartzScheduler.ScheduleJob(job, trigger);

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

        public IScheduler ConfigureQuartz()
        {
            NameValueCollection props = new NameValueCollection {
                {"quartz.serializer.type", "binary" }
            };

            /*NameValueCollection props = new NameValueCollection {
                {"quartz.serializer.type", "binary" },
                {"quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz" },
                {"quartz.jobStore.dataSource", "default" },
                {"quartz.jobStore.tablePrefix", "QRTZ_" },
                {"quartz.dataSource.default.provider", "SQLite" },
                {"quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.SQLiteDelegate, Quartz" },
                {"quartz.dataSource.default.connectionString", "Data Source=Quartz.sqlite3" }
            };*/

            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            var scheduler = factory.GetScheduler().Result;
            scheduler.Start().Wait();

            Console.WriteLine($"Launched from {Environment.CurrentDirectory}");
            Console.WriteLine($"Physical location {AppDomain.CurrentDomain.BaseDirectory}");
            Console.WriteLine($"AppContext.BaseDir {AppContext.BaseDirectory}");
            
            return scheduler;
        }

    }

}
