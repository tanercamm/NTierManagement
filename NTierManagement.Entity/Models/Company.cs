using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTierManagement.Entity.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int CeoID { get; set; }

        [ForeignKey("CeoID")]
        public Person Ceo { get; set; }

        public List<Department> Departments { get; set; }

        public bool IsDeleted { get; private set; }

        public void Delete()
        {
            IsDeleted = true;
            if (Departments != null)
                Departments.ForEach(x => x.Delete());
        }
    }
}