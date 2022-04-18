using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynapseWebAPI
{
    [Table("ProspectiveBuyer")]
    public class ProspectiveBuyer
    {
        [Key]
        public int ProspectiveBuyerKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}