using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Dataset.Mssql.CRMDB.Table
{
    public class pdrga_s_member
    {
        public int id { get; set; }

        public string code { get; set; }

        public string email { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string mobile_no { get; set; }

        public DateTime? birth_date { get; set; }

        public string gender { get; set; }

        public string location { get; set; }

        public string gps_coordinate { get; set; }

        public decimal? shipowner_amt { get; set; }

        public decimal? shipusekkfnet_amt { get; set; }

        public string picture_path { get; set; }

        public string address_line1 { get; set; }

        public string address_line2 { get; set; }

        public string city { get; set; }

        public int? country_id { get; set; }

        public string postal_code { get; set; }

        public string line { get; set; }

        public string facebook { get; set; }

        public string ig { get; set; }

        public string twitter { get; set; }

        public string password { get; set; }

        public string emailconfirm_flag { get; set; }

        public string status { get; set; }

        public DateTime? create_date { get; set; }

        public int? user_id { get; set; }

        public string user_name { get; set; }

        public DateTime? user_date { get; set; }

        public string name
        {
            get { return first_name + " " + last_name; }
        }

    }
}
