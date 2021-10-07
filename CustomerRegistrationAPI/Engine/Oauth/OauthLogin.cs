using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Enum;
using CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Enum;
using CustomerRegistrationModel.Model.Oauth;
using CustomerRegistrationModel.Model.Request.Member;
using CustomerRegistrationModel.Model.Response;
using KKFCoreEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Oauth
{
    public class OauthLogin : EngineBase<OauthLoginReq>
    {
        public OauthLogin()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(OauthLoginReq dataReq, ResponseAPI dataRes)
        {
            var res = new OauthLoginRes();

            var user = pdrga_s_memberAdo.GetInstant().Search(new MemberSearchReq() { Email = new List<string>() { dataReq.email.Trim() } }).FirstOrDefault();

            if (user == null) { throw new Exception(ErrorCode.V006.ToString()); }
            if (user.status != "A") { throw new Exception(ErrorCode.V009.ToString()); }

           

            var pass = EncryptUtil.Hash(dataReq.password.Trim());

            var SoftPassword = EncryptUtil.NewID(user.email);
            if (user.password == pass)
            {

                res.token = pass.NewID();
                res.email = user.email;

                var token = new pdrga_t_token()
                {
                    member_id = user.id,
                    accesstoken_code = this.AccessToken,
                    code = res.token,
                    status = Status.Active.GetValueString(),
                    user_id = user.id
                };

                pdrga_t_tokenAdo.GetInstant().Save(token);
                Store.Token.GetInstant().Save(token);

                var access = pdrga_t_accesstokenAdo.GetInstant().Search(this.AccessToken).FirstOrDefault();
                string subject = "KKF Product Registration Account - Login Notification";
                string body = $"<p><b>Dear {user.name} ,</b></p>" +
                $"<p>This is notify you of a successful login to your account.</p>" +
                $"<p>Login Time: {DateTime.UtcNow.ToString()}</p>" +
                $"<p>IP Address: {access.ipaddress}</p>" +
                $"<p>User Agent: {access.agent}</p>";

                Task.Run(() => KKFCoreEngine.Email.SendMail.Send(user.email, subject, body));

                dataRes.data = res;
            }
            else
            {
                throw new Exception(ErrorCode.V010.ToString());
            }

        }
    }
}
