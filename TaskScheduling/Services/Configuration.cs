using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaskScheduling.Configuration;

namespace TaskScheduling.Services
{
    /// <summary>
    /// Class reads timers settings from the configuration file
    /// </summary>
    public class Configuration : IConfiguration
    {
        /// <summary>
        /// List of the timers configurations
        /// </summary>
        public List<TimerConfiguration> TimersConfigurationList { get; private set; }

        public Configuration()
        {
            TimersConfigurationList = new List<TimerConfiguration>();
        }

        /// <summary>
        /// Method reads the timers configurations from the configuration file located in application
        /// root directory
        /// </summary>
        /// <param name="configurationFileName">Name of the configuration file</param>
        public void Read(string configurationFileName)
        {
            var configurationFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configurationFileName);
            
            var configurationFileContent = File.ReadAllText(configurationFilePath);

            var schedulersConfiguration = JObject.Parse(configurationFileContent)["TimersSettings"];

            TimersConfigurationList = JsonConvert.DeserializeObject<List<TimerConfiguration>>(schedulersConfiguration.ToString());
        }
    }
}