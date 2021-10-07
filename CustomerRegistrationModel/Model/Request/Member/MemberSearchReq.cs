using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Request.Member
{
    public class MemberSearchReq
    {
        public string txtSearch { get; set; }
        public List<int> ID { get; set; }
        public List<string> Username { get; set; }
        public List<string> Email { get; set; }
        public List<string> Status { get; set; }
    }
}
