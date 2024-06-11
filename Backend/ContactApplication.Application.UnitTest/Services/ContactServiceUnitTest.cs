using AutoMapper;
using ContactApplication.API.UnitTest.Data;
using ContactApplication.Application.DTOs;
using ContactApplication.Application.Interfaces.Repository;
using ContactApplication.Application.Model;
using ContactApplication.Application.Services;
using ContactApplication.Domain.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ContactApplication.Application.UnitTest.Services
{
    [TestClass]
    public class ContactServiceUnitTest
    {
        private ContactService _contactService;
        private IMapper _mapper;
        private Mock<IContactRepository> _mockContactRepository;

        [TestInitialize]
        public void Setup()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperTestingProfile());

            });

            _mapper = config.CreateMapper();

            _mockContactRepository = new Mock<IContactRepository>();
            _contactService = new ContactService(_mockContactRepository.Object, _mapper);
        }

        [TestMethod]
        public async Task Should_Save_Contact()
        {
            var newContact = TestData.NewContact();

            var mapContact = _mapper.Map<Contact>(newContact);

            _mockContactRepository.Setup(service => service.Insert(It.Is<Contact>(c =>
                c.Id == mapContact.Id &&
                c.LastName == mapContact.LastName &&
                c.FirstName == mapContact.FirstName &&
                c.CreatedOn == mapContact.CreatedOn &&
                c.UpdatedOn == mapContact.UpdatedOn &&
                c.Email == mapContact.Email
            ))).ReturnsAsync(true);

            var result = await _contactService.Save(newContact);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Should_Update_Contact()
        {
            var contact = TestData.GetContactDTOs().FirstOrDefault(f=> f.Id == 2);

            contact.Email = "abc@example.com";

            var mapContact = _mapper.Map<Contact>(contact);

            _mockContactRepository.Setup(service => service.Update(It.Is<Contact>(c =>
                c.Id == mapContact.Id &&
                c.LastName == mapContact.LastName &&
                c.FirstName == mapContact.FirstName &&
                c.CreatedOn == mapContact.CreatedOn &&
                c.UpdatedOn == mapContact.UpdatedOn &&
                c.Email == mapContact.Email
            ))).ReturnsAsync(true);

            var result = await _contactService.Update(contact);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Should_Delete_Contact()
        {
            _mockContactRepository.Setup(service => service.Delete(2)).ReturnsAsync(true);
            var result = await _contactService.Delete(2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Should_Return_All_Contact()
        {
            TableModel tableModel = new TableModel();
            tableModel.PageNumber = 1; 
            tableModel.PageSize = 5;

            var contacts = TestData.GetContactDTOs();
            var contactList = _mapper.Map<IEnumerable<ContactDTO>, IEnumerable<Contact>>(contacts);
            var contactResult = new ContactResult();

            contactResult.Data = contacts;
            contactResult.TotalRecord = contacts.Count;

            _mockContactRepository.Setup(service => service.GetAll()).ReturnsAsync(contactList);

            var result = await _contactService.GetAll(tableModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TotalRecord, contactResult.TotalRecord);

            for (int i = 0; i < contacts.Count; i++) {
                Assert.AreEqual(result.Data[i].Id, contactResult.Data[i].Id);
                Assert.AreEqual(result.Data[i].FirstName, contactResult.Data[i].FirstName);
                Assert.AreEqual(result.Data[i].LastName, contactResult.Data[i].LastName);
                Assert.AreEqual(result.Data[i].Email, contactResult.Data[i].Email);
                Assert.AreEqual(result.Data[i].CreatedOn, contactResult.Data[i].CreatedOn);
                Assert.AreEqual(result.Data[i].UpdatedOn, contactResult.Data[i].UpdatedOn);
            }

        }

        [TestMethod]
        public async Task Should_Return_Contact_By_Id()
        {
            var contact = TestData.GetContactDTOs().FirstOrDefault(f=> f.Id == 2);
            var mapContact = _mapper.Map<Contact>(contact);

            _mockContactRepository.Setup(service => service.GetById(2)).ReturnsAsync(mapContact);

            var result = await _contactService.GetById(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, contact.Id);
            Assert.AreEqual(result.FirstName, contact.FirstName);
            Assert.AreEqual(result.LastName, contact.LastName);
            Assert.AreEqual(result.Email, contact.Email);
            Assert.AreEqual(result.CreatedOn, contact.CreatedOn);
            Assert.AreEqual(result.UpdatedOn, contact.UpdatedOn);
        }

    }
}
