/*
* 命名空间: Learn.Model.Data
*
* 功 能： N/A
* 类 名： Learn_Student
*
* Version 1.0.0
* Time    2020/9/29 16:13:56
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
using System.ComponentModel.DataAnnotations.Schema;

namespace Learn.Model.Data
{
    /// <summary>
    /// LearnStudent
    /// </summary>
    [Serializable]
    [Table(nameof(Learn_Student))]
    public partial class Learn_Student:BaseEntity
    {
        /// <summary>
        /// User_Name
        /// </summary>
        public string User_Name { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Age
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }

    }
}
