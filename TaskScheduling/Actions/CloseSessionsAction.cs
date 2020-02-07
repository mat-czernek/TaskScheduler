using System;

namespace TaskScheduling.Actions
{
    /// <summary>
    /// This class represents sample action executed by scheduler. It has been implemented just to show how to
    /// execute action on scheduled time.
    /// </summary>
    public class CloseSessionsAction : IAction
    {
        /// <summary>
        /// Method simulates the process of closing the user session
        /// </summary>
        public void Execute()
        {
            Console.WriteLine($"Closing sessions... Execution time [{DateTime.Now}]");
        }
    }
}