using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Response.Member
{
    public class MemberGetRes : Common.StatusUpdateModel
    {
        public string Code;
        public string Fullname;
        public string Email;
        public string Username;
        public string Picture;
        public string Type
        {
            get { return String.IsNullOrEmpty(Code) ? "M" : "S"; }
        }

    }
}
