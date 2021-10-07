using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Enum;
using CustomerRegistrationModel.Model.Request.Member;
using CustomerRegistrationModel.Model.Response;
using KKFCoreEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Apis.Member
{
    public class MemberForgotPassword : EngineBase<MemberForgotPasswordReq>
    {
        public MemberForgotPassword()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(MemberForgotPasswordReq dataReq, ResponseAPI dataRes)
        {
            var user = Store.Member.GetInstant().GetEmail(dataReq.email);

            if (user == null) { throw new Exception(ErrorCode.V004.ToString()); }

            string token = EncryptUtil.NewID(dataReq.email);

            pdrga_t_tokenAdo.GetInstant().Save(new pdrga_t_token()
            {
                member_id = user.id,
                accesstoken_code = this.AccessToken,
                code = token,
                status = CustomerRegistrationModel.Enum.Status.Active.GetValueString(),
                user_id = user.id
            });


            DateTimeOffset localTime = DateTime.Now;
            string subject = "KKF Product Registration Account - Reset Password";
            string body = $"<p><b>Dear {user.name} ,</b></p>" +
            $"<p>You have reset password. {localTime}</p><br/>" +
            $"Change Password Click : <a href=\"{HostReq}/resetpassword?token={token}\">{HostReq}/resetpassword?token={token}</a><br/>";

            Task.Run(() => KKFCoreEngine.Email.SendMail.Send(dataReq.email.Trim(), subject, body));
        }
    }
}
