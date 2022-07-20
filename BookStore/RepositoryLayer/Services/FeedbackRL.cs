using DatabaseLayer.Feedback;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeedbackRL : IFeedbackRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectionString;

        public FeedbackRL(IConfiguration configuration)
        {
            sqlConnectionString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectionString;
        }
        public AddFeedback AddFeedback(AddFeedback addFeedback, int userId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Add_Feedback", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Rating", addFeedback.Rating);
                cmd.Parameters.AddWithValue("@Comment", addFeedback.Comment);
                cmd.Parameters.AddWithValue("@BookId", addFeedback.BookId);
                cmd.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var result = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();

                if (result != 1)
                {
                    return addFeedback;
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

        public List<FeedbackResponse> GetAllFeedbacks(int bookId)
        {
            try
            {
                List<FeedbackResponse> feedbackResponse = new List<FeedbackResponse>();
                SqlCommand cmd = new SqlCommand("SP_GetAll_Feedback", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookId", bookId);

                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        FeedbackResponse feedback = new FeedbackResponse();
                        FeedbackResponse temp;
                        temp = ReadData(feedback, rdr);
                        feedbackResponse.Add(temp);
                    }
                    connection.Close();
                    return feedbackResponse;
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

        public FeedbackResponse ReadData(FeedbackResponse feedback, SqlDataReader rdr)
        {
            feedback.FeedbackId = Convert.ToInt32(rdr["FeedbackId"] == DBNull.Value ? default : rdr["FeedbackId"]);
            feedback.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
            feedback.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
            feedback.Comment = Convert.ToString(rdr["Comment"] == DBNull.Value ? default : rdr["Comment"]);
            feedback.Rating = Convert.ToInt32(rdr["Rating"] == DBNull.Value ? default : rdr["Rating"]);
            feedback.FullName = Convert.ToString(rdr["FullName"] == DBNull.Value ? default : rdr["FullName"]);

            return feedback;
        }
    }
}
