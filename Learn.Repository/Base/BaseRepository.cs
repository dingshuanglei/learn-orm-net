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

        public T Add(T entity)
        {
            string sql = GetInsertSql(entity);
            context.Database.ExecuteSqlRaw(sql);
            return entity;
        }

        #endregion


        #region private method

        /// <summary>
        /// get current-table-name
        /// </summary>
        /// <param name="tableName">tableName</param>
        /// <returns>return table-name</returns>
        private string GetCurrentTableName(string tableName = null)
        {
            string currentTableName = string.Empty;
            if (tableName.IsNullOrEmpty())
            {
                var t = typeof(T);
                if (t != null && !t.Name.IsNullOrEmpty())
                {
                    currentTableName = t.Name;
                }
            }
            else
            {
                currentTableName = tableName;
            }
            if (currentTableName.IsNullOrEmpty())
            {
                throw new ArgumentNullException("get table-name is null");
            }
            return currentTableName;
        }

        /// <summary>
        /// get insert sql
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>return insert sql</returns>
        private string GetInsertSql(T entity)
        {
            string tableName = GetCurrentTableName(entity.GetType().Name);
            string key = string.Format("insert_{0}", tableName);
            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();
            sbSql.AppendFormat("INSERT INTO {0} (", tableName);
            PropertyInfo[] properties = GetAllFields(true);
            foreach (var item in properties)
            {
                sbSql.AppendFormat("{0},", item.Name);
                sbValue.AppendFormat("{0},", GetValue(item, entity));
            }
            sbSql.Remove(sbSql.Length - 1, 1);
            sbValue.Remove(sbValue.Length - 1, 1);
            //todo ;SELECT @@identity return identity
            sbSql.AppendFormat(") VALUES ({0})", sbValue.ToString());
            return sbSql.ToString();
        }

        /// <summary>
        /// get exclude key fields
        /// </summary>
        /// <param name="isExcludeKey">isExcludeKey</param>
        /// <returns>return exclude key fields</returns>
        private PropertyInfo[] GetAllFields(bool isExcludeKey = false)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            // filter abstract virtual property
            properties = properties.Where(p => p.PropertyType.IsAbstract == false && !p.GetMethod.IsVirtual == true).ToArray();
            if (properties == null || properties.Length <= 0)
            {
                throw new ArgumentNullException("value fields is null");
            }
            if (isExcludeKey)
            {
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
                return list.ToArray();
            }
            return properties;
        }

        /// <summary>
        /// get value
        /// </summary>
        /// <param name="property"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string GetValue(PropertyInfo property , T entity)
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
    }
}
