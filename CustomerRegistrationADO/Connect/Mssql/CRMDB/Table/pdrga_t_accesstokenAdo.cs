using CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CustomerRegistrationADO.Connect.Mssql.CRMDB.Table
{
    public class pdrga_t_accesstokenAdo : CRMDBBase
    {
        private static pdrga_t_accesstokenAdo instant;

        public static pdrga_t_accesstokenAdo GetInstant()
        {
            if (instant == null) instant = new pdrga_t_accesstokenAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private pdrga_t_accesstokenAdo() { }

        public List<pdrga_t_accesstoken> ListActive()
        {
            var cmd = new StringBuilder();
            cmd.Append("SELECT * FROM pdrga_t_accesstoken ");
            cmd.Append("WHERE Status='A'");
            return Query<pdrga_t_accesstoken>(cmd.ToString(), null).ToList();
        }

        public List<pdrga_t_accesstoken> Search(string Code, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);

            var cmd = new StringBuilder();
            cmd.Append("SELECT * FROM pdrga_t_accesstoken ");
            cmd.Append("WHERE Code=@Code");
            return Query<pdrga_t_accesstoken>(cmd.ToString(), param).ToList();
        }

        public pdrga_t_accesstoken Get(string Code, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);

            var cmd = new StringBuilder();
            cmd.Append("SELECT * FROM pdrga_t_accesstoken ");
            cmd.Append("WHERE Code=@Code ");


            return Query<pdrga_t_accesstoken>(transac, cmd.ToString(), param).FirstOrDefault();
        }

        public pdrga_t_accesstoken GetByIPAddress(string IPAddress, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@IPAddress", IPAddress);

            var cmd = new StringBuilder();
            cmd.Append("SELECT * FROM pdrga_t_accesstoken ");
            cmd.Append("WHERE IPAddress=@IPAddress ");


            return Query<pdrga_t_accesstoken>(transac, cmd.ToString(), param).FirstOrDefault();
        }

        public int Save(pdrga_t_accesstoken d, SqlTransaction transac = null)
        {
            var accessToken = Get(d.code, transac);
            if (accessToken != null)
            {
                return Update(d, transac);
            }
            return Insert(d, transac);
        }

        private int Insert(pdrga_t_accesstoken d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@code", d.code);
            param.Add("@ipaddress", d.ipaddress);
            param.Add("@agent", d.agent);

            var cmd = new StringBuilder();
            cmd.Append("INSERT INTO pdrga_t_accesstoken (code, ipaddress, agent,  status, user_id,user_name, user_date) ");
            cmd.Append("VALUES (@code, @ipaddress, @agent, 'A', 0,'', GETDATE())");
            return ExecuteNonQuery(transac, cmd.ToString(), param);
        }

        private int Update(pdrga_t_accesstoken d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@code", d.code);

            var cmd = new StringBuilder();
            cmd.Append("UPDATE pdrga_t_accesstoken SET ");
            cmd.Append("user_date=GETDATE() ");
            cmd.Append("WHERE code=@code");
            return ExecuteNonQuery(transac, cmd.ToString(), param);
        }


    }
}
