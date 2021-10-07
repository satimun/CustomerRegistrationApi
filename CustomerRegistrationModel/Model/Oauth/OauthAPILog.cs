using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Oauth
{
    public class OauthAPILog
    {
        public int? SeqID;
        public string RefID;
        public string Token;
        public string APIName;
        public string Status;
        public string StatusMessage;
        public DateTime StartDate;
        public DateTime EndDate;
        public string ServerName;
        public string Input;
        public string Output;
        public string Remark;
    }
}
