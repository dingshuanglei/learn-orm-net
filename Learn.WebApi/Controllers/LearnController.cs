using Learn.Common.BaseResult;
using Learn.IService;
using Learn.Model.Data;
using Microsoft.AspNetCore.Mvc;

namespace Learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnController : ControllerBase
    {
        ILearnStudentService service;
        public LearnController(ILearnStudentService _service)
        {
            service = _service;
        }

        /// <summary>
        /// add
        /// </summary>
        /// <param name="learnStudent">learn_student model</param>
        /// <returns>return true or false</returns>
        [HttpPost]
        [Route(nameof(Add))]
        public AjaxResult Add(Learn_Student learnStudent)
        {
            AjaxResult ajaxResult = new AjaxResult();
            ajaxResult.Result = service.Add(learnStudent);
            return ajaxResult;
        }

        [HttpPost]
        [Route(nameof(Update))]
        public AjaxResult Update(Learn_Student learnStudent)
        {
            AjaxResult ajaxResult = new AjaxResult();
            ajaxResult.Result = service.Update(learnStudent);
            return ajaxResult;
        }
    }
}
