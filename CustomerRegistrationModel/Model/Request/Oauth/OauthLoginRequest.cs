using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Request
{
    public class OauthLoginRequest
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string guid { get; set; }

    }
}
