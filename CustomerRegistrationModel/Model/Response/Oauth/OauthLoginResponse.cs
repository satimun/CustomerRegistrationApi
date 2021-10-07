﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Response
{
    public class OauthLoginResponse
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string token { get; set; }

        public ResultDataResponse result = new ResultDataResponse();
    }

    public class ResultDataResponse
    {
        public string status { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }
}
