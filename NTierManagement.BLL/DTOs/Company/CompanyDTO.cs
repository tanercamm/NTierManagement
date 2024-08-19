using NTierManagement.BLL.DTOs.Department;
using NTierManagement.BLL.DTOs.Person;

namespace NTierManagement.BLL.DTOs.Company
{
    public class CompanyDTO : CompanyBaseDTO
    {
        public PersonBaseDTO Ceo { get; set; }

        public List<DepartmentBaseDTO> Departments { get; set; }
    }
}