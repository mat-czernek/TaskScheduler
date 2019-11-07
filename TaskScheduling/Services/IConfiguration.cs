using TaskScheduling.Configuration;

namespace TaskScheduling.Services
{
    public interface IConfiguration
    {
        ISchedulingConfiguration Scheduling { get; }
        
        void Read(string configurationFileName);
    }
}