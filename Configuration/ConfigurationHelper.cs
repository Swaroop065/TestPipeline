using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPipeline.Constants;

namespace TestPipeline.Configuration
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        public ExternalConnections ExternalConnections { get; }

        public RunConfiguration RunConfiguration { get; }

        public ConfigurationHelper(IConfiguration config)
        {
            ExternalConnections = config.GetSection(nameof(ExternalConnections)).Get<ExternalConnections>();
            RunConfiguration = new RunConfiguration { RunLocation = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.TestPipeline) };
        }

        public string GetAdminApiUrl()
        {
            return ExternalConnections.AdminApiUrl;
        }

        public string GetOperationsPath()
        {
            throw new NotImplementedException();
        }

        public string GetOperationsApiUrl()
        {
            return ExternalConnections.OperationsApiUrl;
        }
        public string GetDeliveryIngressApiUrl()
        {
            return ExternalConnections.DeliveryIngressApiUrl;
        }

        public string GetDeliveryIngressPath()
        {
            throw new NotImplementedException();
        }

        public string GetEnvironmentalStatsApiUrl()
        {
            return ExternalConnections.EnvironmentalStatsApiUrl;
        }
        public string GetDatabase()
        {
            return ExternalConnections.DatabaseName;
        }

        public string GetCustomerApiUrl()
        {
            return ExternalConnections.CustomerApiUrl;
        }


       

      
    }
}

