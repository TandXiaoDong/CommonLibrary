using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using CommonUtils.Logger;

namespace CommonUtils.DBHelper
{
    /// <summary>
    /// 数据访问抽象基础类
    /// Copyright (C) Maticsoft
    /// </summary>
    public class DBHelperMySql
    {
        public enum EarlyWarnType
        {
            EVENT_COUNT = 1,
            ENERGY = 2
        }

        /// <summary>
        /// 该枚举用于DBHelperMySql的构造传参
        /// </summary>
        public enum DbType
        {
            /// <summary>
            /// ssws
            /// </summary>
            DB_SSWS = 0,
            /// <summary>
            /// ssew_data
            /// </summary>
            DB_SSEWDAT = 1,
            /// <summary>
            /// ssew_config
            /// </summary>
            DB_SSEWCFG = 2
        }

        #region Time-Variant Table
        public class t_warnres_
        {
            public const string T_WARNRES_ = "t_warnres_";
            public const string F_ID = "F_ID";
            public const string F_TIME = "TIME";
            public const string F_TYPE = "TYPE";
            public const string F_PERIOD_1 = "PERIOD_1";
            public const string F_PERIOD_2 = "PERIOD_2";
            public const string F_PERIOD_3 = "PERIOD_3";
            public const string F_PERIOD_4 = "PERIOD_4";
            public const string F_PERIOD_5 = "PERIOD_5";
            public const string F_PERIOD_6 = "PERIOD_6";
            public const string F_PERIOD_7 = "PERIOD_7";
            public const string F_PERIOD_8 = "PERIOD_8";
        }

        /// <summary>
        /// 映射有效信号表
        /// 不同的设备对应不同的有效信号表，
        /// 当设备的有效信号在表中存储的记录超出阈值时，
        /// 则新建同样的表，并将表名中的索引增加1
        /// 表名规则如下
        /// effctv_sgnl_(table_index)_(device_id)
        /// </summary>
        public class effctv_sgnl_
        {
            public const string T_EFFCTV_SGNL_ = "effctv_sgnl_";
            public const string F_SIGNAL_INDEX = "SIGNAL_INDEX";
            public const string F_RECORDED_TIME = "RECORDED_TIME";
            public const string F_SIGNAL_TIMESTAMP = "SIGNAL_TIMESTAMP";
            public const string F_SIGNAL_X = "SIGNAL_X";
            public const string F_SIGNAL_Y = "SIGNAL_Y";
            public const string F_SIGNAL_Z = "SIGNAL_Z";
        }

        //修改有效信号表结构，不再使用之前的有效信号表
        //public class t_effictiveres_
        //{
        //    public const string T_EFFICTIVERES_ = "t_effictiveres_";
        //    public const string F_TIME = "TIME";
        //    public const string F_SER_ID = "SER_ID";
        //    public const string F_SEQ_ID = "SEQ_ID";
        //    public const string F_ORIG_X = "ORIG_X";
        //    public const string F_ORIG_Y = "ORIG_Y";
        //    public const string F_ORIG_Z = "ORIG_Z";
        //    public const string F_ORIG_T = "ORIG_T";
        //    public const string F_PARAMS = "PARAMS";
        //}

        public class effctv_sgnl_tmstmp_
        {
            public const string T_EEFCTV_SGNL_TMSTMP_ = "effctv_sgnl_tmstmp_";
            public const string F_SIGNAL_START_TIMESTAMP = "SIGNAL_START_TIMESTAMP";
            public const string F_SIGNAL_END_TIMESTAMP = "SIGNAL_END_TIMESTAMP";
            public const string F_TABLE_INDEX = "TABLE_INDEX";
            public const string F_ALGORITHMS_INFO = "ALGORITHMS_INFO";
        }

        public class effctv_sgnl_anlyss_
        {
            public const string T_EFFCTV_SGNL_ANLYSS_ = "effctv_sgnl_anlyss_";
            /// <summary>
            /// 分析得到该结果所用的有效信号的起始时间戳
            /// </summary>
            public const string F_SIGNAL_START_TIMESTAMP = "SIGNAL_START_TIMESTAMP";
            public const string F_SIGNAL_END_TIMESTAMP = "SIGNAL_END_TIMESTAMP";
            public const string F_EIGENVALUE_MAJOR = "EIGENVALUE_MAJOR";
            public const string F_EIGENVECTOR_MAJOR_X = "EIGENVECTOR_MAJOR_X";
            public const string F_EIGENVECTOR_MAJOR_Y = "EIGENVECTOR_MAJOR_Y";
            public const string F_EIGENVECTOR_MAJOR_Z = "EIGENVECTOR_MAJOR_Z";
            public const string F_EIGENVALUE_SUB = "EIGENVALUE_SUB";
            public const string F_EIGENVECTOR_SUB_X = "EIGENVECTOR_SUB_X";
            public const string F_EIGENVECTOR_SUB_Y = "EIGENVECTOR_SUB_Y";
            public const string F_EIGENVECTOR_SUB_Z = "EIGENVECTOR_SUB_Z";
            public const string F_EIGENVALUE_MINOR = "EIGENVALUE_MINOR";
            public const string F_EIGENVECTOR_MINOR_X = "EIGENVECTOR_MINOR_X";
            public const string F_EIGENVECTOR_MINOR_Y = "EIGENVECTOR_MINOR_Y";
            public const string F_EIGENVECTOR_MINOR_Z = "EIGENVECTOR_MINOR_Z";
            public const string F_PRIME_WAVE_DIREC_X = "PRIME_WAVE_DIREC_X";
            public const string F_PRIME_WAVE_DIREC_Y = "PRIME_WAVE_DIREC_Y";
            public const string F_PRIME_WAVE_DIREC_Z = "PRIME_WAVE_DIREC_Z";
            public const string F_PRIME_WAVE_BREAK_INDEX = "PRIME_WAVE_BREAK_INDEX";
            public const string F_SECOND_WAVE_BREAK_INDEX = "SECOND_WAVE_BREAK_INDEX";
            /// <summary>
            /// 方位角，单位角度，范围[0, 360)，右手坐标系中，x0y平面，以x正方向为0，绕原点旋转，经过360度，回到x正方向
            /// </summary>
            public const string F_AZIMUTH_IN_ANGLE = "AZIMUTH_IN_ANGLE";
            /// <summary>
            /// 倾角，单位角度，范围[0, 180]，右手坐标系中，z轴正方向为0度，负方向为180度
            /// </summary>
            public const string F_INCIDENCE_IN_ANGLE = "INCIDENCE_IN_ANGLE";
            public const string F_DISTANCE_IN_METER = "DISTANCE_IN_METER";
            /// <summary>
            /// 计算有效信号得到该结果时，所用到的算法及参数信息，该字段格式与有效信号起始时间戳表中算法参数字段的格式一致
            /// </summary>
            public const string F_ALGORITHMS_INFO = "ALGORITHMS_INFO";
            /// <summary>
            /// 备用字段，因为得到该结果时，使用了一段噪声信号，因此，预留该字段，以关联确定相应的噪声信号
            /// </summary>
            public const string F_SPECIFIC_USE1 = "SPECIFIC_USE1";
            /// <summary>
            /// 备用字段
            /// </summary>
            public const string F_SPECIFIC_USE2 = "SPECIFIC_USE2";
        }
        #endregion


        public class t_project
        {
            public const string T_PROJECT = "t_project";
            public const string F_PROJ_ID = "PROJ_ID";
            public const string F_PROJ_NAME = "PROJ_NAME";
            /// <summary>
            /// 0未使用，1正在使用
            /// </summary>
            public const string F_PROJ_STATE = "PROJ_STATE";
            public const string F_PROJ_SERVER_PATH = "PROJ_SERVER_PATH";
            public const string F_PROJ_CLIENT_PATH = "PROJ_CLIENT_PATH";
            public const string F_CREATE_DATE = "CREATE_DATE";
            public const string F_MODIFY_DATE = "MODIFY_DATE";
            public const string F_REMARK = "REMARK";
            public const string F_SLOPE_NUMBER = "SLOPE_NUMBER";
            public const string F_PROJ_ROUTINE = "PROJ_ROUTINE";
            public const string F_MILEAGE = "MILEAGE";
            public const string F_START_POSITION = "START_POSITION";
            public const string F_END_POSITION = "END_POSITION";
            public const string F_DESIGNED_BY = "DESIGNED_BY";
            public const string F_CONSTRUCTION_DEPART = "CONSTRUCTION_DEPART";
        }

        public class t_slope
        {
            public const string T_SLOPE = "t_slope";
            public const string F_SLOPE_ID = "SLOPE_ID";
            public const string F_SLOPE_NAME = "SLOPE_NAME";
            public const string F_PROJ_ID = "PROJ_ID";
            /// <summary>
            /// 1左2右
            /// </summary>
            public const string F_SIDE = "SIDE";
            public const string F_LON = "LON";
            public const string F_LAT = "LAT";
            public const string F_PRIME_WAVE_VELOCITY = "PRIME_WAVE_VELOCITY";
            public const string F_SECOND_WAVE_VELOCITY = "SECOND_WAVE_VELOCITY";
            public const string F_ROCK_DENSITY = "ROCK_DENSITY";
            public const string F_ROCK_RIGIDITY = "ROCK_RIGIDITY";
            /// <summary>
            /// 1有效，0无效
            /// </summary>
            public const string F_STATUS = "STATUS";
            public const string F_START_PEG_NO = "START_PEG_NO";
            public const string F_END_PEG_NO = "END_PEG_NO";
            public const string F_DEPART_ID = "DEPART_ID";
            public const string F_DESIGN_BY = "DESIGN_BY";
            public const string F_CREATE_DATE = "CREATE_DATE";
            public const string F_MODIFY_DATE = "MODIFY_DATE";
            public const string F_REMARK = "REMARK";
            public const string F_SLOPE_START_POSITION = "SLOPE_START_POSITION";
            public const string F_SLOPE_END_POSITION = "SLOPE_END_POSITION";
            public const string F_CONSTRUCT_DEPART = "CONSTRUCT_DEPART";
            public const string F_COMPLETED_TIME = "COMPLETED_TIME";
        }

        /// <summary>
        /// 边坡层级信息表，
        /// 不过包含的不仅时层级信息，
        /// 还有边坡的相关属性，
        /// 比如P波，S波速度，
        /// 岩石密度，刚度等
        /// </summary>
        public class t_slope_level
        {
            public const string T_SLOPE_LEVEL = "t_slope_level";
            public const string F_LEVEL_ID = "LEVEL_ID";
            public const string F_SLOPE_ID = "SLOPE_ID";
            public const string F_LEVEL = "LEVEL";
            public const string F_BOTTOM_LENGTH = "BOTTOM_LENGTH";
            public const string F_BOTTOM_WIDTH = "BOTTOM_WIDTH";
            public const string F_HEIGHT = "HEIGHT";
            public const string F_TOP_LENGTH = "TOP_LENGTH";
            public const string F_TOP_WIDTH = "TOP_WIDTH";
            public const string F_TOP_LEFT_OFFSET = "TOP_LEFT_OFFSET";
            public const string F_LEVEL_LEFT_OFFSET = "LEVEL_LEFT_OFFSET";
            public const string F_ANGLE = "ANGLE";
            public const string F_CREATE_DATE = "CREATE_DATE";
            public const string F_MODIFY_DATE = "MODIFY_DATE";
            public const string F_STATUS = "STATUS";
        }

        public class t_device
        {
            public const string T_DEVICE = "t_device";
            public const string F_DEVICE_ID = "DEVICE_ID";
            public const string F_DEVICE_NAME = "DEVICE_NAME";
            public const string F_SLOPE_ID = "SLOPE_ID";
            public const string F_SUITE_ID = "SUITE_ID";
            /// <summary>
            /// 1左，2右
            /// </summary>
            public const string F_SIDE = "SIDE";
            public const string F_LON = "LON";
            public const string F_LAT = "LAT";
            public const string F_IP = "IP";
            /// <summary>
            /// 1有效，0无效
            /// </summary>
            public const string F_STATUS = "STATUS";
            public const string F_DEPART_ID = "DEPART_ID";
            public const string F_REMARK = "REMARK";
            public const string F_X = "X";
            public const string F_Y = "Y";
            public const string F_Z = "Z";
            public const string F_MONITOR_DEPART = "MONITOR_DEPART";
        }

        public class t_device_parameter
        {
            public const string T_DEVICE_PARAMETER = "t_device_parameter";
            public const string F_DEVICE_ID = "DEVICE_ID";
            public const string F_MODEL_TYPE = "MODEL_TYPE";
            public const string F_TIMER_RUN_MOMENT = "TIMER_RUN_MOMENT";
            public const string F_TIMER_STOP_MOMENT = "TIMER_STOP_MOMENT";
            public const string F_TOP_VALUE = "TOP_VALUE";
            public const string F_DELAY_POINT = "DELAY_POINT";
            public const string F_SAMPLE_LENGTH = "SAMPLE_LENGTH";
            public const string F_SIGNAL_LENGTH = "SIGNAL_LENGTH";
            public const string F_FLAG = "FLAG";
            public const string F_REBACK_START_TIME = "REBACK_START_TIME";
            public const string F_REBACK_END_TIME = "REBACK_END_TIME";
            public const string F_FREQUENCY = "FREQUENCY";
            public const string F_RANGE = "RANGE";
            public const string F_REBACK_INTERVAL = "REBACK_INTERVAL";
            public const string F_CHARACTER_VALUE = "CHARACTER_VALUE";
        }

        public class t_alglist
        {
            public const string T_ALGLIST = "t_alglist";
            public const string F_ALG_NAME = "ALG_NAME";
            public const string F_ALG_SPEC = "ALG_SPEC";
            public const string F_PARAM_NUM = "PARAM_NUM";
            public const string F_PARAM_1 = "PARAM_1";
            public const string F_PARAM_2 = "PARAM_2";
            public const string F_PARAM_3 = "PARAM_3";
            public const string F_PARAM_4 = "PARAM_4";
            public const string F_PARAM_5 = "PARAM_5";
            public const string F_PARAM_6 = "PARAM_6";
            public const string F_PARAM_7 = "PARAM_7";
            public const string F_PARAM_8 = "PARAM_8";
            public const string F_PARAM_9 = "PARAM_9";
            public const string F_PARAM_10 = "PARAM_10";
            public const string F_PARAM_11 = "PARAM_11";
            public const string F_PARAM_12 = "PARAM_12";
            public const string F_PARAM_13 = "PARAM_13";
            public const string F_PARAM_14 = "PARAM_14";
            public const string F_PARAM_15 = "PARAM_15";
            public const string F_PARAM_16 = "PARAM_16";
            public const string F_PARAM_17 = "PARAM_17";
            public const string F_PARAM_18 = "PARAM_18";
            public const string F_PARAM_19 = "PARAM_19";
            public const string F_PARAM_20 = "PARAM_20";
            public const string F_ID = "ID";
        }

        public class t_suitelist
        {
            public const string T_SUITELIST = "t_suitelist";
            public const string F_SUITE_NAME = "SUITE_NAME";
            public const string F_SUITE_ID = "SUITE_ID";
            public const string F_CALC_ID = "CALC_ID";
            public const string F_ALG_NAME = "ALG_NAME";
            public const string F_IS_AUTO_PARAM = "IS_AUTO_PARAM";
            public const string F_ALG_SPEC = "ALG_SPEC";
            public const string F_ALG_NUM = "ALG_NUM";
            public const string F_PARAM_1 = "PARAM_1";
            public const string F_PARAM_2 = "PARAM_2";
            public const string F_PARAM_3 = "PARAM_3";
            public const string F_PARAM_4 = "PARAM_4";
            public const string F_PARAM_5 = "PARAM_5";
            public const string F_PARAM_6 = "PARAM_6";
            public const string F_PARAM_70 = "PARAM_70";
            public const string F_PARAM_81 = "PARAM_81";
            public const string F_PARAM_92 = "PARAM_92";
            public const string F_PARAM_10 = "PARAM_10";
            public const string F_PARAM_11 = "PARAM_11";
            public const string F_PARAM_12 = "PARAM_12";
            public const string F_PARAM_13 = "PARAM_13";
            public const string F_PARAM_14 = "PARAM_14";
            public const string F_PARAM_15 = "PARAM_15";
            public const string F_PARAM_16 = "PARAM_16";
            public const string F_PARAM_17 = "PARAM_17";
            public const string F_PARAM_18 = "PARAM_18";
            public const string F_PARAM_19 = "PARAM_19";
            public const string F_PARAM_20 = "PARAM_20";
            public const string F_PARAMS = "PARAMS";
        }

        public class t_user
        {
            public const string T_USER = "t_user";
            public const string F_USER_NAME = "USER_NAME";
            public const string F_PASSWORD = "PASSWORD";
            public const string F_DEPART_ID = "DEPART_ID";
            public const string F_FILE_ID = "FILE_ID";
            public const string F_REAL_NAME = "REAL_NAME";
            public const string F_PHONE = "PHONE";
            public const string F_REMARK = "REMARK";
            public const string F_CREATE_DATE = "CREATE_DATE";
            public const string F_UPDATE_DATE = "UPDATE_DATE";
            /// <summary>
            /// 1有效 0无效
            /// </summary>
            public const string F_STATUS = "STATUS";
        }

        public const string DB_SSWS = "ssws";
        public const string DB_SSEWDAT = "ssew_data";
        public const string DB_SSEWCFG = "ssew_config";

        private static string con_ssws = ConfigurationManager.ConnectionStrings["mysql_ssws"].ToString();
        private static string con_ssew_data = ConfigurationManager.ConnectionStrings["mysql_ssew_data"].ToString();
        private static string con_ssew_config = ConfigurationManager.ConnectionStrings["mysql_ssew_config"].ToString();

        private static string local_con_ssws = ConfigurationManager.ConnectionStrings["local_mysql_ssws"].ToString();
        private static string local_con_ssew_data = ConfigurationManager.ConnectionStrings["local_mysql_ssew_data"].ToString();
        private static string local_con_ssew_config = ConfigurationManager.ConnectionStrings["local_mysql_ssew_config"].ToString();

        private static string connectionString;

        public DBHelperMySql(string dbName, bool bIsLocalServer = false)
        {
            if (dbName.ToLower().Equals("ssws"))
            {
                if (true == bIsLocalServer)
                {
                    connectionString = local_con_ssws;
                }
                else
                {
                    connectionString = con_ssws;
                }
            }
            else if (dbName.ToLower().Equals("ssew_config"))
            {
                if (true == bIsLocalServer)
                {
                    connectionString = local_con_ssew_config;
                }
                else
                {
                    connectionString = con_ssew_config;
                }
            }
            else if (dbName.ToLower().Equals("ssew_data"))
            {
                if (true == bIsLocalServer)
                {
                    connectionString = local_con_ssew_data;
                }
                else
                {
                    connectionString = con_ssew_data;
                }
            }
        }

        public DBHelperMySql(DbType dbName)
        {
            if (dbName == DbType.DB_SSWS)
            {
                connectionString = local_con_ssws;
            }
            else if (dbName == DbType.DB_SSEWCFG)
            {
                connectionString = local_con_ssew_config;
            }
            else if (dbName == DbType.DB_SSEWDAT)
            {
                connectionString = local_con_ssew_data;
            }
        }


        /// <summary>
        /// ！！！!!!
        /// 这个方法涉及具体的业务逻辑，
        /// 最好写到相关的代码中，
        /// </summary>
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "SELECT "
                    + "MAX(" + FieldName + ") + 1"
                + " FROM "
                    + TableName + ";";

            object obj = GetSingle(strsql);

            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }


        public static bool Exists(string strSql)
        {
            return GetSingle(strSql);
        }

        public static bool Exists(string strSql, params MySqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);

            if ((object.Equals(obj, null)) || object.Equals(obj, DBNull.Value))
            {
                return false;
            }
            else
            {
                return int.Parse(obj.ToString()) != 0;
            }
        }

        public static DataSet Query(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    new MySqlDataAdapter(SQLString, connection).Fill(ds, "ds");
                }
                catch (MySqlException ex)
                {
                    Log.Error(ex.Message + " " + SQLString);
                    throw new Exception(ex.Message);
                }

                return ds;
            }
        }

        public static DataSet Query(string SQLString, int Times)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, "ds");
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }

                return ds;
            }
        }

        public static DataSet Query(string strSQL, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, strSQL, cmdParms);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return ds;
                }
            }
        }


        public static bool GetSingle(string strSQL)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        return object.Equals(obj, null) || object.Equals(obj, DBNull.Value);
                    }
                    catch (MySqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        public static object GetSingle(string SQLString, int nTimeout)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = nTimeout;
                        object obj = cmd.ExecuteScalar();
                        return object.Equals(obj, null) || object.Equals(obj, DBNull.Value) ? null : obj;
                    }
                    catch (MySqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        public static object GetSingle(string strSQL, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, strSQL, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        return object.Equals(obj, null) || object.Equals(obj, DBNull.Value) ? null : obj;
                    }
                    catch (MySqlException e)
                    {
                        throw e;
                    }
                }
            }
        }


        /// <summary>
        /// 执行查询语句，返回MySqlDataReader ( 注意：调用该方法后，一定要对MySqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>MySqlDataReader</returns>
        public static MySqlDataReader ExecuteReader(string strSQL)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 执行查询语句，返回MySqlDataReader ( 注意：调用该方法后，一定要对MySqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>MySqlDataReader</returns>
        public static MySqlDataReader ExecuteReader(string SQLString, params MySqlParameter[] cmdParms)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (MySqlException e)
            {
                throw e;
            }
            //finally
            //{
            //    cmd.Dispose();
            //    connection.Close();
            //}

        }


        #region ExecuteSql
        public static bool ExecuteSql(string strSQL)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL, connection))
                {
                    try
                    {
                        connection.Open();
                        return cmd.ExecuteReader().Read();
                    }
                    catch (MySqlException e)
                    {
                        connection.Close();
                        LogHelper.Log.Error(strSQL);
                        LogHelper.Log.Error("[ERROR]DBHelperMySql::ExecuteSql$$$$$$$$$$" + e.Message);
                        throw e;
                    }
                }
            }
        }

        public static int ExecuteSqlViaNonQuery(string strSQL)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                {
                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        conn.Clone();
                        LogHelper.Log.Error(strSQL);
                        LogHelper.Log.Error("[ERROR]DBHelperMySql::ExecuteSql$$$$$$$$$$" + ex.Message);
                        throw ex;
                    }
                }
            }
        }

        public static int ExecuteSqlByTimeout(string strSQL, int nTimeout)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = nTimeout;
                        return cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlToInsertImage(string strSQL, byte[] fs)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(strSQL, connection);
                MySqlParameter myParameter = new MySqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    return cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static object ExecuteSqlThenGet(string SQLString, string content)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQLString, connection);
                MySqlParameter myParameter = new MySqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    return object.Equals(obj, null) || object.Equals(obj, DBNull.Value) ? null : obj;
                }
                catch (MySqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="strContent">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlWithStorePrcedure(string strSQL, string strContent)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(strSQL, connection);
                MySqlParameter myParameter = new MySqlParameter("@content", SqlDbType.NText);
                myParameter.Value = strContent;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (MySqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        public static int ExecuteSqlWithParams(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (MySqlException e)
                    {
                        throw e;
                    }
                }
            }
        }
        #endregion


        #region Transaction
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            PrepareCommand(cmd, conn, trans, myDE.Key.ToString(), (MySqlParameter[])myDE.Value);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static int ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        int count = 0;
                        MySqlCommand cmd = new MySqlCommand();
                        foreach (CommandInfo myDE in cmdList)
                        {
                            PrepareCommand(cmd, conn, trans, myDE.CommandText, (MySqlParameter[])myDE.Parameters);

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                            {
                                if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                bool isHave = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                                if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                                if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                continue;
                            }

                            int val = cmd.ExecuteNonQuery();
                            count += val;
                            if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }

                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                        return count;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(List<string> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                    return count;
                }
                catch
                {
                    tx.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int indentity = 0;
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, myDE.Key.ToString(), cmdParms);
                            cmd.ExecuteNonQuery();
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(List<CommandInfo> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int indentity = 0;
                        foreach (CommandInfo myDE in SQLStringList)
                        {
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Parameters;
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, myDE.CommandText, cmdParms);
                            cmd.ExecuteNonQuery();
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行MySql事务
        /// </summary>
        /// <param name="list">SQL命令行列表</param>
        /// <param name="oracleCmdSqlList">Oracle命令行列表</param>
        /// <returns>执行结果 0-由于SQL造成事务失败 -1 由于Oracle造成事务失败 1-整体事务执行成功</returns>
        public static int ExecuteSqlTran(List<CommandInfo> list, List<CommandInfo> oracleCmdSqlList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    foreach (CommandInfo myDE in list)
                    {
                        PrepareCommand(cmd, conn, tx, myDE.CommandText, (MySqlParameter[])myDE.Parameters);
                        if (myDE.EffentNextType == EffentNextType.SolicitationEvent)
                        {
                            if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                            {
                                tx.Rollback();
                                throw new Exception("违背要求" + myDE.CommandText + "必须符合select count(..的格式");
                            }

                            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                            {
                                //引发事件
                                myDE.OnSolicitationEvent();
                            }
                        }

                        if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                        {
                            if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                            {
                                tx.Rollback();
                                throw new Exception("SQL:违背要求" + myDE.CommandText + "必须符合select count(..的格式");
                            }

                            bool isHave = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                            {
                                tx.Rollback();
                                throw new Exception("SQL:违背要求" + myDE.CommandText + "返回值必须大于0");
                                //return 0;
                            }
                            if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                            {
                                tx.Rollback();
                                throw new Exception("SQL:违背要求" + myDE.CommandText + "返回值必须等于0");
                                //return 0;
                            }

                            continue;
                        }

                        if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && cmd.ExecuteNonQuery() == 0)
                        {
                            tx.Rollback();
                            throw new Exception("SQL:违背要求" + myDE.CommandText + "必须有影响行");
                            //return 0;
                        }

                        cmd.Parameters.Clear();
                    }

                    tx.Commit();
                    return 1;
                }
                catch (MySqlException e)
                {
                    tx.Rollback();
                    throw e;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw e;
                }
            }
        }
        #endregion
    }
}

