/*
* 命名空间: Learn.Repository
*
* 功 能： N/A
* 类 名： LearnStudentRepository
*
* Version 1.0.0
* Time    2020/9/29 16:18:11
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
using Learn.Model.Data;
using Learn.Repository.Base;
using Learn.Repository.DbContexts;

namespace Learn.Repository
{
    /// <summary>
    /// LearnStudentRepository
    /// </summary>
    public partial class LearnStudentRepository: ILearnStudentRepository
    {
        public Learn_Student Add(Learn_Student learnStudent)
        {
            using (MySqlDbContext mySqlDbContext=new MySqlDbContext())
            {
                BaseRepository<Learn_Student> baseRepository = new BaseRepository<Learn_Student>(mySqlDbContext);
                return baseRepository.Add(learnStudent);
            }    
        }
    }
}
