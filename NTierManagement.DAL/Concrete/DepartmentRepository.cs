using NTierManagement.DAL.Abstract;
using NTierManagement.Entity.Context;
using NTierManagement.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierManagement.DAL.Concrete
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ManagementContext context) : base(context)
        {
        }
    }
}
