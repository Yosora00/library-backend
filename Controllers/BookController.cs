using System.Collections.Generic;
using System.Threading.Tasks;
using library_backend.Entities;
using library_backend.Results;
using library_backend.Services;
using library_backend.utils;
using Microsoft.AspNetCore.Mvc;

namespace library_backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private IBookService _bookservice;

        public BookController(IBookService bookservice)
        {
            this._bookservice = bookservice;
        }

        /// <summary>
        /// 添加书
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultBase> Add([FromForm] book b)
        {
            b.id = MyUtils.generateId();
            return await this._bookservice.AddBookAsync(b);
        }

        /// <summary>
        /// 删除书，只需传 book.id
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultBase> Delete([FromForm] book b)
        {
            return await this._bookservice.DeleteBookAsync(b);
        }

        /// <summary>
        /// 更新书的信息，书的id不可修改
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultBase> Update([FromForm] book b)
        {
            return await this._bookservice.UpdateBookAsync(b);
        }

        /// <summary>
        /// 根据书名搜索书
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BookSearchResult> Search([FromForm] string name)
        {
            return await this._bookservice.SearchBookAsync(name);
        }

        /// <summary>
        /// 给书添加标签
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="labelnames"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BookLabelModifyResult> AddLabels(
            [FromForm] string bookid,
            [FromForm] List<string> labelnames
            )
        {
            var labels = new List<label>();
            var b = new book
            {
                id = bookid
            };
            labelnames.ForEach(n =>
            {
                labels.Add(new label
                {
                    name = n
                });
            });
            return await this._bookservice.AddBookLabelsAsync(b, labels);
        }

        /// <summary>
        /// 删除书的标签
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="labelids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BookLabelModifyResult> DeleteLabels(
            [FromForm] string bookid,
            [FromForm] List<string> labelids
            )
        {
            var labels = new List<label>();
            var b = new book
            {
                id = bookid
            };
            labelids.ForEach(n =>
            {
                labels.Add(new label
                {
                    id = n
                });
            });
            return await this._bookservice.DeleteBookLabelsAsync(b, labels);
        }

    }
}