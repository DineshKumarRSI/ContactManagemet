using ContactApplication.Domain.Entity;
using ContactApplication.Infrastructure.Repository;
using ContactApplication.Infrastructure.UnitTest.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Text.Json;

namespace ContactApplication.Infrastructure.UnitTest
{
    [TestClass]
    public class ContactRepositoryTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private string _testFileLocation = "testContacts.json";
        private ContactRepository _contactRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(config => config.GetSection("Database").Value)
                .Returns(_testFileLocation);

            _contactRepository = new ContactRepository(_mockConfiguration.Object);

            if (File.Exists(_testFileLocation))
            {
                File.Delete(_testFileLocation);
            }
            else
            {
                File.Create(_testFileLocation).Close();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_testFileLocation))
            {
                File.Delete(_testFileLocation);
            }
        }

        [TestMethod]
        public async Task Should_Insert_New_Contact()
        {
            var contact = TestData.NewContact();

            var result = await _contactRepository.Insert(contact);
            var contacts = await _contactRepository.GetAll();

            Assert.IsTrue(result);
            Assert.AreEqual(1, contacts.Count());
            Assert.AreEqual(contact.FirstName, contacts.First().FirstName);

        }

        [TestMethod]
        public async Task Should_Insert_ExistingContacts_WithIncrementedId()
        {
            var existingContacts = new List<Contact>();
            existingContacts.Add(TestData.GetContacts().FirstOrDefault(f => f.Id == 1));

            await File.WriteAllTextAsync(_testFileLocation, JsonSerializer.Serialize(existingContacts));
            var contact = TestData.GetContacts().FirstOrDefault(f => f.Id == 2);
            contact.Id = 0;

            var result = await _contactRepository.Insert(contact);

            Assert.IsTrue(result);

            var contacts = await _contactRepository.GetAll();
            Assert.AreEqual(2, contacts.Count());
            Assert.IsNotNull(contacts.Where(w => w.Id == 2));
        }

        [TestMethod]
        public async Task Should_Update_ExistingContact()
        {
            var existingContacts = new List<Contact>();
            existingContacts.Add(TestData.GetContacts().FirstOrDefault(f => f.Id == 1));

            await File.WriteAllTextAsync(_testFileLocation, JsonSerializer.Serialize(existingContacts));

            var updatedContact = new Contact { Id = 1, LastName = "Smith" };

            var result = await _contactRepository.Update(updatedContact);

            Assert.IsTrue(result);
            var contacts = await _contactRepository.GetAll();
            Assert.AreEqual(1, contacts.Count());
            Assert.AreEqual(updatedContact.LastName, contacts.First().LastName);
        }

        [TestMethod]
        public async Task Should_Delete_ExistingContact()
        {

            var existingContacts = new List<Contact>();
            existingContacts.Add(TestData.GetContacts().FirstOrDefault(f => f.Id == 1));

            await File.WriteAllTextAsync(_testFileLocation, JsonSerializer.Serialize(existingContacts));

            var result = await _contactRepository.Delete(1);

            Assert.IsTrue(result);
            var contacts = await _contactRepository.GetAll();
            Assert.AreEqual(0, contacts.Count());
        }

        [TestMethod]
        public async Task Should_Return_Contact_ById()
        {
            var existingContacts = new List<Contact>();
            existingContacts.Add(TestData.GetContacts().FirstOrDefault(f => f.Id == 1));

            await File.WriteAllTextAsync(_testFileLocation, JsonSerializer.Serialize(existingContacts));

            var contact = await _contactRepository.GetById(1);

            Assert.IsNotNull(contact);
            Assert.AreEqual(existingContacts[0].Email, contact.Email);
        }

        [TestMethod]
        public async Task  Should_Return_Empty_Contact_WhenNotExist()
        {
            var contact = await _contactRepository.GetById(1);

            Assert.IsNotNull(contact);
            Assert.AreEqual(0, contact.Id);
            Assert.IsNull(contact.FirstName);
        }

        [TestMethod]
        public async Task Should_Return_All_Contacts()
        {
            var existingContacts = TestData.GetContacts();
            await File.WriteAllTextAsync(_testFileLocation, JsonSerializer.Serialize(existingContacts));

            var contacts = await _contactRepository.GetAll();

            Assert.AreEqual(2, contacts.Count());
        }
    }
}
