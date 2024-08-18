using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace NTierManagement.Entity.Models.Person
{
    public class Person
    {
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
    }
}
