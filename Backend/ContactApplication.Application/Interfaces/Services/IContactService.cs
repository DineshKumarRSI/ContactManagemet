using ContactApplication.Application.DTOs;
using ContactApplication.Application.Model;
namespace ContactApplication.Application.Interfaces.Services
{
    public interface IContactService
    {
        Task<bool> Save(ContactDTO contact);
        Task<bool> Update(ContactDTO contact);
        Task<bool> Delete(int id);
        Task<ContactDTO> GetById(int id);
        Task<ContactResult> GetAll(TableModel model);
    }
}
