using ContactApplication.Application.Interfaces.Repository;
using ContactApplication.Domain.Entity;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ContactApplication.Infrastructure.Repository
{
    public class ContactRepository : IContactRepository
    {

        private readonly string _fileLocation;

        public ContactRepository(IConfiguration configuration)
        {
            _fileLocation = configuration.GetSection("Database").Value;
        }

        public async Task<bool> Insert(Contact contact)
        {
            try
            {
                if (File.Exists(this._fileLocation))
                {
                    var allContacts = File.ReadAllText(this._fileLocation);

                    if (allContacts == null || allContacts == "")
                    {
                        contact.Id = 1;
                        contact.CreatedOn = DateTime.Now;
                        var contactList = new List<Contact>();
                        contactList.Add(contact);
                        await File.WriteAllTextAsync(this._fileLocation, JsonSerializer.Serialize(contactList));
                        return true;
                    }
                    else
                    {
                        var contactList = JsonSerializer.Deserialize<List<Contact>>(allContacts);

                        if (contactList == null)
                        {
                            return false;
                        }

                        var lastId = contactList.Max(m => m.Id);
                        contact.Id = (lastId + 1);
                        contact.CreatedOn = DateTime.Now;
                        contactList.Add(contact);
                        await File.WriteAllTextAsync(this._fileLocation, JsonSerializer.Serialize(contactList));
                        return true;
                    }
                }
                else
                {
                    File.Create(this._fileLocation).Close();
                    contact.Id = 1;
                    contact.CreatedOn = DateTime.Now;
                    var contactList = new List<Contact>();
                    contactList.Add(contact);
                    await File.WriteAllTextAsync(this._fileLocation, JsonSerializer.Serialize(contactList));
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Contact contact)
        {
            try
            {
                if (File.Exists(this._fileLocation))
                {
                    var contactList = await GetAllContacts();

                    var findExisting = contactList.FindIndex(f => f.Id == contact.Id);
                    contactList[findExisting] = contact;
                    contactList[findExisting].UpdatedOn = DateTime.Now;

                    await Save(contactList);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            var allContacts = await GetAllContacts();
            if (allContacts == null)
            {
                return new List<Contact>();
            }
            return allContacts;
        }

        public async Task<Contact> GetById(int id)
        {
            var contacts = await GetAllContacts();

            var findContact = contacts.FirstOrDefault(f => f.Id == id);

            if (findContact == null)
            {
                return new Contact();
            }

            return findContact;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var contacts = await GetAllContacts();

                var findContact = contacts.FirstOrDefault(f => f.Id == id);

                if (findContact == null)
                {
                    return false;
                }

                contacts.Remove(findContact);

                await Save(contacts);

                return true;
            }
            catch
            {
                return false;
            }

        }

        private async Task<bool> Save(List<Contact> contactlist)
        {
            await File.WriteAllTextAsync(this._fileLocation, JsonSerializer.Serialize(contactlist));
            return true;
        }

        private async Task<List<Contact>> GetAllContacts()
        {
            if (File.Exists(this._fileLocation))
            {
                var listString = await File.ReadAllTextAsync(this._fileLocation);

                if (string.IsNullOrEmpty(listString))
                {
                    return new List<Contact> { };
                }

                var deserialize = JsonSerializer.Deserialize<IEnumerable<Contact>>(listString);

                deserialize = deserialize.OrderByDescending(d => d.CreatedOn);
                if (deserialize == null)
                {
                    return new List<Contact>();
                }

                return deserialize.ToList();
            }
            return new List<Contact>();
        }

    }
}
