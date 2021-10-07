﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine
{
    public class DBaseCheckAPI
    {
        private static DBaseCheckAPI instant;

        public static DBaseCheckAPI GetInstant()
        {
            if (instant == null) instant = new DBaseCheckAPI();
            return instant;
        }

        private string conectStr { get; set; }

        private DBaseCheckAPI() { }


        public int DBaseCheck(string conStr = null)
        {
            string FilePath = "D:\\backup";

            var currentDirectory = System.IO.Directory.GetCurrentDirectory();

            string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=dBASE IV;";

            string DBF_FileName = "STOCK.dbf";
            DBF_FileName = "PRODDM";
            /*
                        OdbcConnection obdcconn = new System.Data.Odbc.OdbcConnection();
                        obdcconn.ConnectionString = "{Microsoft dBASE Driver (*.dbf)};DriverID=277;Dbq=" + FilePath + ";";
                        obdcconn.Open();
                        OdbcCommand command = obdcconn.CreateCommand();  
                        command.CommandText = "SELECT * FROM " + FilePath + DBF_FileName;
                      
            */

            OleDbConnection conn = new OleDbConnection(conString);

            OleDbCommand command = new OleDbCommand("select * from " + DBF_FileName, conn);
            conn.Open();

            /*Load data to table*/

            DataTable dt1 = new DataTable();
            dt1.Load(command.ExecuteReader());


            return 1;
        }
    }
}
