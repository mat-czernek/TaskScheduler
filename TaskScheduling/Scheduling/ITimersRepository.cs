using System;
using System.Collections.Generic;
using TaskScheduling.Models;

namespace TaskScheduling.Scheduling
{
    public interface ITimersRepository
    {
        TimerCallbackMethodDelegate OnTimerElapsed { get; set; }

        void ResetTimer(Guid timerId);

        void LoadTimers();

        void DisposeTimers();
    }
}