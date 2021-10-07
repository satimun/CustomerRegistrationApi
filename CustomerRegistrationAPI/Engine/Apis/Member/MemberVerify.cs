using CustomerRegistrationModel.Model.Request.Member;
using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KKFCoreEngine.Util;
using CustomerRegistrationModel.Model.Enum;
using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;

namespace CustomerRegistrationAPI.Engine.Apis.Member
{
    public class MemberVerify : EngineBase<MemberVerifyReq>
    {
        public MemberVerify()
        {
            AllowAnonymous = true;
        }

        protected override void ExecuteChild(MemberVerifyReq dataReq, ResponseAPI dataRes)
        {
            var user = Store.Member.GetInstant().GetEmail(dataReq.email.GetStringValue());

            if (user == null) { throw new Exception(ErrorCode.V004.ToString()); }
            if (user.code == dataReq.code.GetStringValue()) { throw new Exception(ErrorCode.V004.ToString()); }

            // pdrga_s_memberAdo.GetInstant().UpdateStatus(user.id, "A", user.id);
            pdrga_s_memberAdo.GetInstant().UpdateEmailConfirmed(user.id, "A", "Y", user.id);

            user.status = "A";
            Store.Member.GetInstant().Save(user);

            string subject = "KKF Product Registration Account - Verification";
            string body = $"<p><b>Dear {user.name} ,</b></p>" +
            $"<p>Confirm, Your Email Verify Success.</p>";

            Task.Run(() => KKFCoreEngine.Email.SendMail.Send(dataReq.email.Trim(), subject, body));


        }
    }
}
