using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Oauth
{
    public class OauthLogout : EngineBase<dynamic>
    {
        public OauthLogout()
        {
            //AllowAnonymous = true;
        }

        protected override void ExecuteChild(dynamic dataReq, ResponseAPI dataRes)
        {
            pdrga_t_tokenAdo.GetInstant().Delete(Token, UserID);

            Store.Token.GetInstant().Delete(Token);
        }
    }
}
