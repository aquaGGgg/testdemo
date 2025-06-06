using System;

namespace ToyStoreApi.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
