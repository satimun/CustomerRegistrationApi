using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CustomerRegistrationADO.Connect.Mssql.CRMDB
{
    public class PDT_OauthAccessAdo : CRMDBBase
    {
        private static PDT_OauthAccessAdo instant;

        public static PDT_OauthAccessAdo GetInstant()
        {
            if (instant == null) instant = new PDT_OauthAccessAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private PDT_OauthAccessAdo() { }

        public List<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDT_OauthAccess> ListActive()
        {
            string cmd = "SELECT * FROM PDT_OauthAccess " +
                "WHERE Status='A';";
            var res = Query<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDT_OauthAccess>(cmd, null).ToList();
            return res;
        }

        public List<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDT_OauthAccess> Search(string AccessToken_Code, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@AccessToken_Code", AccessToken_Code);

            string cmd = "SELECT * FROM PDT_OauthAccess " +
                "WHERE AccessToken_Code=@AccessToken_Code;";
            var res = Query<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDT_OauthAccess>(cmd, param).ToList();
            return res;
        }

        public int Update(string Code, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);

            string cmd = $"UPDATE PDT_OauthAccess SET " +
                "CountUse=CountUse+1 " +
                "WHERE AccessToken_Code=@AccessToken_Code;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Insert(string AccessToken_Code, string IPAddress, string Agent, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@AccessToken_Code", AccessToken_Code);
            param.Add("@IPAddress", IPAddress);
            param.Add("@Agent", Agent);

            string cmd = "INSERT INTO PDT_OauthAccess (AccessToken_Code, IPAddress, Agent, CountUse, Status, User_ID, User_Date) " +
                "VALUES (@AccessToken_Code, @IPAddress, @Agent, 1, 'A', '', GETDATE());";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }
    }
}
