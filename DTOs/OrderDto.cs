using System;
using System.Collections.Generic;

namespace ToyStoreApi.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }  
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        // Список позиций заказа
        public List<OrderItemDto> Items { get; set; }
    }
}
