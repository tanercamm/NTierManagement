using NTierManagement.Entity.Models.Workplace;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTierManagement.Entity.Models.Person
{
    public class Manager : Person
    {
        [Required]
        public int CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public Company Company { get; set; }

        public List<Leader> Leaders { get; set; } = new List<Leader>();

    }
}
