using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierManagement.BLL.DTOs.Company
{
    public class CreateCompanyDTO
    {
        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int CeoID { get; set; }
    }
}
