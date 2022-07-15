using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Users
{
    public class LoginResponse
    {
        public long MobileNumber { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Token { get; set; }
    }
}
