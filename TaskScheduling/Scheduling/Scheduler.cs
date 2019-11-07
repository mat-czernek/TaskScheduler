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
    public class Scheduler : IScheduler
    {
        private delegate void TimerCallbackMethodDelegate(Guid timerId);
        
        private readonly List<IObserver> _schedulerObservers;

        private readonly IConfiguration _configuration;

        private static List<TimerModel> _installUpdatesTimers = new List<TimerModel>();
        
        private static List<TimerModel> _maintenanceTimers = new List<TimerModel>();

        private bool _isSchedulerStopped;

        public Scheduler(IConfiguration configuration)
        {
            _configuration = configuration;
            
            _schedulerObservers = new List<IObserver>();
        }
        
        public void AttachObserver(IObserver observer)
        {
            _schedulerObservers.Add(observer);
        }
        
        public void DetachObserver(IObserver observer)
        {
            _schedulerObservers.Remove(observer);
        }

        public void Stop()
        {
            _isSchedulerStopped = true;
            _releaseTimers();
        }

        public void Start()
        {
            _isSchedulerStopped = false;
            
            _releaseTimers();
            
            if(_configuration.Scheduling.Maintenance.IsEnabled)
            {
                _maintenanceTimers = _setupTimers(_configuration.Scheduling.Maintenance.Schedules,
                    _maintenanceObserversNotification).ToList();
            }

            if(_configuration.Scheduling.InstallUpdates.IsEnabled)
            {
                _installUpdatesTimers = _setupTimers(_configuration.Scheduling.InstallUpdates.Schedules,
                    _installUpdatesObserversNotification).ToList();
            }
        }


        private void _releaseTimers()
        {
            if (_maintenanceTimers != null && _maintenanceTimers.Any())
            {
                _maintenanceTimers.KillTimers();
                _maintenanceTimers.Clear();
            }

            if (_installUpdatesTimers != null && _installUpdatesTimers.Any())
            {
                _installUpdatesTimers.KillTimers();
                _installUpdatesTimers.Clear();
            }
        }

        private static IEnumerable<TimerModel> _setupTimers(IEnumerable<DateTime> schedules,
            TimerCallbackMethodDelegate timerCallbackMethod)
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
                if(_isSchedulerStopped) return;
                
                foreach (var observer in _schedulerObservers)
                {
                    observer.RequestActionProcessing(ActionTypes.InstallUpdates);
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                // reset timer
                _installUpdatesTimers.ResetTimer(timerId, TimeSpan.FromSeconds(60));
            }
            
        }

        private void _maintenanceObserversNotification(Guid timerId)
        {

            try
            {
                if(_isSchedulerStopped) return;
                
                foreach (var observer in _schedulerObservers)
                {
                    observer.RequestActionProcessing(ActionTypes.Maintenance);
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                // reset timer
                _maintenanceTimers.ResetTimer(timerId, TimeSpan.FromSeconds(60));
            }
            
        }
    }
}