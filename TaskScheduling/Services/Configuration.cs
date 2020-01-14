using System;
using System.IO;
using Newtonsoft.Json.Linq;
using TaskScheduling.Configuration;

namespace TaskScheduling.Services
{
    public class Configuration : IConfiguration
    {
        public SchedulingConfiguration Scheduling { get; private set; }

        public Configuration()
        {
            Scheduling = new SchedulingConfiguration();
        }

        public void Read(string configurationFileName)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configurationFileName);
            
            var fileContent = File.ReadAllText(filePath);

            var jsonObject = JObject.Parse(fileContent);

            Scheduling = jsonObject["SchedulingSettings"].ToObject<SchedulingConfiguration>();
        }
    }
}