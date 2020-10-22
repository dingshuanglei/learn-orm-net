/*
* 命名空间: Learn.Repository.Base
*
* 功 能： N/A
* 类 名： BaseRepository
*
* Version 1.0.0
* Time    2020/9/29 16:20:56
* Author  dingshuanglei
* ──────────────────────────────────
*
* Copyright (c) 2020 dingshuanglei . All rights reserved.
*┌─────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．│
*│　版权所有: 丁双磊  　　　　　                 　　　　　　　　   │
*└─────────────────────────────────┘
*/
using Learn.Model.Data;
using Learn.Repository.DbContexts;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Learn.Repository.Base
{
    /// <summary>
    /// BaseRepository
    /// </summary>
    public partial class BaseRepository<T> where T : BaseEntity
    {
        BaseDbContext context;
        public BaseRepository(BaseDbContext _context)
        {
            context = _context;
        }

        #region base method

        /// <summary>
        /// add entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>return true or false</returns>
        public bool Add(T entity)
        {
            string sql = GetInsertSql(entity);
            context.Database.ExecuteSqlRaw(sql);
            return true;
        }

        /// <summary>
        /// udpate entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>return true or false</returns>
        public bool Update(T entity)
        {
            string sql = GetUpdateSql(entity);
            MySqlParameter[] parameters = GetMySqlParameters(entity.GetType().GetProperties(), entity);
            int row = context.Database.ExecuteSqlRaw(sql, parameters);
            return row > 0 ? true : false;
        }

        #endregion

        #region private method

        #region private table method
        /// <summary>
        /// get current-table-name
        /// </summary>
        /// <returns>return table-name</returns>
        private string GetCurrentTableName()
        {
            string currentTableName = string.Empty;
            var t = typeof(T);
            if (t != null && !t.Name.IsNullOrEmpty())
            {
                currentTableName = t.Name;
            }
            if (currentTableName.IsNullOrEmpty())
            {
                throw new ArgumentNullException("get table-name is null");
            }
            return currentTableName;
        }
        #endregion

        #region private build sql
        /// <summary>
        /// get insert sql
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>return insert sql</returns>
        private string GetInsertSql(T entity)
        {
            string tableName = GetCurrentTableName();
            StringBuilder sbField = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();
            List<PropertyInfo> properties = GetExcludeKeyAllFields();
            foreach (var item in properties)
            {
                sbField.AppendFormat("{0},", item.Name);
                sbValue.AppendFormat("{0},", GetValue(item, entity));
            }
            sbField.Remove(sbField.Length - 1, 1);
            sbValue.Remove(sbValue.Length - 1, 1);
            //todo ;SELECT @@identity return identity
            return string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, sbField, sbValue);
        }

        /// <summary>
        /// get update sql
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string GetUpdateSql(T entity)
        {
            string tableName = GetCurrentTableName();
            StringBuilder stringBuilder = new StringBuilder();
            List<PropertyInfo> properties = GetExcludeKeyAllFields();
            foreach (var item in properties)
            {
                stringBuilder.AppendFormat("{0}=@{0},", item.Name);
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append(" WHERE ");
            List<PropertyInfo> propertyKeys = GetKey();
            foreach (var propertyKey in propertyKeys)
            {
                stringBuilder.AppendFormat("{0}=@{0} AND ", propertyKey.Name);
            }
            stringBuilder.Remove(stringBuilder.Length - 4, 4);
            return string.Format("UPDATE {0} SET {1}", tableName, stringBuilder);
        }
        #endregion

        #region private fields
        /// <summary>
        /// get exclude key fields
        /// </summary>
        /// <returns>return exclude key fields</returns>
        private List<PropertyInfo> GetExcludeKeyAllFields()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            // filter abstract virtual property
            properties = properties.Where(p => p.PropertyType.IsAbstract == false && !p.GetMethod.IsVirtual == true).ToArray();
            if (properties == null || properties.Length <= 0)
            {
                throw new ArgumentNullException("value fields is null");
            }
            List<PropertyInfo> list = new List<PropertyInfo>();
            foreach (var item in properties)
            {
                if (item.CustomAttributes.Any(c => c.AttributeType.Name == nameof(KeyAttribute)))
                {
                    continue;
                }
                list.Add(item);
            }
            if (list == null || list.Count <= 0)
            {
                throw new ArgumentNullException("value fields is null");
            }
            return list;
        }

        /// <summary>
        /// get key-atttribute
        /// </summary>
        /// <returns>return key-attribute</returns>
        private List<PropertyInfo> GetKey()
        {
            List<PropertyInfo> list = new List<PropertyInfo>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var item in properties)
            {
                if (item.CustomAttributes.Any(c => c.AttributeType.Name == nameof(KeyAttribute)))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// get value
        /// </summary>
        /// <param name="property"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string GetValue(PropertyInfo property, T entity)
        {
            var val = property.GetValue(entity);
            if (val == null)
            {
                return "NULL";
            }
            else
            {
                string prefixN = string.Empty;
                if (property.PropertyType.Name == nameof(String) || property.PropertyType.Name == nameof(Char))
                {
                    prefixN = "N";
                }
                if (property.PropertyType.Name == nameof(Boolean))
                {
                    return string.Format("{0}", GetBoolValue(val));
                }
                return string.Format("{0}'{1}'", prefixN, val);
            }
        }
        #endregion

        #region private extend method
        /// <summary>
        /// get bool value
        /// mysql true or false convert 1 or 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int GetBoolValue(object obj)
        {
            //return obj.ToString().ToLower() == "false" ? 0 : 1;
            if (obj.ToString().ToLower() == "false")
            {
                return 0;
            }
            return 1;
        }
        #endregion

        #region private parameters
        /// <summary>
        /// get sql parameters
        /// </summary>
        /// <param name="properties">properties</param>
        /// <param name="entity">entity</param>
        /// <returns>return sql parameters</returns>
        private MySqlParameter[] GetMySqlParameters(PropertyInfo[] properties, T entity)
        {
            List<MySqlParameter> list = new List<MySqlParameter>();
            foreach (var propertie in properties)
            {
                object obj = propertie.GetValue(entity);
                if (obj == null)
                {
                    list.Add(new MySqlParameter($"@{propertie.Name}", DBNull.Value));
                }
                else
                {
                    list.Add(new MySqlParameter($"@{propertie.Name}", propertie.GetValue(entity)));
                }
            }
            if (list == null || list.Count <= 0)
            {
                throw new ArgumentNullException("get sql parameters failed");
            }
            return list.ToArray();
        }
        #endregion

        #endregion
    }
}
