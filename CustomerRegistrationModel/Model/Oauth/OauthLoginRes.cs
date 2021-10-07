using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Oauth
{
    public class OauthLoginRes
    {
        public string username { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}
