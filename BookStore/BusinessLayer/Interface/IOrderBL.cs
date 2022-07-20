using DatabaseLayer.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderBL
    {
        AddOrder AddOrder(AddOrder addOrder, int UserId);
        List<OrdersResponse> GetAllOrders(int UserId);
    }
}
