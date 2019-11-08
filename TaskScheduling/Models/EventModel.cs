using System;
using TaskScheduling.Enums;

namespace TaskScheduling.Models
{
    public delegate void EventCallbackMethodDelegate();
    
    public class EventModel
    {
        public EventType Event { get; set; } = EventType.Undefined;

        public Guid Id { get; set; }
        
        public EventCallbackMethodDelegate EventCallbackMethod { get; set; }

        public EventModel()
        {
            Id = Guid.NewGuid();
        }
    }
}