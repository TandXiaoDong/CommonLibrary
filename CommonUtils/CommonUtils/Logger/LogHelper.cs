using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace CommonUtils.Logger
{
    public class LogHelper
    {
        private static ILog log = null;//log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static ILog Log
        {
            get
            {
                if (log == null)
                {
                    Type type = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
                    log = LogManager.GetLogger("[LOG]");
                }
                return log;
            }
        }

        #region 记录异常
        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stackTrace"></param>
        public static void LogException(string message, string stackTrace)
        {
            log.Debug(string.Format("Exception occured: {0}, stack trace: {1}", message, stackTrace));
        }
        #endregion

        #region 记录日志
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="info"></param>
        public static void LogInfo(string info)
        {
            log.Info(info);
        }

        public static void LogErr(string err)
        {
            log.Error(err);
        }
        #endregion

    }
}