using library_backend.Entities;
using library_backend.Results;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace library_backend.Services
{
    public interface IBookService
    {
        //添加书
        public ResultBase AddBook(book b);

        //删除书
        public ResultBase DeleteBook(book b);

        //更新书的信息
        public ResultBase UpdateBook(book b);

        //根据书名查找书
        public BookSearchResult SearchBook(string name);

        //给书添加标签，必须传 book.id、label.name
        public BookLabelModifyResult AddBookLabels(book b, List<label> labels);

        //书和标签取消关联，必须传 book.id、label.id
        public BookLabelModifyResult DeleteBookLabels(book b, List<label> labels);

    }
}