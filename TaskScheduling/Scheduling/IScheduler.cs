namespace TaskScheduling.Scheduling
{
    public interface IScheduler
    {
        void AttachObserver(IObserver observer);

        void DetachObserver(IObserver observer);

        void Stop();

        void Start();
    }
}