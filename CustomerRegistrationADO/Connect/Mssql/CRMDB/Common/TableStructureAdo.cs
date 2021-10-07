using Dapper;
using CustomerRegistrationModel.Model.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CustomerRegistrationADO.Connect.Mssql.CRMDB.Common
{
    public class TableStructureAdo : CRMDBBase
    {
        private static TableStructureAdo instant;

        public static TableStructureAdo GetInstant()
        {
            if (instant == null) instant = new TableStructureAdo();
            return instant;
        }

        private string conectStr { get; set; }
        private string sql { get; set; }

        private TableStructureAdo() { }

        public List<TableStructureModel> Get(TableStructureModel dataReq)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@tbname", dataReq.table_name);
            sql = String.Empty;
           

            sql = "SELECT distinct col.TABLE_CATALOG, col.TABLE_SCHEMA, col.TABLE_NAME  ,col.COLUMN_NAME AS[COLUMN_NAME], col.ORDINAL_POSITION ,prop.value AS[COLUMN_DESCRIPTION] " +
 $", CASE " +
$"    WHEN ISNULL(NUMERIC_PRECISION, 200) <> 200 THEN DATA_TYPE +'('  " +
$"         + CAST(NUMERIC_PRECISION AS VARCHAR(5)) " +
$"         + ',' + CAST(NUMERIC_SCALE AS VARCHAR(5)) " +
$"         + ')' " +
$"    WHEN ISNULL(CHARACTER_MAXIMUM_LENGTH, 0) = 0 THEN DATA_TYPE " +
$"  ELSE " +
 $"   DATA_TYPE + '(' " +
$"      + CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR(10)) " +
$"      + ')' " +
$"  END AS DATA_TYPE " +
$",REPLACE(REPLACE(COLUMN_DEFAULT, '(', ''), ')', '') as [COLUMN_DEFAULT]  , col.IS_NULLABLE " +
$", (case when col.COLUMN_NAME = con.COLUMN_NAME then 'PK' else '' end) as [TABLE_KEY] " +
$"FROM INFORMATION_SCHEMA.COLUMNS AS col " +
$"INNER JOIN sys.columns AS sc ON sc.object_id = object_id(col.table_schema + '.' + col.table_name)  AND sc.NAME = col.COLUMN_NAME " +
$" LEFT JOIN sys.extended_properties prop ON prop.major_id = sc.object_id  AND prop.minor_id = sc.column_id AND prop.NAME = 'MS_Description' " +
$" LEFT JOIN information_schema.KEY_COLUMN_USAGE con on con.TABLE_NAME = col.TABLE_NAME and OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA+'.' + CONSTRAINT_NAME), 'IsPrimaryKey') = 1    and con.COLUMN_NAME = col.COLUMN_NAME " +
$" WHERE col.TABLE_NAME = @tbname  order by col.ORDINAL_POSITION";

            return Query<TableStructureModel>(sql, param).ToList();
        }

        public string GenModelClass(string TableName, SqlTransaction transac = null, string conStr = null)
        {
            string result = string.Empty;
            DynamicParameters param = new DynamicParameters();
            param.Add("@TableName", TableName);
            param.Add("@Result", result);


            string res = ExecuteScalarSP<string>("GenModelClass", param);

            return res;
        }


    }
}
