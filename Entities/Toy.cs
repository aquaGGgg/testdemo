using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToyStoreApi.Entities
{
    public class Toy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        // Количество 
        public int Stock { get; set; }

        // Навигационное свойство для связей с OrderItem
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
