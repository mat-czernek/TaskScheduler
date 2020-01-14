using TaskScheduling.Configuration;

namespace TaskScheduling.Services
{
    public interface IConfiguration
    {
        SchedulingConfiguration Scheduling { get; }
        
        void Read(string configurationFileName);
    }
}