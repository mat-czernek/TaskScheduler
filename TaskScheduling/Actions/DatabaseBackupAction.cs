using System;

namespace TaskScheduling.Actions
{
    /// <summary>
    /// Class with sample action executed by scheduler
    /// </summary>
    public class DatabaseBackupAction : IAction
    {
        public void Execute()
        {
            Console.WriteLine($"Database backup in progress... Execution time [{DateTime.Now}]");
        }
    }
}