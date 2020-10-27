/*
* 命名空间: Learn.IService
*
* 功 能： N/A
* 类 名： ILearnStudentService
*
* Version 1.0.0
* Time    2020/9/30 15:18:40
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

namespace Learn.IService
{
    /// <summary>
    /// ILearnStudentService
    /// </summary>
    public partial interface ILearnStudentService:IocService
    {
        bool Add(Learn_Student learnStudent);

        bool Update(Learn_Student learnStudent);

        bool Delete(Learn_Student learnStudent);
    }
}
