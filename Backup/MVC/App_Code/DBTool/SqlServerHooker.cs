using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCExercise.App_Code.DBTool
{
    /// <summary>
    /// sql server数据库操作类
    /// </summary>
    public class SqlServerHooker
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string strConn = "Data Source=qds114476253.my3w.com;Initial Catalog=qds114476253_db;User ID=qds114476253;Password=zzc369963;";
        /// <summary>
        /// 创建SqlServer数据库连接对象
        /// </summary>
        /// <returns>返回数据库连接对象</returns>
        public static SqlConnection ConDataBase()
        {
            SqlConnection conn = new SqlConnection(strConn);
            return conn;
        }
        /// <summary>
        /// 查询数据库中符合条件的记录
        /// </summary>
        /// <param name="strComm"></param>
        /// <returns>返回查询结果</returns>
        public static DataTable GetDataTable(string strComm)
        {
            try
            {
                using (SqlConnection con = ConDataBase())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(strComm, con);
                    da.Fill(dt);
                    con.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据指定的sql插入语句往数据库中插入数据
        /// </summary>
        /// <param name="SqlInsert">insert语句</param>
        /// <returns>返回值</returns>
        public static bool InsertDataToTable(string SqlInsert)
        {
            try
            {
                using (SqlConnection con = ConDataBase())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand(SqlInsert, con);
                        com.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据delete语句，从数据库中删除数据
        /// </summary>
        /// <param name="SqlDelete"></param>
        /// <returns>返回值</returns>
        public static bool DeleteDataFromTable(string SqlDelete)
        {
            try
            {
                using (SqlConnection con = ConDataBase())
                {
                    if (con == null) return false;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlCommand com = new SqlCommand(SqlDelete, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据Update语句，更新数据库中数据
        /// </summary>
        /// <param name="SqlUpdate"></param>
        /// <returns>返回值</returns>
        public static bool UpdateDataToTable(string SqlUpdate)
        {
            try
            {
                using (SqlConnection con = ConDataBase())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlCommand com = new SqlCommand(SqlUpdate, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 更改表结构
        /// </summary>
        /// <param name="SqlAlter"></param>
        /// <returns>返回值</returns>
        public static bool AlterTable(string SqlAlter)
        {
            try
            {
                using (SqlConnection con = ConDataBase())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlCommand com = new SqlCommand(SqlAlter, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}