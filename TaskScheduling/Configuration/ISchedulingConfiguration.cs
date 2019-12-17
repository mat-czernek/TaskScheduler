namespace TaskScheduling.Configuration
{
    public interface ISchedulingConfiguration
    {
        SchedulingConfigurationItem DatabaseBackup { get; set; }
        
        SchedulingConfigurationItem CloseSessions { get; set; }
    }
}