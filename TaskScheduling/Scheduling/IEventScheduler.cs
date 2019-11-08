using TaskScheduling.Enums;

namespace TaskScheduling.Scheduling
{
    public interface IEventScheduler : IObserverSubject
    {
        void OnEvent(EventType eventType);
    }
}