using ContactApplication.Application.DTOs;
using ContactApplication.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApplication.API.UnitTest.Data
{
    public static class TestData
    {
        public static List<ContactDTO> GetContactDTOs()
        {
            List<ContactDTO> contacts = new List<ContactDTO>();
            contacts.Add(new ContactDTO
            {
                Id = 1,
                Email = "john@exampl.com",
                FirstName = "Test",
                CreatedOn = DateTime.Now,
                LastName = "Test",
                UpdatedOn = DateTime.Now
            });

            contacts.Add(new ContactDTO
            {
                Id = 2,
                Email = "john2@exampl.com",
                FirstName = "Test2",
                CreatedOn = DateTime.Now,
                LastName = "Test2",
                UpdatedOn = DateTime.Now
            });
            return contacts;
        }

        public static ContactDTO NewContact()
        {
            return new ContactDTO
            {
                Id = 0,
                Email = "new@example.com",
                FirstName = "New",
                LastName = "Test",
                CreatedOn= DateTime.Now,
                UpdatedOn = DateTime.Now
            };
        }


        public static ContactResult GetContactResult()
        {
            var contactResult = new ContactResult
            {
                Data = GetContactDTOs(),
                TotalRecord = GetContactDTOs().Count,
            };

            return contactResult;
        }


    }
}
