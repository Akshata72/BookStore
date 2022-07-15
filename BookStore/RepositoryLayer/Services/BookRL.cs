using DatabaseLayer.Book;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectionString;

        public BookRL(IConfiguration configuration)
        {
            sqlConnectionString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectionString;
        }
        public BookPostModel AddBook(int UserId,BookPostModel bookPost)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_AddBook",connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@BookName",bookPost.BookName);
                cmd.Parameters.AddWithValue("@AuthorName",bookPost.AuthorName);
                cmd.Parameters.AddWithValue("@BookImage", bookPost.BookImage);
                cmd.Parameters.AddWithValue("@BookDetails",bookPost.BookDetails);
                cmd.Parameters.AddWithValue("@ActualPrice",bookPost.ActualPrice);
                cmd.Parameters.AddWithValue("@DiscountPrice",bookPost.DiscountPrice);
                cmd.Parameters.AddWithValue("@Quntity",bookPost.BookQuntity);
                cmd.Parameters.AddWithValue("@Rating",bookPost.BookRating);
                cmd.Parameters.AddWithValue("@RatingCount", bookPost.RatingCount);
                cmd.Parameters.AddWithValue("@UserId",UserId);
                
                var res = cmd.ExecuteNonQuery();
                connection.Close();
                if(res != 0)
                {
                    return bookPost;
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
       
        public string DeleteBook(int UserId, int Bookid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_DeleteBook", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@BookId", Bookid);

                var res = cmd.ExecuteNonQuery();
                connection.Close();
                if (res == 0)
                {
                    return "Failed to delte the book";
                }
                else
                {
                    return "Book deleted successfuly";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public BookPostModel ReadData(int UserId,BookPostModel bookModel, SqlDataReader reader)
        {
            UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
            bookModel.BookId = Convert.ToInt32(reader["BookId"] == DBNull.Value ? default : reader["BookId"]);
            bookModel.BookName = Convert.ToString(reader["BookName"] == DBNull.Value ? default : reader["BookName"]);
            bookModel.AuthorName = Convert.ToString(reader["AuthorName"] == DBNull.Value ? default : reader["AuthorName"]);
            bookModel.BookImage = Convert.ToString(reader["BookImage"] == DBNull.Value ? default : reader["BookImage"]);
            bookModel.BookDetails = Convert.ToString(reader["BookDetails"] == DBNull.Value ? default : reader["BookDetails"]);
            bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"] == DBNull.Value ? default : reader["DiscountPrice"]);
            bookModel.ActualPrice = Convert.ToInt32(reader["ActualPrice"] == DBNull.Value ? default : reader["ActualPrice"]);
            bookModel.BookQuntity = Convert.ToInt32(reader["Quntity"] == DBNull.Value ? default : reader["Quntity"]);
            bookModel.BookRating = (float)Convert.ToDouble(reader["Rating"] == DBNull.Value ? default : reader["Rating"]);
            bookModel.RatingCount = Convert.ToInt32(reader["RatingCount"] == DBNull.Value ? default : reader["RatingCount"]);

            return bookModel;
        }

        public BookPostModel GetBookById(int UserId,int BookId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_GetBookById", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                BookPostModel model = new BookPostModel();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@BookId", BookId);

                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model = ReadData(UserId,model,reader);
                    }
                    connection.Close();
                    return model;
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

        public UpdateBook UpdateBook(int UserId, int BookId, UpdateBook updateBook)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_UpdateBook", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@BookImage", updateBook.BookImage);
                cmd.Parameters.AddWithValue("@BookDetails", updateBook.BookDetails);
                cmd.Parameters.AddWithValue("@ActualPrice", updateBook.ActualPrice);
                cmd.Parameters.AddWithValue("@DiscountPrice", updateBook.DiscountPrice);
                cmd.Parameters.AddWithValue("@Quntity", updateBook.BookQuntity);
                cmd.Parameters.AddWithValue("@Rating", updateBook.BookRating);
                cmd.Parameters.AddWithValue("@RatingCount", updateBook.RatingCount);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@BookId", BookId);

                var res = cmd.ExecuteNonQuery();
                connection.Close();
                if (res != 0)
                {
                    return updateBook;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BookPostModel> GetAllBooks(int UserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_GetAllBook",connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId",UserId);
                connection.Open();
                List<BookPostModel> books = new List<BookPostModel>();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        BookPostModel book = new BookPostModel();
                        BookPostModel temp;
                        temp = ReadData(UserId,book, reader);
                        books.Add(temp);
                    }
                    connection.Close();
                    return books;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
