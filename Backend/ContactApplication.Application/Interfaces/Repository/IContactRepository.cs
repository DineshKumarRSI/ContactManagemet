using ContactApplication.Domain.Entity;

namespace ContactApplication.Application.Interfaces.Repository
{
    public interface IContactRepository
    {
        Task<bool> Insert(Contact contact);
        Task<bool> Update(Contact contact);
        Task<bool> Delete(int id);
        Task<Contact> GetById(int id);
        Task<IEnumerable<Contact>> GetAll();
    }
}
