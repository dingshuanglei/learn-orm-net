/*
* 命名空间: Learn.Service
*
* 功 能： N/A
* 类 名： LearnStudentService
*
* Version 1.0.0
* Time    2020/9/30 15:19:27
* Author  dingshuanglei
* ──────────────────────────────────
*
* Copyright (c) 2020 dingshuanglei . All rights reserved.
*┌─────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．│
*│　版权所有: 丁双磊  　　　　　                 　　　　　　　　   │
*└─────────────────────────────────┘
*/
using Learn.IRepository;
using Learn.IService;
using Learn.Model.Data;

namespace Learn.Service
{
    /// <summary>
    /// LearnStudentService
    /// </summary>
    public partial class LearnStudentService : ILearnStudentService
    {
        ILearnStudentRepository repository;
        public LearnStudentService(ILearnStudentRepository _repository)
        {
            repository = _repository;
        }


        public bool Add(Learn_Student learnStudent)
        {
            if (learnStudent == null)
            {
                return false;
            }
            return repository.Add(learnStudent);
        }
    }
}
