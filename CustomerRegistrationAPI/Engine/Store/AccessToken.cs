using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Store
{
    public class AccessToken
    {
        private static AccessToken instant { get; set; }

        public static AccessToken GetInstant()
        {
            if (instant == null)
            {
                instant = new AccessToken();
                instant.data = new List<pdrga_t_accesstoken>();
            }
            return instant;
        }

        private List<pdrga_t_accesstoken> data;

        public pdrga_t_accesstoken Get(string Code)
        {
            if (data == null)
            {
                data = new List<pdrga_t_accesstoken>();
            }

            pdrga_t_accesstoken res = data.Find(v => v.code == Code);
            if (res == null)
            {
                res = pdrga_t_accesstokenAdo.GetInstant().Get(Code);
                if (res != null)
                {
                    data.Add(res);
                }
            }
            return res;
        }

        public pdrga_t_accesstoken GetByIP(string IPAddress)
        {
            pdrga_t_accesstoken res = data.Find(v => v.ipaddress == IPAddress);
            if (res == null)
            {
                res = pdrga_t_accesstokenAdo.GetInstant().GetByIPAddress(IPAddress);
                if (res != null)
                {
                    data.Add(res);
                }
            }
            return res;
        }

        public void Save(pdrga_t_accesstoken d)
        {
            var tmp = data.Find(v => v.code == d.code);
            if (tmp == null)
            {
                data.Add(d);
            }
            else
            {
                tmp.user_date = d.user_date;
            }
        }

        public void Delete(string Code)
        {
            var tmp = data.Find(v => v.code == Code);
            if (tmp != null)
            {
                data.Remove(tmp);
            }
        }

        public void Clear()
        {
            data?.Clear();
        }


    }
}
