﻿using ITI_Project.DTO;
using ITI_Project.Models;

namespace ITI_Project.Repository
{
        public interface IUserOrderRepository
    {
        IEnumerable<Order> GetOrdersByUserId(string userId);
           Task<IEnumerable<Order>> UserOrders(bool getAll = false);
            Task ChangeOrderStatus(UpdateOrderStatusModel data);
            Task TogglePaymentStatus(int orderId);
            Task<Order?> GetOrderById(int id);
            Task<IEnumerable<OrderStatus>> GetOrderStatuses();
           
        }
}
