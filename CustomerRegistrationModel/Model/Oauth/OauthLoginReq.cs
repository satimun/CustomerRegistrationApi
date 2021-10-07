using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Oauth
{
    public class OauthLoginReq
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool remember_me { get; set; }
    }
}
