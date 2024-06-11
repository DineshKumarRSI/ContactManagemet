using ContactApplication.Application.DTOs;
using ContactApplication.Application.Model;
using ContactApplication.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApplication.Infrastructure.UnitTest.Data
{
    public static class TestData
    {
        public static List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();
            contacts.Add(new Contact
            {
                Id = 1,
                Email = "john@exampl.com",
                FirstName = "Test",
                CreatedOn = DateTime.Now,
                LastName = "Test",
                UpdatedOn = DateTime.Now
            });

            contacts.Add(new Contact
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

        public static Contact NewContact()
        {
            return new Contact
            {
                Id = 0,
                Email = "new@example.com",
                FirstName = "New",
                LastName = "Test",
                CreatedOn= DateTime.Now,
                UpdatedOn = DateTime.Now
            };
        }


      


    }
}
