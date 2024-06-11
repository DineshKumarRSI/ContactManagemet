using AutoMapper;
using ContactApplication.Application.DTOs;
using ContactApplication.Application.Interfaces.Repository;
using ContactApplication.Application.Interfaces.Services;
using ContactApplication.Application.Model;
using ContactApplication.Domain.Entity;

namespace ContactApplication.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<bool> Save(ContactDTO contact)
        {
            var mapObject = _mapper.Map<Contact>(contact);
            return await _contactRepository.Insert(mapObject);
        }

        public async Task<bool> Update(ContactDTO contact)
        {
            var mapObject = _mapper.Map<ContactDTO, Contact>(contact);
            mapObject.Id = contact.Id;

            return await _contactRepository.Update(mapObject);
        }

        public Task<bool> Delete(int id)
        {
            return _contactRepository.Delete(id);
        }

        public async Task<ContactResult> GetAll(TableModel model)
        {
            var contatList = await _contactRepository.GetAll();
            var contactResult = new ContactResult();

            var mapContatList = _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactDTO>>(contatList);


            if (model != null)
            {
                if(string.IsNullOrEmpty(model.Search) == false)
                {
                    mapContatList = mapContatList
                        .Where(f =>
                        f.FirstName.ToLower().Contains(model.Search.ToLower()) == true ||
                        f.LastName.ToLower().Contains(model.Search.ToLower()) == true ||
                        f.Email.ToLower().Contains(model.Search.ToLower()) == true);
                }


                if(model.Sorting != null)
                {
                    mapContatList = Sorting(mapContatList, model.Sorting);
                }

                contactResult.TotalRecord = mapContatList.Count();

                mapContatList = mapContatList.Skip((model.PageNumber -1) * model.PageSize).Take(model.PageSize);
            }

            contactResult.Data = mapContatList.ToList();
            return contactResult;
        }

        public async Task<ContactDTO> GetById(int id)
        {
            var contact = await _contactRepository.GetById(id);

            var mappData = _mapper.Map<Contact, ContactDTO>(contact);
            return mappData;
        }

        private IEnumerable<ContactDTO> Sorting(IEnumerable<ContactDTO> contactDTOs, Sorting sorting)
        {
            switch (sorting.ColumnName)
            {
                case "FirstName":
                    switch (sorting.Order)
                    {
                        case Order.Ascending:
                            contactDTOs = contactDTOs.OrderBy(f => f.FirstName);
                            break;
                        case Order.Descending:
                            contactDTOs = contactDTOs.OrderByDescending(f => f.FirstName);
                            break;
                    }
                    break;
                case "LastName":
                    switch (sorting.Order)
                    {
                        case Order.Ascending:
                            contactDTOs = contactDTOs.OrderBy(f => f.LastName);
                            break;
                        case Order.Descending:
                            contactDTOs = contactDTOs.OrderByDescending(f => f.LastName);
                            break;
                    }
                    break;
                case "Email":
                    switch (sorting.Order)
                    {
                        case Order.Ascending:
                            contactDTOs = contactDTOs.OrderBy(f => f.Email);
                            break;
                        case Order.Descending:
                            contactDTOs = contactDTOs.OrderByDescending(f => f.Email);
                            break;
                    }
                    break;
                case "Id":
                    switch (sorting.Order)
                    {
                        case Order.Ascending:
                            contactDTOs = contactDTOs.OrderBy(f => f.Id);
                            break;
                        case Order.Descending:
                            contactDTOs = contactDTOs.OrderByDescending(f => f.Id);
                            break;
                    }
                    break;
            }

            return contactDTOs;
        }


    }
}
