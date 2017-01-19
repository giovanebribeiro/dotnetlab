using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Quartz.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunProgramExample().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to terminate");
            Console.ReadKey();
        }

        static async Task RunProgramExample()
        {
            try
            {
                StdSchedulerFactory schedulerFactory = new StdSchedulerFactory();
                IScheduler scheduler = schedulerFactory.GetScheduler();

                scheduler.Start();

                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                                                    .WithIdentity("trigger1", "group1")
                                                    .StartNow()
                                                    /*.WithSimpleSchedule(x => x
                                                                            .WithIntervalInSeconds(10)
                                                                            .RepeatForever())*/
                                                    .WithCronSchedule("* * * * * ? *")
                                                    .Build();

                scheduler.ScheduleJob(job, trigger);

                await Task.Delay(TimeSpan.FromSeconds(60));

                scheduler.Shutdown();
            }catch(SchedulerException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
