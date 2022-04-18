using Microsoft.EntityFrameworkCore;
using SynapseWebAPI.Models;

namespace SynapseWebAPI
{
    public class AzureSynapseContext : DbContext
    {
        public AzureSynapseContext(DbContextOptions<AzureSynapseContext> options)
            : base(options)
        {
        }

        public DbSet<ProspectiveBuyer> ProspectiveBuyer { get; set; }

        public DbSet<DimCustomer> Customers { get; set; }

    }
}
