using System;

namespace TaskScheduling.Actions
{
    public class CloseSessionsAction : IAction
    {
        public void Execute()
        {
            Console.WriteLine($"Closing sessions... Execution time [{DateTime.Now}]");
        }
    }
}