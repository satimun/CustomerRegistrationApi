﻿using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Oracle.ManagedDataAccess.Client;

namespace CustomerRegistrationADO.Connect.Oracle.HrmsV10
{
    public abstract class HrmsV10Base
    {
        public static string conString { get; set; }

        private string GetConnect(string conStr)
        {
            // "HrmsV10": "User Id=PERS;Password=pers123;Data Source=191.20.2.36:1521/HRMS:Min Pool Size=10,Incr Pool Size=5;Decr Pool Size=2;Connection Timeout=60;"
            // return string.IsNullOrWhiteSpace(con) ? "User Id=PERS;Password=pers123;Data Source=191.20.2.36:1521/HRMS:Min Pool Size=10,Incr Pool Size=5;Decr Pool Size=2;Connection Timeout=60;" : con;
            return string.IsNullOrEmpty(conStr) ? conString : conStr;

        }

        // Query
        protected IEnumerable<T> Query<T>(string cmd, OracleDynamicParameters param = null, string conStr = null)
        {
            using (OracleConnection conn = new OracleConnection(GetConnect(conStr)))
            {
                conn.Open();
                var res = SqlMapper.Query<T>(conn, cmd, param, null, true, 60);
                conn.Close();
                return res;
            }
        }
        protected IEnumerable<T> Query<T>(OracleTransaction transaction, string cmd, OracleDynamicParameters param = null, string conStr = null)
        {
            if (transaction == null) { return Query<T>(cmd, param, conStr); }
            var res = SqlMapper.Query<T>(transaction.Connection, cmd, param, transaction, true, 60);
            return res;
        }

        public int ExecuteNonQuery(string cmd, List<OracleParameter> param, string conStr = null)
        {
            using (OracleConnection conn = new OracleConnection(GetConnect(conStr)))
            {
                conn.Open();
                OracleCommand command = new OracleCommand(cmd, conn);
                param.ForEach(x => command.Parameters.Add(x));
                var res = command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                if (res != 1) { throw new Exception("Not Success."); }
                return res;
            }
        }

        public int ExecuteNonQuery(OracleTransaction transac, string cmd, List<OracleParameter> param, string conStr = null)
        {
            if (transac == null) { return ExecuteNonQuery(cmd, param, conStr); }

            OracleCommand command = new OracleCommand(cmd, transac.Connection);
            param.ForEach(x => command.Parameters.Add(x));
            var res = command.ExecuteNonQuery();
            if (res != 1) { throw new Exception("Not Success."); }
            return res;
        }

        // open connection
        public static OracleConnection OpenConnection(string conStr = null)
        {
            OracleConnection conn = new OracleConnection(conStr);
            return conn;
        }

        public static OracleTransaction BeginTransaction()
        {
            var conn = OpenConnection();
            conn.Open();
            return conn.BeginTransaction();
        }
    }
}
