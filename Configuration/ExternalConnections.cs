using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPipeline.Configuration
{
    public class ExternalConnections
    {
        public string CustomerApiUrl { get; set; }

        public string CustomerSiteUrl { get; set; }

        public string AdminApiUrl { get; set; }

        public string AdminSiteUrl { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string StripeApiUrl { get; set; }

        public string OperationsApiUrl { get; set; }
        public string DeliveryIngressApiUrl { get; set; }
        public string EnvironmentalStatsApiUrl { get; set; }
    }
}
