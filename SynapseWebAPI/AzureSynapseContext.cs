using Microsoft.EntityFrameworkCore;

namespace SynapseWebAPI
{
    public class ProspectiveBuyerKey
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
    }

    public class AzureSynapseContext : DbContext
    {
        public AzureSynapseContext(DbContextOptions<AzureSynapseContext> options)
            : base(options)
        {
        }

        public DbSet<ProspectiveBuyerKey> Products { get; set; }
    }
}
