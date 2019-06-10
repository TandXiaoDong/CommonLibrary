using System;

namespace CommonUtils.DateTimeHelper
{
    public static class TimeStampConvert
    {
        #region 字符串时间戳转DateTime
        /// <summary>        
        /// 时间戳转为格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        public static DateTime ConvertStringToDateTime(string timeStamp)
        {
            string strTail = "";
            if (timeStamp.Length == 13)
            {
                strTail = "0000";
            }
            else if (timeStamp.Length == 10)
            {
                strTail = "0000000";
            }
            else if (timeStamp.Length == 9)
            {
                strTail = "00000000";
            }

            TimeSpan toNow = new TimeSpan(long.Parse(timeStamp + strTail));
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.Add(toNow);
        }

        #endregion

        #region 日期转时间戳
        /// <summary>
        /// 日期转时间戳
        /// </summary>
        /// <param name="Datetime">需要转换的时间(空值默认为)</param>
        /// <param name="isMillSeconds">是否为毫秒时间戳</param>
        /// <returns>返回的时间戳</returns>
        public static long DatetimeToStamp(DateTime Datetime, bool isMillSeconds = false)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));//当地时区
            //通过减去初始日期来获取时间戳
            long timeStamp; //相差毫秒数
            //如果Datetime为空的话,,就使用当前的时间
            if (isMillSeconds)
            {
                if (Datetime == null)
                {
                    timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds;
                }
                else
                {
                    timeStamp = (long)(Datetime - startTime).TotalMilliseconds;
                }
            }
            else
            {
                if (Datetime == null)
                {
                    timeStamp = (long)(DateTime.Now - startTime).TotalSeconds;
                }
                else
                {
                    timeStamp = (long)(Datetime - startTime).TotalSeconds;
                }
            }

            //返回时间戳
            return timeStamp;
        }
        #endregion

        #region 时间戳转换为日期
        /// <summary>
        /// 时间戳转日期
        /// </summary>
        /// <param name="TimeStamp">时间戳</param>
        /// <returns>转换后的日期</returns>
        public static DateTime StampToDatetime(this long TimeStamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));//当地时区
            DateTime dt = startTime.AddSeconds(TimeStamp);
            //返回转换后的日期
            return dt;
        }
        #endregion

        #region DateTime To DataTimeOffset
        /// <summary>
        /// <see cref="DateTime"/> 转 <see cref="DataTimeOffset"/>
        /// </summary>
        /// <param name="dateTime">源时间</param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime() <= DateTimeOffset.MinValue.UtcDateTime
                       ? DateTimeOffset.MinValue
                       : new DateTimeOffset(dateTime);
        }
        #endregion

        #region 格式化时间
        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal static string formatDate(DateTime? time)
        {
            string dateText = "";
            //如果时间为空则返回空
            if (time == null)
            {
                return "";
            }
            long timeLength = DatetimeToStamp(new DateTime());
            //
            if (true)
            {

            }

            return dateText;
        }
        #endregion
    }
}
