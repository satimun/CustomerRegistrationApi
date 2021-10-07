using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Enum;
using CustomerRegistrationModel.Model.Request.Member;
using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Apis.Member
{
    public class MemberChangePassword : EngineBase<MemberChangePasswordReq>
    {
        protected override void ExecuteChild(MemberChangePasswordReq dataReq, ResponseAPI dataRes)
        {
            if (dataReq.password.Trim() != dataReq.confpass.Trim()) throw new Exception(ErrorCode.V007.ToString());

            var user = Store.Member.GetInstant().GetMember(UserID); 

            if (user == null) { throw new Exception(ErrorCode.V006.ToString()); }

            var softpass = KKFCoreEngine.Util.EncryptUtil.NewID(user.email);

            if (user.password == KKFCoreEngine.Util.EncryptUtil.Hash(KKFCoreEngine.Util.EncryptUtil.Hash(dataReq.oldpass.Trim())))
            {
                user.password = KKFCoreEngine.Util.EncryptUtil.Hash(KKFCoreEngine.Util.EncryptUtil.Hash(dataReq.password.Trim()));
                user.user_id = user.id;

                pdrga_s_memberAdo.GetInstant().Update(user);

                Store.Member.GetInstant().Save(user);
            }
            else
            {
                throw new Exception(ErrorCode.V008.ToString());
            }
        }
    }
}
