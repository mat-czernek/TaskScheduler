namespace TaskScheduling.Configuration
{
    public interface ISchedulingConfiguration
    {
        SchedulingConfigurationItem InstallUpdates { get; set; }
        
        SchedulingConfigurationItem Maintenance { get; set; }
    }
}