using Microsoft.AspNetCore.Mvc;
using library_backend.utils;

namespace library_backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Test(string text)
        {
            return MyUtils.generateId();
        }
    }
}
