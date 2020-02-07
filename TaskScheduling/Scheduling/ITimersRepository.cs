using System;
using System.Collections.Generic;
using TaskScheduling.Models;

namespace TaskScheduling.Scheduling
{
    /// <summary>
    /// Interface represents timers repository along with operations required to manage the timers
    /// </summary>
    public interface ITimersRepository
    {
        /// <summary>
        /// Method executed on scheduled time
        /// </summary>
        TimerCallbackMethodDelegate OnTimerElapsed { get; set; }

        /// <summary>
        /// Method resets timer
        /// </summary>
        /// <param name="timerId">Timer unique ID</param>
        void ResetTimer(Guid timerId);

        /// <summary>
        /// Method loads timers
        /// </summary>
        void LoadTimers();

        /// <summary>
        /// Method dispose timers in repository
        /// </summary>
        void DisposeTimers();
    }
}