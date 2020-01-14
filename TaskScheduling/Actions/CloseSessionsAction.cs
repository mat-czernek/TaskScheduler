using System;

namespace TaskScheduling.Actions
{
    /// <summary>
    /// Class with sample action executed by scheduler
    /// </summary>
    public class CloseSessionsAction : IAction
    {
        public void Execute()
        {
            Console.WriteLine($"Closing sessions... Execution time [{DateTime.Now}]");
        }
    }
}