using System;
using library_backend.Entities;
using library_backend.Results;
using library_backend.Services;
using library_backend.utils;
using Microsoft.AspNetCore.Mvc;

namespace library_backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private IUserService _userservice;
        public UserController(
            IUserService userService
        )
        {
            _userservice = userService;
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultBase Login(string name, string pwd)
        {
            return _userservice.Login(name, pwd);
        }

        /// <summary>
        ///  用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultBase Regist(user user)
        {
            user.id = MyUtils.GenerateId();
            user.point = 0;
            user.type = user.UserType.普通用户;
            return _userservice.Regist(user);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultBase Delete(string id)
        {
            return _userservice.DeleteUser(id);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultBase Update(user user)
        {
            var old = _userservice.GetUserById(user.id);
            if (old == null)
                return new ResultBase { isSuccess = false, message = "用户不存在！" };
            var newUser = new user
            {
                id = user.id,
                name = user.name ?? old.name,
                password = user.password ?? old.password,
                phone = user.phone ?? old.phone,
                email = user.email ?? old.email,
                type = old.type,
                point = old.point
            };
            return _userservice.UpdateUser(newUser);
        }
    }
}