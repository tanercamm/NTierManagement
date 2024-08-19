using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierManagement.BLL.DTOs.Department
{
    public class CreateDepartmentDTO
    {
        public string Subject { get; set; }

        public int Capacity { get; set; }

        public string PhoneNumber { get; set; }
    }
}
