using MySql.Data.MySqlClient;
using System;
using System.Data;
using CommonUtils.FileHelper;

namespace CommonUtils.DBHelper
{
    public class SqlHelper1
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public static string ErrorString;


        /// <summary>
        /// 数据库连接串
        /// </summary>
        private string ConnString;

        /// <summary>
        /// 数据库连接
        /// </summary>
        private MySqlConnection Conn;

        /// <summary>
        /// 数据库连接
        /// </summary>
        private MySqlDataReader reader;


        /// <summary>
        /// 超时（秒）
        /// </summary>
        public int TimeOut;


        /// <summary>
        /// 连接数据库
        /// </summary>
        private void ConnTo()
        {
            try
            {
                Conn = new MySqlConnection(ConnString);
                Conn.Open();
            }
            catch (Exception e)
            {
                AddError(e.Message, ConnString);
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        private void AddError(string message, string sql)
        {
            ErrorString += "数据库连接错误：" + message + "\r\nSQL语句：" + sql + "\r\n";
            if (!string.IsNullOrEmpty(ErrorString) && ErrorString.Length > 1000)
            {
                ErrorString = "";
            }
        }

        /// <summary>
        /// 读取所有数据
        /// </summary>
        private DataTable Read(ref MySqlDataReader reader)
        {
            bool frist = true;
            DataTable dt = new DataTable();
            while (reader.Read())
            {
                if (frist)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string s = reader.GetName(i);
                        dt.Columns.Add(s, typeof(string));
                    }
                    frist = false;
                }
                DataRow dr = dt.NewRow();
                for (int i = 0; i < reader.FieldCount; i++)
                    dr[i] = reader.GetString(i);
                dt.Rows.Add(dr);
            }
            return dt;
        }


        static SqlHelper1()
        {
            ErrorString = "";
        }

        /// <summary>
        /// 初始化数据库链接
        /// </summary>
        /// <param name="connString">数据库链接</param>
        public SqlHelper1()
        {
            this.TimeOut = 100;
            string path = AppDomain.CurrentDomain.BaseDirectory + "Config" + "\\" + "sqlcon.ini";
            string server = INIFile.GetValue("MySqlCon", "server", path);
            string database = INIFile.GetValue("MySqlCon", "database", path);
            string password = INIFile.GetValue("MySqlCon", "password", path);
            string userid = INIFile.GetValue("MySqlCon", "user id", path);
            ConnString = "server=" + server + ";" + "database=" + database + ";" + "password=" + password + ";" + "user id=" + userid + ";" + "charset = utf8";
            ConnTo();
        }


        /// <summary>
        /// 去掉SQL中的特殊字符
        /// </summary>
        public string ReplaceSql(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            value = value.Replace("\\", "\\\\");
            value = value.Replace("'", "''");
            value = value.Replace("\"", "\\\"");
            value = value.Replace("%", "\\%");

            return value;
        }

        public DataTable ExecuteDataTable(string SqlString)
        {
            return ExecuteDataTable(SqlString, null);
        }

        /// <summary>
        /// 执行sql返回DataTable
        /// </summary>
        /// <param name="SqlString">SQL语句</param>
        /// <param name="parms">Sql参数</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable(string SqlString, MySqlParameter[] parms)
        {
            if (Conn == null || Conn.State != ConnectionState.Open)
            {
                ConnTo();
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlString;
                cmd.CommandTimeout = TimeOut;

                if (parms != null)
                {
                    foreach (MySqlParameter pram in parms)
                    {
                        cmd.Parameters.Add(pram);
                    }
                }

                DataTable dt = new DataTable();
                try
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }

                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
                catch (Exception ex)
                {
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                    reader = cmd.ExecuteReader();
                    dt = Read(ref reader);
                }

                return dt;
            }
            catch (Exception e)
            {
                AddError(e.Message, SqlString);
                return null;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }

        public DataRow ExecuteDataTableRow(string SqlString)
        {
            return ExecuteDataTableRow(SqlString, null);
        }

        /// <summary>
        /// 返回第一行
        /// </summary>
        public DataRow ExecuteDataTableRow(string SqlString, MySqlParameter[] parms)
        {
            if (Conn == null || Conn.State != ConnectionState.Open)
            {
                ConnTo();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlString;
                cmd.CommandTimeout = TimeOut;

                if (parms != null)
                {
                    foreach (MySqlParameter pram in parms)
                    {
                        cmd.Parameters.Add(pram);
                    }
                }

                DataTable dt = new DataTable();
                try
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }

                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
                catch
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }

                    reader = cmd.ExecuteReader();
                    dt = Read(ref reader);
                }
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0];
                }
            }
            catch (Exception e)
            {
                AddError(e.Message, SqlString);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }

            return null;
        }

        public string ExecuteFirst(string SqlString)
        {
            return ExecuteFirst(SqlString, null);
        }

        /// <summary>
        /// 返回第一个值
        /// </summary>
        public string ExecuteFirst(string SqlString, MySqlParameter[] parms)
        {
            if (Conn == null || Conn.State != ConnectionState.Open)
            {
                ConnTo();
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlString;
                cmd.CommandTimeout = TimeOut;

                if (parms != null)
                {
                    foreach (MySqlParameter pram in parms)
                    {
                        cmd.Parameters.Add(pram);
                    }
                }

                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                reader = cmd.ExecuteReader();
                string xx = "";
                if (reader.Read())
                {
                    xx = reader[0].ToString();
                }
                return xx;
            }
            catch (Exception e)
            {
                AddError(e.Message, SqlString);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }

            return null;
        }

        public long ExecuteInsertId(string SqlString)
        {
            return ExecuteInsertId(SqlString, null);
        }

        /// <summary>
        /// 返回第一个值
        /// </summary>
        public long ExecuteInsertId(string SqlString, MySqlParameter[] parms)
        {
            if (Conn == null || Conn.State != ConnectionState.Open)
            {
                ConnTo();
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlString;
                cmd.CommandTimeout = TimeOut;

                if (parms != null)
                {
                    foreach (MySqlParameter pram in parms)
                    {
                        cmd.Parameters.Add(pram);
                    }
                }

                cmd.ExecuteNonQuery();
                return cmd.LastInsertedId;
            }
            catch (Exception e)
            {
                AddError(e.Message, SqlString);
            }

            return 0;
        }

        public bool ExecuteNonQuery(string SqlString)
        {
            return ExecuteNonQuery(SqlString, null);
        }

        /// <summary>
        /// 执行无返回SQL语句
        /// </summary>
        /// <param name="SqlString">SQL语句</param>
        /// <param name="parms">Sql参数</param>
        ///<returns>是否执行成功</returns>
        public bool ExecuteNonQuery(string SqlString, MySqlParameter[] parms)
        {
            if (Conn == null || Conn.State != ConnectionState.Open)
            {
                ConnTo();
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlString;
                cmd.CommandTimeout = TimeOut;

                if (parms != null)
                {
                    foreach (MySqlParameter pram in parms)
                    {
                        cmd.Parameters.Add(pram);
                    }
                }
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                AddError(e.Message, SqlString);
                return false;
            }
        }

        public bool ExecuteExists(string SqlString)
        {
            return ExecuteExists(SqlString, null);
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="SqlString">SQL语句</param>
        /// <param name="parms">SQL参数</param>
        /// <returns>是否存在</returns>
        public bool ExecuteExists(string SqlString, MySqlParameter[] parms)
        {
            if (Conn == null || Conn.State != ConnectionState.Open)
            {
                ConnTo();
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlString;
                //cmd.CommandTimeout = TimeOut;

                if (parms != null)
                {
                    foreach (MySqlParameter pram in parms)
                    {
                        cmd.Parameters.Add(pram);
                    }
                }

                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                AddError(e.Message, SqlString);
                return false;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        public void Close()
        {
            if (Conn != null && Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                Conn = null;
            }
            else
            {
                Conn = null;
            }

            GC.Collect();
        }
    }
}
