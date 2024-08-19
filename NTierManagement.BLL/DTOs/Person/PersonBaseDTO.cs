using NTierManagement.Entity.Enums;

namespace NTierManagement.BLL.DTOs.Person
{
    public class PersonBaseDTO
    {
        public int PersonID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Roles Role { get; set; }
    }
}
