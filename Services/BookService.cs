using System;
using library_backend.Entities;
using library_backend.Results;
using library_backend.DataBase;
using library_backend.CommonActions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace library_backend.Services
{
    public class BookService : IBookService
    {
        private DbContext ctx;
        public BookService(DbContext context)
        {
            this.ctx = context;
        }
        public async Task<ResultBase> addBookAsync(book b)
        {
            return await TryCatchAction<ResultBase>.excuteAsync(async () =>
            {
                await this.ctx.Db.Insertable<book>(b).ExecuteCommandAsync();
            });
        }

        public async Task<ResultBase> deleteBookAsync(book b)
        {
            return await TryCatchAction<ResultBase>.excuteAsync(async () =>
            {
                await this.ctx.Db.Deleteable<book>().In<string>(b.id).ExecuteCommandAsync();
            });
        }

        public async Task<ResultBase> updateBookAsync(book b)
        {
            return await TryCatchAction<ResultBase>.excuteAsync(async () =>
            {
                await this.ctx.Db.Updateable<book>(b).ExecuteCommandAsync();
            });
        }

        public async Task<BookSearchResult> searchBookAsync(string name)
        {
            List<book> books = null;
            var res = await TryCatchAction<BookSearchResult>.excuteAsync(async () =>
            {
                books = await this.ctx.Db.Queryable<book>()
                                        .Where(b => b.name.Contains(name))
                                        .ToListAsync();
            }) as BookSearchResult;
            res.books = books;
            return res;
        }

    }
}
