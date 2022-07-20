using DatabaseLayer.Order;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRL :IOrderRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectionString;

        public OrderRL(IConfiguration configuration)
        {
            sqlConnectionString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectionString;
        }
        public AddOrder AddOrder(AddOrder addOrder, int UserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Add_Orders", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookId", addOrder.BookId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AddressId", addOrder.AddressId);

                connection.Open();
                var result = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();

                if (result != 2 && result != 3 && result != 4)
                {
                    return addOrder;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<OrdersResponse> GetAllOrders(int UserId)
        {
            try
            {
                List<OrdersResponse> ordersResponse = new List<OrdersResponse>();
                SqlCommand cmd = new SqlCommand("SP_GetAll_Orders", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        OrdersResponse order = new OrdersResponse();
                        OrdersResponse temp;
                        temp = ReadData(order, rdr);
                        ordersResponse.Add(temp);
                    }
                    connection.Close();
                    return ordersResponse;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrdersResponse ReadData(OrdersResponse order, SqlDataReader rdr)
        {
            order.OrderId = Convert.ToInt32(rdr["OrderId"] == DBNull.Value ? default : rdr["OrderId"]);
            order.AddressId = Convert.ToInt32(rdr["AddressId"] == DBNull.Value ? default : rdr["AddressId"]);
            order.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
            order.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
            order.BooksQty = Convert.ToInt32(rdr["Books_Qty"] == DBNull.Value ? default : rdr["Books_Qty"]);
            order.OrderDateTime = Convert.ToDateTime(rdr["Order_Date"] == DBNull.Value ? default : rdr["Order_Date"]);
            order.OrderDate = order.OrderDateTime.ToString("dd-MM-yyyy");
            order.OrderPrice = Convert.ToInt32(rdr["Order_Price"] == DBNull.Value ? default : rdr["Order_Price"]);
            order.ActualPrice = Convert.ToInt32(rdr["Actual_Price"] == DBNull.Value ? default : rdr["Actual_Price"]);
            order.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
            order.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
            order.AuthorName = Convert.ToString(rdr["AuthorName"] == DBNull.Value ? default : rdr["AuthorName"]);

            return order;
        }
    }
}
