using System;
using TaskScheduling.Enums;

namespace TaskScheduling.Scheduling
{
    /// <summary>
    /// Callback method executed by each timer
    /// </summary>
    /// <param name="timerId">Timer unique identifier used to identify timer and re-schedule execution time</param>
    /// <param name="actionType">Type of the action executed by timer</param>
    public delegate void TimerCallbackMethodDelegate(Guid timerId, ActionType actionType);
}