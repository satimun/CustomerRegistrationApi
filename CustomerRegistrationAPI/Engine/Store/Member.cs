using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Store
{
    public class Member
    {
        private static Member instant { get; set; }

        public static Member GetInstant()
        {
            if (instant == null)
            {
                instant = new Member();
                instant.data = new List<pdrga_s_member>();
            }
            return instant;
        }

        private List<pdrga_s_member> data;

        public pdrga_s_member GetMember(int ID)
        {
            if (data == null)
            {
                data = new List<pdrga_s_member>();
            }

            pdrga_s_member res = data?.Find(v => v?.id == ID);
            if (res == null)
            {
                res = Add(pdrga_s_memberAdo.GetInstant().Get(ID));
                if (res != null)
                {
                    data.Add(res);
                }
            }
            return res;
        }

        public pdrga_s_member GetEmail(string Email)
        {
            if (data == null)
            {
                data = new List<pdrga_s_member>();
            }

            pdrga_s_member res = data?.Find(v => v!= null && v.email == Email);
            if (res == null)
            {
                res = Add(pdrga_s_memberAdo.GetInstant().GetEmail(Email));
                if (res != null)
                {
                    data.Add(res);
                }
            }
            return res;
        }

        public pdrga_s_member Add(pdrga_s_member req)
        {
            data.Add(req);
            return req;
        }

        public void Save(pdrga_s_member d)
        {
            var tmp = data.Find(x => x != null && x.id == d.id);
            if (tmp == null)
            {
                data.Add(d);
            }
            else
            {
                tmp.code = d.code;
                tmp.password = d.password;
                tmp.email = d.email;
                tmp.status = d.status;

                tmp.user_date = d.user_date;
            }
        }

        public string GetUserDetail(int? userId)
        {
            if (userId.HasValue)
            {
                var tmp = GetMember(userId.Value);
                return tmp != null ? string.Concat(tmp.code, " : ", tmp.name) : null;
            }
            return null;
        }

        public string UserNameSplit(string userName)
        {
            var tmp = userName?.Split(" : ");
            if (tmp != null && tmp.Length > 0)
            {
                userName = tmp[0];
            }
            return userName;
        }



    }
}
