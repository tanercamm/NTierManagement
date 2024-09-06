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

        // Kişinin rolünü güncellerken, eğer CEO ise CompanyID ile kontrol sağlanır
        public void UpdateRole(Roles newRole, int? companyId = null, int? departmentId = null)
        {
            if (newRole == Roles.Ceo)
            {
                // CEO olarak atanıyorsa, DepartmentID null olmalı
                if (departmentId.HasValue)
                    throw new InvalidOperationException("A CEO cannot be assigned to a department.");

                // Mevcut CEO varsa bu kişi silinmiş olarak işaretlenir
                if (Role == Roles.Ceo && this.CompanyID != companyId)
                {
                    Delete();
                }

                CompanyID = companyId;
                DepartmentID = null;
            }
            else if (newRole == Roles.Leader || newRole == Roles.Employee)
            {
                // Leader veya Employee ise DepartmentID zorunlu olmalı
                if (!departmentId.HasValue)
                    throw new InvalidOperationException("A Leader or Employee must have a DepartmentID.");

                CompanyID = companyId;
                DepartmentID = departmentId;
            }

            // Yeni rol ataması
            Role = newRole;
        }
    }
}
