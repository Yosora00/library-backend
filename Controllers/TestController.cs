using Microsoft.AspNetCore.Mvc;
using library_backend.utils;
using library_backend.Services;
using library_backend.Results;
using library_backend.Entities;
using System.Threading.Tasks;

namespace library_backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private IBookService _bookservice;
        public TestController(IBookService bookService)
        {
            _bookservice = bookService;
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultBase> Test(string name)
        {
            return await _bookservice.SearchBookAsync(name);
        }
    }
}
