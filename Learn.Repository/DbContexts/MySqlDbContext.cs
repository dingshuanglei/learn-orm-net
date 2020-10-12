/*
* 命名空间: Learn.Repository.DbContexts
*
* 功 能： N/A
* 类 名： MySqlDbContext
*
* Version 1.0.0
* Time    2020/9/29 16:20:31
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
using Microsoft.EntityFrameworkCore;

namespace Learn.Repository.DbContexts
{
    /// <summary>
    /// MySqlDbContext
    /// </summary>
    public partial class MySqlDbContext : BaseDbContext
    {


        public DbSet<Learn_Student> LearnStudent { get; set; }
    }
}
