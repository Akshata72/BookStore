using DatabaseLayer.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        AddOrder AddOrder(AddOrder addOrder, int UserId);
        List<OrdersResponse> GetAllOrders(int UserId);
    }
}
