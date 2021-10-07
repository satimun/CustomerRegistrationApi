using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Oauth;
using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Oauth
{
    public class OauthAccessTokenGet : EngineBase<dynamic>
    {
        public OauthAccessTokenGet()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(dynamic dataReq, ResponseAPI dataRes)
        {
            var res = Store.AccessToken.GetInstant().Get(AccessToken);
            if (res == null)
            {
                res = Store.AccessToken.GetInstant().GetByIP(IPAddress);
                if (res == null)
                {
                    AccessToken = KKFCoreEngine.Util.EncryptUtil.NewID(IPAddress);
                    res = new pdrga_t_accesstoken()
                    {
                        code = AccessToken,
                        ipaddress = IPAddress,
                        agent = UserAgent
                    };
                }
            }

            pdrga_t_accesstokenAdo.GetInstant().Save(res);
            Store.AccessToken.GetInstant().Save(res);

            dataRes.data = new OauthAccessTokenGetRes() { accessToken = res.code };
        }
    }
}
