using System;
using System.Collections.Generic;
using System.Threading;
using TaskScheduling.Models;

namespace TaskScheduling.Extensions
{
    public static class TimerModelListExtensions
    {
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

        public static void KillTimers(this List<TimerModel> timers)
        {
            foreach (var timer in timers)
            {
                timer.Timer.Dispose();
            }
        }
    }
}