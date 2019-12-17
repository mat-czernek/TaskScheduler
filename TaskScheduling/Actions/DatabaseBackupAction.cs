using System;

namespace TaskScheduling.Actions
{
    public class DatabaseBackupAction : IAction
    {
        public void Execute()
        {
            Console.WriteLine($"Database backup in progress... Execution time [{DateTime.Now}]");
        }
    }
}