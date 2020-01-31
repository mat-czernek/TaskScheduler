using System;
using TaskScheduling.Actions;
using TaskScheduling.Enums;
using TaskScheduling.Scheduling;
using TaskScheduling.Services;

namespace TaskSchedulingRunner
{
    internal class Program
    {
        private static IConfiguration _applicationConfiguration;

        private static IScheduler _scheduler;

        private static ITimersRepository _timersRepository;

        public static void Main(string[] args)
        {
           _applicationConfiguration = new Configuration();
           
           _applicationConfiguration.Read("appSettings.json");

           _timersRepository = new TimersRepository(_applicationConfiguration);
           
           _scheduler = new Scheduler(_timersRepository);
           
           var serviceActionsHandler = new ServiceActionsHandler();
           
           _scheduler.AttachObserver(serviceActionsHandler);
           
           _scheduler.Start();
           
           Console.ReadKey();
           
           _scheduler.DetachObserver(serviceActionsHandler);
           
           _scheduler.Stop();
        }
    }
}