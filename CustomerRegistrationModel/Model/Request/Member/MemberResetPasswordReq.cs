using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Request.Member
{
    public class MemberResetPasswordReq
    {
        public string token;
        public string password;
        public string confirmpass;
    }
}
