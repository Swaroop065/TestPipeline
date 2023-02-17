using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPipeline.Configuration
{
    public interface IConfigurationHelper
    {
        string GetAdminApiUrl();

        string GetOperationsPath();

        string GetCustomerApiUrl();
        string GetEnvironmentalStatsApiUrl();

   
    }

}
