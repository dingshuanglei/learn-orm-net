/*
* 命名空间: Learn.Repository.DbContexts
*
* 功 能： N/A
* 类 名： DbHelperSql
*
* Version 1.0.0
* Time    2020/11/5 14:36:44
* Author  dingshuanglei
* ──────────────────────────────────
*
* Copyright (c) 2020 dingshuanglei . All rights reserved.
*┌─────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．│
*│　版权所有: 丁双磊  　　　　　                 　　　　　　　　   │
*└─────────────────────────────────┘
*/
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Learn.Repository.DbContexts
{
    /// <summary>
    /// DbHelperSql
    /// </summary>
    public partial class DbHelperSql
    {
        public string connectionString { get; set; } = "Database=learndb;Server=localhost;Port=3306;UserId=learn_user;Password=learn_user;";

        /// <summary>
        /// query sql return datatable
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(sql, connection);
                    command.Fill(dt);
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return dt;
            }
        }


        public DataTable ExecuteDataTable(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter apdater = new MySqlDataAdapter(cmd);
                    apdater.Fill(dataTable);
                    return dataTable;
                }
            }
        }


        //public List<T> SqlQuery<T>(string sql) where T : class, new()
        //{
        //    var dataTable = ExecuteDataTable(sql);
        //    return DataTableToList<T>(dataTable);
        //}

        //public List<T> SqlQuery<T>(string sql, params MySqlParameter[] parameters) where T : class, new()
        //{
        //    var dataTable = ExecuteDataTable(sql, parameters);
        //    return DataTableToList<T>(dataTable);
        //}

        //public List<T> DataTableToList<T>(DataTable dt) where T : class, new()
        //{
        //    var propertyInfos = typeof(T).GetProperties();
        //    var list = new List<T>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        var t = new T();
        //        foreach (PropertyInfo p in propertyInfos)
        //        {
        //            if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
        //                p.SetValue(t, row[p.Name], null);
        //        }
        //        list.Add(t);
        //    }
        //    return list;
        //}
    }
}
