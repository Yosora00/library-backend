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
    [Route("Api/[controller]/[action]")]
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
        public ResultBase Add(book b)
        {
            b.id = MyUtils.GenerateId();
            return this._bookservice.AddBook(b);
        }

        /// <summary>
        /// 删除书
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultBase Delete(string bookid)
        {
            var b = new book
            {
                id = bookid
            };
            return this._bookservice.DeleteBook(b);
        }

        /// <summary>
        /// 更新书的信息，书的id不可修改
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultBase Update(book b)
        {
            return this._bookservice.UpdateBook(b);
        }

        /// <summary>
        /// 根据书名搜索书
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public BookSearchResult Search(string name)
        {
            return this._bookservice.SearchBook(name);
        }

        /// <summary>
        /// 给书添加标签
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="labelnames"></param>
        /// <returns></returns>
        [HttpPost]
        public BookLabelModifyResult AddLabels(
            string bookid,
            List<string> labelnames
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
            return this._bookservice.AddBookLabels(b, labels);
        }

        /// <summary>
        /// 删除书的标签
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="labelids"></param>
        /// <returns></returns>
        [HttpPost]
        public BookLabelModifyResult DeleteLabels(
            string bookid,
            List<string> labelids
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
            return this._bookservice.DeleteBookLabels(b, labels);
        }

    }
}