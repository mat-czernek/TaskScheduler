using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TaskScheduling.Enums;
using TaskScheduling.Extensions;
using TaskScheduling.Models;
using TaskScheduling.Services;


namespace TaskScheduling.Scheduling
{
    /// <summary>
    /// Class schedules actions using the Tasks (from System.Threading)
    /// </summary>
    public class Scheduler : IScheduler
    {
        /// <summary>
        /// List of the scheduler observers. Each observer implements method executed on scheduled time
        /// </summary>
        private readonly List<IObserver> _schedulerObserversList;
        
        /// <summary>
        /// Repository of timers
        /// </summary>
        private readonly ITimersRepository _timersRepository;

        
        public Scheduler(ITimersRepository timersRepository)
        {
            _timersRepository = timersRepository;

            _timersRepository.OnTimerElapsed += _timerExecutionCallback;
            
            _schedulerObserversList = new List<IObserver>();
        }
        
        /// <summary>
        /// Method attach the observer to the class
        /// </summary>
        /// <param name="observer">Instance of the class which implements the IObserver interface</param>
        public void AttachObserver(IObserver observer)
        {
            _schedulerObserversList.Add(observer);
        }
        
        /// <summary>
        /// Method detach the observer from the class
        /// </summary>
        /// <param name="observer">Instance of the class which implements the IObserver interface</param>
        public void DetachObserver(IObserver observer)
        {
            _schedulerObserversList.Remove(observer);
        }

        /// <summary>
        /// Method starts scheduler by cleaning up the timers in repository and by reading the timers configurztion
        /// </summary>
        public void Start()
        {
            _timersRepository.DisposeTimers();
            _timersRepository.LoadTimers();
        }

        /// <summary>
        /// Method stops scheduler and removes all timers
        /// </summary>
        public void Stop()
        {
            _timersRepository.DisposeTimers();
        }


        /// <summary>
        /// Timer callback method
        /// </summary>
        /// <param name="timerId">Timer unique identifier</param>
        /// <param name="actionType">Action type executed by timer</param>
        private void _timerExecutionCallback(Guid timerId, ActionType actionType)
        {
            foreach (var observer in _schedulerObserversList)
            {
                observer.RequestActionProcessing(actionType);
            }
            
            _timersRepository.ResetTimer(timerId);
        }
        
    }
}