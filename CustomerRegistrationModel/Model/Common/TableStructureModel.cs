using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Common
{
    public class TableStructureModel
    {
        public string table_catalog { get; set; }
        public string table_schema { get; set; }
        public string table_name { get; set; }
        public int ordinal_position { get; set; }
        public string column_name { get; set; }
        public string column_description { get; set; }
        public string data_type { get; set; }
        public string is_nullable { get; set; }
        public int length { get; set; }
        public int precision { get; set; }
        public int scale { get; set; }
        public string column_default { get; set; }
        public string table_key { get; set; }
    }
}
