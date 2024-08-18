using NTierManagement.Entity.Models.Person;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTierManagement.Entity.Models.Workplace
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50)]
        public string Subject { get; set; }

        public int Capacity { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();

        [Required]
        public int LeaderID { get; set; }

        [ForeignKey("LeaderID")]
        public Leader Leader { get; set; }
    }
}
