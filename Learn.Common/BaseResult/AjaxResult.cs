/*
* 命名空间: Learn.Common.BaseResult
*
* 功 能： N/A
* 类 名： AjaxResult
*
* Version 1.0.0
* Time    2020/4/18 21:14:52
* Author  dingshuanglei
* ──────────────────────────────────
*
* Copyright (c) 2020 dingshuanglei . All rights reserved.
*┌─────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．│
*│　版权所有: 丁双磊                  　　　　　　　　　　　　　   │
*└─────────────────────────────────┘
*/

namespace Learn.Common.BaseResult
{
    /// <summary>
    /// AjaxResult
    /// </summary>
    public partial class AjaxResult
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public string StatusCode { get; set; } = "200";
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool Result { get; set; } = false;

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
