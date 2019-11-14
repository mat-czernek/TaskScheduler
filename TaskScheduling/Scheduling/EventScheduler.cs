using System;
using System.Collections.Generic;
using System.Linq;
using TaskScheduling.Enums;
using TaskScheduling.Models;
using TaskScheduling.Services;

namespace TaskScheduling.Scheduling
{
    public class EventScheduler : IEventScheduler
    {
        private readonly List<IObserver> _schedulerObserversList;

        private static List<EventModel> _maintenanceEventsList = new List<EventModel>();

        private static List<EventModel> _installUpdatesEventsList = new List<EventModel>();
        
        public EventScheduler(IConfiguration applicationConfiguration)
        {
            var applicationConfiguration1 = applicationConfiguration;
            
            _schedulerObserversList = new List<IObserver>();

            _maintenanceEventsList = _initialiseEvents(applicationConfiguration1.Scheduling.Maintenance.Events,
                _maintenanceObserversNotification).ToList();

            _installUpdatesEventsList = _initialiseEvents(applicationConfiguration1.Scheduling.InstallUpdates.Events,
                _installUpdatesObserversNotification).ToList();
            
        }

        private static IEnumerable<EventModel> _initialiseEvents(IEnumerable<EventType> events,
            EventCallbackMethodDelegate eventCallbackMethod)
        {
            foreach (var eventType in events)
            {
                var eventModel = new EventModel();
                eventModel.Event = eventType;
                eventModel.EventCallbackMethod += eventCallbackMethod;

                yield return eventModel;
            }
        }

        public void OnEvent(EventType eventType)
        {
            _maintenanceEventsList.Where(item => item.Event == eventType).ToList().ForEach(item => item.EventCallbackMethod());
            
            _installUpdatesEventsList.Where(item => item.Event == eventType).ToList().ForEach(item => item.EventCallbackMethod());
        }

        public void AttachObserver(IObserver observer)
        {
            _schedulerObserversList.Add(observer);
        }

        public void DetachObserver(IObserver observer)
        {
            _schedulerObserversList.Remove(observer);
        }
        
        private void _installUpdatesObserversNotification()
        {
            try
            {
                foreach (var observer in _schedulerObserversList)
                {
                    observer.RequestActionProcessing(ActionType.InstallUpdates);
                }
            }
            catch
            {
                // ignore
            }

        }

        private void _maintenanceObserversNotification()
        {
            try
            {
                foreach (var observer in _schedulerObserversList)
                {
                    observer.RequestActionProcessing(ActionType.Maintenance);
                }
            }
            catch
            {
                // ignore
            }

        }
    }
}