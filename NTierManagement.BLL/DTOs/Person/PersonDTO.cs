using NTierManagement.BLL.DTOs.Company;
using NTierManagement.BLL.DTOs.Department;

namespace NTierManagement.BLL.DTOs.Person
{
    public class PersonDTO : PersonBaseDTO
    {
        public CompanyBaseDTO Company { get; set; }

        public DepartmentBaseDTO Department { get; set; }
    }
}
