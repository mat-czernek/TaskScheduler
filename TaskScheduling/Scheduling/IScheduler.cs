using TaskScheduling.Enums;

namespace TaskScheduling.Scheduling
{
    public interface IScheduler : IObserverSubject
    {
        void Stop();

        void Start();
    }
}