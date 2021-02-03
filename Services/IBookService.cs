using library_backend.Entities;
using library_backend.Results;

namespace library_backend.Services
{
    public interface IBookService
    {
        public ResultBase addBook(book b);
        public ResultBase deleteBook(book b);
        public ResultBase updateBook(book b);
        public ResultBase searchBook(string name);
        public book getBook(string id);

    }
}
