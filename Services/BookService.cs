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
        public ResultBase AddBook(book b)
        {
            return TryCatchAction<ResultBase>.Excute(() =>
            {
                this.ctx.Db.Insertable<book>(b).ExecuteCommand();
            });
        }

        public ResultBase DeleteBook(book b)
        {
            return TryCatchAction<ResultBase>.Excute(() =>
            {
                this.ctx.Db.Deleteable<book>().In<string>(b.id).ExecuteCommand();
            });
        }

        public ResultBase UpdateBook(book b)
        {
            return TryCatchAction<ResultBase>.Excute(() =>
            {
                this.ctx.Db.Updateable<book>(b).ExecuteCommand();
            });
        }

        public BookSearchResult SearchBook(string name)
        {
            List<book> books = null;
            var res = TryCatchAction<BookSearchResult>.Excute(() =>
            {
                books = this.ctx.Db.Queryable<book>()
                                            .Where(b => b.name.Contains(name))
                                            .ToList();
            }) as BookSearchResult;
            res.books = books;
            return res;
        }

        public BookLabelModifyResult AddBookLabels(book b, List<label> labels)
        {
            var bkares = new BookLabelModifyResult()
            {
                isSuccess = false,
                errorlist = new List<string>()
            };

            //检查书是否存在
            var bk = this.ctx.Db.Queryable<book>().Where(e => e.id == b.id).ToList();
            if (bk.Count == 0)
            {
                return bkares;
            }

            foreach (var l in labels)
            {
                var id = this._labelservice.GetLabelId(l.name);
                if (id == null)
                {
                    id = MyUtils.generateId();
                    l.id = id;
                    var res = _labelservice.AddLabel(l);
                }
                else
                {
                    var bl = this.ctx.Db.Queryable<booklabel>().Where(bl => bl.bookId == b.id && bl.labelId == id).ToList();
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
                        bookId = b.id,
                        labelId = id
                    };
                    var res = TryCatchAction<ResultBase>.Excute(() =>
                    {
                        this.ctx.Db.Insertable<booklabel>(bl).ExecuteCommand();
                    });
                    if (!res.isSuccess)
                        bkares.errorlist.Add(l.name);
                }
            }
            if (bkares.errorlist.Count == 0)
                bkares.isSuccess = true;
            return bkares;
        }

        public BookLabelModifyResult DeleteBookLabels(book b, List<label> labels)
        {
            var bkares = new BookLabelModifyResult()
            {
                isSuccess = false,
                errorlist = new List<string>()
            };

            //检查书是否存在
            var bk = this.ctx.Db.Queryable<book>().Where(e => e.id == b.id).ToList();
            if (bk.Count == 0)
            {
                return bkares;
            }

            foreach (var l in labels)
            {
                var res = TryCatchAction<ResultBase>.Excute(() =>
                {
                    this.ctx.Db.Deleteable<booklabel>().Where(bl => bl.bookId == b.id && bl.labelId == l.id).ExecuteCommand();
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