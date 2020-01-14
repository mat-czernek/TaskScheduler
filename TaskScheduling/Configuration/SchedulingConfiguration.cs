namespace TaskScheduling.Configuration
{
    public class SchedulingConfiguration
    {
        public SchedulingConfigurationItem DatabaseBackup { get; set; }
        
        public SchedulingConfigurationItem CloseSessions { get; set; }
    }
}