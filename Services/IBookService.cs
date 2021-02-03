using library_backend.Entities;
using library_backend.Results;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace library_backend.Services
{
    public interface IBookService
    {
        public Task<ResultBase> addBookAsync(book b);
        public Task<ResultBase> deleteBookAsync(book b);
        public Task<ResultBase> updateBookAsync(book b);
        public Task<BookSearchResult> searchBookAsync(string name);

    }
}
