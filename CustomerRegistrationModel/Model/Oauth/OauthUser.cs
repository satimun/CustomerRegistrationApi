﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Oauth
{
    public class OauthUser
    {
        public int? SeqID;
        public string UserID;
        public string Email;
        public string UserName;
        public string Password;
        public string SoftPassword;

        public string BrnCode;
        public string GroupID;
        public string CodPos;
        public string DteeffEx;  
        public string CodPosNew;

        public string ApproverLv;
        public string UserEdit;
        public DateTime? UserDate;

        public string code;
        public string description;
        public string status;
        public bool? type;
    }
}
