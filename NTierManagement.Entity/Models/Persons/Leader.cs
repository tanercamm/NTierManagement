using NTierManagement.Entity.Models.Workplace;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NTierManagement.Entity.Models.Person
{
    public class Leader : Person
    {
        [Required]
        public int DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }

    }
}
