using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Request.Member
{
    public class MemberNotifyReq
    {
        public long? ID;
        public string RefKey;
        public Common.CodeDesModel Title;
        public Common.CodeDesModel Message;
        public long NotifyDate;
        public string GroupType;
        public bool IsRead;
        public bool IsClick;
    }
}
