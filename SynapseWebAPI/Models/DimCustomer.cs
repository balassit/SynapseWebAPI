using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynapseWebAPI.Models
{
    [Table("DimCustomer")]
    public class DimCustomer
    {
        [Key]
        public int CustomerKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
