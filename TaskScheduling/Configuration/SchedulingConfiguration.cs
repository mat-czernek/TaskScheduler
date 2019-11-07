namespace TaskScheduling.Configuration
{
    public class SchedulingConfiguration : ISchedulingConfiguration
    {
        public SchedulingConfigurationItem InstallUpdates { get; set; }
        
        public SchedulingConfigurationItem Maintenance { get; set; }
    }
}