using System;
using TaskScheduling.Actions;
using TaskScheduling.Configuration;
using TaskScheduling.Scheduling;
using TaskScheduling.Services;

namespace TaskSchedulingRunner
{
    internal class Program
    {
        public static ISchedulingConfiguration SchedulingSettings { get; set; }
        
        public static IConfiguration AppConfiguration { get; set; }

        public static void Main(string[] args)
        {
           AppConfiguration = new Configuration();
           
           AppConfiguration.Read("appSettings.json");

           var actionsHandler = new ServiceActionsHandler();

           var scheduler = new Scheduler(AppConfiguration);
           
           scheduler.Start();

           scheduler.AttachObserver(actionsHandler);
           
           scheduler.Stop();
           
           scheduler.DetachObserver(actionsHandler);
           

           Console.ReadKey();
        }
    }
}