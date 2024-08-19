using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierManagement.BLL.DTOs.Person
{
    public class UpdatePersonDTO : CreatePersonDTO
    {
        public int PersonID { get; set; }
    }
}