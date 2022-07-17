using DatabaseLayer.WishList;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishListRL :IWishListRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectionString;

        public WishListRL(IConfiguration configuration)
        {
            sqlConnectionString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectionString;
        }

        public string AddToWishList(int UserId, int BookId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_AddWishList", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@BookId", BookId);
                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();
                if (result != 0)
                {
                    return "WishList Added Sucssefully..";
                }
                else
                {
                    return "WishList not Added";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public WishListResponse ReadData(WishListResponse wishList, SqlDataReader rdr)
        {
            wishList.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
            wishList.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
            wishList.WishListId = Convert.ToInt32(rdr["WishListId"] == DBNull.Value ? default : rdr["WishListId"]);
            wishList.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
            wishList.AuthorName = Convert.ToString(rdr["AuthorName"] == DBNull.Value ? default : rdr["AuthorName"]);
            wishList.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
            wishList.DiscountPrice = Convert.ToInt32(rdr["DiscountPrice"] == DBNull.Value ? default : rdr["DiscountPrice"]);
            wishList.ActualPrice = Convert.ToInt32(rdr["ActualPrice"] == DBNull.Value ? default : rdr["ActualPrice"]);

            return wishList;
        }
        public List<WishListResponse> GetAllFromWishList(int UserId)
        {
            try
            {
                List<WishListResponse> wishListResponse = new List<WishListResponse>();
                SqlCommand cmd = new SqlCommand("Sp_GetAll_FromWishList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId",UserId);

                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        WishListResponse wishList = new WishListResponse();
                        WishListResponse temp;
                        temp = ReadData(wishList, rdr);
                        wishListResponse.Add(temp);
                    }
                    connection.Close();
                    return wishListResponse;
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

        public string RemoveFromWishList(int UserId, int WishListId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_RemoveWishList",connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId",UserId);
                cmd.Parameters.AddWithValue("@WishListId",WishListId);
                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return "Item Removed from WishList Successfully";
                }
                else
                {
                    return "Failed to Remove item from WishList";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
