using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Request.Member
{
    public class MemberChangePasswordReq
    {
        public string password;
        public string confpass;
        public string oldpass;
    }
}
