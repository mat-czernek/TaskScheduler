using System;

namespace TaskScheduling.Models
{
    /// <summary>
    /// CLass represents timer model along with properties required to configure it
    /// </summary>
    public class TimerModel
    {
        public System.Threading.Timer Timer { get; set; }
        
        /// <summary>
        /// Defines time in minutes after which timer should be restarted
        /// </summary>
        public int RepeatCycle { get; set; }

        /// <summary>
        /// Timer unique identifier
        /// </summary>
        public Guid Id { get; }

        public TimerModel()
        {
            Id = Guid.NewGuid();
        }
    }
}