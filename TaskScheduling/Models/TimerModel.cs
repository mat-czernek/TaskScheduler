using System;

namespace TaskScheduling.Models
{
    public class TimerModel
    {
        public System.Threading.Timer Timer { get; set; }

        public Guid Id { get; }

        public TimerModel()
        {
            Id = Guid.NewGuid();
        }
    }
}