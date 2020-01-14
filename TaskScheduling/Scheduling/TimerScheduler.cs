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
    public class TimerScheduler : ITimerScheduler
    {
        private delegate void TimerCallbackMethodDelegate(Guid timerId);
        
        /// <summary>
        /// List of the class/object observers
        /// </summary>
        private readonly List<IObserver> _schedulerObserversList;

        /// <summary>
        /// Configuration binding
        /// </summary>
        private readonly IConfiguration _applicationConfiguration;

        /// <summary>
        /// List of timers (schedules) for close sessions action
        /// </summary>
        private static List<TimerModel> _closeSessionsTimersList = new List<TimerModel>();
        
        /// <summary>
        /// List of timers (schedules) for update database action 
        /// </summary>
        private static List<TimerModel> _updateDatabaseTimersList = new List<TimerModel>();

        /// <summary>
        /// Determines whether the scheduler is running or not
        /// </summary>
        private bool _isRunning;

        
        public TimerScheduler(IConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
            
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
        /// Method stops scheduler and removes all timers
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            _cleanTimers();
        }

        /// <summary>
        /// Method starts the scheduler
        /// </summary>
        public void Start()
        {
            _isRunning = true;
            
            _cleanTimers();
            
            if(_applicationConfiguration.Scheduling.CloseSessions.IsEnabled)
            {
                _updateDatabaseTimersList = _initializeTimers(_applicationConfiguration.Scheduling.CloseSessions.Schedules,
                    _closeSessionsObserversNotification, _applicationConfiguration.Scheduling.CloseSessions.RandomDelayInterval).ToList();
            }

            if(_applicationConfiguration.Scheduling.DatabaseBackup.IsEnabled)
            {
                _closeSessionsTimersList = _initializeTimers(_applicationConfiguration.Scheduling.DatabaseBackup.Schedules,
                    _databaseBackupsObserversNotification, _applicationConfiguration.Scheduling.DatabaseBackup.RandomDelayInterval).ToList();
            }
        }

        /// <summary>
        /// Method dispose all timers and removes them from collection
        /// </summary>
        private static void _cleanTimers()
        {
            if (_updateDatabaseTimersList != null && _updateDatabaseTimersList.Any())
            {
                _updateDatabaseTimersList.KillTimers();
                _updateDatabaseTimersList.Clear();
            }

            if (_closeSessionsTimersList != null && _closeSessionsTimersList.Any())
            {
                _closeSessionsTimersList.KillTimers();
                _closeSessionsTimersList.Clear();
            }
        }

        private static IEnumerable<TimerModel> _initializeTimers(IEnumerable<DateTime> schedules,
            TimerCallbackMethodDelegate timerCallbackMethod, int randomDelay)
        {
            foreach (var schedule in schedules)
            {
                var executionTime = schedule.TimeOfDay - DateTime.Now.TimeOfDay;

                // execution time already missed
                if (executionTime < TimeSpan.Zero)
                {
                    // setup execution time for the next 24 hours
                    executionTime = executionTime.Add(TimeSpan.FromHours(24));
                }
                
                var random = new Random();

                executionTime += TimeSpan.FromMinutes(random.Next(0, randomDelay));
                
                var timerModel = new TimerModel();
                
                timerModel.Timer = new Timer(callback => { timerCallbackMethod(timerModel.Id); }, null, executionTime,
                    Timeout.InfiniteTimeSpan);

                yield return timerModel;
            }
        }
        
        private void _databaseBackupsObserversNotification(Guid timerId)
        {
            try
            {
                if(!_isRunning) return;
                
                foreach (var observer in _schedulerObserversList)
                {
                    observer.RequestActionProcessing(ActionType.DatabaseBackup);
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                if(timerId != Guid.Empty)
                    _closeSessionsTimersList.RescheduleTimer(timerId, TimeSpan.FromHours(24));
            }
            
        }

        private void _closeSessionsObserversNotification(Guid timerId)
        {

            try
            {
                if(!_isRunning) return;
                
                foreach (var observer in _schedulerObserversList)
                {
                    observer.RequestActionProcessing(ActionType.CloseSessions);
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                if(timerId != Guid.Empty)
                    _updateDatabaseTimersList.RescheduleTimer(timerId, TimeSpan.FromHours(24));
            }
            
        }
    }
}