/*
* 命名空间: Learn.Repository.DbContexts
*
* 功 能： N/A
* 类 名： BaseDbContext
*
* Version 1.0.0
* Time    2020/9/29 16:20:11
* Author  dingshuanglei
* ──────────────────────────────────
*
* Copyright (c) 2020 dingshuanglei . All rights reserved.
*┌─────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．│
*│　版权所有: 丁双磊  　　　　　                 　　　　　　　　   │
*└─────────────────────────────────┘
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Learn.Repository.DbContexts
{
    /// <summary>
    /// BaseDbContext
    /// </summary>
    public partial class BaseDbContext : DbContext
    {
        //2.x
        //输出到debug输出
        //public static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });
        // 输出到Console ConsoleLoggerOptions
        //IOptionsMonitor<ConsoleLoggerOptions> options;
        //3.x
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var dbSection = configuration.GetSection("dbConn");
            var mySqlConnString = dbSection.GetSection("dbMySql")["LearnMySql"];
            var mySqlEnable = dbSection.GetSection("dbMySql")["Enable"];

            if (mySqlEnable.ToBoolean())
            {
                optionsBuilder.UseMySQL(mySqlConnString);
                optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            }
        }

    }
}
