using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierManagement.BLL.DTOs.Department
{
    public class UpdateDepartmentDTO : CreateDepartmentDTO
    {
        public int DepartmentID { get; set; }
    }
}