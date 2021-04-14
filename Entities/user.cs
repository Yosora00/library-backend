using System;
using library_backend.utils;
using SqlSugar;

namespace library_backend.Entities
{
    [SugarTable("user")]
    public class user
    {
        public enum UserType
        {
            普通用户,
            管理员
        }

        public user()
        {
            id = MyUtils.GenerateId();
            type = UserType.普通用户;
            point = 0;
        }

        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public UserType type { get; set; }
        public double point { get; set; }

    }
}