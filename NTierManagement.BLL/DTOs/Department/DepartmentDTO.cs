using NTierManagement.BLL.DTOs.Company;
using NTierManagement.BLL.DTOs.Person;

namespace NTierManagement.BLL.DTOs.Department
{
    public class DepartmentDTO : DepartmentBaseDTO
    {
        public CompanyBaseDTO Company { get; set; }

        public List<PersonBaseDTO> People { get; set; }
    }
}