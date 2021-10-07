﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Oauth
{
    public class OauthToken
    {
        public string Code;
        public string UserCode;
        public string AccessToken_Code;
        public string BrnCode;
        public DateTime ExpiryTime;
        public string Type;
        public string Status;
        public string UpdateBy;
        public DateTime? Timestamp;
    }
}
