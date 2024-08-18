using NTierManagement.Entity.Models.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NTierManagement.Entity.Models.Workplace
{
    public class Company
    {
        public int CompanyID { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; } 

        [StringLength(200)]
        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        public List<Leader> Leaders { get; set; } = new List<Leader>();

        public List<Employee> Employees { get; set; } = new List<Employee>();

        [Required]
        public int ManagerID { get; set; }

        [ForeignKey("ManagerID")]
        public Manager Manager { get; set; }

    }
}
