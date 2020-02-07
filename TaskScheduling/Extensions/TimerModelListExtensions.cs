using System;
using System.Collections.Generic;
using System.Threading;
using TaskScheduling.Models;

namespace TaskScheduling.Extensions
{
    /// <summary>
    /// The class extends the List<TimerModel> collection by additional methods used to manage timers
    /// </summary>
    public static class TimerModelListExtensions
    {
        /// <summary>
        /// Method reschedules the timer by time defined in configuration ile
        /// </summary>
        /// <param name="timers">Collection of timers</param>
        /// <param name="timerId">Timer unique ID - only timer with this ID will be rescheduled</param>
        /// <param name="nextCycle">Number of minutes after which timer executes action again</param>
        public static void RescheduleTimer(this List<TimerModel> timers, Guid timerId, TimeSpan nextCycle)
        {
            if(timerId == Guid.Empty)
                return;
            
            var timerModelIndex = timers.FindIndex(tmr => tmr.Id == timerId);

            if(timerModelIndex < 0)
                return;
            
            timers[timerModelIndex].Timer
                .Change(nextCycle, Timeout.InfiniteTimeSpan);
        }

        
        /// <summary>
        /// Method dispose all timers in collection
        /// </summary>
        /// <param name="timers">Collection of timers to be disposed</param>
        public static void DisposeTimers(this List<TimerModel> timers)
        {
            foreach (var timer in timers)
            {
                timer.Timer.Dispose();
            }
        }
    }
}