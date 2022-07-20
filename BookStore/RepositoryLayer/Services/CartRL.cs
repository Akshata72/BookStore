using DatabaseLayer.Carts;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL :ICartRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectionString;

        public CartRL(IConfiguration configuration)
        {
            sqlConnectionString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectionString;
        }

        public Addcart AddToCart(Addcart cart, int UserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AddToCart",connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@BookId",cart.BookId);
                cmd.Parameters.AddWithValue("@BookQuantity", cart.BookQuntity);
                connection.Open();
                var result =cmd.ExecuteNonQuery();
                connection.Close();
                if (result != 0)
                {
                    return cart;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<ResponseCart> GetAllFromCart(int UserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId",UserId);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<ResponseCart> listcart = new List<ResponseCart>();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        ResponseCart cart = new ResponseCart();
                        UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                        cart.BookId = Convert.ToInt32(reader["BookId"] == DBNull.Value ? default : reader["BookId"]);
                        cart.BookName = Convert.ToString(reader["BookName"] == DBNull.Value ? default : reader["BookName"]);
                        cart.AuthorName = Convert.ToString(reader["AuthorName"] == DBNull.Value ? default : reader["AuthorName"]);
                        cart.BookImage = Convert.ToString(reader["BookImage"] == DBNull.Value ? default : reader["BookImage"]);
                        //cart.BookDetails = Convert.ToString(reader["BookDetails"] == DBNull.Value ? default : reader["BookDetails"]);
                        cart.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"] == DBNull.Value ? default : reader["DiscountPrice"]);
                        cart.ActualPrice = Convert.ToInt32(reader["ActualPrice"] == DBNull.Value ? default : reader["ActualPrice"]);
                        cart.BookQuntity = Convert.ToInt32(reader["Book_Quantity"]);
                        listcart.Add(cart);
                    }

                    connection.Close();
                    return listcart;
                }
                else
                {
                    connection.Close();
                    return null;
                }


            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RemoveFromCart(int UserId,int CartId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_RemoveFrom_Cart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@CartId", CartId);

                var res = cmd.ExecuteNonQuery();
                connection.Close();
                if (res != 0)
                {
                    return "Delete from Cart successfuly";
                    
                }
                else
                {
                    return "Failed to Remove From Cart";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateCart(int BookQuntity, int CartId, int UserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@CartId", CartId);
                cmd.Parameters.AddWithValue("@BookQuantity", BookQuntity);
                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();
                if (result != 0)
                {
                    return "BookQuntity Updated Succsefully..";
                }
                else
                {
                    return "BookQuntity Not Updated Succsefully..";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
