using NTierManagement.BLL.DTOs.Person;

namespace NTierManagement.BLL.Interfaces
{
    public interface IPersonService
    {
        Task<List<PersonDTO>> GetAllWithDetailsAsync();

        Task<List<PersonBaseDTO>> GetAllAsync();

        Task<PersonDTO> GetByIdAsync(int id);

        Task AddAsync(CreatePersonDTO entity);

        Task UpdateAsync(UpdatePersonDTO entity);

        Task DeleteAsync(int id);
    }
}