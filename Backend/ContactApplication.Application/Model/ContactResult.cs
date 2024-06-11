using ContactApplication.Application.DTOs;

namespace ContactApplication.Application.Model
{
    public class ContactResult
    {
        public int TotalRecord { get; set; }
        public List<ContactDTO> Data { get; set; }
    }
}
