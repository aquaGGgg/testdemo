using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToyStoreApi.DTOs;
using ToyStoreApi.Entities;
using ToyStoreApi.Repositories.Interfaces;
using ToyStoreApi.Services.Interfaces;

namespace ToyStoreApi.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IToyRepository _toyRepository;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IToyRepository toyRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _toyRepository = toyRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(o => MapToDto(o));
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return null;

            return MapToDto(order);
        }

        public async Task<OrderDto> CreateAsync(OrderDto orderDto)
        {
            if (!await _customerRepository.ExistsAsync(orderDto.CustomerId))
                throw new ArgumentException("Клиент не найден.");

            if (orderDto.Items == null || !orderDto.Items.Any())
                throw new ArgumentException("Заказ должен содержать хотя бы одну позицию.");

            var newOrder = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderDate = orderDto.OrderDate == default
                    ? DateTime.UtcNow
                    : orderDto.OrderDate,
                Items = new List<OrderItem>()
            };

            decimal total = 0;
            foreach (var itemDto in orderDto.Items)
            {
                if (!await _toyRepository.ExistsAsync(itemDto.ToyId))
                    throw new ArgumentException($"Игрушка с ID={itemDto.ToyId} не найдена.");

                var toy = await _toyRepository.GetByIdAsync(itemDto.ToyId);

                if (toy.Stock < itemDto.Quantity)
                    throw new ArgumentException($"Недостаточно товара на складе (ID={toy.Id}).");

                // Уменьшаем запас на складе
                toy.Stock -= itemDto.Quantity;
                await _toyRepository.UpdateAsync(toy);

                var orderItem = new OrderItem
                {
                    ToyId = itemDto.ToyId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = toy.Price
                };

                newOrder.Items.Add(orderItem);
                total += toy.Price * itemDto.Quantity;
            }

            newOrder.TotalAmount = total;

            await _orderRepository.AddAsync(newOrder);

            orderDto.Id = newOrder.Id;
            return orderDto;
        }

        public async Task<bool> UpdateAsync(int id, OrderDto orderDto)
        {
            if (!await _orderRepository.ExistsAsync(id))
                return false;

            var existingOrder = await _orderRepository.GetByIdAsync(id);

            foreach (var oldItem in existingOrder.Items)
            {
                var toyInStock = await _toyRepository.GetByIdAsync(oldItem.ToyId);
                toyInStock.Stock += oldItem.Quantity;
                await _toyRepository.UpdateAsync(toyInStock);
            }

            existingOrder.Items.Clear();

            decimal newTotal = 0;
            foreach (var itemDto in orderDto.Items)
            {
                if (!await _toyRepository.ExistsAsync(itemDto.ToyId))
                    throw new ArgumentException($"Игрушка с ID={itemDto.ToyId} не найдена.");

                var toy = await _toyRepository.GetByIdAsync(itemDto.ToyId);

                if (toy.Stock < itemDto.Quantity)
                    throw new ArgumentException($"Недостаточно товара на складе (ID={toy.Id}).");

                toy.Stock -= itemDto.Quantity;
                await _toyRepository.UpdateAsync(toy);

                var newItem = new OrderItem
                {
                    OrderId = existingOrder.Id,
                    ToyId = itemDto.ToyId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = toy.Price
                };

                existingOrder.Items.Add(newItem);
                newTotal += toy.Price * itemDto.Quantity;
            }

            existingOrder.TotalAmount = newTotal;
            existingOrder.OrderDate = orderDto.OrderDate == default
                ? existingOrder.OrderDate
                : orderDto.OrderDate;

            await _orderRepository.UpdateAsync(existingOrder);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _orderRepository.ExistsAsync(id))
                return false;

            var order = await _orderRepository.GetByIdAsync(id);

            foreach (var item in order.Items)
            {
                var toy = await _toyRepository.GetByIdAsync(item.ToyId);
                toy.Stock += item.Quantity;
                await _toyRepository.UpdateAsync(toy);
            }

            await _orderRepository.DeleteAsync(id);
            return true;
        }

        private OrderDto MapToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ToyId = i.ToyId,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
}
