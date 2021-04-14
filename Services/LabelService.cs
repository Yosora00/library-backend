using System.Threading.Tasks;
using library_backend.Entities;
using library_backend.Results;
using library_backend.CommonActions;
using library_backend.DataBase;
using library_backend.utils;

namespace library_backend.Services
{
    public class LabelService : ILabelService
    {
        private DbContext ctx;
        public LabelService(DbContext context)
        {
            this.ctx = context;
        }
        public ResultBase AddLabel(label l)
        {
            return TryCatchAction<ResultBase>.Excute(() =>
            {
                this.ctx.Db.Insertable<label>(l).ExecuteCommand();
            });
        }

        public ResultBase DeleteLabel(label l)
        {
            return TryCatchAction<ResultBase>.Excute(() =>
            {
                this.ctx.Db.Deleteable<label>().In<string>(l.id).ExecuteCommand();
            });
        }

        public ResultBase UpdateLabel(label l)
        {
            return TryCatchAction<ResultBase>.Excute(() =>
            {
                this.ctx.Db.Updateable<label>(l).ExecuteCommand();
            });
        }

        public string GetLabelId(string name)
        {
            var list = this.ctx.Db.Queryable<label>().Where(l => l.name == name).ToList();
            if (list.Count >= 1)
                return list[0].id;
            return null;
        }
    }
}