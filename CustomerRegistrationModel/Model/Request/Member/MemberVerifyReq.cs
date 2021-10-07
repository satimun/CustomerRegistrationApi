using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Request.Member
{
    public class MemberVerifyReq
    {
        public string email { get; set; }
        public string code { get; set; }
    }
}
