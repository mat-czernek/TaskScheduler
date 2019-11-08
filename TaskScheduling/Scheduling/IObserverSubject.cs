namespace TaskScheduling.Scheduling
{
    public interface IObserverSubject
    {
        void AttachObserver(IObserver observer);

        void DetachObserver(IObserver observer);
    }
}