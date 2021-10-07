using CustomerRegistrationModel.Model.Enum;
using CustomerRegistrationModel.Model.Request.Member;
using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Apis.Member
{
    public class MemberPictureChange : EngineBase<MemberPictureChangeReq>
    {
        protected override void ExecuteChild(MemberPictureChangeReq dataReq, ResponseAPI dataRes)
        {
            var user = Store.Member.GetInstant().GetMember(UserID);

            if (user == null) { throw new Exception(ErrorCode.V006.ToString()); }

            KKFCoreEngine.Util.FileUtil.SaveImageFile(dataReq.picture_path, "Members", user.code);
        }
    }
}
