using System;
using System.Diagnostics;
using System.IO;

namespace CommonUtils.Logger
{
    public class LogHelper1
    {
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        private static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

        private LogHelper1()
        {

        }

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static void Error(string info)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info);
            }
        }

        public static void Error(string info, Exception se)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }

        public static void WriteLog(string info)
        {
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(1);
            if (null != sf)
            {
                info += sf.GetMethod().Name + sf.GetFileLineNumber();
            }

            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }
    }
}
