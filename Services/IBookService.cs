using library_backend.Entities;
using library_backend.Results;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace library_backend.Services
{
    public interface IBookService
    {
        public Task<ResultBase> AddBookAsync(book b);
        public Task<ResultBase> DeleteBookAsync(book b);
        public Task<ResultBase> UpdateBookAsync(book b);
        public Task<BookSearchResult> SearchBookAsync(string name);

    }
}
