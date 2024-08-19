using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierManagement.BLL.DTOs.Company
{
    public class UpdateCompanyDTO : CreateCompanyDTO
    {
        public int CompanyID { get; set; }
    }
}
