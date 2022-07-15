using DatabaseLayer.Users;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL :IUserRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectionString;
        
        public UserRL(IConfiguration configuration)
        {   
            sqlConnectionString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectionString;
        }

        public LoginResponse Login(LoginUser loginUser)
        {
            try
            {
                if (string.IsNullOrEmpty(loginUser.EmailId) || string.IsNullOrEmpty(loginUser.Password))
                    return null;
                else
                {

                    SqlCommand cmd = new SqlCommand("SP_User_Login", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", loginUser.EmailId);
                    //cmd.Parameters.AddWithValue("@Password", loginUser.Password);
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        LoginResponse loginResponse = new LoginResponse();

                        while (reader.Read())
                        {
                            var userId = Convert.ToInt64(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                            var password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);


                            loginResponse.FullName = Convert.ToString(reader["FullName"] == DBNull.Value ? default : reader["FullName"]);
                            loginResponse.EmailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? default : reader["EmailId"]);
                            loginResponse.MobileNumber = Convert.ToInt64(reader["MobileNumber"] == DBNull.Value ? default : reader["MobileNumber"]);


                            var decryptedPassword = DecryptedPassword(password);
                            if (decryptedPassword == loginUser.Password)
                            {
                                loginResponse.Token = GetJWTToken(loginUser.EmailId, userId);

                               return loginResponse;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        return null;

                    }
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return default;
        }
        public static string EncryptPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string DecryptedPassword(string encryptedPassword)
        {
            byte[] b;
            string decrypted;
            try
            {
                if (string.IsNullOrEmpty(encryptedPassword))
                {
                    return null;
                }
                else
                {
                    b = Convert.FromBase64String(encryptedPassword);
                    decrypted = Encoding.ASCII.GetString(b);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string GetJWTToken(string EmailId, long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("EmailId", EmailId),
                    new Claim("UserId",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public UserReg Registration(UserReg userReg)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_UserRegistration",connection);
                cmd.CommandType = CommandType.StoredProcedure;
                var encryptedPassword = EncryptPassword(userReg.Password);
                cmd.Parameters.AddWithValue("@FullName", userReg.FullName);
                cmd.Parameters.AddWithValue("@EmailId",userReg.EmailId);
                cmd.Parameters.AddWithValue("@MobileNumber",userReg.MobileNumber);
                cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                var res = cmd.ExecuteNonQuery();
                connection.Close();
                if (res != 0)
                {
                    return userReg;
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
        private string GenrateToken(string Email)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", Email)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenrateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {

                if (ex.MessageQueueErrorCode ==
                   MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
            }
        }

        public bool ForgotPassword(string EmailId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_ForgatePassword", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId",EmailId);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var userId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);

                        //string token = GenerateSecurityToken(EmailId, userId);
                        MessageQueue messageQueue = new MessageQueue();
                        messageQueue.Path = @".\Private$\FundooQueue";
                        //ADD MESSAGE TO QUEUE
                        if (MessageQueue.Exists(messageQueue.Path))
                        {
                            messageQueue = new MessageQueue(@".\Private$\FundooQueue");
                        }
                        else
                        {
                            messageQueue = MessageQueue.Create(messageQueue.Path);
                        }
                        Message Mymessage = new Message();
                        Mymessage.Formatter = new BinaryMessageFormatter();
                        Mymessage.Body = GenrateToken(EmailId);
                        Mymessage.Label = "Forget Password Label";
                        messageQueue.Send(Mymessage);
                        Message msg = messageQueue.Receive();
                        msg.Formatter = new BinaryMessageFormatter();
                        EmailService.SendEmail(EmailId, msg.Body.ToString());
                        messageQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                        messageQueue.BeginReceive();
                        messageQueue.Close();
                        return true;
                    }
                }
                else return false;
                connection.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return default;
        }

        public bool ResetPassword(string EmailId,PasswordModel passwordModel)
        {
            try
            {
                if (passwordModel.NewPassword == passwordModel.ConfirmPassword)
                {
                    SqlCommand cmd = new SqlCommand("Sp_ResetPassword", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var encryptedPassword = EncryptPassword(passwordModel.ConfirmPassword);
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    cmd.Parameters.AddWithValue("@Password",encryptedPassword);
                    connection.Open();
                    var res = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (res != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                else
                {
                    Console.WriteLine("New Password and Confirm Password not same");
                      return false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
