using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QICore.NDapper
{
    public class Class1
    {
        #region 数据库类型，返回C#类型
        public class SqlServerDbTypeMap
        {
            
            public static string MapCsharpType(string dbtype)
            {
                if (string.IsNullOrEmpty(dbtype)) return dbtype;
                dbtype = dbtype.ToLower();
                string csharpType = "object";
                switch (dbtype)
                {
                    case "bigint": csharpType = "long"; break;
                    case "binary": csharpType = "byte[]"; break;
                    case "bit": csharpType = "bool"; break;
                    case "char": csharpType = "string"; break;
                    case "date": csharpType = "DateTime"; break;
                    case "datetime": csharpType = "DateTime"; break;
                    case "datetime2": csharpType = "DateTime"; break;
                    case "datetimeoffset": csharpType = "DateTimeOffset"; break;
                    case "decimal": csharpType = "decimal"; break;
                    case "float": csharpType = "double"; break;
                    case "image": csharpType = "byte[]"; break;
                    case "int": csharpType = "int"; break;
                    case "money": csharpType = "decimal"; break;
                    case "nchar": csharpType = "string"; break;
                    case "ntext": csharpType = "string"; break;
                    case "numeric": csharpType = "decimal"; break;
                    case "nvarchar": csharpType = "string"; break;
                    case "real": csharpType = "Single"; break;
                    case "smalldatetime": csharpType = "DateTime"; break;
                    case "smallint": csharpType = "short"; break;
                    case "smallmoney": csharpType = "decimal"; break;
                    case "sql_variant": csharpType = "object"; break;
                    case "sysname": csharpType = "object"; break;
                    case "text": csharpType = "string"; break;
                    case "time": csharpType = "TimeSpan"; break;
                    case "timestamp": csharpType = "byte[]"; break;
                    case "tinyint": csharpType = "byte"; break;
                    case "uniqueidentifier": csharpType = "Guid"; break;
                    case "varbinary": csharpType = "byte[]"; break;
                    case "varchar": csharpType = "string"; break;
                    case "xml": csharpType = "string"; break;
                    default: csharpType = "object"; break;
                }
                return csharpType;
            }
        }
        #endregion
    }
}
#region 数据库帮助类型
public class DapperHelper
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    private static readonly string connectionString = "";

    /// <summary>
    /// 查询列表
    /// </summary>
    /// <param name="sql">查询的sql</param>
    /// <param name="param">替换参数</param>
    /// <returns></returns>
    public static List<T> Query<T>(string sql, object param)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            return con.Query<T>(sql, param).ToList();
        }
    }

    /// <summary>
    /// 查询第一个数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static T QueryFirst<T>(string sql, object param)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            return con.QueryFirst<T>(sql, param);
        }
    }

    /// <summary>
    /// 查询第一个数据没有返回默认值
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static T QueryFirstOrDefault<T>(string sql, object param)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            return con.QueryFirstOrDefault<T>(sql, param);
        }
    }

    /// <summary>
    /// 查询单条数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static T QuerySingle<T>(string sql, object param)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            return con.QuerySingle<T>(sql, param);
        }
    }

    /// <summary>
    /// 查询单条数据没有返回默认值
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static T QuerySingleOrDefault<T>(string sql, object param)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            return con.QuerySingleOrDefault<T>(sql, param);
        }
    }

    /// <summary>
    /// 增删改
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Execute(string sql, object param)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            return con.Execute(sql, param);
        }
    }

    /// <summary>
    /// Reader获取数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static IDataReader ExecuteReader(string sql, object param)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            return con.ExecuteReader(sql, param);
        }
    }

    /// <summary>
    /// Scalar获取数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static object ExecuteScalar(string sql, object param)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            return con.ExecuteScalar(sql, param);
        }
    }

    /// <summary>
    /// Scalar获取数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static T ExecuteScalarFor<T>(string sql, object param)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            return con.ExecuteScalar<T>(sql, param);
        }
    }

    /// <summary>
    /// 带参数的存储过程
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static List<T> ExecutePro<T>(string proc, object param)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            List<T> list = con.Query<T>(proc,
                param,
                null,
                true,
                null,
                CommandType.StoredProcedure).ToList();
            return list;
        }
    }


    /// <summary>
    /// 事务1 - 全SQL
    /// </summary>
    /// <param name="sqlarr">多条SQL</param>
    /// <param name="param">param</param>
    /// <returns></returns>
    public static int ExecuteTransaction(string[] sqlarr)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    int result = 0;
                    foreach (var sql in sqlarr)
                    {
                        result += con.Execute(sql, null, transaction);
                    }

                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
    }

    /// <summary>
    /// 事务2 - 声明参数
    ///demo:
    ///dic.Add("Insert into Users values (@UserName, @Email, @Address)",
    ///        new { UserName = "jack", Email = "380234234@qq.com", Address = "上海" });
    /// </summary>
    /// <param name="Key">多条SQL</param>
    /// <param name="Value">param</param>
    /// <returns></returns>
    public static int ExecuteTransaction(Dictionary<string, object> dic)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    int result = 0;
                    foreach (var sql in dic)
                    {
                        result += con.Execute(sql.Key, sql.Value, transaction);
                    }

                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
    }
}

#endregion

