using System;

namespace TaskScheduling.Actions
{
    public class Maintenance : IAction
    {
        public void Execute()
        {
            Console.WriteLine($"Maintenance in progress... Execution time [{DateTime.Now}]");
        }
    }
}