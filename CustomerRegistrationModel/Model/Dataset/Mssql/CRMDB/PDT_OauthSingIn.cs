﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB
{
    public class PDT_OauthSingIn
    {
        public string User_ID;
        
        public string Token_Code;
        public string AccessToken_Code;
        public DateTime  ExpiryTime;
        public string Type;
        public string Status;
        public string User_edit;
        public DateTime? User_date;
    }
}
