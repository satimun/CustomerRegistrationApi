using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table
{
    public class pdrga_t_accesstoken
    {
        public string code { get; set; }

        public string ipaddress { get; set; }

        public string agent { get; set; }

        public string status { get; set; }

        public int? user_id { get; set; }

        public string user_name { get; set; }

        public DateTime? user_date { get; set; }

    }
}
