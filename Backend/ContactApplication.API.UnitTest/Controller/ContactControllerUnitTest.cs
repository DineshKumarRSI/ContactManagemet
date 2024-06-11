using ContactApplication.API.Controllers;
using ContactApplication.API.UnitTest.Data;
using ContactApplication.Application;
using ContactApplication.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ContactApplication.API.UnitTest.Controller
{
    [TestClass]
    public class ContactControllerUnitTest
    {
        private Mock<IContactService> _mockContactService;
        private ContactController _contactController;


        [TestInitialize]
        public void Setup()
        {
            _mockContactService = new Mock<IContactService>();
            _contactController = new ContactController(_mockContactService.Object);
        }


        [TestMethod]
        public async Task Should_Return_All_Contact()
        {
            TableModel tableModel = new TableModel();
            var contactResult = TestData.GetContactResult();
            _mockContactService.Setup(service => service.GetAll(tableModel)).ReturnsAsync(contactResult);

            var result = await _contactController.GetAllContact(tableModel);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(result, contactResult);
        }

        [TestMethod]
        public async Task Should_Retrun_Contact_ById()
        {
            var contact = TestData.GetContactDTOs().FirstOrDefault(w=> w.Id == 2);
            _mockContactService.Setup(service => service.GetById(2)).ReturnsAsync(contact);

            var result = await _contactController.GetById(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, contact);
        }

        [TestMethod]
        public async Task Should_Save_Valid_Contact()
        {
            var contact = TestData.NewContact();

            _mockContactService.Setup(service => service.Save(contact)).ReturnsAsync(true);

            var result = await _contactController.Save(contact);

            Assert.IsInstanceOfType(result, typeof(OkResult));

        }

        [TestMethod]
        public async Task Should_Return_BadRequest_For_Invalid_Contact()
        {
            var contact = TestData.NewContact();
            contact.Email = "wrong_email";
            _mockContactService.Setup(service => service.Save(contact)).ReturnsAsync(false);

            var result = await _contactController.Save(contact);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task  Should_Update_Valid_Contact()
        {
            var contact = TestData.GetContactDTOs().FirstOrDefault(w => w.Id == 2);
            contact.FirstName = "Updated First Name";
            
            _mockContactService.Setup(service => service.Update(contact)).ReturnsAsync(true);

            var result = await _contactController.Update(contact);

            Assert.IsInstanceOfType(result, typeof(OkResult));

        }

        [TestMethod]
        public async Task Should_Return_BadRequest_For_Invalid_Update_Contact()
        {
            var contact = TestData.GetContactDTOs().FirstOrDefault(w => w.Id == 2);
            contact.FirstName = "Updated First Name";
            contact.Email = "wrong_email";
            _mockContactService.Setup(service => service.Update(contact)).ReturnsAsync(false);

            var result = await _contactController.Update(contact);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task Should_Delete_Valid_Contact()
        {
            _mockContactService.Setup(service => service.Delete(2)).ReturnsAsync(true);

            var result = await _contactController.Delete(2);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task Should_Return_BadRequest_For_InValid_Contact()
        {
            _mockContactService.Setup(service => service.Delete(2)).ReturnsAsync(false);

            var result = await _contactController.Delete(2);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

    }
}
