using TaskScheduling.Enums;

namespace TaskScheduling.Scheduling
{
    public interface ITimerScheduler : IObserverSubject
    {
        void Stop();

        void Start();
    }
}