using TaskScheduling.Enums;
using TaskScheduling.Scheduling;

namespace TaskScheduling.Actions
{
    public class ServiceActionsHandler : IObserver
    {
        public void RequestActionProcessing(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.InstallUpdates:
                {
                    var installUpdates = new InstallUpdatesAction();
                    installUpdates.Execute();
                    return;
                }

                case ActionType.Maintenance:
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