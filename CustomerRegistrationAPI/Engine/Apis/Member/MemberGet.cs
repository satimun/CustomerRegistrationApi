using CustomerRegistrationModel.Model.Response;
using CustomerRegistrationModel.Model.Response.Member;
using KKFCoreEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Apis.Member
{
    public class MemberGet : EngineBase<dynamic>
    {
        protected override void ExecuteChild(dynamic dataReq, ResponseAPI dataRes)
        {
            var res = Store.Member.GetInstant().GetMember(UserID);
            var data = new MemberGetRes();
            if(res != null)
            {
                data = new MemberGetRes()
                {
                    Code = res.code,
                    Fullname = res.name,
                    Email = res.email,
                    Status = res.status,
                    user_id = res.user_id,
                    user_name= Store.Member.GetInstant().GetUserDetail(res.user_id),
                    user_date = DateTimeUtil.GetNumber(res.user_date),
                    Picture = res.picture_path
                };
            }
            dataRes.data = data;
        }
    }
}
