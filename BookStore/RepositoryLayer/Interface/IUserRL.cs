using DatabaseLayer.Users;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserReg Registration(UserReg userReg);
        public LoginResponse Login(LoginUser loginUser);
        public bool ForgotPassword(string EmailId);
        public bool ResetPassword(string EmailId,PasswordModel passwordModel);
    }
}
