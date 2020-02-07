using TaskScheduling.Enums;
using TaskScheduling.Scheduling;

namespace TaskScheduling.Actions
{
    /// <summary>
    /// Class execute action based on the action type. This has been done to easy manage new action types in future.
    /// </summary>
    public class ServiceActionsHandler : IObserver
    {
        /// <summary>
        /// Method executes the action based on the type
        /// </summary>
        /// <param name="actionType">Type of the action to be executed</param>
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
            }
        }
    }
}