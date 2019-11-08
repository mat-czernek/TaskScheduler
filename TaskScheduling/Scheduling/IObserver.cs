using TaskScheduling.Actions;
using TaskScheduling.Enums;

namespace TaskScheduling.Scheduling
{
    public interface IObserver
    {
        void RequestActionProcessing(ActionType actionType);
    }
}