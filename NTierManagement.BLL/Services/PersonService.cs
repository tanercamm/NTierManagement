using NTierManagement.BLL.DTOs.Person;
using NTierManagement.BLL.Interfaces;

namespace NTierManagement.BLL.Services
{
    public class PersonService : IPersonService
    {

        public Task<List<PersonDTO>> GetAllWithDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<PersonBaseDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }        

        public Task<PersonDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(CreatePersonDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdatePersonDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
