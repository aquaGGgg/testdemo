using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToyStoreApi.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        // Дата регистрации
        public DateTime RegisteredAt { get; set; }

        // Навигационное свойство для заказов
        public ICollection<Order> Orders { get; set; }
    }
}
