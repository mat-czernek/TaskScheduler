using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TaskScheduling.Extensions;
using TaskScheduling.Models;
using TaskScheduling.Services;

namespace TaskScheduling.Scheduling
{
    /// <summary>
    /// Class stores timers along with their configuration
    /// </summary>
    public class TimersRepository : ITimersRepository
    {
        /// <summary>
        /// Application configuration service
        /// </summary>
        private readonly IConfiguration _applicationConfiguration;

        /// <summary>
        /// List of the timers build based on the settings from configuration file
        /// </summary>
        private static readonly List<TimerModel> TimersList = new List<TimerModel>();
        
        /// <summary>
        /// Callback method executed on timer schedule
        /// </summary>
        public TimerCallbackMethodDelegate OnTimerElapsed { get; set; }
        
        
        public TimersRepository(IConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }

        /// <summary>
        /// Method resets the single timer by setting up the countdown to the default value from the configuration file
        /// </summary>
        /// <param name="timerId">Timer unique identifier</param>
        public void ResetTimer(Guid timerId)
        {
            if(timerId == Guid.Empty)
                return;
            
            var timerModelIndex = TimersList.FindIndex(tmr => tmr.Id == timerId);

            if(timerModelIndex < 0)
                return;

            var timerToUpdate = TimersList[timerModelIndex];
            
            timerToUpdate.Timer.Change(TimeSpan.FromMinutes(timerToUpdate.RepeatCycle), Timeout.InfiniteTimeSpan);

            TimersList[timerModelIndex] = timerToUpdate;
        }

        /// <summary>
        /// Method dispose all timers in repository
        /// </summary>
        public void DisposeTimers()
        {
            if (TimersList == null || !TimersList.Any()) return;
            
            TimersList.KillTimers();
            TimersList.Clear();
        }
        
        /// <summary>
        /// Method loads timers into the repository based on the data from the application configuration
        /// </summary>
        public void LoadTimers()
        {
            foreach (var timerConfiguration in _applicationConfiguration.TimersConfigurationList)
            {
                if(!timerConfiguration.IsEnabled) continue;
                
                foreach (var schedule in timerConfiguration.Schedules)
                {
                    var executionTime = schedule.TimeOfDay - DateTime.Now.TimeOfDay;

                    // execution time already missed
                    if (executionTime < TimeSpan.Zero)
                    {
                        // setup execution time for the next 24 hours
                        executionTime = executionTime.Add(TimeSpan.FromHours(24));
                    }
                
                    var random = new Random();

                    executionTime += TimeSpan.FromMinutes(random.Next(0, timerConfiguration.RandomDelayInterval));
                
                    var timerModel = new TimerModel();
                
                    timerModel.Timer = new Timer(callback => { OnTimerElapsed(timerModel.Id, timerConfiguration.Type); }, null, executionTime,
                        Timeout.InfiniteTimeSpan);

                    timerModel.RepeatCycle = timerConfiguration.RepeatCycle;

                    TimersList.Add(timerModel);
                }
            }
        }
    }
}