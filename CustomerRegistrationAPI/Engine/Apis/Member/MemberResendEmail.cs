using CustomerRegistrationModel.Model.Enum;
using CustomerRegistrationModel.Model.Request.Member;
using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Apis.Member
{
    public class MemberResendEmail : EngineBase<MemberVerifyReq>
    {
        public MemberResendEmail()
        {
            AllowAnonymous = true;
        }

        protected override void ExecuteChild(MemberVerifyReq dataReq, ResponseAPI dataRes)
        {

            var d = Store.Member.GetInstant().GetEmail(dataReq.email);  

            if (d == null) { throw new Exception(ErrorCode.V004.ToString()); }

            string subject = "Your KKF Product Registration Account - Verify Your Email Address.";
            string body = $"<p><b>Dear {d.name} ,</b></p>" +
            $"<p>Thank you for member up with KKF Customer Registration. Please verify your email account by clicking the link below.</p><br/>" +
            $"Confirm Email Click : <a href=\"{HostReq}/member/signup?email={dataReq.email}&code={d.code}\">{HostReq}/member/signup?email={dataReq.email}&code={d.code}</a><br/>";

            Task.Run(() => KKFCoreEngine.Email.SendMail.Send(dataReq.email.Trim(), subject, body));
        }
    }
}
