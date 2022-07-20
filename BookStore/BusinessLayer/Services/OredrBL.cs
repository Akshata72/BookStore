using BusinessLayer.Interface;
using DatabaseLayer.Order;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OredrBL : IOrderBL
    {
        private readonly IOrderRL orderRL;
        public OredrBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }
        public AddOrder AddOrder(AddOrder addOrder, int userId)
        {
            try
            {
                return orderRL.AddOrder(addOrder, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrdersResponse> GetAllOrders(int userId)
        {
            try
            {
                return orderRL.GetAllOrders(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
