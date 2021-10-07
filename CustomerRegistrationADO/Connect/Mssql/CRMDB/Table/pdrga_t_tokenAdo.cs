using CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CustomerRegistrationADO.Connect.Mssql.CRMDB.Table
{
    public class pdrga_t_tokenAdo : CRMDBBase
    {
        private static pdrga_t_tokenAdo instant;

        public static pdrga_t_tokenAdo GetInstant()
        {
            if (instant == null) instant = new pdrga_t_tokenAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private pdrga_t_tokenAdo() { }

        public pdrga_t_token Get(string Code, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);

            var cmd = new StringBuilder();
            cmd.Append("SELECT * FROM pdrga_t_token ");
            cmd.Append("WHERE Code=@Code");
            return Query<pdrga_t_token>(transac, cmd.ToString(), param).FirstOrDefault();
        }


        public int Save(pdrga_t_token d, SqlTransaction transac = null)
        {
            var token = Get(d.code, transac);
            if (token != null)
            {
                return Update(d);
            }
            return Insert(d, transac);
        }

        private int Update(pdrga_t_token d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", d.code);
            param.Add("@Status", d.status);

            var cmd = new StringBuilder();
            cmd.Append("UPDATE pdrga_t_token SET ");
            cmd.Append("Status=@Status, ");
            cmd.Append("user_date=GETDATE() ");
            cmd.Append("WHERE Code=@Code");


            return ExecuteNonQuery(transac, cmd.ToString(), param);
        }

        private int Insert(pdrga_t_token d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", d.code);
            param.Add("@member_id", d.member_id);
            param.Add("@accesstoken_code", d.accesstoken_code);
            param.Add("@User_ID", d.user_id);

            var cmd = new StringBuilder();
            cmd.Append("INSERT INTO pdrga_t_token (Code, member_id, accesstoken_code,  Status, User_ID, user_date) ");
            cmd.Append("VALUES (@Code, @member_id, @accesstoken_code,  'A', @User_ID, GETDATE())");
            return ExecuteNonQuery(transac, cmd.ToString(), param);
        }

        public int Delete(string Code, int userID = 0, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);
            param.Add("@User_ID", userID);

            var cmd = new StringBuilder();
            cmd.Append("UPDATE pdrga_t_token SET ");
            cmd.Append("Status='C', ");
            cmd.Append("User_ID=@User_ID, ");
            cmd.Append("user_date=GETDATE() ");
            cmd.Append("WHERE Code=@Code");
            return ExecuteNonQuery(transac, cmd.ToString(), param);
        }


    }
}
