using NTierManagement.BLL.DTOs.Person;

namespace NTierManagement.BLL.DTOs.Company
{
    public class CreateCompanyDTO
    {
        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public CreateCeoDTO Ceo { get; set; }
    }
}
