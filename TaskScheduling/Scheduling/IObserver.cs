using TaskScheduling.Actions;
using TaskScheduling.Enums;

namespace TaskScheduling.Scheduling
{
    /// <summary>
    /// Object observer methods
    /// </summary>
    public interface IObserver
    {
        void RequestActionProcessing(ActionType actionType);
    }
}