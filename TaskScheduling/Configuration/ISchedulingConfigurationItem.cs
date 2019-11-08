using System;
using System.Collections.Generic;
using TaskScheduling.Enums;

namespace TaskScheduling.Configuration
{
    public interface ISchedulingConfigurationItem
    {
        bool IsEnabled { get; set; }
        
        List<DateTime> Schedules { get; set; }
        
        List<EventType> Events { get; set; }
        
        int RandomDelayInterval { get; set; }
    }
}