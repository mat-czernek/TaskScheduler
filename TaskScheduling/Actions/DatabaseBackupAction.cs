using System;

namespace TaskScheduling.Actions
{
    /// <summary>
    /// This class represents sample action executed by scheduler. It has been implemented just to show how to
    /// execute action on scheduled time.
    /// </summary>
    public class DatabaseBackupAction : IAction
    {
        /// <summary>
        /// Method simulates the process of database backup creation
        /// </summary>
        public void Execute()
        {
            Console.WriteLine($"Database backup in progress... Execution time [{DateTime.Now}]");
        }
    }
}