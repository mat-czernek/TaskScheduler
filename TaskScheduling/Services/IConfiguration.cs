using System.Collections.Generic;
using TaskScheduling.Configuration;

namespace TaskScheduling.Services
{
    public interface IConfiguration
    {
        List<TimerConfiguration> TimersConfigurationList { get; }
        
        void Read(string configurationFileName);
    }
}