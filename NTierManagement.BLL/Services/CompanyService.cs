using NTierManagement.BLL.DTOs.Company;
using NTierManagement.BLL.Interfaces;

namespace NTierManagement.BLL.Services
{
    public class CompanyService : ICompanyService
    {

        public Task<List<CompanyDTO>> GetAllWithDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<CompanyBaseDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CompanyDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(CreateCompanyDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateCompanyDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
