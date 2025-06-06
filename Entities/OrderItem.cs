using System.ComponentModel.DataAnnotations.Schema;

namespace ToyStoreApi.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        // Внешние ключи
        public int OrderId { get; set; }
        public int ToyId { get; set; }

        // Навигационные свойства
        public Order Order { get; set; }
        public Toy Toy { get; set; }

        public int Quantity { get; set; }

        // Цена на момент заказа
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }
}
