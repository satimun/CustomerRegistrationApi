using CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Request.Member
{
    public class MemberSignUpReq : pdrga_s_member
    {
        public string confirmpass { get; set; }
    }
}
