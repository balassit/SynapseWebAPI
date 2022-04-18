using Microsoft.AspNetCore.OData.Query;
using SynapseWebAPI.Models;
using System.Data.CData.AzureSynapse;

namespace SynapseWebAPI
{
    public class QuerySynapse : IQuerySynapse
    {
        private readonly IConfiguration configuration;
        private readonly IServiceProvider serviceProvider;
        private readonly string connectionString;
        public QuerySynapse(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            this.configuration = configuration;
            this.serviceProvider = serviceProvider;
            connectionString = this.configuration.GetConnectionString("AzureSynapseContext");
        }

        public IQueryable Query(ODataQueryOptions<ProspectiveBuyer> query)
        {
            AzureSynapseContext context = serviceProvider.GetService<AzureSynapseContext>();
            IQueryable? items = query.ApplyTo(from buyer in context.ProspectiveBuyer select buyer);
            return items;
        }


        public IQueryable<DimCustomer> Query(ODataQueryOptions<DimCustomer> query)
        {
            AzureSynapseContext context = serviceProvider.GetService<AzureSynapseContext>();
            IQueryable? items = query.ApplyTo(from customer in context.Customers select customer);
            return (IQueryable<DimCustomer>)items;

            /*List<string> result = new();
            using AzureSynapseConnection connection = new(this.connectionString);
            AzureSynapseCommand command = new("Select * FROM DimCustomer", connection);
            AzureSynapseDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                object title = reader["Title"];
                Console.WriteLine($"test: {title}");
                if (title != null)
                {
                    result.Add(title.ToString());
                }
            }
            return result;*/
        }
    }
}
