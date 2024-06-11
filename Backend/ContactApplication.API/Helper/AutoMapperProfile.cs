using AutoMapper;
using ContactApplication.Application.DTOs;
using ContactApplication.Domain.Entity;

namespace ContactApplication.API.Helper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Contact, ContactDTO>();
            CreateMap<ContactDTO, Contact>();
        }
    }
}
