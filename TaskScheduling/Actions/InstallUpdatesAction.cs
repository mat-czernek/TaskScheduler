using System;

namespace TaskScheduling.Actions
{
    public class InstallUpdatesAction : IAction
    {
        public void Execute()
        {
            Console.WriteLine($"Installing updates... Execution time [{DateTime.Now}]");
        }
    }
}