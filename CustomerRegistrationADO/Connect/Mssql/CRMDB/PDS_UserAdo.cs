using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerRegistrationADO.Connect.Mssql.CRMDB
{
    public class PDS_UserAdo : CRMDBBase
    {
        private static PDS_UserAdo instant;

        public static PDS_UserAdo GetInstant()
        {
            if (instant == null) instant = new PDS_UserAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private PDS_UserAdo() { }


        public List<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDS_User> GetData(CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDS_User d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@USER_ID", d.USER_ID);


            string cmd = "SELECT *     " +
                "  FROM CRMDB.dbo.V_PDMAS_S_USER " +
                " WHERE (@USER_ID IS NULL OR USER_ID=@USER_ID) " +
              
                ";";

            var res = Query<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDS_User>(cmd, param).ToList();

            return res;
        }

        public List<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDS_User> Login(CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDS_User d)
        {          
            DynamicParameters param = new DynamicParameters();
            param.Add("@USER_ID", d.USER_ID);
            param.Add("@PASSWORD", d.PASSWORD);


            string cmd = "SELECT *     " +
                "FROM CRMDB.dbo.PDS_USER " +
                 " WHERE (@USER_ID IS NULL OR USER_ID=@USER_ID) " +
                 "   AND (@PASSWORD IS NULL OR PASSWORD=@PASSWORD) " +     
                 ";";

            var res = Query<CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.PDS_User>(cmd, param).ToList();

            return res;
        }
    }
}
