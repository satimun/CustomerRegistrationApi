using CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Request.Member;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using KKFCoreEngine.Util;

namespace CustomerRegistrationADO.Connect.Mssql.CRMDB.Table
{
    public class pdrga_s_memberAdo : CRMDBBase
    {
        private static pdrga_s_memberAdo instant;

        public static pdrga_s_memberAdo GetInstant()
        {
            if (instant == null) instant = new pdrga_s_memberAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private pdrga_s_memberAdo() { }

        public List<pdrga_s_member> Search(pdrga_s_member d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", d.id);
            param.Add("@code", d.code);
            param.Add("@email", d.email);
            param.Add("@user_id", d.user_id);
            param.Add("@user_name", d.user_name);
            param.Add("@status", d.status);

            var cmd = new StringBuilder();
            cmd.Append("SELECT * FROM pdrga_s_member ");
            cmd.Append("WHERE (@id IS NULL OR id=@id) ");
            cmd.Append("AND (@code IS NULL OR code=@code) ");
            cmd.Append("AND (@user_name IS NULL OR user_name=@user_name) ");
            cmd.Append("AND (@email IS NULL OR email=@email) ");
            cmd.Append("AND (@status IS NULL OR status=@status) ");

            return Query<pdrga_s_member>(transac, cmd.ToString(), param).ToList();
        }

        public List<pdrga_s_member> Search(MemberSearchReq d, SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();

            param.Add("@ID", d.ID.ListNull());
            param.Add("@Email", d.Email.ListNull());
            param.Add("@Status", d.Status.ListNull());
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            var cmd = new StringBuilder();
            cmd.Append("SELECT * FROM pdrga_s_member ");
            cmd.Append($"WHERE (@ID IS NULL OR ID IN ('{ d.ID.Join("','") }'))");
            cmd.Append($"AND (@Email IS NULL OR Email IN ('{ d.Email.Join("','") }')) ");
            cmd.Append($"AND (@Status IS NULL OR Status IN ('{ d.Status.Join("','") }')) ");
            cmd.Append($"AND (first_name LIKE @txtSearch OR last_name LIKE @txtSearch OR Email LIKE @txtSearch OR location LIKE @txtSearch) ");
            cmd.Append("ORDER BY first_name");

            return Query<pdrga_s_member>(cmd.ToString(), param).ToList();
        }

        public pdrga_s_member Get(int ID)
        {
            var param = new Dapper.DynamicParameters();

            param.Add("@ID", ID);

            var cmd = new StringBuilder();
            cmd.Append("SELECT * FROM pdrga_s_member ");
            cmd.Append($"WHERE ID=@ID");

            return Query<pdrga_s_member>(cmd.ToString(), param).FirstOrDefault();
        }

        public pdrga_s_member GetEmail(string Email)
        {
            var param = new Dapper.DynamicParameters();

            param.Add("@Email", Email);

            var cmd = new StringBuilder();
            cmd.Append("SELECT * FROM pdrga_s_member ");
            cmd.Append($"WHERE Email=@Email");

            return Query<pdrga_s_member>(cmd.ToString(), param).FirstOrDefault();
        }

        public int Insert(pdrga_s_member d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@code", d.code);
            param.Add("@email", d.email);
            param.Add("@password", d.password);
            param.Add("@first_name", d.first_name);
            param.Add("@last_name", d.last_name);
            param.Add("@mobile_no", d.mobile_no);
            param.Add("@location", d.location);
            param.Add("@shipowner_amt", d.shipowner_amt);
            param.Add("@shipusekkfnet_amt", d.shipusekkfnet_amt);
            param.Add("@picture_path", d.picture_path);
            param.Add("@status", d.status);
            param.Add("@create_date", d.create_date);
            param.Add("@user_id", d.user_id);
            param.Add("@user_name", d.user_name);
            param.Add("@user_date", d.user_date);


            var cmd = new StringBuilder();
            cmd.Append("INSERT INTO pdrga_s_member (code, email, password, first_name, last_name, mobile_no, location ");
            cmd.Append(", shipowner_amt ,shipusekkfnet_amt ,picture_path");
            cmd.Append(", status, create_date, user_id,user_name,user_date)");
            cmd.Append(" VALUES (  @code, @email, @password, @first_name, @last_name, @mobile_no, @location");
            cmd.Append(" , @shipowner_amt ,@shipusekkfnet_amt ,@picture_path");
            cmd.Append(" ,@status, GETDATE(), @user_id,@user_name, @user_date)");
            return ExecuteNonQuery(transac, cmd.ToString(), param);
        }

        public int UpdateEmailConfirmed(int ID, string status, string confirm_flag, int userID = 0, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@status", status);
            param.Add("@user_id", userID);
            param.Add("@confirm_flag", confirm_flag);
            param.Add("@ID", ID);

            var cmd = new StringBuilder();
            cmd.Append("UPDATE pdrga_s_member SET ");
            cmd.Append("status=@status, ");
            cmd.Append("emailconfirm_flag=@confirm_flag, ");
            cmd.Append("user_id=@user_id, ");
            cmd.Append("user_date=GETDATE() ");
            cmd.Append("WHERE ID=@ID");
            return ExecuteNonQuery(transac, cmd.ToString(), param);
        }

        public int UpdateStatus(int ID, string status, int userID = 0, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@status", status);
            param.Add("@user_id", userID);
            param.Add("@ID", ID);

            var cmd = new StringBuilder();
            cmd.Append("UPDATE pdrga_s_member SET ");
            cmd.Append("status=@status, ");
            cmd.Append("user_id=@user_id, ");
            cmd.Append("user_date=GETDATE() ");
            cmd.Append("WHERE ID=@ID");
            return ExecuteNonQuery(transac, cmd.ToString(), param);
        }

        public int UpdatePassword(pdrga_s_member d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", d.id);
            param.Add("@password", d.password);
            param.Add("@user_id", d.user_id);
            param.Add("@user_name", d.user_name);
            param.Add("@user_date", d.user_date);

            var cmd = new StringBuilder();
            cmd.Append("UPDATE pdrga_s_member SET ");
            cmd.Append("password=@password, ");
            cmd.Append("user_id=@user_id, ");
            cmd.Append("user_date=GETDATE() ");
            cmd.Append("WHERE ID=@id");
            return ExecuteNonQuery(transac, cmd.ToString(), param);
        }

        public int Update(pdrga_s_member d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", d.id);
            param.Add("@first_name", d.first_name);
            param.Add("@last_name", d.last_name);
            param.Add("@mobile_no", d.mobile_no);
            param.Add("@location", d.location);
            param.Add("@shipowner_amt", d.shipowner_amt);
            param.Add("@shipusekkfnet_amt", d.shipusekkfnet_amt);
            param.Add("@picture_path", d.picture_path);
            param.Add("@user_id", d.user_id);
            param.Add("@user_name", d.user_name);
            param.Add("@user_date", d.user_date);

            var cmd = new StringBuilder();
            cmd.Append("UPDATE pdrga_s_member SET ");
            cmd.Append("first_name=@first_name, ");
            cmd.Append("last_name=@last_name, ");
            cmd.Append("mobile_no=@mobile_no, ");
            cmd.Append("location=@location, ");
            cmd.Append("shipowner_amt=@shipowner_amt, ");
            cmd.Append("shipusekkfnet_amt=@shipusekkfnet_amt, ");
            cmd.Append("picture_path=@picture_path, ");
            cmd.Append("user_id=@user_id, ");
            cmd.Append("user_name=@user_name, ");
            cmd.Append("user_date=GETDATE() ");
            cmd.Append("WHERE ID=@ID");
            return ExecuteNonQuery(transac, cmd.ToString(), param);

        }

    }
}
