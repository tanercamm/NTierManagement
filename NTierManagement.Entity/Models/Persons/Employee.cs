using NTierManagement.Entity.Models.Workplace;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTierManagement.Entity.Models.Person
{
    public class Employee : Person
    {
        [Required]
        public int DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }
    }
}
