using TaskScheduling.Enums;
using TaskScheduling.Scheduling;

namespace TaskScheduling.Actions
{
    public class ServiceActionsHandler : IObserver
    {
        public void RequestActionProcessing(ActionTypes actionType)
        {
            switch (actionType)
            {
                case ActionTypes.InstallUpdates:
                {
                    var installUpdates = new InstallUpdatesAction();
                    installUpdates.Execute();
                    return;
                }

                case ActionTypes.Maintenance:
                {
                    var selfHeal = new Maintenance();
                    selfHeal.Execute();
                    return;
                }
                
                default:
                    break;
                    
            }
        }
    }
}