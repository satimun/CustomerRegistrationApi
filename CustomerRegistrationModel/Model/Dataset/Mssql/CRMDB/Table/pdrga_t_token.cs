using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table
{
    public class pdrga_t_token
    {
        public int member_id { get; set; }

        public string code { get; set; }

        public string accesstoken_code { get; set; }

        public string status { get; set; }

        public int? user_id { get; set; }

        public string user_name { get; set; }

        public DateTime? user_date { get; set; }

        public List<SocketModel> _socket;

        public class SocketModel
        {
            public string socketId;
            public int? registerproductId;
        }

    }
}
