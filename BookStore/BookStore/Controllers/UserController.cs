using BusinessLayer.Interface;
using DatabaseLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        IUserBL userBl;
        public UserController(IUserBL userBl)
        {
            this.userBl = userBl;
        }
        [HttpPost("Register")]
        public IActionResult Registration(UserReg userReg)
        {
            try
            {
                this.userBl.Registration(userReg);
                return this.Ok(new { success = true, message = "Registration Successful" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("Login")]
        public ActionResult UserLogin(LoginUser userLogin)
        {
            try
            {
                var result = this.userBl.Login(userLogin);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"login Successfull ", Response = result });

                }
                return this.BadRequest(new { success = false, message = $"Login failed" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost("ForgetPassword/{Email}")]
        public ActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = this.userBl.ForgotPassword(Email);
                if (result != false)
                {
                    return this.Ok(new { success = true, message = $"Email has send" });

                }
                return this.BadRequest(new { success = false, message = $"Login failed" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public ActionResult ResetPassword(PasswordModel passwordModel)
        {
            try
            {
                var currentUser = HttpContext.User;
                var Email = (currentUser.Claims.FirstOrDefault(c => c.Type == "Email").Value);
                bool result = this.userBl.ResetPassword(Email, passwordModel);
                if (passwordModel.NewPassword != passwordModel.ConfirmPassword)
                {
                    return this.BadRequest(new { success = false, message = "New Password and Confirm Password must be same" });
                }
                return this.Ok(new { success = true, message = "Password change Successfully" });

            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
