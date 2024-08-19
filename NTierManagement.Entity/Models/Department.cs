using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTierManagement.Entity.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [StringLength(50)]
        public string Subject { get; set; }

        public int Capacity { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public int LeaderID { get; set; }

        [ForeignKey("LeaderID")]
        public Person Leader { get; set; }

        [Required]
        public int CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public Company Company { get; set; }

        public List<Person> People { get; set; }
    }
}
