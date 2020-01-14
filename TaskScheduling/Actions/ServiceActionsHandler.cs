using TaskScheduling.Enums;
using TaskScheduling.Scheduling;

namespace TaskScheduling.Actions
{
    /// <summary>
    /// Class implements execution of various types of actions
    /// </summary>
    public class ServiceActionsHandler : IObserver
    {
        public void RequestActionProcessing(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.DatabaseBackup:
                {
                    var installUpdates = new DatabaseBackupAction();
                    installUpdates.Execute();
                    return;
                }

                case ActionType.CloseSessions:
                {
                    var selfHeal = new CloseSessionsAction();
                    selfHeal.Execute();
                    return;
                }
                
                default:
                    break;
                    
            }
        }
    }
}