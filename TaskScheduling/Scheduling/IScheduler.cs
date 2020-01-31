using TaskScheduling.Enums;

namespace TaskScheduling.Scheduling
{
    public interface IScheduler : IObserverSubject
    {
        /// <summary>
        /// Method starts the scheduler
        /// </summary>
        void Start();
        
        /// <summary>
        /// Method stops the scheduler
        /// </summary>
        void Stop();
    }
}