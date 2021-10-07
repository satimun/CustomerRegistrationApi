using CustomerRegistrationAPI.Constant;
using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.PDK2
{
    public class OauthAccessTokenGetApi : EngineBase<dynamic>
    {
        public OauthAccessTokenGetApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(dynamic dataReq, ResponseAPI dataRes)
        {

            //var res = CustomerRegistrationADO.Connect.Mssql.Product.PDT_OauthAccessAdo.GetInstant().Search(AccessToken);

            //if (res.Count == 1)
            //{
            //    CustomerRegistrationADO.Connect.Mssql.Product.PDT_OauthAccessAdo.GetInstant().Update(this.AccessToken);
            //}
            //else
            //{
            //    this.AccessToken = KKFCoreEngine.Util.EncryptUtil.NewID(this.IPAddress);
            //    CustomerRegistrationADO.Connect.Mssql.Product.PDT_OauthAccessAdo.GetInstant().Insert(this.AccessToken, this.IPAddress, this.UserAgent);
            //}

            dataRes.data = new CustomerRegistrationModel.Model.Response.OauthAccessTokenResponse() { accessToken = this.AccessToken };

            StaticValue.GetInstant().AccessKey();

        }
    }
}
