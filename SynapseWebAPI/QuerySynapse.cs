using System.Data.CData.AzureSynapse;

namespace SynapseWebAPI
{
    public class QuerySynapse : IQuerySynapse
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        public QuerySynapse(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = this.configuration.GetConnectionString("AzureSynapseContext");
        }

        public void Query()
        {
            using AzureSynapseConnection connection = new(this.connectionString);
            AzureSynapseCommand command = new("Select * FROM DimEmployee", connection);
            AzureSynapseDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"test: {reader["Title"]}");
            }
        }
    }
}
