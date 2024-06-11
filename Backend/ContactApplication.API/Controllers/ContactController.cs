using ContactApplication.Application;
using ContactApplication.Application.DTOs;
using ContactApplication.Application.Interfaces.Services;
using ContactApplication.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace ContactApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("customers")]
        public async Task<ContactResult> GetAllContact(TableModel tableModel)
        {
            return await _contactService.GetAll(tableModel);
        }

        [HttpGet("customers/{id}")]
        public async Task<ContactDTO> GetById(int id)
        {
            return await _contactService.GetById(id);
        }

        [HttpPost("add/customer")]
        public async Task<IActionResult> Save(ContactDTO contactDTO)
        {
            if (ModelState.IsValid)
            {
               var status =  await _contactService.Save(contactDTO);

                if (status)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost("update/customer")]
        public async Task<IActionResult> Update(ContactDTO contactDTO)
        {
            if (ModelState.IsValid)
            {
                var status = await _contactService.Update(contactDTO);

                if (status)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpDelete("delete/customer/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id> 0)
            {
                var status = await _contactService.Delete(id);

                if (status)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}
