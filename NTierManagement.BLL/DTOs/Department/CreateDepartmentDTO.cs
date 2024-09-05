using NTierManagement.BLL.DTOs.Person;

namespace NTierManagement.BLL.DTOs.Department
{
    public class CreateDepartmentDTO
    {
        public string Subject { get; set; }

        public int Capacity { get; set; }

        public string PhoneNumber { get; set; }

        public int CompanyID { get; set; }

        public CreateLeaderDTO Leader { get; set; }
    }
}
