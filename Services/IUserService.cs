using library_backend.Entities;
using library_backend.Results;

namespace library_backend.Services
{
    public interface IUserService
    {
        public ResultBase Regist(user user);
        public ResultBase Login(string name, string pwd);
        public ResultBase UpdateUser(user user);
        public ResultBase DeleteUser(string id);
        public user GetUserById(string id);
        public user GetUserByName(string name);
    }
}