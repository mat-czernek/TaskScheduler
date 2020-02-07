namespace TaskScheduling.Scheduling
{
    /// <summary>
    /// Observer API
    /// </summary>
    public interface IObserverSubject
    {
        void AttachObserver(IObserver observer);

        void DetachObserver(IObserver observer);
    }
}