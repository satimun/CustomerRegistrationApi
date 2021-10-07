using CustomerRegistrationADO.Connect.Mssql.CRMDB;
using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Enum;
using CustomerRegistrationModel.Model.Request.Member;
using CustomerRegistrationModel.Model.Response;
using CustomerRegistrationModel.Model.Response.Member;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Apis.Member
{
    public class MemberSignUp : EngineBase<MemberSignUpReq>
    {
        private bool Issucces = true;
        private string msg = "SUCCESS";

        public MemberSignUp()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(MemberSignUpReq dataReq, ResponseAPI dataRes)
        {
            if (Store.Member.GetInstant().GetEmail(dataReq.email.Trim()) != null)
            {
                throw new Exception($"Email '{dataReq.email}' is already in use.");
            }


            if (dataReq.password.Trim() != dataReq.confirmpass.Trim()) throw new Exception(ErrorCode.V007.ToString());



            var pass = KKFCoreEngine.Util.EncryptUtil.Hash(dataReq.password.Trim());
            var softpass = KKFCoreEngine.Util.EncryptUtil.NewID(dataReq.email);

            var conn = CRMDBBase.OpenConnection();
            conn.Open();
            SqlTransaction transac = conn.BeginTransaction();
            try
            {
                var newID = KKFCoreEngine.Util.EncryptUtil.NewID(dataReq.email);
                var d = new pdrga_s_member()
                {
                    //code = KKFCoreEngine.Util.EncryptUtil.MD5(newID),
                    code = "",
                    email = dataReq.email.Trim(),
                    first_name = dataReq.first_name.Trim(),
                    last_name = dataReq.last_name.Trim(),
                    password = pass,
                    mobile_no = dataReq.mobile_no,
                    location = dataReq.location,
                    shipowner_amt = dataReq.shipowner_amt,
                    shipusekkfnet_amt = dataReq.shipusekkfnet_amt,
                    picture_path = dataReq.picture_path,
                    status = "N",
                    user_id = 0
                };

                pdrga_s_memberAdo.GetInstant().Insert(d, transac);


                Store.Member.GetInstant().Save(d);

                string subject = "Your KKF Product Registration Account - Verify Your Email Address.";
                string body = $"<p><b>Dear {d.name} ,</b></p>" +
                $"<p>Thank you for member up with KKF Product Registration. Please verify your email account by clicking the link below.</p><br/>" +
                $"Confirm Email Click : <a href=\"{HostReq}/signup?email={dataReq.email}&code={d.code}\">{HostReq}/member/signup?email={dataReq.email}&code={d.code}&emailconfirm=y</a><br/>";

                Task.Run(() => KKFCoreEngine.Email.SendMail.Send(dataReq.email.Trim(), subject, body));

                dataRes.data = new MemberRegisterRes() { email = dataReq.email.Trim() };
            }
            catch (Exception ex)
            {
                Issucces = false;
                transac.Rollback();
                msg = ex.Message;
            }
            finally
            {
                if (Issucces) { transac.Commit(); }
                conn.Close();
            }

            if (!Issucces) { throw new Exception(msg); }
        }
    }
}
