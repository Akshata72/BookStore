using BusinessLayer.Interface;
using DatabaseLayer.Users;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL :IUserBL
    {
        IUserRL userRl;
        public UserBL(IUserRL userRL)
        {
            userRl = userRL;
        }

        public bool ForgotPassword(string EmailId)
        {
            try
            {
               return this.userRl.ForgotPassword(EmailId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LoginResponse Login(LoginUser loginUser)
        {
            try
            {
                return this.userRl.Login(loginUser);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserReg Registration(UserReg userReg)
        {
            try
            {
               return this.userRl.Registration(userReg);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string EmailId, PasswordModel passwordModel)
        {
            try
            {
                return this.userRl.ResetPassword(EmailId, passwordModel);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
