using Learn.Repository.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Learn.Repository.Base
{
    /// <summary>
    /// InitDbTable
    /// </summary>
    public static partial class InitDbTable
    {
        static List<DbTable> BuildTable()
        {
            List<DbTable> tables = new List<DbTable>();

            tables.Add(new DbTable()
            {
                Sort = 1,
                //Grant all privileges on learndb.* to 'learn_user@'@'%'
                //> 1410 - You are not allowed to create a user with GRANT
                //Sql = @"CREATE DATABASE IF NOT EXISTS learndb;"
            });

            tables.Add(new DbTable()
            {
                Sort = 2,
                Sql = @"USE learndb;"
            });

            tables.Add(new DbTable()
            {
                Sql = @"CREATE TABLE IF NOT EXISTS `learn_student`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `age` int(0) NULL DEFAULT NULL,
  `gender` char(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `create_uid` bigint(0) NULL DEFAULT NULL,
  `create_date` datetime(0) NULL DEFAULT NULL,
  `edit_uid` bigint(0) NULL DEFAULT NULL,
  `edit_date` datetime(0) NULL DEFAULT NULL,
  `is_delete` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;"
            });

            return tables;
        }

        /// <summary>
        /// init table
        /// </summary>
        public static void InitTable()
        {
            List<DbTable> tables = BuildTable();
            if (tables.Count > 0)
            {
                using (MySqlDbContext mySqlDbContext = new MySqlDbContext())
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var item in tables.OrderBy(t => t.Sort))
                    {
                        if (item.Sql.IsNullOrEmpty())
                        {
                            continue;
                        }
                        stringBuilder.AppendLine(item.Sql);
                    }
                    mySqlDbContext.Database.ExecuteSqlRaw(stringBuilder.ToString());
                }
            }
        }
    }

    public class DbTable
    {
        public int Sort { get; set; } = 999;
        public string Sql { get; set; }
    }
}
