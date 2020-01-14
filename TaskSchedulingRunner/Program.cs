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

        public static void Main(string[] args)
        {
           AppConfiguration = new Configuration();
           
           AppConfiguration.Read("appSettings.json");

           var actionsHandler = new ServiceActionsHandler();

           TimeSchedule = new TimerScheduler(AppConfiguration);
           
           TimeSchedule.AttachObserver(actionsHandler);
           
           TimeSchedule.Start();
           
           Console.ReadKey();
           
           TimeSchedule.Stop();
           
           TimeSchedule.DetachObserver(actionsHandler);
        }
    }
}