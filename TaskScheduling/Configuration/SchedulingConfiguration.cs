namespace TaskScheduling.Configuration
{
    public class SchedulingConfiguration : ISchedulingConfiguration
    {
        public SchedulingConfigurationItem DatabaseBackup { get; set; }
        
        public SchedulingConfigurationItem CloseSessions { get; set; }
    }
}