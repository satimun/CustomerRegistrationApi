using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Enum;
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
    public class MemberResetPassword : EngineBase<MemberResetPasswordReq>
    {
        public MemberResetPassword()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(MemberResetPasswordReq dataReq, ResponseAPI dataRes)
        {
            if (dataReq.password.Trim() != dataReq.confirmpass.Trim()) throw new Exception(ErrorCode.V007.ToString());

            var token = Store.Token.GetInstant().Get(dataReq.token);

            if (token == null) { throw new Exception(ErrorCode.V005.ToString()); }
            if (token.accesstoken_code != AccessToken ) { throw new Exception(ErrorCode.V005.ToString()); }

            var user = Store.Member.GetInstant().GetMember(token.member_id);  

            if (user == null) { throw new Exception(ErrorCode.V006.ToString()); }
            if (user.status != EnumUtil.GetValueString(Status.Active)) { throw new Exception(ErrorCode.V006.ToString()); }

            var SoftPassword = EncryptUtil.NewID(user.email);
            user.password = EncryptUtil.Hash(dataReq.password.Trim());
            user.user_id = user.id;

            pdrga_s_memberAdo.GetInstant().UpdatePassword(user);
            Store.Member.GetInstant().Save(user);

            DateTimeOffset localTime = DateTime.Now;
            string subject = "KKF Product Registration Account - Password Changed";
            string body = $"<p><b>Dear {user.name} ,</b></p>" +
            $"<p>Your Password Changed {localTime}</p><br/>";

            Task.Run(() => KKFCoreEngine.Email.SendMail.Send(user.email.Trim(), subject, body));
        }
    }
}
