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
        private readonly List<IObserver> _schedulerObservers;

        private readonly IConfiguration _configuration;

        private readonly List<EventModel> _maintenanceEvents;

        private readonly List<EventModel> _installUpdatesEvents;
        
        public EventScheduler(IConfiguration configuration)
        {
            _configuration = configuration;
            
            _schedulerObservers = new List<IObserver>();
            
            _maintenanceEvents = new List<EventModel>();
            
            _installUpdatesEvents = new List<EventModel>();
            
            _setupEvents();
        }


        private void _setupEvents()
        {
            foreach (var maintenanceEvent in _configuration.Scheduling.Maintenance.Events)
            {
                var eventModel = new EventModel {Event = maintenanceEvent};
                eventModel.EventCallbackMethod += _maintenanceObserversNotification;
                _maintenanceEvents.Add(eventModel);
            }

            foreach (var installUpdatesEvent in _configuration.Scheduling.InstallUpdates.Events)
            {
                var eventModel = new EventModel {Event = installUpdatesEvent};
                eventModel.EventCallbackMethod += _installUpdatesObserversNotification;
                _installUpdatesEvents.Add(eventModel);
            }
        }

        public void OnEvent(EventType eventType)
        {
            _maintenanceEvents.Where(item => item.Event == eventType).ToList().ForEach(item => item.EventCallbackMethod());
            
            _installUpdatesEvents.Where(item => item.Event == eventType).ToList().ForEach(item => item.EventCallbackMethod());
        }

        public void AttachObserver(IObserver observer)
        {
            _schedulerObservers.Add(observer);
        }

        public void DetachObserver(IObserver observer)
        {
            _schedulerObservers.Remove(observer);
        }
        
        private void _installUpdatesObserversNotification()
        {
            try
            {
                foreach (var observer in _schedulerObservers)
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
                foreach (var observer in _schedulerObservers)
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