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
        public Company? Company { get; set; }

        public int? DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }

        public bool IsDeleted { get; private set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void Validate()
        {
            // CEO (Role 0) için sadece CompanyID gerekli
            if (Role == Roles.Ceo && (!CompanyID.HasValue || DepartmentID.HasValue))
            {
                throw new Exception("A person with the CEO role should belong only to a company and cannot belong to a department.");
            }

            // Leader (Role 1) ve Employee (Role 2) için hem CompanyID hem de DepartmentID gerekli
            if ((Role == Roles.Leader || Role == Roles.Employee) && (!CompanyID.HasValue || !DepartmentID.HasValue))
            {
                throw new Exception("A person with the Leader or Employee role must belong to both a company and a department.");
            }

            // Jobless (Role 3) için CompanyID ve DepartmentID boş olmalı
            if (Role == Roles.Jobless && (CompanyID.HasValue || DepartmentID.HasValue))
            {
                throw new Exception("A person with the Jobless role cannot belong to a company or a department.");
            }
        }
    }
}
