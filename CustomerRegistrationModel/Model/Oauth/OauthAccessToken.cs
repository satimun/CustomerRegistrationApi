using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Oauth
{
    public class OauthAccessToken
    {
        public string Code;
        public string BrnCode;
        public string Ipaddress;
        public string Agent;
        public int CountUse;
        public string Status;
        public int? UpdateBy;
        public DateTime? timestamp;
    }
}
