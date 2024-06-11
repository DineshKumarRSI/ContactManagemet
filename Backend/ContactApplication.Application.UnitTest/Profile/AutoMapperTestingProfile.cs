using AutoMapper;
using ContactApplication.Application.DTOs;
using ContactApplication.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApplication.Application.UnitTest
{
    public class AutoMapperTestingProfile : Profile
    {
        public AutoMapperTestingProfile()
        {
            CreateMap<Contact, ContactDTO>();
            CreateMap<ContactDTO, Contact>();
        }
    }
}
