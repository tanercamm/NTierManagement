using NTierManagement.BLL.DTOs.Company;

namespace NTierManagement.BLL.Interfaces
{
    public interface ICompanyService
    {
        Task<List<CompanyDTO>> GetAllWithDetailsAsync();

        Task<List<CompanyBaseDTO>> GetAllAsync();

        Task<CompanyDTO> GetByIdAsync(int id);

        Task AddAsync(CreateCompanyDTO entity);

        Task UpdateAsync(UpdateCompanyDTO entity);

        Task DeleteAsync(int id);
    }
}
