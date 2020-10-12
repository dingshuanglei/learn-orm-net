/*
* 命名空间: Learn.IRepository
*
* 功 能： N/A
* 类 名： ILearnStudentRepository
*
* Version 1.0.0
* Time    2020/9/29 16:18:29
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

namespace Learn.IRepository
{
    /// <summary>
    /// ILearnStudentRepository
    /// </summary>
    public partial interface ILearnStudentRepository
    {
        Learn_Student Add(Learn_Student learnStudent);

    }
}
