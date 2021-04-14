using System;
using library_backend.CommonActions;
using library_backend.DataBase;
using library_backend.Entities;
using library_backend.Results;

namespace library_backend.Services
{
    public class UserService : IUserService
    {
        private DbContext _ctx;
        public UserService(
            DbContext context
        )
        {
            _ctx = context;
        }

        public ResultBase Login(string name, string pwd)
        {
            return TryCatchAction<ResultBase>.Excute(() =>
            {
                var res = _ctx.Db.Queryable<user>()
                                .Where(u => u.name == name && u.password == pwd)
                                .ToList();
                //登陆成功
                if (res.Count == 1)
                {
                    return;
                }
                throw new Exception("用户名或密码错误！");
            });
        }

        public ResultBase Regist(user user)
        {
            return TryCatchAction<ResultBase>.Excute(() =>
            {
                _ctx.Db.Insertable<user>(user).ExecuteCommand();
            });
        }

        public ResultBase DeleteUser(string id)
        {
            return TryCatchAction<ResultBase>.Excute(() =>
            {
                var res = _ctx.Db.Deleteable<user>()
                                .Where(u => u.id == id)
                                .ExecuteCommand();
            });
        }

        public user GetUserById(string id)
        {
            var res = _ctx.Db.Queryable<user>().Where(u => u.id == id).ToList();
            if (res.Count == 1)
            {
                return res[0];
            }
            return null;
        }

        public user GetUserByName(string name)
        {
            var res = _ctx.Db.Queryable<user>().Where(u => u.name == name).ToList();
            if (res.Count == 1)
            {
                return res[0];
            }
            return null;
        }


        public ResultBase UpdateUser(user user)
        {
            return TryCatchAction<ResultBase>.Excute(() =>
            {
                _ctx.Db.Updateable<user>(user).ExecuteCommand();
            });
        }
    }
}