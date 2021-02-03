using System.Threading.Tasks;
using library_backend.Entities;
using library_backend.Results;
using library_backend.CommonActions;
using library_backend.DataBase;

namespace library_backend.Services
{
    public class LabelService : ILabelService
    {
        private DbContext ctx;
        public LabelService(DbContext context)
        {
            this.ctx = context;
        }
        public async Task<ResultBase> AddLabelAsync(label l)
        {
            return await TryCatchAction<ResultBase>.ExcuteAsync(async () =>
            {
                await this.ctx.Db.Insertable<label>(l).ExecuteCommandAsync();
            });
        }

        public async Task<ResultBase> DeleteLabelAsync(label l)
        {
            return await TryCatchAction<ResultBase>.ExcuteAsync(async () =>
            {
                await this.ctx.Db.Deleteable<label>().In<string>(l.id).ExecuteCommandAsync();
            });
        }

        public async Task<ResultBase> UpdateLabelAsync(label l)
        {
            return await TryCatchAction<ResultBase>.ExcuteAsync(async () =>
            {
                await this.ctx.Db.Updateable<label>(l).ExecuteCommandAsync();
            });
        }
    }
}
