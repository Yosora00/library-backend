using library_backend.Entities;
using library_backend.Results;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace library_backend.Services
{
    public interface IBookService
    {
        //添加书
        public Task<ResultBase> AddBookAsync(book b);

        //删除书
        public Task<ResultBase> DeleteBookAsync(book b);

        //更新书的信息
        public Task<ResultBase> UpdateBookAsync(book b);

        //根据书名查找书
        public Task<BookSearchResult> SearchBookAsync(string name);

        //给书添加标签，必须传 book.id、label.name
        public Task<BookLabelAddResult> AddBookLabelsAsync(book b, List<label> labels);

        //书和标签取消关联，必须传 book.id、label.id
        public Task<BookLabelAddResult> DeleteBookLabelsAsync(book b, List<label> labels);

    }
}
