using SqlSugar;

namespace library_backend.DataBase
{
    public class DbContext
    {
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source = data/data.db",
                DbType = DbType.Sqlite,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
            // 调式代码 用来打印SQL

            // Db.Aop.OnLogExecuting = (sql, pars) =>
            // {
            //     Console.WriteLine(sql + "\r\n" +
            //         Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            //     Console.WriteLine();
            // };

        }
    }
}
