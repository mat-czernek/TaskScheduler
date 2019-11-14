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
    public class TimerScheduler : ITimerScheduler
    {
        private delegate void TimerCallbackMethodDelegate(Guid timerId);
        
        private readonly List<IObserver> _schedulerObserversList;

        private readonly IConfiguration _applicationConfiguration;

        private static List<TimerModel> _installUpdatesTimersList = new List<TimerModel>();
        
        private static List<TimerModel> _maintenanceTimersList = new List<TimerModel>();

        private bool _isRunning;

        public TimerScheduler(IConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
            
            _schedulerObserversList = new List<IObserver>();
        }
        
        public void AttachObserver(IObserver observer)
        {
            _schedulerObserversList.Add(observer);
        }
        
        public void DetachObserver(IObserver observer)
        {
            _schedulerObserversList.Remove(observer);
        }

        public void Stop()
        {
            _isRunning = false;
            _deleteTimers();
        }

        public void Start()
        {
            _isRunning = true;
            
            _deleteTimers();
            
            if(_applicationConfiguration.Scheduling.Maintenance.IsEnabled)
            {
                _maintenanceTimersList = _initializeTimers(_applicationConfiguration.Scheduling.Maintenance.Schedules,
                    _maintenanceObserversNotification, _applicationConfiguration.Scheduling.Maintenance.RandomDelayInterval).ToList();
            }

            if(_applicationConfiguration.Scheduling.InstallUpdates.IsEnabled)
            {
                _installUpdatesTimersList = _initializeTimers(_applicationConfiguration.Scheduling.InstallUpdates.Schedules,
                    _installUpdatesObserversNotification, _applicationConfiguration.Scheduling.InstallUpdates.RandomDelayInterval).ToList();
            }
        }

        private static void _deleteTimers()
        {
            if (_maintenanceTimersList != null && _maintenanceTimersList.Any())
            {
                _maintenanceTimersList.KillTimers();
                _maintenanceTimersList.Clear();
            }

            if (_installUpdatesTimersList != null && _installUpdatesTimersList.Any())
            {
                _installUpdatesTimersList.KillTimers();
                _installUpdatesTimersList.Clear();
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
        
        private void _installUpdatesObserversNotification(Guid timerId)
        {
            try
            {
                if(!_isRunning) return;
                
                foreach (var observer in _schedulerObserversList)
                {
                    observer.RequestActionProcessing(ActionType.InstallUpdates);
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                if(timerId != Guid.Empty)
                    _installUpdatesTimersList.RescheduleTimer(timerId, TimeSpan.FromHours(24));
            }
            
        }

        private void _maintenanceObserversNotification(Guid timerId)
        {

            try
            {
                if(!_isRunning) return;
                
                foreach (var observer in _schedulerObserversList)
                {
                    observer.RequestActionProcessing(ActionType.Maintenance);
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                if(timerId != Guid.Empty)
                    _maintenanceTimersList.RescheduleTimer(timerId, TimeSpan.FromHours(24));
            }
            
        }
    }
}