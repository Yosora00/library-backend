using System;
using library_backend.Entities;
using library_backend.Results;
using library_backend.DataBase;
using library_backend.CommonActions;
using System.Threading.Tasks;
using System.Collections.Generic;
using library_backend.utils;

namespace library_backend.Services
{
    public class BookService : IBookService
    {
        private DbContext ctx;
        private ILabelService _labelservice;
        public BookService(
            DbContext context,
            ILabelService labelservice
            )
        {
            this.ctx = context;
            this._labelservice = labelservice;
        }
        public async Task<ResultBase> AddBookAsync(book b)
        {
            return await TryCatchAction<ResultBase>.ExcuteAsync(async () =>
            {
                await this.ctx.Db.Insertable<book>(b).ExecuteCommandAsync();
            });
        }

        public async Task<ResultBase> DeleteBookAsync(book b)
        {
            return await TryCatchAction<ResultBase>.ExcuteAsync(async () =>
            {
                await this.ctx.Db.Deleteable<book>().In<string>(b.id).ExecuteCommandAsync();
            });
        }

        public async Task<ResultBase> UpdateBookAsync(book b)
        {
            return await TryCatchAction<ResultBase>.ExcuteAsync(async () =>
            {
                await this.ctx.Db.Updateable<book>(b).ExecuteCommandAsync();
            });
        }

        public async Task<BookSearchResult> SearchBookAsync(string name)
        {
            List<book> books = null;
            var res = await TryCatchAction<BookSearchResult>.ExcuteAsync(async () =>
            {
                books = await this.ctx.Db.Queryable<book>()
                                        .Where(b => b.name.Contains(name))
                                        .ToListAsync();
            }) as BookSearchResult;
            res.books = books;
            return res;
        }

        public async Task<BookLabelModifyResult> AddBookLabelsAsync(book b, List<label> labels)
        {
            var bkares = new BookLabelModifyResult()
            {
                isSuccess = false,
                errorlist = new List<string>()
            };

            //检查书是否存在
            var bk = await this.ctx.Db.Queryable<book>().Where(e => e.id == b.id).ToListAsync();
            if (bk.Count == 0)
            {
                return bkares;
            }

            foreach (var l in labels)
            {
                var id = await this._labelservice.GetLabelIdAsync(l.name);
                if (id == null)
                {
                    id = MyUtils.generateId();
                    l.id = id;
                    var res = await _labelservice.AddLabelAsync(l);
                }
                else
                {
                    var bl = await this.ctx.Db.Queryable<booklabel>().Where(bl => bl.bookId == b.id && bl.labelId == id).ToListAsync();
                    if (bl.Count > 0)
                    {
                        bkares.errorlist.Add(l.name);
                        continue;
                    }
                }

                if (id != null)
                {
                    var bl = new booklabel
                    {
                        id = MyUtils.generateId(),
                        bookId = b.id,
                        labelId = id
                    };
                    var res = await TryCatchAction<ResultBase>.ExcuteAsync(async () =>
                    {
                        await this.ctx.Db.Insertable<booklabel>(bl).ExecuteCommandAsync();
                    });
                    if (!res.isSuccess)
                        bkares.errorlist.Add(l.name);
                }
            }
            if (bkares.errorlist.Count == 0)
                bkares.isSuccess = true;
            return bkares;
        }

        public async Task<BookLabelModifyResult> DeleteBookLabelsAsync(book b, List<label> labels)
        {
            var bkares = new BookLabelModifyResult()
            {
                isSuccess = false,
                errorlist = new List<string>()
            };

            //检查书是否存在
            var bk = await this.ctx.Db.Queryable<book>().Where(e => e.id == b.id).ToListAsync();
            if (bk.Count == 0)
            {
                return bkares;
            }

            foreach (var l in labels)
            {
                var res = await TryCatchAction<ResultBase>.ExcuteAsync(async () =>
                {
                    await this.ctx.Db.Deleteable<booklabel>().Where(bl => bl.bookId == b.id && bl.labelId == l.id).ExecuteCommandAsync();
                });
                if (!res.isSuccess)
                    bkares.errorlist.Add(l.name);
            }

            if (bkares.errorlist.Count == 0)
                bkares.isSuccess = true;
            return bkares;
        }
    }
}
