namespace TaskScheduling.Actions
{
    /// <summary>
    /// Interface defines contract of action executed by scheduler
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Method executes the sample operation
        /// </summary>
        void Execute();
    }
}