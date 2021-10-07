using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;
using CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Store
{
    public class Token
    {
        private static Token instant { get; set; }

        public static Token GetInstant()
        {
            if (instant == null)
            {
                instant = new Token();
                instant.data = new List<pdrga_t_token>();
            }
            return instant;
        }

        private List<pdrga_t_token> data;

        public List<pdrga_t_token> List { get { return data; } }

        public pdrga_t_token Get(string Code)
        {
            if (data == null)
            {
                data = new List<pdrga_t_token>();
            }

            pdrga_t_token res = data.Find(v => v.code == Code);
            if (res == null)
            {
                res = pdrga_t_tokenAdo.GetInstant().Get(Code);
                if (res != null)
                {
                    data.Add(res);
                }
            }
            return res;
        }

        public void Save(pdrga_t_token d)
        {
            var tmp = data.Find(v => v.code == d.code);
            if (tmp == null)
            {
                data.Add(d);
            }
            else
            {
                tmp.status = d.status;
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


    }
}
