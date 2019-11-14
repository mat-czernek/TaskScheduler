using System;
using TaskScheduling.Actions;
using TaskScheduling.Enums;
using TaskScheduling.Scheduling;
using TaskScheduling.Services;

namespace TaskSchedulingRunner
{
    internal class Program
    {
        private static IConfiguration AppConfiguration { get; set; }
        
        private static ITimerScheduler TimeSchedule { get; set; }
        
        private static IEventScheduler EventSchedule { get; set; }

        public static void Main(string[] args)
        {
           AppConfiguration = new Configuration();
           
           AppConfiguration.Read("appSettings.json");

           var actionsHandler = new ServiceActionsHandler();

           TimeSchedule = new TimerScheduler(AppConfiguration);
           
           EventSchedule = new EventScheduler(AppConfiguration);
           
           TimeSchedule.AttachObserver(actionsHandler);
           
           EventSchedule.AttachObserver(actionsHandler);
           
           TimeSchedule.Start();
           
           EventSchedule.OnEvent(EventType.OnUserLogoff);
           
           Console.ReadKey();
           
           TimeSchedule.Stop();
           
           TimeSchedule.DetachObserver(actionsHandler);
           
           EventSchedule.DetachObserver(actionsHandler);
        }
    }
}