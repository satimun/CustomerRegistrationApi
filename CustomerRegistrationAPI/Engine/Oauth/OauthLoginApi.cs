using CustomerRegistrationAPI.Constant;
using CustomerRegistrationModel.Model.Request;
using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.PDK2
{
    public class OauthLoginApi : EngineBase<OauthLoginRequest>
    {
        public OauthLoginApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(OauthLoginRequest dataReq, ResponseAPI dataRes)
        {
            var res = new OauthLoginResponse();

            try
            {
                if (String.IsNullOrEmpty(dataReq.guid))
                {
                    throw new Exception("Can't Access Site.");
                }

                var user = CustomerRegistrationADO.Connect.Mssql.CRMDB.PDS_UserAdo.GetInstant().GetData(new CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDS_User() { USER_ID = dataReq.user_id.Trim() }).FirstOrDefault();
                if (user == null) { throw new Exception("Username Not Found."); }
                //if (user.USE_FLAG != "Y") { throw new Exception("Username is not Confirm."); }

                //  var pass = dataReq.password.Trim();
                var pass = KKFCoreEngine.Util.EncryptUtil.ENDCodeNEW(dataReq.password.Trim());

                /*
                  var config = Ado.Mssql.Table.UserConfig.GetInstant().Search(user.ID);
                  if (config.Where(x => x.TwoFactorEnable).ToList().Count != 0)
                  {
                      var authenticator = new TwoFactorAuthenticator();
                      var isValid = authenticator.ValidateTwoFactorPIN(user.Code, dataReq.twofactor.Replace(" ", ""));
                      if (!isValid)
                      {
                          throw new Exception("T000: 2FA Code invalid.");
                      }
                  }

               */
                // if (user.UserPw == Core.Util.EncryptUtil.Hash(pass + user.SoftPassword))
                //if (user.PASSWORD.Trim() == pass.Trim())
                if (!String.IsNullOrEmpty(pass) && !String.IsNullOrEmpty(dataReq.guid))
                {
                    var _token = KKFCoreEngine.Util.EncryptUtil.Hash(pass);
                    res.token = KKFCoreEngine.Util.EncryptUtil.NewID(_token);
                    res.user_name = user.USER_NAME;
                    res.user_id = user.USER_ID;

                    CustomerRegistrationADO.Connect.Mssql.CRMDB.PDT_OauthSingInAdo.GetInstant().Insert(new CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDT_OauthSingIn()
                    {
                        User_ID = user.USER_ID,
                        AccessToken_Code = this.AccessToken,
                        Token_Code = res.token,
                        Status = "A",
                        Type = "L",
                        ExpiryTime = DateTime.Now.AddMinutes(480)

                    }, user.USER_ID);

                    /*
                    if (config.TrueForAll(x => x.EmailLogin == true))
                    {
                        var access = Ado.Mssql.Table.AccessToken.GetInstant().Search(this.AccessToken).FirstOrDefault();
                        string subject = "Login Notification";
                        string body = $"<p><b>Dear {user.Username} ,</b></p>" +
                        $"<p>This is notify you of a successful login to your account.</p>" +
                        $"<p>Login Time: {DateTime.UtcNow.ToString()}</p>" +
                        $"<p>IP Address: {access.IPAddress}</p>" +
                        $"<p>User Agent: {access.Agent}</p>";

                        Task.Run(() => Core.SendMail.SendMail.Send(user.Email, subject, body));
                    }
                   */
                    dataRes.data = res;
                    StaticValue.GetInstant().TokenKey();
                }
                else
                {
                    throw new Exception("Username or Password was incorrect");
                }

                res.result.status = "S";
                //res.result.statusdesc = "S : ทำรายการสำเร็จ (Success)";
                res.result.message = "";

                dataRes.code = "200";
                dataRes.status = "OK";
                dataRes.message = "";
            }
            catch (Exception ex)
            {
                res.result.status = "F";
                //res.result.statusdesc = "F : ทำรายการไม่สำเร็จ (Fail)";
                res.result.message = ex.Message;

                dataRes.code = "400";
                dataRes.status = "Bad Request";
                dataRes.message = ex.Message;

                throw new Exception(ex.Message);
            }

            

        }
    }
}
