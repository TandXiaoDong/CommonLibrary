using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Collections;
using CommonUtils.Logger;

namespace CommonUtils.DB
{
    public class SQLServer
    {
        static string _SqlConnectionString;
        public static string SqlConnectionString
        {
            get
            {
                return _SqlConnectionString;
            }
            set
            {
                _SqlConnectionString = value;
            }
        }


        /// <summary>
        /// 设置参数内容
        /// </summary>
        /// <param name="ParamName">名称</param>
        /// <param name="DbType">数据类型</param>
        /// <param name="Size">长度大小</param>
        /// <param name="paramevalue">值</param>
        /// <param name="direction">类型</param>
        /// <returns></returns>
        public static SqlParameter SetDataParameter(string ParamName, SqlDbType DbType, Int32 Size, object paramevalue, ParameterDirection Direction)
        {
            SqlParameter param = new SqlParameter();
            param.SqlDbType = DbType;
            param.ParameterName = ParamName;

            if (Size > 0)
            {
                param.Size = Size;
            }
            if (paramevalue.ToString() != "" && paramevalue != null && Direction != ParameterDirection.Output)
            {
                param.Value = paramevalue;
            }

            param.Direction = Direction;
            return param;
        }
        /// <summary>
        /// 设置参数内容
        /// </summary>
        /// <param name="ParamName">名称</param>
        /// <param name="DbType">数据类型</param>
        /// <param name="Direction">类型</param>
        /// <returns></returns>
        public static SqlParameter SetDataParameter(string ParamName, SqlDbType DbType, ParameterDirection Direction)
        {
            SqlParameter param = new SqlParameter();
            param.SqlDbType = DbType;
            param.ParameterName = ParamName;
            param.Direction = Direction;
            return param;
        }

        #region 私有方法
        /// <summary>
        /// 将SqlParameter参数数组(参数值)分配给DbCommand命令.
        /// 这个方法将给任何一个参数分配DBNull.Value;
        /// 该操作将阻止默认值的使用.
        /// </summary>
        /// <param name="command">命令名</param>
        /// <param name="commandParameters">SqlParameters数组</param>
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            try
            {
                if (command == null)
                    throw new ArgumentNullException("command");
                if (commandParameters != null)
                {
                    foreach (SqlParameter p in commandParameters)
                    {
                        if (p != null)
                        {
                            // 检查未分配值的输出参数,将其分配以DBNull.Value.
                            if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) &&
                                (p.Value == null))
                            {
                                p.Value = DBNull.Value;
                            }
                            command.Parameters.Add(p);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error($"AttachParameters Exception {ex.Message}\r\n{ex.StackTrace}");
            }
            //应用完成后清除原所有参数值
            // ClearIDataParameter();
        }

        /// <summary>
        /// 预处理用户提供的命令,数据库连接/事务/命令类型/参数
        /// </summary>
        /// <param name="transaction">一个有效的事务或者是null值</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">存储过程名或都SQL命令文本</param>
        /// <param name="commandParameters">和命令相关联的SqlParameter参数数组,如果没有参数为'null'</param>
        /// <param name="mustCloseConnection"><c>true</c> 如果连接是打开的,则为true,其它情况下为false.</param>
        private static void PrepareCommand(SqlConnection Connection, SqlCommand Command, SqlTransaction transaction, CommandType commandType, 
            string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            try
            {
                if (Command == null)
                    throw new ArgumentNullException("command");
                if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

                if (Connection.State != ConnectionState.Open)
                {
                    mustCloseConnection = true;
                    Connection.Open();
                }
                else
                {
                    mustCloseConnection = false;
                }

                // 给命令分配一个数据库连接.
                Command.Connection = Connection;

                // 设置命令文本(存储过程名或SQL语句)
                Command.CommandText = commandText;

                // 分配事务
                if (transaction != null)
                {
                    if (transaction.Connection == null)
                        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                    Command.Transaction = transaction;
                }

                // 设置命令类型.
                Command.CommandType = commandType;

                // 分配命令参数
                if (commandParameters != null)
                {
                    AttachParameters(Command, commandParameters);
                }
            }
            catch(Exception ex)
            {
                mustCloseConnection = false;
                LogHelper.Log.Error($"{ex.Message}\r\n{ex.StackTrace}");
            }
        }
        #endregion

        #region ExecuteDataSet 数据表
        /// <summary>
        /// 执行指定数据库连接字符串的命令,返回DataSet.
        /// </summary>
        /// <param name="commandText">存储过程名称或SQL语句</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public static DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet((SqlTransaction)null, commandText, CommandType.Text, (SqlParameter[])null);
        }
        /// <summary>
        /// 执行指定数据库连接字符串的命令,返回DataSet.
        /// </summary>
        /// <param name="commandText">存储过程名称或SQL语句</param>
        /// <param name="commandtype">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public static DataSet ExecuteDataSet(SqlTransaction transaction, string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            if (SqlConnectionString == null || SqlConnectionString.Length == 0) throw new ArgumentNullException("ConnectionString");

            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {

                connection.Open();
                bool mustCloseConnection = false;
                SqlCommand Command = new SqlCommand();
                PrepareCommand(connection, Command, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
                try
                {
                    SqlDataAdapter sdap = new SqlDataAdapter();
                    sdap.SelectCommand = Command;

                    DataSet ds = new DataSet();
                    sdap.Fill(ds);
                    Command.Parameters.Clear();//清空

                    Command.Dispose();
                    if (mustCloseConnection)
                        connection.Close();
                    return ds;
                }
                catch (Exception ex)
                {
                    Command.Parameters.Clear();//清空

                    Command.Dispose();
                    if (mustCloseConnection)
                        connection.Close();

                    return new DataSet();
                }
            }
        }
        /// <summary>
        /// 执行指定数据库连接字符串的命令,返回DataSet.
        /// </summary>
        /// <param name="commandText">存储过程名称或SQL语句</param>
        /// <param name="commandTytpe">命令类型 (存储过程,命令文本或其它)</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public static DataSet ExecuteDataSet(string commandText, CommandType commandTytpe)
        {
            return ExecuteDataSet((SqlTransaction)null, commandText, commandTytpe, (SqlParameter[])null);
        }
        /// <summary>
        /// 执行指定数据库连接字符串的命令,返回DataSet.
        /// </summary>
        /// <param name="outParameters">输出输出参数结果集合,例:{Name,Value}</param>
        /// <param name="commandText">存储过程名称或SQL语句</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public static DataSet ExecuteDataSet(ref Hashtable outParameters, string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            if (SqlConnectionString == null || SqlConnectionString.Length == 0) throw new ArgumentNullException("ConnectionString");

            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {

                connection.Open();

                bool mustCloseConnection = false;
                SqlCommand Command = new SqlCommand();
                PrepareCommand(connection, Command, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
                try
                {
                    SqlDataAdapter sdap = new SqlDataAdapter();
                    sdap.SelectCommand = Command;

                    DataSet ds = new DataSet();
                    sdap.Fill(ds);

                    if (outParameters != null)
                    {
                        for (int i = 0; i < Command.Parameters.Count; i++)
                        {
                            if (Command.Parameters[i].Direction == ParameterDirection.Output)
                            {
                                if (!outParameters.Contains(Command.Parameters[i].ParameterName))
                                {
                                    outParameters.Add(Command.Parameters[i].ParameterName, Command.Parameters[i].Value.ToString());
                                }
                            }
                        }
                    }
                    Command.Parameters.Clear();//清空

                    Command.Dispose();
                    if (mustCloseConnection)
                        connection.Close();
                    return ds;
                }
                catch (Exception ex)
                {
                    Command.Parameters.Clear();//清空

                    Command.Dispose();
                    if (mustCloseConnection)
                        connection.Close();

                    return new DataSet();
                }
            }
        }
        /// <summary>
        /// 执行指定数据库连接字符串的命令,返回DataSet.
        /// </summary>
        /// <param name="outParameters">输出输出参数结果集合,例:{Name,Value}</param>
        /// <param name="SqlSPro">存储过程</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public static DataSet ExecuteDataSet(ref Hashtable outParameters, string SqlSPro, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(ref outParameters, SqlSPro, CommandType.StoredProcedure, commandParameters);
        }
        /// <summary>
        /// 执行指定数据库连接字符串的命令,返回DataSet.
        /// </summary>
        /// <param name="outParameters">输出输出参数结果集合,例:{Name,Value}</param>
        /// <param name="SqlSPro">存储过程</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public static DataSet ExecuteDataSet(string SqlSPro, params SqlParameter[] commandParameters)
        {
            Hashtable outParameters = null;
            return ExecuteDataSet(ref outParameters, SqlSPro, CommandType.StoredProcedure, commandParameters);
        }
        #endregion

        #region ExecuteScalar 返回结果集中的第一行第一列

        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (SqlConnectionString == null || SqlConnectionString.Length == 0) throw new ArgumentNullException("ConnectionString");

            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {

                connection.Open();
                bool mustCloseConnection = false;
                SqlCommand Command = new SqlCommand();
                try
                {
                    PrepareCommand(connection, Command, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
                    object rvalue = Command.ExecuteScalar();
                    Command.Parameters.Clear();//清空
                    Command.Dispose();
                    if (mustCloseConnection)
                        connection.Close();
                    return rvalue;
                }
                catch (Exception ex)
                {
                    Command.Parameters.Clear();//清空
                    if (mustCloseConnection)
                        connection.Close();

                    return null;
                }

            }
        }
        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(CommandType commandType, string commandText)
        {
            return ExecuteScalar((SqlTransaction)null, commandType, commandText, (SqlParameter[])null);
        }
        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar((SqlTransaction)null, commandType, commandText, commandParameters);
        }
        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static object ExecuteScalar(CommandType commandType, string commandText, SqlTransaction transaction)
        {
            return ExecuteScalar(transaction, commandType, commandText, (SqlParameter[])null);
        }
        /// <summary>
        /// 返回一第数据
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(CommandType.Text, commandText, (SqlTransaction)null);
        }
        #endregion

        #region ExecuteDataReader 数据阅读器

        public static DbDataReader ExecuteDataReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            DbDataReader reader = null;
            bool mustCloseConnection = false;
            SqlCommand Command = new SqlCommand();
            try
            {
                PrepareCommand(connection, Command, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

                reader = Command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                //reader = Command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                Command.Parameters.Clear();//清空
                //Command.Dispose();
            }
            catch (Exception ex)
            {

            }
            return reader;
        }
        public static DbDataReader ExecuteDataReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            if (SqlConnectionString == null || SqlConnectionString.Length == 0) throw new ArgumentNullException("ConnectionString");
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(SqlConnectionString);

                connection.Open();
                return ExecuteDataReader(connection, transaction, commandType, commandText, (SqlParameter[])null);
            }
            catch
            {
                // If we fail to return the SqlDatReader, we need to close the connection ourselves
                if (connection != null) connection.Close();
                throw;
            }
        }
        public static DbDataReader ExecuteDataReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (SqlConnectionString == null || SqlConnectionString.Length == 0) throw new ArgumentNullException("ConnectionString");
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(SqlConnectionString);

                connection.Open();
                return ExecuteDataReader(connection, (SqlTransaction)null, commandType, commandText, commandParameters);
            }
            catch
            {
                // If we fail to return the SqlDatReader, we need to close the connection ourselves
                if (connection != null) connection.Close();
                throw;
            }
        }
        public static DbDataReader ExecuteDataReader(CommandType commandType, string commandText)
        {
            return ExecuteDataReader(commandType, commandText, (SqlParameter[])null);
        }
        public static DbDataReader ExecuteDataReader(string commandText)
        {
            return ExecuteDataReader(CommandType.Text, commandText);
        }
        public static DbDataReader ExecuteDataReader(SqlConnection connection, out List<string[]> outParameters, string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            DbDataReader reader = null;
            outParameters = new List<string[]>();
            try
            {
                bool mustCloseConnection = false;
                SqlCommand Command = new SqlCommand();
                PrepareCommand(connection, Command, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

                reader = Command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);// (CommandBehavior.CloseConnection);
                Command.Parameters.Clear();//清空
                Command.Dispose();

            }
            catch (Exception ex)
            {
            }
            return reader;
        }
        public static DbDataReader ExecuteDataReader(out List<string[]> outParameters, string SqlSPro, params SqlParameter[] commandParameters)
        {
            if (SqlConnectionString == null || SqlConnectionString.Length == 0) throw new ArgumentNullException("ConnectionString");
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(SqlConnectionString);

                connection.Open();
                return ExecuteDataReader(connection, out outParameters, SqlSPro, CommandType.StoredProcedure, commandParameters);
            }
            catch
            {
                // If we fail to return the SqlDatReader, we need to close the connection ourselves
                if (connection != null) connection.Close();
                throw;
            }
        }
        #endregion

        #region ExecuteDataRow 返回结果集中第一行
        /// <summary>
        /// 执行指定数据库连接字符串的命令,返回DataSet第一行.
        /// </summary>
        /// <param name="commandText">存储过程名称或SQL语句</param>
        /// <param name="commandtype">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回一个包含结果集的DataSet中的第一行</returns>
        public static DataRow ExecuteDataRow(SqlTransaction transaction, string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            try
            {
                DataRow row = null;
                DataSet ds = ExecuteDataSet(transaction, commandText, commandType, commandParameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = (DataRow)ds.Tables[0].Rows[0];
                }
                ds.Dispose();
                return row;
            }
            catch
            {
                return null;
            }
        }
        public static DataRow ExecuteDataRow(string commandText, CommandType commandType)
        {
            return ExecuteDataRow((SqlTransaction)null, commandText, commandType, (SqlParameter[])null);
        }
        public static DataRow ExecuteDataRow(string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            return ExecuteDataRow((SqlTransaction)null, commandText, commandType, commandParameters);
        }
        public static DataRow ExecuteDataRow(string commandText)
        {
            return ExecuteDataRow((SqlTransaction)null, commandText, CommandType.Text, (SqlParameter[])null);
        }
        #endregion

        #region ExecuteNonQuery方法
        public static int ExecuteNonQuery(ref Hashtable OutPut, string commandText, params SqlParameter[] commandParameters)
        {
            if (_SqlConnectionString == null || _SqlConnectionString.Length == 0) throw new ArgumentNullException("ConnectionString");

            using (SqlConnection connection = new SqlConnection(_SqlConnectionString))
            {
                connection.Open();
                // 创建DbCommand命令,并进行预处理

                bool mustCloseConnection = false;
                SqlCommand Command = new SqlCommand();
                PrepareCommand(connection, Command, (SqlTransaction)null, CommandType.StoredProcedure, commandText, commandParameters, out mustCloseConnection);

                // 执行命令
                int retval = Command.ExecuteNonQuery();
                for (int i = 0; i < Command.Parameters.Count; i++)
                {
                    if (Command.Parameters[i].Direction == ParameterDirection.Output)
                    {
                        if (!OutPut.Contains(Command.Parameters[i].ParameterName.ToString()))
                        {
                            OutPut.Add(Command.Parameters[i].ParameterName.ToString(), Command.Parameters[i].Value.ToString());
                        }
                    }
                }
                // 清除参数,以便再次使用.
                Command.Parameters.Clear();

                Command.Dispose();

                return retval;
            }
        }
        /// <summary>
        /// DataTable批量添加到数据库
        /// </summary>
        /// <param name="TableName">要写入的表名</param>
        /// <param name="dt">DataTable表</param>
        public static void CopyExecutNonQuery(string TableName, DataTable dt)
        {
            if (SqlConnectionString == null || SqlConnectionString.Length == 0) throw new ArgumentNullException("ConnectionString");
            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();
                using (SqlTransaction Trans = connection.BeginTransaction())
                {
                    using (SqlBulkCopy sqlBC = new SqlBulkCopy(connection, SqlBulkCopyOptions.FireTriggers, Trans))
                    {
                        try
                        {
                            //一次批量的插入的数据量
                            sqlBC.BatchSize = dt.Rows.Count;
                            //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除
                            sqlBC.BulkCopyTimeout = 360;

                            //設定 NotifyAfter 属性，以便在每插入10000 条数据时，呼叫相应事件。
                            // sqlBC.NotifyAfter = 1000;
                            // sqlBC.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsCopied);

                            //设置要批量写入的表
                            sqlBC.DestinationTableName = TableName;

                            //自定义的datatable和数据库的字段进行对应
                            //sqlBC.ColumnMappings.Add("id", "tel");
                            //sqlBC.ColumnMappings.Add("name", "neirong");
                            //for (int i = 0; i < dtColum.Count; i++)
                            //{
                            //    sqlBC.ColumnMappings.Add(dtColum[i].ColumnName.ToString(), dtColum[i].ColumnName.ToString());
                            //}
                            //批量写入
                            sqlBC.WriteToServer(dt);
                            Trans.Commit();
                        }
                        catch
                        {
                            Trans.Rollback();
                        }

                    }
                }
            }
        }
        /// <summary>
        ///  执行指定数据库连接对象的命令
        /// </summary>
        /// <param name="commandType">命令类型(存储过程,命令文本或其它.)</param>
        /// <param name="commandText">T存储过程名称或SQL语句</param>
        /// <param name="commandParameters">SqlParamter参数数组</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (SqlConnectionString == null || SqlConnectionString.Length == 0)
                throw new ArgumentNullException("ConnectionString");

            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();
                // 创建DbCommand命令,并进行预处理

                bool mustCloseConnection = false;
                SqlCommand Command = new SqlCommand();
                PrepareCommand(connection, Command, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

                // 执行命令
                int retval = Command.ExecuteNonQuery();
                // 清除参数,以便再次使用.
                Command.Parameters.Clear();

                Command.Dispose();

                return retval;
            }
        }
        public static int ExecuteNonQuery(SqlTransaction Transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {

            bool mustCloseConnection = false;
            SqlCommand Command = new SqlCommand(); ;
            PrepareCommand(Transaction.Connection, Command, Transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            try
            {
                // 执行命令
                int retval = Command.ExecuteNonQuery();

                // 清除参数,以便再次使用.
                Command.Parameters.Clear();
                if (Transaction != null)
                {
                    Transaction.Commit();
                }

                Command.Dispose();
                if (mustCloseConnection)
                {
                    Transaction.Connection.Close();
                    Transaction.Connection.Dispose();
                }
                return retval;
            }
            catch (Exception ex)
            {
                if (Transaction != null)
                {
                    Transaction.Rollback();
                }
                LogHelper.Log.Error("ExecuteNonQuery ERROR "+ex.Message);
                Command.Dispose();
                return 0;
            }
        }
        public static int ExecuteNonQuery(out int Scope_Identity, SqlTransaction Transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            Scope_Identity = 0;


            bool mustCloseConnection = false;
            SqlCommand Command = new SqlCommand();
            PrepareCommand(Transaction.Connection, Command, Transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            try
            {
                // 执行命令
                int retval = Command.ExecuteNonQuery();
                // 清除参数,以便再次使用.
                Command.Parameters.Clear();
                Command.CommandType = CommandType.Text;
                Command.CommandText = "SELECT SCOPE_IDENTITY()";
                Scope_Identity = int.Parse(Command.ExecuteScalar().ToString());

                Command.Dispose();
                if (mustCloseConnection)
                {
                    Transaction.Connection.Close();
                    Transaction.Connection.Dispose();
                }
                return retval;
            }
            catch (Exception ex)
            {
                Command.Dispose();
                Transaction.Connection.Close();
                Transaction.Connection.Dispose();
                return 0;
            }
        }
        /// <summary>
        /// 执行指定数据库连接对象的命令,并输出最后执行的结果编号
        /// </summary>
        /// <param name="Scope_Identity">输出最后执行结果</param>
        /// <param name="commandType">命令类型(存储过程,命令文本或其它.)</param>
        /// <param name="commandText">T存储过程名称或SQL语句</param>
        /// <param name="commandParameters">SqlParamter参数数组</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(out int Scope_Identity, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {

            Scope_Identity = 0;
            if (SqlConnectionString == null || SqlConnectionString.Length == 0) throw new ArgumentNullException("ConnectionString");

            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {

                connection.Open();
                // 创建DbCommand命令,并进行预处理

                bool mustCloseConnection = false;
                SqlCommand Command = new SqlCommand();
                PrepareCommand(connection, Command, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

                try
                {
                    // 执行命令
                    int retval = Command.ExecuteNonQuery();
                    // 清除参数,以便再次使用.
                    Command.Parameters.Clear();
                    Command.CommandType = CommandType.Text;
                    Command.CommandText = "SELECT SCOPE_IDENTITY()";
                    Scope_Identity = int.Parse(Command.ExecuteScalar().ToString());

                    Command.Dispose();
                    return retval;
                }
                catch (Exception ex)
                {

                    Command.Dispose();
                    return 0;
                }
            }
        }
        /// <summary>
        /// 执行指定数据库连接对象的命令
        /// </summary>
        /// <param name="connection">一个有效的数据库连接对象</param>
        /// <param name="commandType">命令类型(存储过程,命令文本或其它.)</param>
        /// <param name="commandText">存储过程名称或SQL语句</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(CommandType commandType, string CommandText)
        {
            if (CommandText == null || CommandText.Length == 0) throw new ArgumentNullException("commandText");
            return ExecuteNonQuery(commandType, CommandText, (SqlParameter[])null);
        }
        /// <summary>
        /// 执行指定数据库连接对象的命令,将对象数组的值赋给存储过程参数.
        /// </summary>
        /// <param name="connection">一个有效的数据库连接对象</param>
        /// <param name="spName">存储过程名</param>
        /// <param name="parameterValues">分配给存储过程输入参数的对象数组</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(string spName, params SqlParameter[] commandParameters)
        {
            try
            {
                if (spName == null || spName.Length == 0)
                    throw new ArgumentNullException("spName");

                // 如果有参数值
                if ((commandParameters != null) && (commandParameters.Length > 0))
                {
                    return ExecuteNonQuery(CommandType.Text, spName, commandParameters);
                }
                else
                {
                    return ExecuteNonQuery(CommandType.Text, spName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error($"{ex.Message}\r\n{ex.StackTrace}");
                return -1;
            }
        }

        /// <summary>
        /// 执行带事务的SQL语句
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string commandText)
        {
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");
            //SqlTransaction Transaction = BBDataProvider.Transaction;
            if (SqlConnectionString == null || SqlConnectionString.Length == 0) throw new ArgumentNullException("ConnectionString");

            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();
                SqlTransaction Transaction = connection.BeginTransaction();

                return ExecuteNonQuery(Transaction, CommandType.Text, commandText, (SqlParameter[])null);
            }
        }
        /// <summary>
        /// 执行存储过程，返回Output结果
        /// </summary>
        /// <param name="commandText">存储过程名</param>
        /// <param name="commandType">命令类型(存储过程)</param>
        /// <param name="commandParameters">SqlParamter参数数组</param>
        /// <returns>Output结果</returns>
        public static List<string[]> ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            if (SqlConnectionString == null || SqlConnectionString.Length == 0) throw new ArgumentNullException("ConnectionString");

            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();
                bool mustCloseConnection = false;
                SqlCommand Command = new SqlCommand();
                PrepareCommand(connection, Command, (SqlTransaction)null, CommandType.StoredProcedure, commandText, commandParameters, out mustCloseConnection);
                try
                {
                    Command.ExecuteNonQuery();
                }
                catch { }

                List<string[]> outParameters = new List<string[]>();
                for (int i = 0; i < Command.Parameters.Count; i++)
                {
                    if (Command.Parameters[i].Direction == ParameterDirection.Output)
                    {
                        string[] parameteritem = { Command.Parameters[i].ParameterName.ToString(), Command.Parameters[i].Value.ToString() };
                        outParameters.Add(parameteritem);
                    }
                }
                Command.Parameters.Clear();//清空

                Command.Dispose();
                if (mustCloseConnection)
                    connection.Close();
                return outParameters;
            }
        }

        #endregion
    }
}
