using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Request
{
    public class SearchReq
    {
        public int? id { get; set; }
        public string code { get; set; }
        public string txtSearch { get; set; }
        public string flag { get; set; }
        public string user_id { get; set; }
        public DateTime? user_date { get; set; }
        public DateTime? user_datetime { get; set; }
        public string mode { get; set; }
        public int?[] ids { get; set; }
        public DateTime? effdate_st { get; set; }
        public DateTime? effdate_en { get; set; }
        
    }
}
