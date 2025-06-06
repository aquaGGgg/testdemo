using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyStoreApi.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        // Сумма заказа (вычисляемая вручную в сервисе)
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        // Внешний ключ
        public int CustomerId { get; set; }

        // Навигационное свойство
        public Customer Customer { get; set; }

        // Коллекция позиций заказа
        public ICollection<OrderItem> Items { get; set; }
    }
}
