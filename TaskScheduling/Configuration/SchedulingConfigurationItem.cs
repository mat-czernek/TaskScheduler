using System;
using System.Collections.Generic;
using TaskScheduling.Enums;

namespace TaskScheduling.Configuration
{
    public class SchedulingConfigurationItem : ISchedulingConfigurationItem
    {
        public bool IsEnabled { get; set; }
        
        public List<DateTime> Schedules { get; set; }
        
        public List<EventType> Events { get; set; }
        
        public int RandomDelayInterval { get; set; }
    }
}