using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierManagement.BLL.DTOs.Department
{
    public class UpdateDepartmentDTO
    {
        public int DepartmentID { get; set; }

        public string Subject { get; set; }

        public int Capacity { get; set; }

        public string PhoneNumber { get; set; }

        public int? LeaderID { get; set; }
    }
}