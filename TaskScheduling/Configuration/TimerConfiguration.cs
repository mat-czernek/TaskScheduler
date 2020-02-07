using System;
using System.Collections.Generic;
using TaskScheduling.Enums;

namespace TaskScheduling.Configuration
{
    /// <summary>
    /// Class represents timer configuration. Each public property reflects the single option from the configuration file
    /// </summary>
    public class TimerConfiguration
    {
        /// <summary>
        /// Gets or sets type of the action executed by timer
        /// </summary>
        public ActionType Type { get; set; }
        
        /// <summary>
        /// Gets or sets the flag used to indicate whether the timer is enabled or not
        /// </summary>
        public bool IsEnabled { get; set; }
        
        /// <summary>
        /// List of the schedules from configuration file for particular timer
        /// </summary>
        public List<DateTime> Schedules { get; set; }

        /// <summary>
        /// Defines number of minutes of random delay of timer execution
        /// </summary>
        public int RandomDelayInterval { get; set; }
        
        /// <summary>
        /// Defines number of minutes after which timer must be restarted
        /// </summary>
        public int RepeatCycle { get; set; }
    }
}