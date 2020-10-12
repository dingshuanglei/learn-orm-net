/*
* 命名空间: Learn.Model.Data
*
* 功 能： N/A
* 类 名： BaseEntity
*
* Version 1.0.0
* Time    2020/9/29 15:58:38
* Author  dingshuanglei
* ──────────────────────────────────
*
* Copyright (c) 2020 dingshuanglei . All rights reserved.
*┌─────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．│
*│　版权所有: 丁双磊  　　　　　                 　　　　　　　　   │
*└─────────────────────────────────┘
*/
using System;
using System.ComponentModel.DataAnnotations;

namespace Learn.Model.Data
{
    /// <summary>
    /// BaseEntity
    /// </summary>
    public partial class BaseEntity
    {
        /// <summary>
        /// Id<para></para>
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Create_Uid<para></para>
        /// </summary>
        public long Create_Uid { get; set; }
        /// <summary>
        /// Create_Date<para></para>
        /// </summary>
        public DateTime Create_Date { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// edit_uid<para></para>
        /// </summary>
        public long? Edit_Uid { get; set; }
        /// <summary>
        /// Edit_Date<para></para>
        /// </summary>
        public DateTime? Edit_Date { get; set; }
        /// <summary>
        /// is_delete<para></para>
        /// value: true or false<para></para>
        /// default false 0<para></para>
        /// </summary>
        public bool Is_Delete { get; set; } = false;
    }
}
