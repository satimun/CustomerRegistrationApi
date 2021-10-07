using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CustomerRegistrationADO.Connect.Mssql.CRMDB
{
    public class PDT_OauthSingInAdo : CRMDBBase
    {
        private static PDT_OauthSingInAdo instant;

        public static PDT_OauthSingInAdo GetInstant()
        {
            if (instant == null) instant = new PDT_OauthSingInAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private PDT_OauthSingInAdo() { }

        public List<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDT_OauthSingIn> ListActive()
        {
            string cmd = "SELECT * FROM PDT_OauthSingIn " +
                "WHERE ExpiryTime > GETDATE() AND Status = 'A';";
            var res = Query<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDT_OauthSingIn>(cmd, null).ToList();
            return res;
        }

        public List<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDT_OauthSingIn> Search(string Token_Code, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Token_Code", Token_Code);

            string cmd = "SELECT * FROM PDT_OauthSingIn " +
                "WHERE Token_Code=@Token_Code;";
            var res = Query<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDT_OauthSingIn>(cmd, param).ToList();
            return res;
        }

        public int Get(string Token_Code, DateTime ExpiryTime, string User_edit = "", SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Token_Code", Token_Code);
            param.Add("@ExpiryTime", ExpiryTime);
            param.Add("@User_edit", User_edit);

            string cmd = $"UPDATE PDT_OauthSingIn SET " +
                "ExpiryTime=@ExpiryTime, " +
                "User_edit;=@User_edit, " +
                "Timestamp=GETDATE() " +
                "WHERE Token_Code=@Token_Code;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(string Token_Code, string User_edit = "", SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Token_Code", Token_Code);
            param.Add("@User_edit", User_edit);

            string cmd = $"UPDATE PDT_OauthSingIn SET " +
                "Status='C', " +
                "UpdateBy=@UpdateBy, " +
                "Timestamp=GETDATE() " +
                "WHERE Token_Code=@Token_Code;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Insert(CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDT_OauthSingIn d, string Token_Code = "", SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@User_ID", d.User_ID);
            param.Add("@Token_Code", d.Token_Code);
            
            param.Add("@AccessToken_Code", d.AccessToken_Code);
            param.Add("@ExpiryTime", d.ExpiryTime);
            param.Add("@Type", d.Type);
            param.Add("@User_edit", d.User_ID);

            string cmd = "INSERT INTO PDT_OauthSingIn (User_ID, Token_Code, AccessToken_Code, ExpiryTime, Type, Status, User_edit, User_date) " +
                "VALUES (@User_ID, @Token_Code, @AccessToken_Code, @ExpiryTime, @Type, 'A', @User_edit, GETDATE());";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

    }
}
 