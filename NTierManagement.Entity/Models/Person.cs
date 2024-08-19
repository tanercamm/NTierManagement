using NTierManagement.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTierManagement.Entity.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }

        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(15)]
        public string LastName { get; set; }

        public int Age { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Roles Role { get; set; }

        public int? CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public Company Company { get; set; }

        public int? DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }
    }
}
