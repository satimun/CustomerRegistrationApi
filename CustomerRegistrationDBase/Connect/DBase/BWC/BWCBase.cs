﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistrationDBase.Connect.DBase.BWC
{
    public abstract class BWCBase
    {
        public static string conString { get; set; }

        public string SetConnect(string source)
        {
            //return $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={source};Extended Properties=dBASE IV;Persist Security Info=False;"; //User ID=.\administrator;Password=abc123;
            return $"Provider=vfpoledb.1;Data Source={source};Mode=ReadWrite|Share Deny None; Collating Sequence=machine;"; //machine

            // VFPOLEDB.1; Mode = ReadWrite | Share Deny None; Password = ''; Collating Sequence = MACHINE
        }

        private string GetConnect(string conStr)
        {
            return string.IsNullOrEmpty(conStr) ? conString : conStr;
        }

        protected IEnumerable<T> Query<T>(string cmd, DynamicParameters parameter = null, string conStr = null)
        {
            
            using (OleDbConnection conn = new OleDbConnection(GetConnect(conStr)))
            {
                
                var res = SqlMapper.Query<T>(conn, cmd, parameter, null, true,30);
                return res;
            }
        }

        protected int Excuce(string cmd, DynamicParameters parameter = null, string conStr = null)
        {
            using (OleDbConnection conn = new OleDbConnection(GetConnect(conStr)))
            {
                var res = SqlMapper.Execute(conn, cmd, parameter, null,30);
                return res;
            }
        }
    }
}
